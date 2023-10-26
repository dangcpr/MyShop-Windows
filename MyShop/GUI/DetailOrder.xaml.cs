using MyShop.Classes;
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

using static MyShop.Classes.MyModel;
using static MyShop.UserControls.OrdersUC;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for DetailOrder.xaml
    /// </summary>
    public partial class DetailOrder : Window
    {
        public DetailOrder()
        {
            InitializeComponent();
        }

        public MyShop.Classes.MyModel _myModel;

        private void loadedDetailOrder(object sender, RoutedEventArgs e)
        {
            _myModel = new MyShop.Classes.MyModel();

            int order_id = MyShop.Classes.MyModel._orderIdSelected;

            Debug.WriteLine(order_id);

            _myModel.detailOrderProduct = new List<DetailOrderProduct>();

            _myModel.detailOrderProduct = MyShop.DAO.orderDAO.getDetailOrder(order_id);

        }
    }
}
