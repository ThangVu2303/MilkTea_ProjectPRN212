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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        Account acc;
        private DispatcherTimer timer;

        private void StartClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            txtTime.Content = DateTime.Now.ToString("HH:mm:ss dd-MM-yyyy");

        }
        public CustomerWindow(Account account)
        {
            InitializeComponent();
            acc = account;
            StartClock();
            LoadProduct();
            txtUsername.Content = "Welcome, " + acc.FullName;
            LoadCate();
            LoadProfile();
        }

        public void LoadProfile()
        {
            var cus = MilkTeaContext.Ins.Customers.FirstOrDefault(x=>x.AccountId == acc.AccountId);
            txtFullName.Text = acc.FullName;
            txtUser.Text = acc.Username;
            txtPassword.Text = acc.Password;
            txtPhone.Text = acc.Phone;
            txtPoints.Text = cus.Point.ToString();

        }
        public void LoadCate()
        {
            var cat = MilkTeaContext.Ins.Categories.Select(x => x.CategoryName).ToList();
            cat.Insert(0, "All");
            cbCategoryFilter.ItemsSource = cat;
            cbCategoryFilter.SelectedIndex = 0;
        }
        public void LoadProduct()
        {
            var pds = MilkTeaContext.Ins.Products.Include(x => x.Category).ToList();
            lvProducts.ItemsSource = pds;
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
        private void cbCategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string catId = cbCategoryFilter.SelectedItem.ToString();
            if (catId.Equals("All"))
            {
                lvProducts.ItemsSource = MilkTeaContext.Ins.Products.Include(x => x.Category).ToList();
            }
            else
            {
                var pds = MilkTeaContext.Ins.Products.Include(x => x.Category).Where(x => x.Category.CategoryName.Equals(catId)).ToList();
                lvProducts.ItemsSource = pds;
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var accountToUpdate = MilkTeaContext.Ins.Accounts.FirstOrDefault(a => a.AccountId == acc.AccountId);

            if (accountToUpdate != null)
            {
                
                accountToUpdate.FullName = txtFullName.Text;
                accountToUpdate.Username = txtUser.Text;
                accountToUpdate.Password = txtPassword.Text; 
                accountToUpdate.Phone = txtPhone.Text;

                MilkTeaContext.Ins.Accounts.Update(accountToUpdate);
                MilkTeaContext.Ins.SaveChanges();
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
