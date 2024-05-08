using System.Windows;
using System.Drawing.Printing;
using System.Windows.Controls;
using Kakadu.Backend.Services;


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
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
        }

        public void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintService printdef = new PrintService();
            printdef.Print("Kakadu ^)");
        }
    }
}
