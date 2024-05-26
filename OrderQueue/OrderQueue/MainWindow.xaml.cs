using System;
using System.Collections.Generic;
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
        private long from = DateTimeOffset.Now.ToUnixTimeSeconds();
        private long to = DateTimeOffset.Now.ToUnixTimeSeconds();
        private PeriodicTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            ConfigureLogging();
            _orderStatusService = new OrderStatusService();
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(30));
            StartTimer();
        }

        private void ConfigureLogging()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("serilog.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
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
                Dispatcher.Invoke(() => MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error));
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
