using Kakadu.Backend.Services;
using Kakadu.WebServer;

namespace WebServer.Order
{
    public class OrderStatusRequestProcessor
    {
        private readonly IOrderService _orderService;
        private readonly OrderToJsonConverter _orderToJsonConverter;

        public OrderStatusRequestProcessor(IOrderService orderService, OrderToJsonConverter orderToJsonConverter)
        {
            _orderService = orderService;
            _orderToJsonConverter = orderToJsonConverter;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
            var timestamp = DateTime.Parse(httpRequest.Parameters["timestamp"]);

            var from = timestamp.AddSeconds(-1);
            var to = timestamp.AddSeconds(1);

            var orders = _orderService.GetAllByUpdatedAt(from, to);

            var json = _orderToJsonConverter.Convert(orders);

            var response = new HttpResponse
            {
                Body = json,
                Status = HttpStatus.OK
            };

            return response;
        }
    }
}
