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
    /// Interaction logic for CreateCustomerWindow.xaml
    /// </summary>

    public partial class CreateCustomerWindow : Window
    {
        Account acc;
        private DispatcherTimer timer;
        public CreateCustomerWindow(Account account)
        {
            InitializeComponent();
            acc = account;
            txtUsername.Content = "Welcome, " + acc.FullName;
            StartClock();
        }
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

        private void ButtonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra số điện thoại có bị trùng không
                if (MilkTeaContext.Ins.Accounts.Any(a => a.Phone == txtPhone.Text))
                {
                    MessageBox.Show("Phone number already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Tạo tài khoản mới cho khách hàng
                Account newAccount = new Account
                {
                    Username = txtUser.Text, // Username là số điện thoại (có thể thay đổi)
                    Password = txtPassword.Text, // Mật khẩu mặc định, cần reset sau
                    FullName = txtFullName.Text,
                    Phone = txtPhone.Text,
                    RoleId = 3 
                };

                MilkTeaContext.Ins.Accounts.Add(newAccount);
                MilkTeaContext.Ins.SaveChanges();

                // Tạo khách hàng tương ứng
                Customer newCustomer = new Customer
                {
                    AccountId = newAccount.AccountId,
                    Point = int.TryParse(txtPoints.Text, out int points) ? points : 0
                };

                MilkTeaContext.Ins.Customers.Add(newCustomer);
                MilkTeaContext.Ins.SaveChanges();
                MessageBox.Show("Customer created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staff = new StaffWindow(acc);
            staff.Show();
            this.Close();

        }
    }
}
