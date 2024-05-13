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
            var rootObj = GetJsonObject(json);


            var itemsJson = GetJsonArray(rootObj, "items");
            foreach (var item in itemsJson)
            {
                var orderItemRequest = new OrderRequest.OrderItemRequest();
                var productIdJson = GetJsonProperty(item, "productId");
                if (productIdJson.HasValue)
                    orderItemRequest.ProductId = productIdJson.Value;

                var quantityJson = GetJsonProperty(item, "quantity");
                if (quantityJson.HasValue)
                    orderItemRequest.Quantity = quantityJson.Value;
                orderRequest.Items.Add(orderItemRequest);
            }

            return orderRequest;
        }

        private string[] GetJsonArray(string json, string propertyName)
        {
            var parts = json.Split($"\"{propertyName}\":");

            if (parts.Length != 2)
            {
                throw new ArgumentException($"Property '{propertyName}' not found");
            }

            var value = parts[1].Trim();
            if (!value.StartsWith("[") || !value.EndsWith("]"))
                throw new ArgumentException("Invalid JSON array format");

            return value.Substring(1, value.Length - 2).Trim().Split("},");
        }

        private string GetJsonObject(string json)
        {
            json = json.Trim();
            if (!json.StartsWith("{") || !json.EndsWith("}"))
                throw new ArgumentException("Invalid JSON format");

            return json.Substring(1, json.Length - 2);          
        }


        private int? GetJsonProperty(string json, string propertyName)
        {
            var poperties = json.Split(',');
            foreach (var poperty in poperties)
            {

                var parts = poperty.Split($"\"{propertyName}\":");

                if (parts.Length != 2)
                {
                    continue;
                }

                string value = parts[1].Trim().Trim('}');

                if (int.TryParse(value, out var result))
                    return result;
                else
                    return null;

            }
            throw new ArgumentException($"Property '{propertyName}' not found");
        }

      
       
    }
}