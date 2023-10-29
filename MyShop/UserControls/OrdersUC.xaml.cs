using Microsoft.Win32;
using MyShop.BUS;
using MyShop.Classes;
using MyShop.DAO;
using MyShop.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using static MyShop.Classes.MyModel;
using static MyShop.helpers.MyShopHelpers;
using static MyShop.Classes.DetailOrderProduct;
using System.Data;
using System.Configuration;

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for OrdersUC.xaml
    /// </summary>
    public partial class OrdersUC : UserControl
    {
        public OrdersUC()
        {
            InitializeComponent();
        }

        List<MyShop.Classes.OrderProduct> orderProductList;
        MyShop.Classes.MyModel _myModel;

        public static DataGrid dtOrder = new DataGrid();
        public static int recentPage = 1;
        public static int orderIdSelected = -1;
        public static int _orderIdSelected = -1;
        public static Order orderChoose = new Order();

        private void saveScreen()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["LastScreen"].Value = "3";
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void handleOrdersUCLoaded(object sender, RoutedEventArgs e)
        {
            /*
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["LastScreen"].Value = "3";
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
            */
            _myModel = new MyShop.Classes.MyModel();

            orderProductList = new List<MyShop.Classes.OrderProduct>();
            orderProductList = orderDAO.getOrderProductList();

            _myModel.recentOrderProductPage = 1;

            // Calulate total page
            orderProductPageCount = (orderProductList.Count() + 4 - 1) / 4;          

            // Get product list per page
            var listPerPage = getOrderProductListPerPage(orderProductList, _myModel.recentOrderProductPage, 4);

            orderManageDataGrid.ItemsSource = listPerPage;
            dtOrder = orderManageDataGrid;

            this.DataContext = _myModel;
        }

        private void handlePrevDataGrid(object sender, RoutedEventArgs e)
        {
            if (_myModel.recentOrderProductPage > 1) _myModel.recentOrderProductPage--;

            // Get product list per page
            var listPerPage = getOrderProductListPerPage(orderProductList, _myModel.recentOrderProductPage, 4);

            orderManageDataGrid.ItemsSource = listPerPage;
            dtOrder = orderManageDataGrid;
        }

        private void handleNextDataGrid(object sender, RoutedEventArgs e)
        {
            if (_myModel.recentOrderProductPage < orderProductPageCount) _myModel.recentOrderProductPage++;

            // Get product list per page
            var listPerPage = getOrderProductListPerPage(orderProductList, _myModel.recentOrderProductPage, 4);

            orderManageDataGrid.ItemsSource = listPerPage;
            dtOrder = orderManageDataGrid;
        }

        public class Compare
        {
            public DateTime order_date { get; set; }
        }

        private void handleSortDate(object sender, RoutedEventArgs e)
        {
            DateTime? fromDate = orderFromDatePicker.SelectedDate;
            DateTime? toDate = orderToDatePicker.SelectedDate;

            if (fromDate.HasValue && toDate.HasValue)
            {
                string fromFormatted = fromDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string toFormatted = toDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var sortList = orderProductList;

                var compare = new Compare();
                compare.order_date = (DateTime)fromDate;

                sortList.Sort((x, compare) => DateTime.Compare(x.order_date, compare.order_date));

                orderManageDataGrid.ItemsSource = sortList;
                dtOrder = orderManageDataGrid;
            }
            else
            {
                _myModel.recentOrderProductPage = 1;

                // Get product list per page
                var listPerPage = getOrderProductListPerPage(orderProductList, _myModel.recentOrderProductPage, 4);

                orderManageDataGrid.ItemsSource = listPerPage;
                dtOrder = orderManageDataGrid;
            }
        }

        private void orderManageDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            IList<DataGridCellInfo> selectedcells = e.AddedCells;

            foreach (DataGridCellInfo di in selectedcells)
            {
                //Cast the DataGridCellInfo.Item to the source object type
                //In this case the ItemsSource is a DataTable and individual items are DataRows
                MyShop.Classes.OrderProduct dvr = (MyShop.Classes.OrderProduct)di.Item;
                orderIdSelected = (int)dvr.order_id;
                orderChoose.order_id = (int)dvr.order_id;
                orderChoose.customer_id = (int)dvr.customer_id;
                orderChoose.deliver_address = (string)dvr.deliver_address;
                orderChoose.status = (string)dvr.status;
            }
        }

        private void handleAddOrder(object sender, RoutedEventArgs e)
        {
            var scren = new AddOrder();
            scren.Show();
        }

        private void handleEditOrder(object sender, RoutedEventArgs e)
        {
            if (orderIdSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để xem");
                return;
            }
            UpdateOrder updateOrder = new UpdateOrder();
            updateOrder.Show();
        }

        private void handleDeleteOrder(object sender, RoutedEventArgs e)
        {
            if (orderIdSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            Debug.WriteLine("OrderID" + orderIdSelected.ToString());

            MessageBoxResult result = MessageBox.Show("Do you want to remove " + orderIdSelected, "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (categoryBUS.checkCategoryBUS() == true)
                    {
                        orderDAO.deleteOrderProduct(orderIdSelected);

                        MessageBox.Show("Xóa thành công");

                        orderProductList = orderDAO.getOrderProductList();

                        _myModel.recentOrderProductPage = 1;

                        // Calulate total page
                        orderProductPageCount = (orderProductList.Count() + 4 - 1) / 4;

                        // Get product list per page
                        var listPerPage = getOrderProductListPerPage(orderProductList, _myModel.recentOrderProductPage, 4);

                        orderManageDataGrid.ItemsSource = listPerPage;
                        dtOrder = orderManageDataGrid;

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                return;
            }
        }

        private void handleViewDetailOrder(object sender, RoutedEventArgs e)
        {
            if (orderIdSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            var screen = new ViewDetailOrder();
            screen.Show();
        }

        private void orderManageDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
