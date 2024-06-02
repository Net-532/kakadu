using Kakadu.Backend.Entities;
using Kakadu.Backoffice.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Backoffice.Views
{
    public partial class Products : UserControl
    {
        private ProductManager productManager;

        public Products()
        {
            InitializeComponent();
            productManager = new ProductManager();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            List<Product> products = productManager.LoadItems();
            if (products != null)
            {
                dataGrid.ItemsSource = products;
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog(productManager.AddItem);
            if (dialog.ShowDialog() != true)
            {
                MessageBox.Show("Неможливо додати новий товар.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadProducts();
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
    }
}
