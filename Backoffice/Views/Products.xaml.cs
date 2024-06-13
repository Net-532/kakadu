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
            EditButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
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
            dialog.ShowDialog();
            LoadProducts();
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as Product;
            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей продукт  ?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    productManager.DeleteItem(selectedItem.Id);
                    LoadProducts(); ;
                }
                
            }
           
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
            LoadProducts();

        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = dataGrid.SelectedItem as Product;
            bool isProductSelected = selectedProduct != null;
            EditButton.IsEnabled = isProductSelected;
            DeleteButton.IsEnabled = isProductSelected;
        }
    }
}
