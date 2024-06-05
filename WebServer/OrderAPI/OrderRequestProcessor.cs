using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.WebServer.Core;

namespace Kakadu.WebServer.OrderAPI
{
    public class OrderRequestProcessor
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly JsonToOrderRequestConverter _jsonToOrderRequestConverter;
        private readonly OrderToJsonConverter _orderToJsonConverter;
        private readonly PrintService _orderPrintService;
        private readonly OrderToPlainTextConverter _orderToPlainTextConverter;

        public OrderRequestProcessor(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _jsonToOrderRequestConverter = new JsonToOrderRequestConverter();
            _orderToJsonConverter = new OrderToJsonConverter();
            _orderPrintService = new PrintService();
            _orderToPlainTextConverter = new OrderToPlainTextConverter(_productService);
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {          
            var orderRequest = _jsonToOrderRequestConverter.Convert(httpRequest.Body);

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
                }).ToList(),
                OrderDate = DateTime.Now
            };

            order.TotalPrice = order.Items.Sum(item => item.Amount);
            order.Status = "Processing";
            order.UpdatedAt = order.OrderDate;

            var savedOrder = _orderRepository.Save(order);

            var kitchenReceipt = _orderToPlainTextConverter.ConvertToKitchenReceipt(savedOrder);
            _orderPrintService.Print(kitchenReceipt);

            var jsonResponse = _orderToJsonConverter.Convert(savedOrder);


            var response = new HttpResponse
            {
                Body = jsonResponse
            };
            return response;
        }
    }
}

