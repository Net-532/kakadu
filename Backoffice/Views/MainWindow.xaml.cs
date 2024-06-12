using System.Windows;
using Backoffice.Views;

namespace Kakadu.Backoffice.Views
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProductIcon_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Products();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Orders();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Users();
        }

        private void ExitIcon_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
