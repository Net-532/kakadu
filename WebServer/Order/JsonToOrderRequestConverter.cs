using System;
using System.Collections.Generic;
using static Kakadu.WebServer.Order.OrderRequest;

namespace Kakadu.WebServer.Order
{

    public class JsonObject
    {
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            Properties[key] = value;
        }
    }

    public class JsonToOrderRequestConverter
    {
        public OrderRequest Convert(string json)
        {
            try
            {
               
                var jsonObject = ParseJson(json);

                var orderRequest = new OrderRequest();
                orderRequest.Items = new List<OrderItemRequest>();

                
                foreach (var item in (List<object>)jsonObject.Properties["items"])
                {
                    var orderItem = new OrderItemRequest();
                    var itemProperties = (Dictionary<string, object>)item;

                    orderItem.ProductId = (int)itemProperties["productId"];
                    orderItem.Quantity = (int)itemProperties["quantity"];

                    orderRequest.Items.Add(orderItem);
                }

                return orderRequest;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Помилка конвертації JSON: {ex.Message}");
                return null;
            }
        }

        private JsonObject ParseJson(string json)
        {
            var jsonObject = new JsonObject();

          
            var tokens = json.Split(',', '{', '}', ':');

            string currentKey = null;
            foreach (var token in tokens)
            {
                var trimmedToken = token.Trim();

                if (!string.IsNullOrEmpty(trimmedToken))
                {
                    if (trimmedToken.StartsWith("\"") && trimmedToken.EndsWith("\""))
                    {
                        currentKey = trimmedToken.Trim('"');
                    }
                    else
                    {
                       
                        jsonObject.Add(currentKey, ParseValue(trimmedToken));
                    }
                }
            }

            return jsonObject;
        }

        private object ParseValue(string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            else
            {
                return value.Trim('"');
            }
        }
    }
}
