using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;
using System;
using System.Text;

namespace Kakadu.Backend.Services
{
    public class OrderEmailService
    {
        private readonly IOrderService _orderService;
        private readonly EmailService _emailService;
        private readonly IProductService _productService;

        public OrderEmailService(IOrderService orderService, EmailService emailService, IProductService productService)
        {
            _orderService = orderService;
            _emailService = emailService;
            _productService = productService;
        }

        public void SendOrderbyEmail(int orderId, string recipient)
        {
            Order order = _orderService.GetById(orderId);

            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("<p class=\"receipt-info\">Kakadu</p>");
            bodyBuilder.AppendLine("<p class=\"receipt-info\">м. Чернівці, вул. Павла Каспрука 2</p>");
            bodyBuilder.AppendLine($"<p class=\"receipt-info\">Чек # {order.OrderNumber}</p>");
            bodyBuilder.AppendLine("<hr>");
            bodyBuilder.AppendLine("<div class=\"container-receipt\">");
            bodyBuilder.AppendLine($"<div class=\"name\"><p>Дата: {order.OrderDate}</p></div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<hr>");

            foreach (var item in order.Items)
            {
                Product product = _productService.GetById(item.ProductId);

                bodyBuilder.AppendLine("<div class=\"container-receipt\">");
                bodyBuilder.AppendLine($"<div class=\"name\">{product.Title}: {item.Quantity} x {item.Price}</div>");
                bodyBuilder.AppendLine("</div>");
            }

            bodyBuilder.AppendLine("<hr>");
            bodyBuilder.AppendLine("<div class=\"container-receipt\">");
            bodyBuilder.AppendLine($"<div class=\"name\">Сума: {order.TotalPrice} грн</div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<hr>");
            bodyBuilder.AppendLine("<p class=\"receipt-thanks\">Дякуємо за покупку!</p>");

            string fullMessage = bodyBuilder.ToString();

            _emailService.SendEmail(recipient, fullMessage, "Order Details");
        }
    }
}
