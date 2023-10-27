using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        public MyShop.BUS.orderBUS orderBUS = new MyShop.BUS.orderBUS();
        public List<int> customerIdList = new List<int>();
        public List<MyShop.Classes.OrderProduct> sortOrderList = new List<MyShop.Classes.OrderProduct>();
        public List<MyShop.Classes.OrderProduct> orderProductList = new List<Classes.OrderProduct>();
        public List<MyShop.Classes.Discount> discountList = new List<Classes.Discount>();

        private void handleLoadAddOrder(object sender, RoutedEventArgs e)
        {
            bool checkOrderBUS = orderBUS.checkOrder();

            if (checkOrderBUS == true)
            {
                orderProductList = MyShop.DAO.orderDAO.getOrderProductList();

                var lastOrder = orderProductList[orderProductList.Count - 1];
                var newOrderId = lastOrder.order_id + 1;
                ProductIDTextBox.Text = newOrderId.ToString();

                customerIdList.Clear();
                for (var i = 0; i < orderProductList.Count(); i++)
                {
                    if (!customerIdList.Contains(orderProductList[i].customer_id))
                    {
                        customerIdList.Add(orderProductList[i].customer_id);
                        sortOrderList.Add(orderProductList[i]);
                    }
                }

                customerIdCombobox.ItemsSource = sortOrderList;

                // Get discount list
                discountList = MyShop.DAO.discountDAO.GetDiscountList();
                discountCombobox.ItemsSource = discountList;
            }
        }

        private void SubmitAddOrder(object sender, RoutedEventArgs e)
        {
            bool checkOrderBUS = orderBUS.checkOrder();

            if (checkOrderBUS == true)
            {
                var order_id_new = ProductIDTextBox.Text;

                int customerIdChoose = customerIdCombobox.SelectedIndex;
                var customer_id = sortOrderList[customerIdChoose].customer_id;

                var deliver_address = nameDeliverAddressTextBox.Text;

                int discountChoose = discountCombobox.SelectedIndex;
                var discount_id = discountList[discountChoose].discount_id;
                var product_id = discountList[discountChoose].product_id;

                var quantity = nameQuanityTextBox.Text;

                try
                {
                    MyShop.DAO.orderDAO.addOrderProduct(order_id_new, customer_id.ToString(),
                        deliver_address, discount_id.ToString(), product_id.ToString(), quantity);

                    MessageBox.Show("Thêm sản phẩm thành công");

                    // Reset
                    ProductIDTextBox.Text = "";
                    nameDeliverAddressTextBox.Text = "";
                    nameQuanityTextBox.Text = "";
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                    Debug.WriteLine(er);
                }
            }
        }
    }
}
