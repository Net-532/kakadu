using System.Text;
using System.Globalization;
using Kakadu.Backend.Services;
using Kakadu.Backend.Repositories;

namespace Kakadu.WebServer.OrderAPI
{
    public class OrderToJsonConverter
    {
        private static IProductService productService = new ProductService(new ProductRepositoryXML());

        public string Convert(Kakadu.Backend.Entities.Order order)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{")
                .Append($"\"orderNumber\": {order.OrderNumber},")
                .Append($"\"id\": \"{order.Id}\",")
                .Append($"\"orderDate\": \"{order.OrderDate.ToString("dd.MM.yyyy")}\",")
                .Append($"\"orderTime\": \"{order.OrderDate.ToString("HH:mm:ss")}\",")
                .Append($"\"totalPrice\": {order.TotalPrice.ToString(CultureInfo.InvariantCulture)},")
                .Append($"\"status\": \"{order.Status}\",")  
                .Append("\"items\": [");

            foreach (var item in order.Items)
            {
                jsonBuilder.Append("{")
                    .Append($"\"title\": \"{productService.GetById(item.ProductId).Title}\",")
                    .Append($"\"quantity\": {item.Quantity},")
                    .Append($"\"price\": {item.Price.ToString(CultureInfo.InvariantCulture)},")
                    .Append($"\"amount\": {item.Amount.ToString(CultureInfo.InvariantCulture)}")
                    .Append("},");
            }

            if (order.Items.Count > 0)
            {
                jsonBuilder.Length--;
            }

            jsonBuilder.Append("]}");

            return jsonBuilder.ToString();
        }

        public string Convert(List<Kakadu.Backend.Entities.Order> orders)
        {
            StringBuilder jsonsBuilder = new StringBuilder();
            jsonsBuilder.Append("[");

            foreach (var order in orders)
            {
                jsonsBuilder.Append(Convert(order));
                jsonsBuilder.Append(",");
            }

            if (orders.Count > 0)
            {
                jsonsBuilder.Length--;
            }

            jsonsBuilder.Append("]");
            return jsonsBuilder.ToString();
        }
    }
}
