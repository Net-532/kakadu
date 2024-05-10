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
                    var amount = product.Price * item.Quantity;
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
            var totalPrice = orderItems.Sum(item => item.Amount);

            var order = new Backend.Entities.Order
            {
                Items = orderItems,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice
            };

            order.Status = OrderStatus.Processing;
            _orderRepository.Save(order);

            var response = new HttpResponse
            {
                Body = "{}"
            };
            return response;
        }
    }
    }

