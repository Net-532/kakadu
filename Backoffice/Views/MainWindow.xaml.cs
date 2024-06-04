using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Backoffice.Views;

namespace Kakadu.Backoffice.Views
{
    public partial class MainWindow : Window
    {
        private OrderManager orderManager;

        public MainWindow()
        {
            InitializeComponent();
            orderManager = new OrderManager();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Products();
            MainGrid.Visibility = Visibility.Hidden;
        }
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Users();
            MainGrid.Visibility = Visibility.Hidden;
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Visible;
            OrderButtonsPanel.Visibility = Visibility.Visible;
          
            LoadOrders();
        }

     
      

       

        


    }
}
