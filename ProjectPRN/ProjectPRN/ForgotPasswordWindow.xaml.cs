using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            //string email = txtEmail.Text.Trim();

            //if (string.IsNullOrEmpty(email))
            //{
            //    MessageBox.Show("Vui lòng nhập email của bạn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //var acc = MilkTeaContext.Ins.Accounts.FirstOrDefault(x => x.Email == email);
            //if (acc != null)
            //{
            //    // Tạo mật khẩu mới ngẫu nhiên
            //    string newPassword = GenerateNewPassword();
            //    acc.Password = newPassword;
            //    MilkTeaContext.Ins.SaveChanges();

            //    // Gửi email chứa mật khẩu mới
            //    bool emailSent = SendEmail(email, newPassword);
            //    if (emailSent)
            //    {
            //        MessageBox.Show($"Mật khẩu mới đã được gửi đến email: {email}",
            //                        "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Gửi email thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }

            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private string GenerateNewPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8); // Mật khẩu 8 ký tự ngẫu nhiên
        }

        private bool SendEmail(string recipientEmail, string newPassword)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int port = 587;
                string senderEmail = "duongdinhcuong2004@gmail.com";
                string senderPassword = "vtcw ichq hnne zrju"; // Hoặc App Password

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipientEmail);
                mail.Subject = "Reset Password - MilkTea POS";
                mail.Body = $"Xin chào,\n\nMật khẩu mới của bạn là: {newPassword}\nVui lòng đăng nhập và đổi lại mật khẩu.\n\nTrân trọng,\nMilkTea POS Team";
                mail.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient(smtpServer, port);
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}