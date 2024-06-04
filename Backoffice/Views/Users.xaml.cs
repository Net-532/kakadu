using Kakadu.Backend.Entities;
using Kakadu.Backoffice.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Backoffice.Views
{
   
    public partial class Users : UserControl
    {
        private UserManager userManager;

        public Users()
        {
            InitializeComponent();
            userManager = new UserManager();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
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
    }
}
