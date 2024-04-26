using System.Windows;
using Kakadu.Backend.Entities;
using Kakadu.Backoffice.Services;

namespace Kakadu.Backoffice.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                AuthenticationService AuthService = new AuthenticationService();
                User user = AuthService.Authenticate(username, password);

                MainWindow Main = new MainWindow();
                Main.Show();

                this.Close();
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }


}
