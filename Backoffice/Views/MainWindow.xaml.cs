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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
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

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            infoWindow infoWindow = new infoWindow();
            infoWindow.Show();
            
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
