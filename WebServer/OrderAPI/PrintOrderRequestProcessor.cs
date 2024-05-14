using Kakadu.Backend.Services;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Entities;

namespace Kakadu.WebServer.OrderAPI
{
    public class PrintOrderRequestProcessor
    {
        private static IProductService productService = new ProductService(new ProductRepositoryXML());
        private static IOrderService orderService = new OrderService(new OrderRepositoryXML());
        private static OrderToPlainTextConverter converter = new OrderToPlainTextConverter(productService);
        private static PrintService printService = new PrintService();

        public PrintOrderRequestProcessor() { }
        public HttpResponse Process(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();
            try
            {
                int orderId = Convert.ToInt32(httpRequest.Parameters["orderId"]);
                Order order = orderService.GetById(orderId);

                string orderStr = converter.Convert(order);

                printService.Print(orderStr);

                response.Status = HttpStatus.OK;

                response.Body = "{}";

                return response;
            }
            catch(Exception ex)
            {
                response.Status =HttpStatus.NotFound;
                response.Body = ex.Message;
                return response;
            }
        }
    }
}
