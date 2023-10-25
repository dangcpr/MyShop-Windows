using Microsoft.Win32;
using MyShop.Classes;
using MyShop.DAO;
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

        private void handleOrdersUCLoaded(object sender, RoutedEventArgs e)
        {
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

        }

        private void handleAddOrder(object sender, RoutedEventArgs e)
        {

        }

        private void handleEditOrder(object sender, RoutedEventArgs e)
        {

        }

        private void handleDeleteOrder(object sender, RoutedEventArgs e)
        {

        }

        private void handleViewDetailOrder(object sender, RoutedEventArgs e)
        {

        }      
    }
}
