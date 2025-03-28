using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using ProjectPRN.Business;
using ProjectPRN.Models;
using Path = System.IO.Path;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for AdminProductPage.xaml
    /// </summary>
    public partial class AdminProductPage : Page
    {

        private readonly MilkTeaContext _context;
        private readonly ProductBusiness _productBusiness = new ProductBusiness();
        private readonly CategoryBusiness _categoryBusiness = new CategoryBusiness();

        public AdminProductPage()
        {

            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProduct();
            LoadCategories();
        }

        private void LoadProduct()
        {
            var products = _productBusiness.GetProducts();
            dataGridView1.ItemsSource = products;

            if (products.Any())
            {
                txtProductId.Text = products.First().ProductId;
                txtProductName.Text = products.First().ProductName;
                txtPrice.Text = products.First().Price.ToString();
                txtOrigin.Text = products.First().Origin;
                comboBox1.SelectedValue = products.First().CategoryId;
                txtPicture.Text = products.First().Image;
            }
        }

        private void LoadCategories()
        {
            comboBox1.ItemsSource = _categoryBusiness.GetCategories();
        }

        private bool ValidForm()
        {
            bool flag = true;
            string strError = "";

            // Kiểm tra ProductId
            if (string.IsNullOrWhiteSpace(txtProductId.Text) || txtProductId.Text.Length > 50)
            {
                flag = false;
                strError += "Product ID không được để trống và không vượt quá 50 ký tự!\n";
                txtProductId.Focus();
            }

            // Kiểm tra ProductName
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || txtProductName.Text.Length > 50)
            {
                flag = false;
                strError += "Tên sản phẩm không được để trống và không vượt quá 50 ký tự!\n";
                txtProductName.Focus();
            }

            // Kiểm tra Price
            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                flag = false;
                strError += "Giá phải là số hợp lệ và không nhỏ hơn 0!\n";
                txtPrice.Focus();
            }

            // Kiểm tra Origin
            if (string.IsNullOrWhiteSpace(txtOrigin.Text) || txtOrigin.Text.Length > 50)
            {
                flag = false;
                strError += "Nguồn gốc không được để trống và không vượt quá 50 ký tự!\n";
                txtOrigin.Focus();
            }

            // Kiểm tra CategoryId
            if (comboBox1.SelectedValue == null || !int.TryParse(comboBox1.SelectedValue.ToString(), out int categoryId))
            {
                flag = false;
                strError += "Vui lòng chọn một danh mục hợp lệ!\n";
                comboBox1.Focus();
            }

            // Kiểm tra Image
            if (string.IsNullOrWhiteSpace(txtPicture.Text) || txtPicture.Text.Length > 250)
            {
                flag = false;
                strError += "Hình ảnh không được để trống và không vượt quá 250 ký tự!\n";
                txtPicture.Focus();
            }
            else if (!File.Exists(txtPicture.Text))
            {
                flag = false;
                strError += "Đường dẫn hình ảnh không tồn tại!\n";
                txtPicture.Focus();
            }

            if (!flag)
            {
                MessageBox.Show(strError);
            }
            return flag;
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (ofd.ShowDialog() == true)
            {
                string targetDirectory = @"D:\Ki 5_PRN_Trenlop\Project\ProjectPRN 123\ProjectPRN\ProjectPRN\ProductImage\";
                string targetPath = Path.Combine(targetDirectory, Path.GetFileName(ofd.FileName));

                try
                {
                    File.Copy(ofd.FileName, targetPath, true);
                    txtPicture.Text = targetPath;
                    pictureBox1.Source = new BitmapImage(new Uri(targetPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying file: {ex.Message}", "Error");
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtOrigin.Text = "";
            comboBox1.SelectedIndex = -1;
            txtPicture.Text = "";
            pictureBox1.Source = null;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidForm())
            {
                try
                {
                    var product = new Product
                    {
                        ProductId = txtProductId.Text.Trim(),
                        ProductName = txtProductName.Text.Trim(),
                        Price = Convert.ToDecimal(txtPrice.Text.Trim()),
                        Origin = txtOrigin.Text.Trim(),
                        CategoryId = Convert.ToInt32(comboBox1.SelectedValue),
                        Image = txtPicture.Text.Trim(),
                        Quantity = 10 // Giá trị mặc định vì không có trường nhập liệu
                    };

                    _productBusiness.InsertProduct(product);
                    LoadProduct();
                    MessageBox.Show("Thêm sản phẩm thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}\nChi tiết: {ex.InnerException?.Message}", "Lỗi");
                }
            }
        }



        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Product
                {
                    ProductId = txtProductId.Text,
                    ProductName = txtProductName.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Origin = txtOrigin.Text,
                    CategoryId = Convert.ToInt32(comboBox1.SelectedValue),
                    Image = txtPicture.Text
                };

                _productBusiness.UpdateProduct(product);
                LoadProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = new Product
                {
                    ProductId = txtProductId.Text,
                    ProductName = txtProductName.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    Origin = txtOrigin.Text,
                    CategoryId = Convert.ToInt32(comboBox1.SelectedValue),
                    Image = txtPicture.Text
                };

                _productBusiness.DeleteProduct(product);
                LoadProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TxtPicture_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPicture.Text) && File.Exists(txtPicture.Text))
            {
                try
                {
                    pictureBox1.Source = new BitmapImage(new Uri(txtPicture.Text));
                }
                catch
                {
                    pictureBox1.Source = null;
                }
            }
            else
            {
                pictureBox1.Source = null;
            }
        }

        private void DataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridView1.SelectedItem is Product selectedProduct)
            {
                txtProductId.Text = selectedProduct.ProductId;
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtOrigin.Text = selectedProduct.Origin;
                comboBox1.SelectedValue = selectedProduct.CategoryId;
                txtPicture.Text = selectedProduct.Image;
            }
        }
    }
}