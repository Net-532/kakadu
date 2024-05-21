using System;
using System.Collections.Generic;
using System.Windows;
using OrderStatusClient;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService;
        private List<Order> orders = new List<Order>();

        public MainWindow()
        {
            InitializeComponent();
            _orderStatusService = new OrderStatusService();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long from = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                long to = from; // Для цього прикладу використовуємо поточний момент часу
                var newOrders = await _orderStatusService.GetStatus(from, to);

                if (newOrders != null)
                {
                    foreach (var order in newOrders)
                    {
                        if (!orders.Exists(o => o.OrderNumber == order.OrderNumber && o.Status == order.Status))
                        {
                            orders.Add(order);
                            ordersListBox.Items.Add($"Order Number: {order.OrderNumber}, Status: {order.Status}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
