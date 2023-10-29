using MyShop.BUS;
using MyShop.Classes;
using MyShop.DAO;
using MyShop.UserControls;
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

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for ViewDetailOrder.xaml
    /// </summary>
    public partial class ViewDetailOrder : Window
    {
        public ViewDetailOrder()
        {
            InitializeComponent();
        }


        public MyShop.BUS.orderBUS orderBUS = new MyShop.BUS.orderBUS();
        public orderDAO orderDAO = new orderDAO();
        public List<int> customerIdList = new List<int>();
        public List<int> orderProductList = new List<int>();
        public List<int> ProductList = new List<int>();
        List<addOrder> addOrders = new List<addOrder>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool checkOrderBUS = orderBUS.checkOrder();

            if (checkOrderBUS == true)
            {
                //customerIdList.Clear();
                //customerIdCombobox.Items.Clear();

                customerIdList = customerDAO.getListCustomerID();

                ProductList = productDAO.getListProductID();

                OrderIDTextBox.Text = orderDAO.getMaxOrderID().ToString();

                foreach (var customer in customerIdList)
                {
                    customerIdCombobox.Items.Add(customer.ToString());
                }

                // Get discount list
                //discountList = MyShop.DAO.discountDAO.GetDiscountList();
                //discountCombobox.ItemsSource = discountList;
            }

            OrderIDTextBox.Text = OrdersUC.orderChoose.order_id.ToString();
            customerIdCombobox.Text = OrdersUC.orderChoose.customer_id.ToString();
            nameDeliverAddressTextBox.Text = OrdersUC.orderChoose.deliver_address.ToString();
            statusComboBox.Text = OrdersUC.orderChoose.status.ToString();
            addOrders = orderDAO.getDetailOrderShort(OrdersUC.orderChoose.order_id);
            productOrderDataGrid.ItemsSource = addOrders;
        }
    }
}
