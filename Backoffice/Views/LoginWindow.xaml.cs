using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Kakadu.Backend.Entities;
using Kakadu.Backend.Repositories;
using Kakadu.Backend.Services;
using Kakadu.Backoffice.Services;

namespace Kakadu.Backoffice.Views
{

    public partial class LoginWindow : Window
    {
        private static readonly IUserService _userService = new UserService(new UserRepositoryDB());

        public LoginWindow()
        {
            InitializeComponent();
        }
    
        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Логін")
            {
                UsernameTextBox.Text = "";
            }
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Логін";
            }
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "00000")
            {
                PasswordBox.Password = "";
            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Password = "00000";
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                AuthenticationService AuthService = new AuthenticationService(_userService);
                User user = AuthService.Authenticate(username, password);

                MainWindow Main = new MainWindow(username);
                Main.Show();

                this.Close();
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
