using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ProjectPRN.Business;
using ProjectPRN.Models;

namespace ProjectPRN
{
    public partial class AdminStaticPage : Page
    {
        private readonly OrdersBusiness _orderBusiness = new OrdersBusiness();
        private readonly OrdersDetailBusiness _ordersDetailBusiness = new OrdersDetailBusiness();
        private readonly StaffBusiness _staffBusiness = new StaffBusiness();

        public AdminStaticPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshDgvOrders();
                if (!dgvOrder.Items.Cast<Order>().Any())
                {
                    MessageBox.Show("Không có đơn hàng nào để hiển thị.", "Thông báo");
                }
                lblTotal.Content = $"Total Revenue: {_orderBusiness.GetTotalRev():N0} VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
            }
        }

        private void RefreshDgvOrders()
        {
            try
            {
                var orders = _orderBusiness.GetOrders();
                dgvOrder.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn hàng: {ex.Message}", "Lỗi");
                dgvOrder.ItemsSource = null;
            }
        }

        private void RefreshDgvOrders(string dateFrom, string dateTo)
        {
            try
            {
                var orders = _orderBusiness.GetOrdersByDate(dateFrom, dateTo);
                dgvOrder.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc đơn hàng: {ex.Message}", "Lỗi");
                dgvOrder.ItemsSource = null;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dateFrom = dpFrom.SelectedDate.HasValue ? dpFrom.SelectedDate.Value.ToString("yyyy-MM-dd") : "1900-01-01";
                string dateTo = dpTo.SelectedDate.HasValue ? dpTo.SelectedDate.Value.ToString("yyyy-MM-dd") : "9999-12-31";

                RefreshDgvOrders(dateFrom, dateTo);
                lblTotal.Content = $"Total Revenue: {_orderBusiness.GetTotalRevByDate(dateFrom, dateTo):N0} VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi");
            }
        }

        private void DgvOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgvOrder.SelectedItem is Order selectedOrder)
                {
                    int orderId = selectedOrder.OrderId;
                    int staffId = selectedOrder.StaffId;

                    var orderDetails = _ordersDetailBusiness.GetOrdersDetailsById(orderId);
                    var orderDetailsWithProductName = orderDetails.Select(od => new
                    {
                        ProductName = od.Product?.ProductName ?? od.ProductId,
                        od.Quantity,
                        od.Price
                    });
                    dgvOrderDetails.ItemsSource = orderDetailsWithProductName;

                    var staff = _staffBusiness.GetStaffById(staffId.ToString()).FirstOrDefault();
                    if (staff != null)
                    {
                        txtEId.Text = staff.StaffId.ToString();
                        txtEName.Text = staff.Account?.FullName ?? "N/A";
                        txtEDob.Text = staff.DateOfBirth.ToString("yyyy-MM-dd");
                        txtEEmail.Text = staff.Account.Email ?? "N/A";
                        txtEPhone.Text = staff.Account?.Phone ?? "N/A";
                        rdbMale1.IsChecked = staff.Gender == "Male";
                        rdbFemale1.IsChecked = staff.Gender == "Female";
                    }
                    else
                    {
                        ClearStaffInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn đơn hàng: {ex.Message}", "Lỗi");
            }
        }

        private void ClearStaffInfo()
        {
            txtEId.Text = txtEName.Text = txtEDob.Text = txtEEmail.Text = txtEPhone.Text = "";
            rdbMale1.IsChecked = rdbFemale1.IsChecked = false;
        }
    }
}
