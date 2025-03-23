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
using System.Windows.Threading;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for OrderViewWindow.xaml
    /// </summary>
    public partial class OrderViewWindow : Window
    {

        Account acc;
        private DispatcherTimer timer;
        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Cập nhật mỗi giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            txtTime.Content = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");

        }

        public OrderViewWindow(Account account)
        {
            InitializeComponent();
            acc = account;
            txtUsername.Content = "Welcome, " + acc.FullName;
            StartClock();
            LoadOrders();
        }

        private void LoadOrders()
        {
            lvOrders.ItemsSource = MilkTeaContext.Ins.Orders.Include(x=>x.Staff).ThenInclude(x=>x.Account).Include(x=>x.Customer).ThenInclude(x => x.Account).ToList();
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvOrders.SelectedItem is Order selectedOrder)
            {
              lvOrderDetails.ItemsSource = MilkTeaContext.Ins.OrdersDetails
                    .Where(od => od.OrderId == selectedOrder.OrderId).Include(x=>x.Product)
                    .ToList();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staffWindow = new StaffWindow(acc);
            staffWindow.Show();
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
