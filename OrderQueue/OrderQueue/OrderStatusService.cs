using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace OrderStatusClient
{
    public class OrderStatusService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly string _baseAddress = "http://127.0.0.1:8085";

        public OrderStatusService()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("serilog.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public async Task<List<Order>> GetStatus(long from, long to)
        {
            string url = $"{_baseAddress}/orderStatuses?from={from}&to={to}";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
                    Log.Information("HTTP GET Response: {Orders}", orders);
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
