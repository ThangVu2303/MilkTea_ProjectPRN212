using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ProjectPRN.Business;

using ProjectPRN.Models;

namespace ProjectPRN
{
    public partial class AdminEmployee : Page
    {
        private readonly AccountBusiness _accountBusiness = new AccountBusiness();
        private readonly RoleBusiness _roleBusiness = new RoleBusiness();
        private readonly StaffBusiness _staffBusiness = new StaffBusiness();

        public AdminEmployee()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAccount();
            LoadRoles();
        }

        private void LoadAccount()
        {
            var accounts = _accountBusiness.GetAccounts();
            dataGridView1.ItemsSource = accounts;

            // Bindings
            if (accounts.Any())
            {
                txtUserId.Text = accounts.First().AccountId.ToString();
                txtUsername.Text = accounts.First().Username;
                txtPassword.Text = accounts.First().Password;
                txtFullName.Text = accounts.First().FullName; // Thêm FullName
                txtPhone.Text = accounts.First().Phone;       // Thêm Phone
                cboRoleId.SelectedValue = accounts.First().RoleId;
            }
            else
            {
                Clear(); // Xóa form nếu không có dữ liệu
            }
        }

        private void LoadRoles()
        {
            cboRoleId.ItemsSource = _roleBusiness.GetRoles();
            cboRoleId.DisplayMemberPath = "RoleName"; // Hiển thị tên vai trò
            cboRoleId.SelectedValuePath = "RoleId";   // Giá trị được chọn là RoleId
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidForm())
            {
                try
                {
                    var account = new Account
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text.Trim(),
                        FullName = txtFullName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        RoleId = int.Parse(cboRoleId.SelectedValue.ToString())
                    };

                    _accountBusiness.InsertAccount(account);
                    txtUserId.Text = account.AccountId.ToString(); // Cập nhật AccountId sau khi chèn
                    LoadAccount();
                    MessageBox.Show("Thêm tài khoản thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
                }
            }
        }

        private bool ValidForm()
        {
            bool flag = true;
            string strError = "";

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || !Regex.IsMatch(txtUsername.Text.Trim(), @"^[0-9a-zA-Z\s]+$"))
            {
                flag = false;
                strError += "Tên người dùng không được để trống hoặc chứa ký tự đặc biệt!\n";
                txtUsername.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text) || !Regex.IsMatch(txtPassword.Text.Trim(), @"^[0-9a-zA-Z\s]+$"))
            {
                flag = false;
                strError += "Mật khẩu không được để trống hoặc chứa ký tự đặc biệt!\n";
                txtPassword.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text) || txtFullName.Text.Length > 100)
            {
                flag = false;
                strError += "Họ tên không được để trống và không vượt quá 100 ký tự!\n";
                txtFullName.Focus();
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Length > 15 || !Regex.IsMatch(txtPhone.Text.Trim(), @"^[0-9]+$"))
            {
                flag = false;
                strError += "Số điện thoại không được để trống, chỉ chứa số và không vượt quá 15 ký tự!\n";
                txtPhone.Focus();
            }

            if (cboRoleId.SelectedValue == null || !int.TryParse(cboRoleId.SelectedValue.ToString(), out int roleId))
            {
                flag = false;
                strError += "Vui lòng chọn một vai trò hợp lệ!\n";
                cboRoleId.Focus();
            }

            if (!flag)
            {
                MessageBox.Show(strError);
            }
            return flag;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtUserId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullName.Text = ""; // Thêm FullName
            txtPhone.Text = "";    // Thêm Phone
            cboRoleId.SelectedIndex = -1;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text) || !int.TryParse(txtUserId.Text, out int accountId))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa!");
                return;
            }

            try
            {
                var account = new Account { AccountId = accountId };

                // Nếu là Staff (RoleId = 2), xóa bản ghi trong Staff trước
                if (int.Parse(cboRoleId.SelectedValue.ToString()) == 2)
                {
                    _staffBusiness.DeleteStaff(accountId);
                }

                _accountBusiness.DeleteAccount(account);
                LoadAccount();
                Clear();
                MessageBox.Show("Xóa tài khoản thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text) || !int.TryParse(txtUserId.Text, out int accountId))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để cập nhật!");
                return;
            }

            if (ValidForm())
            {
                try
                {
                    var account = new Account
                    {
                        AccountId = accountId,
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text.Trim(),
                        FullName = txtFullName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        RoleId = int.Parse(cboRoleId.SelectedValue.ToString())
                    };

                    _accountBusiness.UpdateAccount(account);
                    LoadAccount();
                    MessageBox.Show("Cập nhật tài khoản thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
                }
            }
        }



        private void DataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridView1.SelectedItem is Account selectedAccount)
            {
                txtUserId.Text = selectedAccount.AccountId.ToString();
                txtUsername.Text = selectedAccount.Username;
                txtPassword.Text = selectedAccount.Password;
                txtFullName.Text = selectedAccount.FullName;
                txtPhone.Text = selectedAccount.Phone;
                cboRoleId.SelectedValue = selectedAccount.RoleId;
            }
        }

       
    }
}