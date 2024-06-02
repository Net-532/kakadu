using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;

namespace Kakadu.OrderQueue
{
    public class OrderStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _orderStatusEndpoint;

        public OrderStatusService()
        {
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            _orderStatusEndpoint = ConfigurationManager.AppSettings["OrderStatusEndpoint"];
            _httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<List<Order>> GetStatus(long from, long to)
        {
            string url = $"{_orderStatusEndpoint}?from={from}&to={to}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                    Log.Information("HTTP GET Response: {Orders}", orders != null ? string.Join(", ", orders) : "No orders");
                    return orders;
                }
                else
                {
                    Log.Error("HTTP GET Request failed with status code: {StatusCode}", response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while sending HTTP GET request: {ExceptionMessage}", ex.Message);
                return null;
            }
        }
    }
}
