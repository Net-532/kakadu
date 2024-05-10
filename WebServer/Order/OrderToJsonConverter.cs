using System.Text;
using System.Globalization;
using Kakadu.Backend.Services;
using Kakadu.Backend.Repositories;

namespace Kakadu.WebServer
{
    public class OrderToJsonConverter
    {
        private static IProductService productService = new ProductService(new ProductRepositoryXML());
        public string Convert(Kakadu.Backend.Entities.Order order)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{").Append($"\"orderNumber\": {order.OrderNumber},")
            .Append($"\"orderDate\": \"{order.OrderDate.ToString("dd.MM.yyyy")}\",")
            .Append($"\"orderTime\": \"{order.OrderDate.ToString("HH:mm:ss")}\",")
            .Append($"\"totalPrice\": {order.TotalPrice.ToString(CultureInfo.InvariantCulture)},")
            .Append("\"items\": [");

            foreach (var item in order.Items)
            {
                jsonBuilder.Append("{");
                jsonBuilder.Append($"\"title\": \"{productService.GetById(item.ProductId).Title}\",");
                jsonBuilder.Append($"\"quantity\": {item.Quantity},");
                jsonBuilder.Append($"\"price\": {item.Price.ToString(CultureInfo.InvariantCulture)},");
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
