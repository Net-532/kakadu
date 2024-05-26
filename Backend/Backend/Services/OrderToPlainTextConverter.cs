using Kakadu.Backend.Entities;
using System.Text;
using Kakadu.Backend.Repositories;


namespace Kakadu.Backend.Services
{
    public class OrderToPlainTextConverter
    {
        private readonly IProductService _productService;
       

        public OrderToPlainTextConverter(IProductService productService)
        {
            _productService = productService;
        }

        public string Convert(Order order)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("           Kakadu");
            sb.AppendLine("Адреса: вул. Павла Каспрука 2");
            sb.AppendLine();
            sb.AppendLine($"            Чек №: {order.OrderNumber}");
            sb.AppendLine("********************************");
            sb.AppendLine($"Дата: {order.OrderDate:dd.MM.yyyy}");
            sb.AppendLine($"Час: {order.OrderDate:HH:mm:ss}");
            sb.AppendLine("********************************");

            int maxWidth = 28;
            foreach (var item in order.Items)
            {
                Product product = _productService.GetById(item.ProductId);
                string title = product.Title;
                int quantity = item.Quantity;
                int padding = maxWidth - title.Length - item.Price.ToString("0.00").Length;
                string spaces = new string(' ', padding);
                sb.AppendLine($"{title}{spaces}{quantity} x {item.Price:0.00}");
            }

            sb.AppendLine("--------------------------------");
            sb.AppendLine($"Сума                  {order.TotalPrice:0.00} грн");
            sb.AppendLine("--------------------------------");
            sb.AppendLine("       Дякуємо за покупку!");

            return sb.ToString();
        }

        public string ConvertToKitchenReceipt(Order order)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"            Чек №: {order.OrderNumber}");
            sb.AppendLine("********************************");
            sb.AppendLine($"Дата: {order.OrderDate:dd.MM.yyyy}");
            sb.AppendLine($"Час: {order.OrderDate:HH:mm:ss}");
            sb.AppendLine("********************************");


            int maxWidth = 28;
            int position = 1;

            foreach (var item in order.Items)
            {
                Product product = _productService.GetById(item.ProductId);
                string title = product.Title;
                int quantity = item.Quantity;
                int padding = maxWidth - title.Length - quantity.ToString().Length - position.ToString().Length;
                string spaces = new string(' ', padding);

                sb.AppendLine($"{position}.{title}{spaces}{quantity}");

                position++;
            }

            return sb.ToString();
        }

    }
    
}
