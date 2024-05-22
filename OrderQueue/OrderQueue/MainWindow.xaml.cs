using System;
using System.Collections.Generic;
using System.Windows;
using OrderStatusClient;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService;
        private HashSet<Order> orders = new HashSet<Order>();
        private long from = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        private long to = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public MainWindow()
        {
            InitializeComponent();
            _orderStatusService = new OrderStatusService();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newOrders = await _orderStatusService.GetStatus(from, to);

                foreach (var order in newOrders)
                {
                    orders.Add(order); 
                }

                UpdateUI();


                from = to;
                to = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUI()
        {
            ordersNewListBox.Items.Clear();
            ordersDoneListBox.Items.Clear();

            foreach (var order in orders)
            {
                if (order.Status == "Processing")
                {
                    ordersNewListBox.Items.Add(order.OrderNumber);
                }
                else
                {
                    ordersDoneListBox.Items.Add(order.OrderNumber);
                }
            }
        }
    }
}
