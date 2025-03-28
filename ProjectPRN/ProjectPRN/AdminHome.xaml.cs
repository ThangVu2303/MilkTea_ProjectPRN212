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
using ProjectPRN.Business;

namespace ProjectPRN
{
    /// <summary>
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Page
    {
        private readonly OrdersBusiness _ordersBusiness = new OrdersBusiness();
        private readonly ProductBusiness _productBusiness = new ProductBusiness();
        private readonly StaffBusiness _staffBusiness = new StaffBusiness(); // Thay cho EmployeeBusiness

        public AdminHome()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Total Orders
                int totalOrders = _ordersBusiness.GetOrders().Count;
                labelTotalOrders.Content = totalOrders.ToString();

                // Total Products
                int totalProducts = _productBusiness.GetProducts().Count;
                labelTotalProducts.Content = totalProducts.ToString();

                // Total Employees (Staff)
                int totalEmployees = _staffBusiness.GetStaffIds().Count - 1;
                labelTotalEmployees.Content = totalEmployees.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}