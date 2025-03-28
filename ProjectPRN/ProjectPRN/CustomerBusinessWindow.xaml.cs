using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for CustomerBusinessWindow.xaml
    /// </summary>
    public partial class CustomerBusinessWindow : Window
    {
        public CustomerBusinessWindow()
        {
            InitializeComponent();
            LoadTopCustomers();
        }
        private void LoadTopCustomers()
        {
            var topCustomers = MilkTeaContext.Ins.Orders
                .Include(o => o.Customer).ThenInclude(x => x.Account)
                .GroupBy(o => new { o.Customer.CustomerId, o.Customer.Account.FullName })
                .Select(g => new
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.FullName,
                    TotalAmount = g.Sum(o => o.Total)
                })
                .OrderByDescending(c => c.TotalAmount)
                .Take(5)
                .ToList();
            TopCustomersDataGrid.ItemsSource = topCustomers;
        }

        private void MenuItemOrders_Click(object sender, RoutedEventArgs e)
        {
            AdminBusinessWindow adminBusiness = new AdminBusinessWindow();
            adminBusiness.Show();
            this.Close();

        }

        private void MenuItemCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerBusinessWindow customerBusiness = new CustomerBusinessWindow();
            customerBusiness.Show();
            this.Close();
        }

        private void MenuItemProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductBusinessWindow productBusiness = new ProductBusinessWindow();
            productBusiness.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to logout?",
                "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
