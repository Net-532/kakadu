using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;

namespace Kakadu.WebServer.OrderAPI
{
    public class PrintOrderRequestProcessor
    {
        private IOrderService _orderService;
        private OrderToPlainTextConverter _converter;
        private static PrintService printService = new PrintService();

        public PrintOrderRequestProcessor(IOrderService orderService, OrderToPlainTextConverter converter)
        {
            _orderService = orderService;
            _converter = converter;
        }

        public HttpResponse Process(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();
            int orderId = Convert.ToInt32(httpRequest.Parameters["orderId"]);
            Order order = _orderService.GetById(orderId);

            string orderStr = _converter.Convert(order);

            printService.Print(orderStr);

            response.Status = HttpStatus.OK;

            response.Body = "{}";

            return response;
        }
    }
}
