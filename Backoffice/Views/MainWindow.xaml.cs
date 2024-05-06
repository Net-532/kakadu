using Kakadu.Backend.Entities;
using System.Collections.Generic;
using System.Windows;

namespace Kakadu.Backoffice.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ProductManager productManager;
        public MainWindow()
        {
            InitializeComponent();
            productManager = new ProductManager();
        }


        private void LoadProducts()
        {
            List<Product> products = productManager.LoadItems();
            if (products != null)
            {
                dataGrid.ItemsSource = products;
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as Product;
            if (selectedItem != null) {
                productManager.DeleteItem(selectedItem.Id);
            }

            //Reload Grid
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
                else
                {
                    MessageBox.Show("Please select a product to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            //Reload Grid
            LoadProducts();
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {

            var dialog = new ProductDialog(productManager.AddItem);
            if (dialog.ShowDialog() != true) {
                MessageBox.Show("Can't add new product.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            DeleteButton.Click += DeleteProduct;
            EditButton.Click += EditProduct;
            AddButton.Click += AddProduct;
        }
    }
}
