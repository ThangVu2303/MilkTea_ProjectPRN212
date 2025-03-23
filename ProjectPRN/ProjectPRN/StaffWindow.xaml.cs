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
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        Account acc;
        private DispatcherTimer timer;
        private int customerPoints = 0;
        private decimal totalPrice = 0;
        private const decimal pointValue = 1000;
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
        public StaffWindow(Account account)
        {
            InitializeComponent();
            acc = account;
            LoadProduct();
            LoadCate();
            LoadCustomer();
            txtUsername.Content = "Welcome, " + acc.FullName;
            StartClock();
        }
         List<CartItem> listCart = new List<CartItem>();

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                
                StackPanel parentPanel = button.Parent as StackPanel;
                if (parentPanel != null)
                {
                    TextBox txtQuantity = parentPanel.Children.OfType<TextBox>().FirstOrDefault();
                    Product selectedProduct = button.DataContext as Product;
                    if (selectedProduct != null && txtQuantity != null && int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
                    {
                        var existingItem = listCart.FirstOrDefault(p => p.Product.ProductId == selectedProduct.ProductId);
                        int currentCartQuantity = existingItem != null ? existingItem.Quantity : 0;
                        if (currentCartQuantity + quantity > selectedProduct.Quantity)
                        {
                            MessageBox.Show($"Không đủ hàng! Số lượng tối đa có thể mua: {selectedProduct.Quantity - currentCartQuantity}");
                            return;
                        }
                        AddToCart(selectedProduct, quantity);
                    }
                    else
                    {

                        MessageBox.Show("Vui lòng nhập số lượng hợp lệ!");
                    }
                }
            }
        }

        
       
        public void AddToCart(Product product, int quantity)
        {
            var existingItem = listCart.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                
            }
            else
            {
                listCart.Add(new CartItem { Product = product, Quantity = quantity});
            }

            // Cập nhật UI
            lvCart.ItemsSource = null;
            lvCart.ItemsSource = listCart;

            // Cập nhật tổng tiền
            totalPrice = lvCart.Items.Cast<CartItem>().Sum(item => item.Subtotal);
            chkUsePoints_Checked(null, null);
            txtTotalPrice.Text = totalPrice.ToString("N0") + " VND";
        }
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CartItem selectedItem)
            {
                RemoveFromCart(selectedItem);
                lvCart.Items.Refresh();
                totalPrice = lvCart.Items.Cast<CartItem>().Sum(item => item.Subtotal);
                txtTotalPrice.Text = totalPrice.ToString("N0") + " VND";
            }
        }
        public void RemoveFromCart(CartItem product)
        {
            var item = listCart.FirstOrDefault(c => c.Product.ProductId == product.Product.ProductId);
            if (item != null) listCart.Remove(item);
            
        }

        public void LoadCustomer()
        {
            var cds = MilkTeaContext.Ins.Customers.Include(x=>x.Account).ToList();
            lvCustomers.ItemsSource = cds;
        }

        
        public void LoadProduct()
        {
            var pds = MilkTeaContext.Ins.Products.Include(x => x.Category).ToList();
            lvProducts.ItemsSource = pds;
        }
        public void LoadCate()
        {
            var cat = MilkTeaContext.Ins.Categories.Select(x=>x.CategoryName).ToList();
            cat.Insert(0, "All");
            cbCategoryFilter.ItemsSource = cat;
            cbCategoryFilter.SelectedIndex = 0;
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
                lvProducts.ItemsSource = MilkTeaContext.Ins.Products.Include(x=>x.Category).ToList();
            }
            else
            {
                var pds = MilkTeaContext.Ins.Products.Include(x=>x.Category).Where(x=>x.Category.CategoryName.Equals(catId)).ToList();
                lvProducts.ItemsSource = pds;
            }
        }
        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string phone = txtPhoneNumber.Text.Trim();
                // Tìm khách hàng theo số điện thoại
                var customer = MilkTeaContext.Ins.Customers.Include(c => c.Account).FirstOrDefault(c => c.Account.Phone == phone);

            if (customer != null)
            {
                txtCustomerInfo.Text = $"{customer.Account.FullName}";
                customerPoints = customer.Point.GetValueOrDefault(); 
                txtCustomerPoints.Text = customerPoints.ToString();
            }
            else
            {
                txtCustomerInfo.Text = "Customer not found!";
                customerPoints = 0;
                txtCustomerPoints.Text = "0";
            }

        }

        private void chkUsePoints_Checked(object sender, RoutedEventArgs e)
        {
            decimal discount = chkUsePoints.IsChecked == true ? Math.Min(customerPoints * pointValue, totalPrice) : 0;
            txtTotalPrice.Text = (totalPrice - discount).ToString("N0") + " VND";
        }

        
    }
}
