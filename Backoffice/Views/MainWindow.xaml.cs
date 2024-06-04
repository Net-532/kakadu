using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Kakadu.Backend.Services;

namespace Kakadu.Backoffice.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ProductManager productManager;
        private OrderManager orderManager;


        public MainWindow()
        {
            InitializeComponent();
            productManager = new ProductManager();
            orderManager = new OrderManager();
            
        }

        private void LoadProducts()
        {
            List<Product> products = productManager.LoadItems();
            if (products != null)
            {
                dataGrid.ItemsSource = products;
            }
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

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as Product;
            if (selectedItem != null)
            {
                productManager.DeleteItem(selectedItem.Id);
            }

            LoadProducts();
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as Product;
            if (selectedItem != null)
            {
                var dialog = new ProductDialog(selectedItem);
                if (dialog.ShowDialog() == true)
                {
                    productManager.EditItem(selectedItem.Id, selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Виберіть продукт для редагування.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadProducts();
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

        private void AddProduct(object sender, RoutedEventArgs e)
        {

            var dialog = new ProductDialog(productManager.AddItem);
            if (dialog.ShowDialog() != true) {
                MessageBox.Show("Неможливо додати новий товар.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            LoadProducts();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Visible;
            LoadProducts();

            ProductButtonsPanel.Visibility = Visibility.Visible;
            OrderButtonsPanel.Visibility = Visibility.Hidden;

            DeleteButton.Click -= DeleteProduct;
            EditButton.Click -= EditProduct;
            AddButton.Click -= AddProduct;

            DeleteButton.Click += DeleteProduct;
            EditButton.Click += EditProduct;
            AddButton.Click += AddProduct;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Visible;
            ProductButtonsPanel.Visibility = Visibility.Hidden;
            OrderButtonsPanel.Visibility = Visibility.Visible;
            LoadOrders();
        }


        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = dataGrid.SelectedItem as Order;
            if (selectedItem != null)
            {

                int orderId = selectedItem.Id;

                try
                {

                    orderManager.Print(orderId);
                    MessageBox.Show("Замовлення успішно роздруковано!");
                }
                catch (EntityNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть замовлення для друку.");
            }

        }
    }
}
