using System;
using System.Collections.Generic;

namespace Kakadu.WebServer.Order
{
    public class JsonToOrderRequestConverter
    {
        public OrderRequest Convert(string json)
        {
            var orderRequest = new OrderRequest();

            if (string.IsNullOrWhiteSpace(json))
                return orderRequest;

            var itemsStart = json.IndexOf("items", StringComparison.OrdinalIgnoreCase);
            if (itemsStart == -1)
                return orderRequest;

            itemsStart = json.IndexOf("[", itemsStart);
            if (itemsStart == -1)
                return orderRequest;

            var itemsEnd = json.IndexOf("]", itemsStart);
            if (itemsEnd == -1)
                return orderRequest;

            var itemsJson = json.Substring(itemsStart + 1, itemsEnd - itemsStart - 1);

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

            return orderRequest;
        }

        private string GetJsonObject(ref string json)
        {
            var start = json.IndexOf("{");
            if (start == -1)
                return null;

            var end = FindClosingBracket(json, start);
            if (end == -1)
                return null;

            var result = json.Substring(start, end - start + 1);
            json = json.Substring(end + 1).Trim();
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