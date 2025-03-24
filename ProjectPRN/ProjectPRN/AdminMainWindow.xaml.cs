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
using ProjectPRN.Models;
using System.Windows.Threading;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        private readonly Account _account;
        private readonly DispatcherTimer _timer;

        // Hàm tạo mặc định cho XAML
        public AdminMainWindow()
        {
            InitializeComponent();
            _account = null;

            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        // Hàm tạo có tham số
        public AdminMainWindow(Account account) : this() 
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            _account = account;
            txtWelcome.Content = $"Chào mừng {_account.Username}!";
            WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHomePage();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void LoadHomePage()
        {
            try
            {
                panelDetail.Navigate(new AdminHome());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang chủ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            LoadHomePage();
        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                panelDetail.Navigate(new AdminEmployee());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang nhân viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                panelDetail.Navigate(new AdminProductPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang sản phẩm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnStatic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                panelDetail.Navigate(new AdminStaticPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang thống kê: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Close(); 
            }
        }
    }
}