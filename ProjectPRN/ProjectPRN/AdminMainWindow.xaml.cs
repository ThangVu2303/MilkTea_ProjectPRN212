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
            _account = null; // Hoặc xử lý trường hợp này phù hợp

            // Khởi tạo timer trong hàm tạo mặc định
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        // Hàm tạo có tham số cho việc khởi tạo bằng code
        public AdminMainWindow(Account account) : this() // Gọi hàm tạo mặc định
        {
            _account = account;
            txtWelcome.Content = $"Chào mừng {_account.Username}!";
            WindowState = WindowState.Maximized;
            // Timer đã được khởi tạo trong hàm tạo mặc định, không cần làm lại
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetHome();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Content = dt.ToString("HH:mm:ss");
        }

        private void GetHome()
        {
            panelDetail.Navigate(new AdminHome());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            GetHome();
        }

        private void BtnEmployee_Click(object sender, RoutedEventArgs e)
        {
            panelDetail.Navigate(new AdminEmployee());
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            panelDetail.Navigate(new AdminProductPage());
        }

        private void BtnStatic_Click(object sender, RoutedEventArgs e)
        {
            panelDetail.Navigate(new AdminStaticPage());
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            Close();
        }
    }
}
