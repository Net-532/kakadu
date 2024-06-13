using Kakadu.Backend.Entities;
using System.Windows;
using System;

namespace Kakadu.Backoffice.Views
{
    public partial class ProductDialog : Window
    {
        private readonly Product _selectedItem;
        private bool isNew { set; get; }
        private readonly Action<Product> _SaveItem;
        public ProductDialog(Product selectedItem = null)
        {
            InitializeComponent();
            _selectedItem = selectedItem;

            if (_selectedItem != null)
            {
               
                TitleTextBox.Text = _selectedItem.Title;
                PriceTextBox.Text = _selectedItem.Price.ToString();
                PhotoUrlTextBox.Text = _selectedItem.PhotoUrl;
                DescriptionTextBox.Text = _selectedItem.Description;
                isNew = false;
            }
            else
            {
                _selectedItem = new Product();
                isNew = true;
            }
        }

        public ProductDialog(Action<Product> SaveItem)
        {
            InitializeComponent();

            _SaveItem = SaveItem;

            _selectedItem = new Product();

            

            isNew = true;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem.Title = TitleTextBox.Text;
            _selectedItem.Price = decimal.Parse(PriceTextBox.Text);
            _selectedItem.PhotoUrl = PhotoUrlTextBox.Text;
            _selectedItem.Description = DescriptionTextBox.Text;

            if (isNew) {
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
