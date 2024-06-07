using System.Windows;

namespace Kakadu.Backoffice.Views
{
    public partial class infoWindow : Window
    {
        public infoWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}