using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;
using OrderStatusClient;

namespace Kakadu.OrderQueue
{
    public partial class MainWindow : Window
    {
        private readonly OrderStatusService _orderStatusService;
        private List<string> newOrders;
        private List<string> completedOrders;

        public MainWindow()
        {
            InitializeComponent();
            _orderStatusService = new OrderStatusService();
            newOrders = new List<string>();
            completedOrders = new List<string>();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                string status = await _orderStatusService.GetStatus(timestamp);
                if (!newOrders.Contains(status) && !completedOrders.Contains(status))
                {
                    newOrders.Add(status);
                    newOrdersListBox.Items.Add(status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
