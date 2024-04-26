using System.Windows;
using System.Windows.Controls;

namespace Kakadu.Backoffice.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestLabel.Content = null;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
