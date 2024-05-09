using System;
using System.Collections.Generic;

namespace Kakadu.WebServer.Order
{
    public class JsonToOrderRequestConverter
    {
        public OrderRequest Convert(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON string is empty");

            var orderRequest = new OrderRequest();
            var rootObj = GetJsonObject(ref json);

            if (rootObj != null)
            {
                var itemsJson = GetJsonArray(rootObj, "items");
                if (itemsJson != null)
                {
                    while (itemsJson.Length > 0)
                    {
                        var itemJson = GetJsonObject(ref itemsJson);
                        if (itemJson == null)
                            break;

                        var orderItemRequest = new OrderRequest.OrderItemRequest();

                        var productIdJson = GetJsonProperty(itemJson, "productId");
                        if (productIdJson.HasValue)
                            orderItemRequest.ProductId = productIdJson.Value;

                        var quantityJson = GetJsonProperty(itemJson, "quantity");
                        if (quantityJson.HasValue)
                            orderItemRequest.Quantity = quantityJson.Value;

                        orderRequest.Items.Add(orderItemRequest);
                    }
                }
            }

            return orderRequest;
        }

        private string GetJsonArray(string json, string propertyName)
        {
             var parts = json.Split($"{propertyName}:");

            if (parts.Length != 2)
            {
                throw new ArgumentException($"Property '{propertyName}' not found");
            }

            var value = parts[1].Trim();
            if (!value.StartsWith("[") || !value.EndsWith("]"))
                throw new ArgumentException("Invalid JSON array format");

            return value.Substring(1, value.Length - 2);
        }

        private string GetJsonObject(ref string json)
        {
            json = json.Trim();
            if (!json.StartsWith("{") || !json.EndsWith("}"))
                throw new ArgumentException("Invalid JSON format");

            var result = json.Substring(1, json.Length - 2);
            json = ""; // Очищення рядка json, оскільки ми вже отримали його значення
            return result;
        }


        private int? GetJsonProperty(string json, string propertyName)
        {
            var start = json.IndexOf($"\"{propertyName}\":", StringComparison.OrdinalIgnoreCase);
            if (start == -1)
                return null;

            start += propertyName.Length + 3;
            int end;

            if (json[start] == '"')
            {
                // Значення обмежене лапками
                start++;
                end = FindClosingQuote(json, start);
                if (end == -1)
                    return null;
            }
            else
            {
                // Значення не обмежене лапками
                end = json.IndexOfAny(new char[] { ',', '}' }, start);
                if (end == -1)
                    return null;
            }

            var value = json.Substring(start, end - start).Trim();

            if (int.TryParse(value, out var result))
                return result;
            else
                return null;
        }

        private int FindClosingBracket(string json, int start)
        {
            var bracketStack = new Stack<char>();
            bracketStack.Push(json[start]);

            for (var i = start + 1; i < json.Length; i++)
            {
                if (json[i] == '{')
                    bracketStack.Push('{');
                else if (json[i] == '}')
                {
                    bracketStack.Pop();
                    if (bracketStack.Count == 0)
                        return i;
                }
            }

            return -1;
        }

        private int FindClosingQuote(string json, int start)
        {
            bool escaped = false;

            for (var i = start; i < json.Length; i++)
            {
                if (json[i] == '\\')
                {
                    escaped = !escaped;
                }
                else if (json[i] == '"' && !escaped)
                {
                    return i;
                }
                else
                {
                    escaped = false;
                }
            }

            return -1;
        }
    }
}