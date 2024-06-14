using System.Windows;
using Backoffice.Views;

namespace Kakadu.Backoffice.Views
{
    public partial class MainWindow : Window
    {
      

        public MainWindow(string username)
        {
            InitializeComponent();
            GreetingTextBlock.Text = $"Привіт, {username}";
        }

        private void ProductIcon_Click(object sender, RoutedEventArgs e)
        {
           
            MainFrame.Content = new Products();
        }

        private void OrderIcon_Click(object sender, RoutedEventArgs e)
        {
          
            MainFrame.Content = new Orders();
        }

        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
         
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
