using Kakadu.Backend.Repositories;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;

namespace Kakadu.WebServer.Order
{
    public class OrderRequestProcessor

    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductService _productService;

        public OrderRequestProcessor(IOrderRepository orderRepository, ProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public void Process(OrderRequest orderRequest)
        {
            var order = new Backend.Entities.Order
            {
                Items = orderRequest.Items.Select((item, index) => new OrderItem
                {
                    Id = index + 1,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = _productService.GetById(item.ProductId).Price,
                }).ToList(),
               
                OrderDate = DateTime.UtcNow
            };
            order.TotalPrice = CalculateTotalPrice(order.Items);
            _orderRepository.Save(order);
        }
        private decimal CalculateTotalPrice(List<OrderItem> orderItems)
        {
            decimal totalPrice = 0;

            foreach (var item in orderItems)
            { 
                totalPrice += item.Price * item.Quantity;
            }

            return totalPrice;
        }
    }
}