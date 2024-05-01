// HttpRequestDispatcher.cs
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using System.Text;

namespace Kakadu.WebServer
{
    public enum HttpStatus
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }

    public class HttpRequestDispatcher
    {
        public HttpResponse Dispatch(HttpRequest httpRequest)
        {
            HttpResponse response = new HttpResponse();

            switch (httpRequest.RootPath)
            {
                case "/products":
                    response = ProcessProductsRequest(httpRequest);
                    break;
                case "/orders":
                    response = ProcessOrdersRequest(httpRequest);
                    break;
                default:
                    response.Status = HttpStatus.NotFound;
                    response.Body = $"No endpoint found for the {httpRequest.RootPath}";
                    break;
            }

            response.Headers.Add("Content-Type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        private HttpResponse ProcessProductsRequest(HttpRequest request)
        {
            HttpResponse response = new HttpResponse();

            IProductRepository productRepository = new ProductRepositoryXML();
            IProductService productService = new ProductService(productRepository);

            List<Product> products = productService.GetAll();
            if (products != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("[");
                foreach (Product product in products)
                {
                    string priceString = product.Price.ToString();
                    priceString = priceString.Replace(",", ".");
                    string productJson = $"{{\"id\": {product.Id}, \"title\": \"{product.Title}\", \"price\": {priceString}, \"photoUrl\": \"{product.PhotoUrl}\", \"description\": \"{product.Description}\"}},";
                    jsonBuilder.Append(productJson);
                }

                if (products.Count > 0)
                {
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                }

                jsonBuilder.Append("]");
                response.Body = jsonBuilder.ToString();
            }
            else
            {
                response.Status = HttpStatus.InternalServerError;
                response.Body = "Error occurred while fetching products";
            }

            return response;
        }

        private HttpResponse ProcessOrdersRequest(HttpRequest request)
        {

            // Не має опису та реалізації IOrderSevice та OrderService

            //HttpResponse response = new HttpResponse();

            //IOrderRepository orderRepository = new OrderRepositoryXML();
            //IOrderService orderService = new OrderService(orderRepository);

            //List<Order> orders = orderService.GetAll();
            //if (orders != null)
            //{
            //    StringBuilder jsonBuilder = new StringBuilder();
            //    jsonBuilder.Append("[");
            //    foreach (Order order in orders)
            //    {
            //        string totalPriceString = order.TotalPrice.ToString();
            //        totalPriceString = totalPriceString.Replace(",", ".");
            //        string orderJson = $"{{\"orderNumber\": {order.OrderNumber}, \"id\": {order.Id}, \"totalPrice\": {totalPriceString}, \"orderDate\": \"{order.OrderDate}\", \"status\": \"{order.Status}\"}},";
            //        jsonBuilder.Append(orderJson);
            //    }

            //    if (orders.Count > 0)
            //    {
            //        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            //    }

            //    jsonBuilder.Append("]");
            //    response.Body = jsonBuilder.ToString();
            //}
            //else
            //{
            //    response.Status = HttpStatus.InternalServerError;
            //    response.Body = "Error occurred while fetching orders";
            //}

            //return response;
            return null;
        }

    }
}
