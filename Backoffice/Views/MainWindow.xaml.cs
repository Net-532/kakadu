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
        private ProductManager productManager;
        private OrderManager orderManager;
        private UserManager userManager;

        public MainWindow()
        {
            InitializeComponent();
            productManager = new ProductManager();
            orderManager = new OrderManager();
            userManager = new UserManager();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Products();
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Orders();
            MainGrid.Visibility = Visibility.Hidden;
        }

        private void LoadUsers()
        {
            List<User> users = userManager.LoadItems();
            if (users != null)
            {
                dataGrid.ItemsSource = users;
            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            var userDialog = new UserDialog(userManager.AddItem);
            if (userDialog.ShowDialog() != true)
            {
                MessageBox.Show("Неможливо додати нового користувача.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadUsers();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as User;
            if (selectedItem != null)
            {
                userManager.DeleteItem(selectedItem.Id);
            }
            LoadUsers();
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem as User;
            if (selectedItem != null)
            {
                var dialog = new UserDialog(selectedItem);
                if (dialog.ShowDialog() == true)
                {
                    userManager.EditItem(selectedItem.Id, selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Виберіть користувача для редагування.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            LoadUsers();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {

            MainFrame.Content = null;
            MainGrid.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Visible;
            LoadUsers();

            UserButtonsPanel.Visibility = Visibility.Visible;

            DeleteUserButton.Click -= DeleteUser;
            EditUserButton.Click -= EditUser;
            AddUserButton.Click -= AddUser;

            DeleteUserButton.Click += DeleteUser;
            EditUserButton.Click += EditUser;
            AddUserButton.Click += AddUser;
        }
    }
}
