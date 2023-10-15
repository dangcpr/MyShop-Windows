using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using static MyShop.Classes.Accounts;
using static MyShop.DAO.accountsDAO;
using static MyShop.DAO.productDAO;
using static MyShop.BUS.productBUS;
using static MyShop.BUS.orderBUS;
using static MyShop.Classes.MyModel;
using static MyShop.UserControls.DashboardUC;
using System.Runtime.CompilerServices;
using LiveCharts.Wpf;
using LiveCharts;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using MyShop.Classes;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();      
        }
        
        // BUS Implement
        MyShop.Classes.Accounts _user;
        MyShop.BUS.accountsBUS _accountsBUS;

        List<MyShop.Classes.Product> _productList;
        List<MyShop.Classes.ProductSpeedStats> _speedStatsTable;
        List<MyShop.Classes.ProductTopLimit> _productTopLimitList;
        MyShop.BUS.productBUS _productBUS;

        // Model Implement
        MyShop.Classes.MyModel _myModel;
        List<MyShop.Classes.Order> _monthOrderList;
        List<MyShop.Classes.Order> _weekOrderList;
        List<MyShop.Classes.Order> _preMonthOrderList;
        List<MyShop.Classes.Order> _preWeekOrderList;
        MyShop.BUS.orderBUS _orderBUS;

        private void handleDashboardLoaded(object sender, RoutedEventArgs e)
        {
            //var _myModel = new MyShop.Classes.MyModel();
            _myModel = new MyShop.Classes.MyModel();

            _accountsBUS = new MyShop.BUS.accountsBUS();
            bool checkAccountBUS = _accountsBUS.checkAccountsBUS();

            _productBUS = new MyShop.BUS.productBUS();
            bool checkProductBus = _productBUS.checkProductInSale();

            _myModel.speedStats = new ChartValues<double>();

            _orderBUS = new MyShop.BUS.orderBUS();
            bool checkOrderBUS = _orderBUS.checkOrderMonth();

            if (checkAccountBUS == true)
            {
                var username = accountsDAO.userAccount.username;
                _myModel.username = username;
            }

            if (checkProductBus == true)
            {
                _productList = MyShop.DAO.productDAO.getProductList();
                _myModel.count_product = _productList.Count();              

                _speedStatsTable = MyShop.DAO.productDAO.getSpeedStats();
                _myModel.productInventorySum = MyShop.DAO.productDAO.getProductInventorySum();

                foreach (var speed in _speedStatsTable)
                {
                    _myModel.speedStats.Add(Convert.ToDouble(speed.in_num_cat));
                }

                _myModel.productTopLimit = _productTopLimitList;
            }

            if (checkOrderBUS == true)
            {
                _monthOrderList = MyShop.DAO.orderDAO.getOrderList("month");
                _myModel.count_order_month = _monthOrderList.Count();

                _weekOrderList = MyShop.DAO.orderDAO.getOrderList("week");
                _myModel.count_order_week = _weekOrderList.Count();

                _preMonthOrderList = MyShop.DAO.orderDAO.getOrderList("preMonth");
                _myModel.count_change_order_month = _monthOrderList.Count() - _preMonthOrderList.Count();

                if(_myModel.count_change_order_month < 0) 
                {
                    _myModel.count_change_order_month = _myModel.count_change_order_month * (-1);
                    _myModel.image_change_order_month = "../assets/icons/decrease.png";
                }
                else
                {
                    _myModel.image_change_order_month = "../assets/icons/increase.png";
                }

                _preWeekOrderList = MyShop.DAO.orderDAO.getOrderList("preWeek");
                _myModel.count_change_order_week = _weekOrderList.Count() - _preWeekOrderList.Count();

                if (_myModel.count_change_order_week < 0)
                {
                    _myModel.count_change_order_week = _myModel.count_change_order_week * (-1);
                    _myModel.image_change_order_week = "../assets/icons/decrease.png";
                }
                else
                {
                    _myModel.image_change_order_week = "../assets/icons/increase.png";
                }
            }
           
            // DataContext implement
            this.DataContext = _myModel;
        }

        private void handleDashboardSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Mobile view
            if (e.PreviousSize != new Size() && e.PreviousSize.Width < 864)
            {
                dashboardNavTop.Visibility = Visibility.Collapsed;
                dashboardNavTopMobile.Visibility = Visibility.Visible;

                tab1Name.Visibility = Visibility.Collapsed;
                tab2Name.Visibility = Visibility.Collapsed;
                tab3Name.Visibility = Visibility.Collapsed;
                tab4Name.Visibility = Visibility.Collapsed;

                tab1.Width = 100;
                tab2.Width = 100;
                tab3.Width = 100;
                tab4.Width = 100;
            }
            else
            {
                dashboardNavTop.Visibility = Visibility.Visible;
                dashboardNavTopMobile.Visibility = Visibility.Collapsed;

                tab1Name.Visibility = Visibility.Visible;
                tab2Name.Visibility = Visibility.Visible;
                tab3Name.Visibility = Visibility.Visible;
                tab4Name.Visibility = Visibility.Visible;

                tab1.Width = 250;
                tab2.Width = 250;
                tab3.Width = 250;
                tab4.Width = 250;
            }
        }

        private void handleChange(object sender, RoutedEventArgs e)
        {
            _user.username = "Nguyễn Văn A";
        }

        private void handleLogout(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Do you can to log out?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (confirm == MessageBoxResult.Yes)
            {
                _user = null;
                var loginScreen = new Login();
                loginScreen.Show();
                this.Close();
            }
        }     
        
        private void DashboardUC_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("===> DashboardUC_Loaded Check");
        }
    }
}
