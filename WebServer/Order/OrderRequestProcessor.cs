using Kakadu.Backend.Repositories;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;

namespace Kakadu.WebServer.Order
{
    public class OrderRequestProcessor
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderRequestProcessor(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
            var orderRequest = new OrderRequest();
            var order = new Backend.Entities.Order
            {
                Items = orderRequest.Items.Select((item, index) =>
                {
                    var product = _productService.GetById(item.ProductId);
                    return new OrderItem
                    {
                        Id = index + 1,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = product.Price,
                        Amount = product.Price * item.Quantity
                    };
                }
                ).ToList(),
                OrderDate = DateTime.Now
            };

            order.TotalPrice = order.Items.Sum(item => item.Amount);
            order.Status = "Processing";
            _orderRepository.Save(order);

            var response = new HttpResponse
            {
                Body = "{}"
            };
            return response;
        }
    }
}

