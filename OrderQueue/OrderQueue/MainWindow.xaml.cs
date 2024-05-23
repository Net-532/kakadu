using System;
using System.Collections.Generic;
using System.Windows;
using OrderStatusClient;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService;
        private IDictionary<int, string> orders = new Dictionary<int, string>();
        private long from = DateTimeOffset.Now.ToUnixTimeSeconds();
        private long to = DateTimeOffset.Now.ToUnixTimeSeconds();

        public MainWindow()
        {
            InitializeComponent();
            _orderStatusService = new OrderStatusService();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                to = DateTimeOffset.Now.ToUnixTimeSeconds(); 
                var newOrders = await _orderStatusService.GetStatus(from, to);

                foreach (var order in newOrders)
                {
                    orders[order.OrderNumber] = order.Status; 
                }

                UpdateUI();

                from = to;
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
