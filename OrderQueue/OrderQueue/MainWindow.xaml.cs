﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService = new OrderStatusService();
        private readonly IDictionary<int, string> orders = new Dictionary<int, string>();
        private long from = DateTimeOffset.Now.ToUnixTimeSeconds();
        private long to = DateTimeOffset.Now.ToUnixTimeSeconds();

        public MainWindow()
        {
            InitializeComponent();
            ConfigureLogging();
            StartTimer();
        }

        private void ConfigureLogging()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("serilog.json")
                .Build();
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                Log.Information("Logging is configured successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to configure logging: {ex.Message}", "Error");
            }
        }

        private async void StartTimer()
        {
            var _requestIntervalSeconds = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RequestIntervalSeconds"]);
            var _timer = new PeriodicTimer(TimeSpan.FromSeconds(_requestIntervalSeconds));

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
                Log.Information("Requesting order status updates from {From} to {To}", FormatDate(from), FormatDate(to));

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
              UpdateTime();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while updating orders: {ExceptionMessage}", ex.Message);
            }
        }

        private static string FormatDate(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
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

        private void UpdateTime()
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");
            updateTimeTextBlock.Text = $"Оновлено: {currentTime}";
        }
    }
}
