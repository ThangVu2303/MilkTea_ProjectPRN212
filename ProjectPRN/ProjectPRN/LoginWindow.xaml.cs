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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            var acc = MilkTeaContext.Ins.Accounts.FirstOrDefault(x=>x.Username == username && x.Password == password);
            if (acc != null) { 
                if(acc.RoleId == 1)
                {
                    MessageBox.Show($"Chào mừng {acc.FullName}! ",
                                   "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    AdminMainWindow admin = new AdminMainWindow();
                    admin.Show();
                    this.Close();
                }
                if (acc.RoleId == 2)
                {
                    MessageBox.Show($"Chào mừng nhân viên {acc.FullName}! ",
                                   "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    StaffWindow staffWindow = new StaffWindow(acc);
                    staffWindow.Show();
                    this.Close();
                }
                if (acc.RoleId == 3)
                {
                    MessageBox.Show($"Chào mừng khách hàng {acc.FullName}! ",
                                   "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    CustomerWindow customerWindow = new CustomerWindow(acc);  
                    customerWindow.Show();
                    this.Close();
                }
                if (acc.RoleId == 4)
                {
                    MessageBox.Show($"Chào mừng admin {acc.FullName}! ",
                                   "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    AdminBusinessWindow adminBusiness = new AdminBusinessWindow();
                    adminBusiness.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonShutdown_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn thoát khỏi ứng dụng", "Xác nhận thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void ForgotPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
        }

    }
}
