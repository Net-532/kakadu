using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderStatusClient
{
    public class OrderStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly string _orderStatusEndpoint;

        public OrderStatusService(string baseUrl, string orderStatusEndpoint)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _orderStatusEndpoint = orderStatusEndpoint;
        }

        public async Task<List<Order>> GetStatus(long from, long to)
        {
            var response = await _httpClient.GetAsync($"{_orderStatusEndpoint}?from={from}&to={to}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Order>>();
        }
    }
}
