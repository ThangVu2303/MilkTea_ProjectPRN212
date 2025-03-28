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
    /// Interaction logic for ProductBusinessWindow.xaml
    /// </summary>
    public partial class ProductBusinessWindow : Window
    {
        public ProductBusinessWindow()
        {
            InitializeComponent();
            LoadFilters();
            LoadTopSellingProducts();
        }
        private void LoadFilters()
        {
            var months = Enumerable.Range(1, 12).Select(m => m.ToString()).ToList();
            months.Insert(0, "All");

            var years = MilkTeaContext.Ins.Orders
                        .Select(o => o.DateCreate.Year)
                        .Distinct()
                        .OrderByDescending(y => y)
                        .Select(y => y.ToString())
                        .ToList();
            years.Insert(0, "All");

            cbMonth.ItemsSource = months;
            cbYear.ItemsSource = years;

            cbMonth.SelectedIndex = 0;
            cbYear.SelectedIndex = 0;
        }


        private void LoadTopSellingProducts()
        {
            int selectedMonth = cbMonth.SelectedItem.ToString() == "All" ? 0 : int.Parse(cbMonth.SelectedItem.ToString());
            int selectedYear = cbYear.SelectedItem.ToString() == "All" ? 0 : int.Parse(cbYear.SelectedItem.ToString());

            var query = MilkTeaContext.Ins.OrdersDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .AsQueryable();

            if (selectedYear > 0)
            {
                query = query.Where(od => od.Order.DateCreate.Year == selectedYear);
            }
            if (selectedMonth > 0)
            {
                query = query.Where(od => od.Order.DateCreate.Month == selectedMonth);
            }

            var topProducts = query
                .GroupBy(od => new { od.Product.ProductId, od.Product.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalSold = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(p => p.TotalSold)
                .Take(5)
                .ToList();

            TopProductsDataGrid.ItemsSource = topProducts;
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadTopSellingProducts();
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
