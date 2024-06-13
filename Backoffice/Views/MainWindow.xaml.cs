using Kakadu.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
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

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Products();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Orders();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            HideGreeting();
            MainFrame.Content = new Users();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
