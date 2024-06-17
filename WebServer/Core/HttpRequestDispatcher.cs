using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.WebServer.OrderAPI;
using Kakadu.WebServer.ProductAPI;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Kakadu.WebServer.Core
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
        private static IProductRepository productRepository = new ProductRepositoryDB();
        private static IProductService productService = new ProductService(productRepository);
        private static IOrderItemRepository orderItemRepository = new OrderItemRepositoryDB();
        private static IOrderRepository orderRepository = new OrderRepositoryDB(orderItemRepository);
        private static IOrderService orderService = new OrderService(orderRepository);
        private static EmailService emailService = new EmailService();
        private static OrderEmailService orderEmailService = new OrderEmailService(orderService, emailService, productService);
        private static ProductRequestProcessor productRequestProcessor = new ProductRequestProcessor(productService, new ProductToJsonConverter());
        private static OrderRequestProcessor orderRequestProcessor = new OrderRequestProcessor(orderRepository, productService);
        private static OrderStatusRequestProcessor orderStatusRequestProcessor = new OrderStatusRequestProcessor(orderService, new OrderToJsonConverter());
        private static OrderToPlainTextConverter converter = new OrderToPlainTextConverter(productService);
        private static PrintOrderRequestProcessor printOrderRequestProcessor = new PrintOrderRequestProcessor(orderService, converter);
        private static SendMailRequestProcessor sendMailRequestProcessor = new SendMailRequestProcessor(orderEmailService);


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
                case "/orderStatuses":
                    response = ProcessOrderStatusesRequest(httpRequest);
                    break;
                case "/print":
                    response = printOrderRequestProcessor.Process(httpRequest);
                    break;
                case "/send":
                    response = sendMailRequestProcessor.Process(httpRequest);
                    break;
                default:
                    response.Status = HttpStatus.NotFound;
                    response.Body = $"No endpoint found for the {httpRequest.RootPath}";
                    break;
            }
            response.Status = HttpStatus.OK;
            response.Headers.Add("Content-Type", "application/json");
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        private HttpResponse ProcessProductsRequest(HttpRequest request)
        {
            return productRequestProcessor.Process(request);
        }

        private HttpResponse ProcessOrdersRequest(HttpRequest request)
        {
            return orderRequestProcessor.Process(request);
        }

        private HttpResponse ProcessOrderStatusesRequest(HttpRequest request)
        {
            return orderStatusRequestProcessor.Process(request);
        }
    }
}
