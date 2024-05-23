using Kakadu.Backend.Services;
using Kakadu.WebServer.Core;

namespace Kakadu.WebServer.OrderAPI
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
            var fromUnixTimestamp = long.Parse(httpRequest.Parameters["from"]);
            var toUnixTimestamp = long.Parse(httpRequest.Parameters["to"]); 

            var from = DateTimeOffset.FromUnixTimeSeconds(fromUnixTimestamp).LocalDateTime;
            var to = DateTimeOffset.FromUnixTimeSeconds(toUnixTimestamp).LocalDateTime;

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
