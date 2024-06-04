using Kakadu.Backend.Entities;
using Kakadu.Backoffice.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Backoffice.Views
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private OrderManager orderManager;
        public Orders()
        {
            InitializeComponent();
            orderManager = new OrderManager();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            List<Order> orders = new List<Order>();
            if (string.IsNullOrEmpty(SearchBox.Text))
            {
                dataGrid.ItemsSource = orderManager.LoadItems();
            }
            else
            {
                int number = Convert.ToInt32(SearchBox.Text);
                Order numOrder = orderManager.GetByNumber(number);
                if (numOrder != null)
                {
                    orders.Add(numOrder);
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = orders;
                }
                else
                {
                    MessageBox.Show("Замовлення з таким номером не існує", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void ChangeOrderStatus(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as Order;
            if (selectedItem != null)
            {
                orderManager.ChangeStatus(selectedItem.Id, "Done");
                LoadOrders();
            }
        }

        private void SearchBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void SearchOrder(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }
    }
}
