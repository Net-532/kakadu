using System.Windows;
using Backoffice.Views;
using System.Windows.Media.Imaging;

namespace Kakadu.Backoffice.Views
{
    public partial class MainWindow : Window
    {
        private string _username;
        private string _userImagePath;

        public MainWindow(string username, string userImagePath)
        {
            InitializeComponent();
            _username = username;
            _userImagePath = userImagePath;
            GreetUser();
        }

        private void GreetUser()
        {
            GreetingTextBlock.Text = $"Привіт, {_username}";


            
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_userImagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
              
            
            GreetingPanel.Visibility = Visibility.Visible;
        }

        private void HideGreeting()
        {
            GreetingPanel.Visibility = Visibility.Collapsed;
        }

        private void ProductIcon_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Products();
        }

        private void OrderIcon_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Orders();
        }

        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Users();
        }

        private void ExitIcon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =MessageBox.Show("Ви дійсно бажаєте вийти?", "Вихід", MessageBoxButton.YesNo,MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

    }
}
