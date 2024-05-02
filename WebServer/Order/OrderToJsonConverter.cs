using System.Text;
using Kakadu.Backend.Entities;
using System;

namespace Kakadu.WebServer
{
    public class OrderToJsonConverter
    {
        public string Convert(Kakadu.Backend.Entities.Order order)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.Append($"\"orderNumber\": {order.OrderNumber},");
            jsonBuilder.Append($"\"orderDate\": \"{order.OrderDate.ToString("dd.MM.yyyy")}\",");
            jsonBuilder.Append($"\"totalPrice\": {order.TotalPrice},");
            jsonBuilder.Append("\"items\": [");

            foreach (var item in order.Items)
            {
                jsonBuilder.Append("{");
                jsonBuilder.Append($"\"title\": \"{item.ProductId}\",");
                jsonBuilder.Append($"\"quantity\": {item.Quantity},");
                jsonBuilder.Append($"\"price\": {item.Price},");
                jsonBuilder.Append($"\"amount\": {item.Quantity * item.Price}"); 
                jsonBuilder.Append("},");
            }

            if (order.Items.Count > 0)
            {
                jsonBuilder.Length--;
            }

            jsonBuilder.Append("]}");

            return jsonBuilder.ToString();
        }
    }
}
