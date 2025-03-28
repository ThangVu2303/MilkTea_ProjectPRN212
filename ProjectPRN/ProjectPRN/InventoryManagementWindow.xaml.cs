using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN
{
    public partial class InventoryManagementWindow : Page
    {
        public InventoryManagementWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadRawMaterials();
                LoadRecipes();
                LoadDisposedMaterials();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRawMaterials()
        {
            lvRawMaterials.ItemsSource = MilkTeaContext.Ins.RawMaterials.ToList();
        }

        private void LoadRecipes()
        {
            lvRecipes.ItemsSource = MilkTeaContext.Ins.Recipes
                .Include(r => r.Product)
                .Include(r => r.Material)
                .ToList();
        }

        private void LoadDisposedMaterials()
        {
            lvDisposedMaterials.ItemsSource = MilkTeaContext.Ins.DisposedMaterials
                .Include(d => d.Material)
                .ToList();
        }

        private void LoadProducts()
        {
            cbProducts.ItemsSource = MilkTeaContext.Ins.Products.ToList();
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            var dialog = CreateDialog("Thêm Nguyên Liệu", 300, 300);
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var txtName = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var txtUnit = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var txtCost = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var txtQuantity = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var btnSave = new Button { Content = "Lưu", Margin = new Thickness(0, 10, 0, 0) };

            stackPanel.Children.Add(new TextBlock { Text = "Tên nguyên liệu:" });
            stackPanel.Children.Add(txtName);
            stackPanel.Children.Add(new TextBlock { Text = "Đơn vị:" });
            stackPanel.Children.Add(txtUnit);
            stackPanel.Children.Add(new TextBlock { Text = "Giá/Đơn vị:" });
            stackPanel.Children.Add(txtCost);
            stackPanel.Children.Add(new TextBlock { Text = "Số lượng:" });
            stackPanel.Children.Add(txtQuantity);
            stackPanel.Children.Add(btnSave);

            btnSave.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtUnit.Text))
                {
                    MessageBox.Show("Tên nguyên liệu và đơn vị không được để trống!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCost.Text, out decimal cost) || !float.TryParse(txtQuantity.Text, out float qty) || cost < 0 || qty < 0)
                {
                    MessageBox.Show("Giá hoặc số lượng không hợp lệ! Vui lòng nhập số không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var material = new RawMaterial
                    {
                        MaterialName = txtName.Text,
                        Unit = txtUnit.Text,
                        CostPerUnit = cost,
                        Quantity = qty
                    };
                    MilkTeaContext.Ins.RawMaterials.Add(material);
                    MilkTeaContext.Ins.SaveChanges();
                    LoadRawMaterials();
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu nguyên liệu: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            dialog.Content = stackPanel;
            dialog.ShowDialog();
        }

        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.Tag == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định nguyên liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int materialId = (int)button.Tag;
            var material = MilkTeaContext.Ins.RawMaterials.FirstOrDefault(m => m.MaterialId == materialId);
            if (material == null)
            {
                MessageBox.Show("Không tìm thấy nguyên liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = CreateDialog("Chỉnh sửa Nguyên Liệu", 300, 300);
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var txtName = new TextBox { Text = material.MaterialName, Margin = new Thickness(0, 0, 0, 10) };
            var txtUnit = new TextBox { Text = material.Unit, Margin = new Thickness(0, 0, 0, 10) };
            var txtCost = new TextBox { Text = material.CostPerUnit.ToString(), Margin = new Thickness(0, 0, 0, 10) };
            var txtQuantity = new TextBox { Text = material.Quantity.ToString(), Margin = new Thickness(0, 0, 0, 10) };
            var btnSave = new Button { Content = "Lưu", Margin = new Thickness(0, 10, 0, 0) };

            stackPanel.Children.Add(new TextBlock { Text = "Tên nguyên liệu:" });
            stackPanel.Children.Add(txtName);
            stackPanel.Children.Add(new TextBlock { Text = "Đơn vị:" });
            stackPanel.Children.Add(txtUnit);
            stackPanel.Children.Add(new TextBlock { Text = "Giá/Đơn vị:" });
            stackPanel.Children.Add(txtCost);
            stackPanel.Children.Add(new TextBlock { Text = "Số lượng:" });
            stackPanel.Children.Add(txtQuantity);
            stackPanel.Children.Add(btnSave);

            btnSave.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtUnit.Text))
                {
                    MessageBox.Show("Tên nguyên liệu và đơn vị không được để trống!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCost.Text, out decimal cost) || !float.TryParse(txtQuantity.Text, out float qty) || cost < 0 || qty < 0)
                {
                    MessageBox.Show("Giá hoặc số lượng không hợp lệ! Vui lòng nhập số không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    material.MaterialName = txtName.Text;
                    material.Unit = txtUnit.Text;
                    material.CostPerUnit = cost;
                    material.Quantity = qty;
                    MilkTeaContext.Ins.SaveChanges();
                    LoadRawMaterials();
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật nguyên liệu: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            dialog.Content = stackPanel;
            dialog.ShowDialog();
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = cbProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = CreateDialog("Thêm Công Thức", 300, 300);
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var cbMaterials = new ComboBox
            {
                ItemsSource = MilkTeaContext.Ins.RawMaterials.ToList(),
                DisplayMemberPath = "MaterialName",
                Margin = new Thickness(0, 0, 0, 10)
            };
            var txtQty = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var btnSave = new Button { Content = "Lưu" };

            stackPanel.Children.Add(new TextBlock { Text = "Nguyên liệu:" });
            stackPanel.Children.Add(cbMaterials);
            stackPanel.Children.Add(new TextBlock { Text = "Số lượng cần thiết:" });
            stackPanel.Children.Add(txtQty);
            stackPanel.Children.Add(btnSave);

            btnSave.Click += (s, ev) =>
            {
                var selectedMaterial = cbMaterials.SelectedItem as RawMaterial;
                if (selectedMaterial == null)
                {
                    MessageBox.Show("Vui lòng chọn một nguyên liệu!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!float.TryParse(txtQty.Text, out float qty) || qty <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số dương.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    var recipe = new Recipe
                    {
                        ProductId = selectedProduct.ProductId,
                        MaterialId = selectedMaterial.MaterialId,
                        QuantityRequired = qty
                    };
                    MilkTeaContext.Ins.Recipes.Add(recipe);
                    MilkTeaContext.Ins.SaveChanges();
                    LoadRecipes();
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu công thức: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            dialog.Content = stackPanel;
            dialog.ShowDialog();
        }

        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.Tag == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định công thức.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int recipeId = (int)button.Tag;
            var recipe = MilkTeaContext.Ins.Recipes
                .Include(r => r.Product)
                .Include(r => r.Material)
                .FirstOrDefault(r => r.RecipeId == recipeId);
            if (recipe == null)
            {
                MessageBox.Show("Không tìm thấy công thức!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = CreateDialog("Chỉnh sửa Công Thức", 300, 200);
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var cbMaterials = new ComboBox
            {
                ItemsSource = MilkTeaContext.Ins.RawMaterials.ToList(),
                DisplayMemberPath = "MaterialName",
                SelectedItem = recipe.Material,
                Margin = new Thickness(0, 0, 0, 10)
            };
            var txtQty = new TextBox { Text = recipe.QuantityRequired.ToString(), Margin = new Thickness(0, 0, 0, 10) };
            var btnSave = new Button { Content = "Lưu" };

            stackPanel.Children.Add(new TextBlock { Text = "Nguyên liệu:" });
            stackPanel.Children.Add(cbMaterials);
            stackPanel.Children.Add(new TextBlock { Text = "Số lượng cần thiết:" });
            stackPanel.Children.Add(txtQty);
            stackPanel.Children.Add(btnSave);

            btnSave.Click += (s, ev) =>
            {
                var selectedMaterial = cbMaterials.SelectedItem as RawMaterial;
                if (selectedMaterial == null)
                {
                    MessageBox.Show("Vui lòng chọn một nguyên liệu!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!float.TryParse(txtQty.Text, out float qty) || qty <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số dương.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    recipe.MaterialId = selectedMaterial.MaterialId;
                    recipe.QuantityRequired = qty;
                    MilkTeaContext.Ins.SaveChanges();
                    LoadRecipes();
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật công thức: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            dialog.Content = stackPanel;
            dialog.ShowDialog();
        }

        private void DisposeMaterial_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.Tag == null)
            {
                MessageBox.Show("Lỗi: Không thể xác định nguyên liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int materialId = (int)button.Tag;
            var material = MilkTeaContext.Ins.RawMaterials.FirstOrDefault(m => m.MaterialId == materialId);

            if (material == null)
            {
                MessageBox.Show("Không tìm thấy nguyên liệu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = CreateDialog("Xử lý Nguyên Liệu", 300, 150);
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            var txtQty = new TextBox { Margin = new Thickness(0, 0, 0, 10) };
            var btnDispose = new Button { Content = "Xử lý" };

            stackPanel.Children.Add(new TextBlock { Text = $"Số lượng xử lý (Tối đa: {material.Quantity}):" });
            stackPanel.Children.Add(txtQty);
            stackPanel.Children.Add(btnDispose);

            btnDispose.Click += (s, ev) =>
            {
                if (!float.TryParse(txtQty.Text, out float qty) || qty <= 0 || qty > material.Quantity)
                {
                    MessageBox.Show($"Số lượng không hợp lệ! Nhập số từ 0 đến {material.Quantity}.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    material.Quantity -= qty;
                    var disposed = new DisposedMaterial
                    {
                        MaterialId = materialId,
                        Quantity = qty,
                        DateDisposed = DateTime.Now
                    };
                    MilkTeaContext.Ins.DisposedMaterials.Add(disposed);
                    MilkTeaContext.Ins.SaveChanges();
                    LoadRawMaterials();
                    LoadDisposedMaterials();
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xử lý nguyên liệu: {ex.Message}", "Lỗi cơ sở dữ liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            dialog.Content = stackPanel;
            dialog.ShowDialog();
        }

        private Window CreateDialog(string title, double width, double height)
        {
            return new Window
            {
                Title = title,
                Width = width,
                Height = height,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this),
                ResizeMode = ResizeMode.NoResize
            };
        }
    }
}