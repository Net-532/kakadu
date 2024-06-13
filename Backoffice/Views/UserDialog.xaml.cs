using Kakadu.Backend.Entities;
using System.Windows;
using System;

namespace Kakadu.Backoffice.Views
{
    public partial class UserDialog : Window
    {
        private readonly User _selectedItem;
        private bool isNew { set; get; }
        private readonly Action<User> _SaveItem;
        public UserDialog(User selectedItem = null)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
                IdTextBox.Text = _selectedItem.Id.ToString();
                UsernameTextBox.Text = _selectedItem.Username;
                FirstNameTextBox.Text = _selectedItem.FirstName;
                LastNameTextBox.Text = _selectedItem.LastName;
                PasswordTextBox.Text = _selectedItem.Password;
                isNew = false;
            }
            else
            {
                _selectedItem = new User();
                isNew = true;
            }
        }

        public UserDialog(Action<User> SaveItem)
        {
            InitializeComponent();

            _SaveItem = SaveItem;

            _selectedItem = new User();

            IdTextBox.Text = "Automated";

            isNew = true;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem.Username = UsernameTextBox.Text;
            _selectedItem.FirstName = FirstNameTextBox.Text;
            _selectedItem.LastName = LastNameTextBox.Text;
            _selectedItem.Password = PasswordTextBox.Text;

            if (isNew)
            {
                _SaveItem(_selectedItem);
            }

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
