﻿using Kakadu.Backend.Repositories;
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
        private static IProductRepository productRepository = new ProductRepositoryXML();
        private static IProductService productService = new ProductService(productRepository);
        private static IOrderRepository orderRepository = new OrderRepositoryXML();
        private static IOrderService orderService = new OrderService(orderRepository);
        private static ProductRequestProcessor productRequestProcessor = new ProductRequestProcessor(productService, new ProductToJsonConverter());
        private static OrderRequestProcessor orderRequestProcessor = new OrderRequestProcessor(orderRepository, productService);
        private static OrderStatusRequestProcessor orderStatusRequestProcessor = new OrderStatusRequestProcessor(orderService, new OrderToJsonConverter());

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