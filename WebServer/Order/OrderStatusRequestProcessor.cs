using Kakadu.Backend.Services;
using Kakadu.WebServer;
using System.Text.Json;

namespace WebServer.Order
{
    public class OrderStatusRequestProcessor
    {
        private readonly IOrderService _orderService;

        public OrderStatusRequestProcessor(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
           
            var timestamp = DateTime.Parse(httpRequest.Parameters["timestamp"]);

            var from = timestamp.AddSeconds(-1);

            var to = timestamp.AddSeconds(1);

            var orders = _orderService.GetAllByUpdatedAt(from, to);

            var json = JsonSerializer.Serialize(orders);

            var response = new HttpResponse
            {
                Body = json
            };

            return response;
        }
    }
}
