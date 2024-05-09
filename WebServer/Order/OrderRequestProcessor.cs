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
                Items = orderRequest.Items.Select((item, index) =>
                {
                    var product_price = _productService.GetById(item.ProductId).Price;
                    var amount = product_price * item.Quantity;
                    return new OrderItem
                    {
                        Id = index + 1,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product_price,
                        Amount = amount
                    };
                }).ToList(),
                OrderDate = DateTime.Now
            };

            order.TotalPrice = order.Items.Sum(item => item.Amount);
            _orderRepository.Save(order);
        }
    }
}