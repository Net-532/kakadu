using Kakadu.Backend.Entities;
using System.Text;
using Kakadu.Backend.Repositories;


namespace Kakadu.Backend.Services
{
    public class OrderToPlainTextConverter
    {
        private readonly IProductRepository _productRepository;
       

        public OrderToPlainTextConverter(IProductRepository productRepository)
        {
            _productRepository = productRepository;
           
        }
        public static string Convert(Order order, IProductRepository productRepository)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("           Kakadu");
            sb.AppendLine("Адреса: вул. Павла Каспрука 2");
            sb.AppendLine();
            sb.AppendLine("            Чек");
            sb.AppendLine("********************************");
            sb.AppendLine($"Дата: {order.OrderDate:dd.MM.yyyy}");
            sb.AppendLine($"Час: {order.OrderDate:HH:mm}");
            sb.AppendLine("********************************");

            foreach (var item in order.Items)
            {
                int maxWidth = 28; 
                Product product = productRepository.GetById(item.ProductId);
                string title = product != null ? product.Title : $" {item.ProductId}";

                string[] titleParts = title.Split(' ');

                foreach (string part in titleParts)
                {
                    int padding = maxWidth - part.Length - item.Price.ToString("0.00").Length;
                    string spaces = new string(' ', padding);
                    sb.AppendLine($"{part}{spaces}{item.Price:0.00} грн");
                }
            }

            sb.AppendLine("--------------------------------");
            sb.AppendLine($"Сума                  {order.TotalPrice:0.00} грн");
            sb.AppendLine("--------------------------------");
            sb.AppendLine("       Дякуємо за покупку!");

            return sb.ToString();
        }

    }
    
}
