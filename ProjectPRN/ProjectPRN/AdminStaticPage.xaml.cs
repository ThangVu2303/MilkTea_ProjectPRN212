using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.Win32;
using ProjectPRN.Business;
using ProjectPRN.Models;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for AdminStaticPage.xaml
    /// </summary>
    public partial class AdminStaticPage : Page
    {
        private readonly OrdersBusiness _orderBusiness = new OrdersBusiness();
        private readonly OrdersDetailBusiness _ordersDetailBusiness = new OrdersDetailBusiness();
        private readonly StaffBusiness _staffBusiness = new StaffBusiness();
        private readonly AdminBusiness _adminBusiness = new AdminBusiness();

        public AdminStaticPage()
        {

            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load danh sách StaffId vào ComboBox
                var staffIds = _staffBusiness.GetStaffIds();
                if (staffIds.Any())
                {
                    var staffList = staffIds.Select(id => id.ToString()).ToList();
                    staffList.Insert(0, "--- All ---");
                    cbbEmployeeId.ItemsSource = staffList;
                    cbbEmployeeId.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên nào.", "Thông báo");
                    cbbEmployeeId.ItemsSource = new List<string> { "--- All ---" };
                    cbbEmployeeId.SelectedIndex = 0;
                }

                // Load danh sách Orders ban đầu
                RefreshDgvOrders();
                if (!dgvOrder.Items.Cast<Order>().Any())
                {
                    MessageBox.Show("Không có đơn hàng nào để hiển thị.", "Thông báo");
                }

                // Load tổng doanh thu ban đầu
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

        private void RefreshDgvOrders(string search, string dateFrom, string dateTo)
        {
            try
            {
                var orders = _orderBusiness.GetOrdersByEmployeeIdAndDate(search, dateFrom, dateTo);
                dgvOrder.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc đơn hàng: {ex.Message}", "Lỗi");
                dgvOrder.ItemsSource = null;
            }
        }

        //private void BtnExport_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Office.Interop.Excel.Application excelApp = null;
        //    Microsoft.Office.Interop.Excel.Workbook workbook = null;
        //    Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

        //    try
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx" };
        //        if (sfd.ShowDialog() == true)
        //        {
        //            excelApp = new Microsoft.Office.Interop.Excel.Application();
        //            workbook = excelApp.Workbooks.Add(Type.Missing);
        //            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

        //            // Headers
        //            for (int i = 0; i < dgvOrder.Columns.Count; i++)
        //            {
        //                worksheet.Cells[1, i + 1] = dgvOrder.Columns[i].Header;
        //            }

        //            // Data
        //            var orders = dgvOrder.Items.Cast<Order>().ToList();
        //            if (orders.Any())
        //            {
        //                for (int i = 0; i < orders.Count; i++)
        //                {
        //                    worksheet.Cells[i + 2, 1] = orders[i].OrderId;
        //                    worksheet.Cells[i + 2, 2] = orders[i].StaffId;
        //                    worksheet.Cells[i + 2, 3] = orders[i].Total;
        //                    worksheet.Cells[i + 2, 4] = orders[i].DateCreate.ToString("yyyy-MM-dd");
        //                }

        //                // Total Revenue
        //                worksheet.Cells[orders.Count + 3, 1] = lblTotal.Content;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
        //                return;
        //            }

        //            worksheet.Columns.AutoFit();
        //            workbook.SaveAs(sfd.FileName);
        //            MessageBox.Show("Xuất dữ liệu thành công!", "Thành công");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
        //    }
        //    finally
        //    {
        //        if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //        if (workbook != null)
        //        {
        //            workbook.Close(false);
        //            Marshal.ReleaseComObject(workbook);
        //        }
        //        if (excelApp != null)
        //        {
        //            excelApp.Quit();
        //            Marshal.ReleaseComObject(excelApp);
        //        }
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrderDisplay();
        }

        private void CbbEmployeeId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOrderDisplay();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOrderDisplay();
        }

        private void UpdateOrderDisplay()
        {
            try
            {
                string search = txtSearch.Text?.Trim() ?? "";
                string dateFrom = dpFrom.SelectedDate.HasValue ? dpFrom.SelectedDate.Value.ToString("yyyy-MM-dd") : "1900-01-01";
                string dateTo = dpTo.SelectedDate.HasValue ? dpTo.SelectedDate.Value.ToString("yyyy-MM-dd") : "9999-12-31";

                if (cbbEmployeeId.SelectedItem?.ToString() == "--- All ---")
                {
                    RefreshDgvOrders();
                    lblTotal.Content = $"Total Revenue: {_orderBusiness.GetTotalRev():N0} VND";
                }
                else if (int.TryParse(cbbEmployeeId.SelectedItem?.ToString(), out int staffId))
                {
                    RefreshDgvOrders(staffId.ToString(), dateFrom, dateTo);
                    lblTotal.Content = $"Total Revenue: {_orderBusiness.GetTotalRevByDate(dateFrom, dateTo):N0} VND";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật hiển thị: {ex.Message}", "Lỗi");
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

                    // Load Order Details với ProductName thay vì ProductId
                    var orderDetails = _ordersDetailBusiness.GetOrdersDetailsById(orderId);
                    var orderDetailsWithProductName = orderDetails.Select(od => new
                    {
                        ProductName = od.Product?.ProductName ?? od.ProductId,
                        od.Quantity,
                        od.Price
                    });
                    dgvOrderDetails.ItemsSource = orderDetailsWithProductName;

                    // Load Staff Info
                    var staff = _staffBusiness.GetStaffById(staffId.ToString()).FirstOrDefault();
                    if (staff != null)
                    {
                        txtEId.Text = staff.StaffId.ToString();
                        txtEName.Text = staff.Account?.FullName ?? "N/A";
                        txtEDob.Text = staff.DateOfBirth.ToString("yyyy-MM-dd");
                        txtEEmail.Text = staff.Email ?? "N/A";
                        txtEPhone.Text = staff.Account?.Phone ?? "N/A";
                        rdbMale1.IsChecked = staff.Gender == "Male";
                        rdbFemale1.IsChecked = staff.Gender == "Female";
                    }
                    else
                    {
                        ClearStaffInfo();
                    }

                    // Load Admin (Manager) Info
                    var admin = _adminBusiness.GetAdmins().FirstOrDefault();
                    if (admin != null)
                    {
                        txtMId.Text = admin.AdminId.ToString();
                        txtMName.Text = admin.Account?.FullName ?? "N/A";
                        txtMDob.Text = admin.Dob.ToString("yyyy-MM-dd");
                        txtMEmail.Text = admin.Email ?? "N/A";
                        txtMPhone.Text = admin.Account?.Phone ?? "N/A";
                        rdbMale2.IsChecked = admin.Gender == "Male";
                        rdbFemale2.IsChecked = admin.Gender == "Female";
                    }
                    else
                    {
                        ClearAdminInfo();
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

        private void ClearAdminInfo()
        {
            txtMId.Text = txtMName.Text = txtMDob.Text = txtMEmail.Text = txtMPhone.Text = "";
            rdbMale2.IsChecked = rdbFemale2.IsChecked = false;
        }
    }
}
