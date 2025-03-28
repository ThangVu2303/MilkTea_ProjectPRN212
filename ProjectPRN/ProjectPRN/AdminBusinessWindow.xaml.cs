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
    /// Interaction logic for AdminBusinessWindow.xaml
    /// </summary>
    public partial class AdminBusinessWindow : Window
    {
        public AdminBusinessWindow()
        {
            InitializeComponent();
            var months = new List<string> { "All" };
            months.AddRange(Enumerable.Range(1, 12).Select(m => m.ToString()));
            cbMonth.ItemsSource = months;
            cbMonth.SelectedIndex = 0; // Mặc định chọn "All"

            var years = new List<string> { "All" };
            years.AddRange(Enumerable.Range(2020, DateTime.Now.Year - 2019).Select(y => y.ToString()));
            cbYear.ItemsSource = years;
            cbYear.SelectedIndex = 0; // Mặc định chọn "All"

            LoadOrderStatistics(); // Hiển thị dữ liệu ban đầu
        }

        private void LoadOrderStatistics(string month = "All", string year = "All")
        {
            try
            {
                var query = MilkTeaContext.Ins.Orders.AsQueryable();

                if (month != "All")
                    query = query.Where(o => o.DateCreate.Month == int.Parse(month));
                if (year != "All")
                    query = query.Where(o => o.DateCreate.Year == int.Parse(year));

                var totalOrders = query.Count();
                var totalOriginalAmount = query.Sum(o => o.OriginalTotal);
                var totalAmount = query.Sum(o => o.Total);

                var disposedQuery = MilkTeaContext.Ins.DisposedMaterials.AsQueryable();
                if (month != "All")
                    disposedQuery = disposedQuery.Where(d => d.DateDisposed.Month == int.Parse(month));
                if (year != "All")
                    disposedQuery = disposedQuery.Where(d => d.DateDisposed.Year == int.Parse(year));

                var totalDisposedAmount = disposedQuery.Sum(x => (decimal)x.Quantity * x.Material.CostPerUnit);

                var profitPercentage = totalOriginalAmount > 0
                    ? ((totalAmount - totalOriginalAmount - totalDisposedAmount) / totalOriginalAmount) * 100
                    : 0;

                txtTotalOrder.Text = $"{totalOrders} đơn hàng";
                txtTotalOriginalAmount.Text = $"{totalOriginalAmount:N0} VND";
                txtTotalAmount.Text = $"{totalAmount:N0} VND";
                txtProfit.Text = $"{profitPercentage:N2} %";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

       


        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
           
            string selectedMonth = cbMonth.SelectedItem.ToString();
            string selectedYear = cbYear.SelectedItem.ToString();
            LoadOrderStatistics(selectedMonth, selectedYear);
        }

    }
}

