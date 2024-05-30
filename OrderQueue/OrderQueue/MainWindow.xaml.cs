using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using OrderStatusClient;
using Serilog;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService;
        private IDictionary<int, string> orders = new Dictionary<int, string>();
        private long from;
        private long to;
        private PeriodicTimer _timer;
        private readonly int _requestIntervalSeconds;

        public MainWindow()
        {
            InitializeComponent();
            ConfigureLogging();

            _requestIntervalSeconds = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RequestIntervalSeconds"]);
            _orderStatusService = new OrderStatusService();
            from = DateTimeOffset.Now.ToUnixTimeSeconds();
            to = DateTimeOffset.Now.ToUnixTimeSeconds();
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(_requestIntervalSeconds));
            StartTimer();
        }

        private void ConfigureLogging()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("serilog.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                Log.Information("Logging is configured successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to configure logging: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void StartTimer()
        {
            while (await _timer.WaitForNextTickAsync())
            {
                await UpdateOrders();
            }
        }

        private async Task UpdateOrders()
        {
            try
            {
                to = DateTimeOffset.Now.ToUnixTimeSeconds();
                var fromDateTime = DateTimeOffset.FromUnixTimeSeconds(from).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                var toDateTime = DateTimeOffset.FromUnixTimeSeconds(to).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

                Log.Information("Requesting order status updates from {From} to {To}", fromDateTime, toDateTime);
                var newOrders = await _orderStatusService.GetStatus(from, to);

                if (newOrders != null)
                {
                    foreach (var order in newOrders)
                    {
                        orders[order.OrderNumber] = order.Status;
                    }

                    Log.Information("Received {OrderCount} orders from the server", newOrders.Count);
                }
                else
                {
                    Log.Information("Received no orders from the server");
                }

                UpdateUI();
                from = to;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while updating orders: {ExceptionMessage}", ex.Message);
            }
        }

        private void UpdateUI()
        {
            ordersNewListBox.Items.Clear();
            ordersDoneListBox.Items.Clear();

            foreach (var order in orders)
            {
                if (order.Value == "Processing")
                {
                    ordersNewListBox.Items.Add(order.Key);
                }
                else
                {
                    ordersDoneListBox.Items.Add(order.Key);
                }
            }
        }
    }
}
