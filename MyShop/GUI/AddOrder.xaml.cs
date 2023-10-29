using MyShop.Classes;
using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<int> orderProductList = new List<int>();
        public List<int> ProductList = new List<int>();
        int selected = -1;
        public List<MyShop.Classes.Discount> discountList = new List<Classes.Discount>();

        private void handleLoadAddOrder(object sender, RoutedEventArgs e)
        {
            //productOrderDataGrid.AddingNewItem()
            bool checkOrderBUS = orderBUS.checkOrder();

            if (checkOrderBUS == true)
            {
                //customerIdList.Clear();
                //customerIdCombobox.Items.Clear();

                customerIdList = customerDAO.getListCustomerID();

                ProductList = productDAO.getListProductID();

                OrderIDTextBox.Text = (orderDAO.getMaxOrderID() + 1).ToString();

                foreach (var customer in customerIdList)
                {
                    customerIdCombobox.Items.Add(customer.ToString());
                }

                foreach (var product in ProductList)
                {
                    productIDTextBox.Items.Add(product.ToString());
                }

                // Get discount list
                //discountList = MyShop.DAO.discountDAO.GetDiscountList();
                //discountCombobox.ItemsSource = discountList;
            }
        }

        private void SubmitAddOrder(object sender, RoutedEventArgs e)
        {
            bool checkOrderBUS = orderBUS.checkOrder();

            if(customerIdCombobox.Text.Length == 0 || nameDeliverAddressTextBox.Text.Length == 0)
            {
                MessageBox.Show("Không được bỏ trống Customer ID và Address");
                return;
            }

            if (checkOrderBUS == true)
            {
                try
                {
                    MyShop.DAO.orderDAO.addOrderProduct(Int32.Parse(OrderIDTextBox.Text), Int32.Parse(customerIdCombobox.Text), nameDeliverAddressTextBox.Text, addOrders);

                    MessageBox.Show("Thêm đơn hàng thành công");

                    // Reset
                    this.Close();
                    
                    //nameQuanityTextBox.Text = "";
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.ToString());
                    Debug.WriteLine(er);
                }
            }
        }

        private void productOrderDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            IList<DataGridCellInfo> selectedcells = e.AddedCells;

            productIDTextBox.Text = String.Empty;
            quantityIDTextBox.Text = String.Empty;
            discountCombobox.Text = String.Empty;

            selected = productOrderDataGrid.SelectedIndex;
            Debug.WriteLine(selected);

            foreach (DataGridCellInfo di in selectedcells)
            {
                //Cast the DataGridCellInfo.Item to the source object type
                //In this case the ItemsSource is a DataTable and individual items are DataRows
                MyShop.Classes.addOrder dvr = (MyShop.Classes.addOrder)di.Item;

                productIDTextBox.Text = dvr.productID.ToString();
                quantityIDTextBox.Text = dvr.quantity.ToString();
                discountIDTextBox.Text = dvr.discountID.ToString();
            }
        }

        List<addOrder> addOrders = new List<addOrder>();
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(productIDTextBox.Text.Length == 0 || quantityIDTextBox.Text.Length == 0)
            {
                MessageBox.Show("Không được bỏ trống Product ID và Quantity");
                return;
            }
            addOrder addOrderC = new addOrder();
            addOrderC.productID = Int32.Parse(productIDTextBox.Text);
            addOrderC.quantity = Int32.Parse(quantityIDTextBox.Text);

            int x = 0;

            bool isDiscountID = Int32.TryParse(discountIDTextBox.Text, out x);

            addOrderC.discountID = isDiscountID ? Int32.Parse(discountIDTextBox.Text) : null;

            addOrders.Add(addOrderC);

            productOrderDataGrid.ItemsSource = addOrders;
            productOrderDataGrid.Items.Refresh();

            productIDTextBox.Text = String.Empty;
            quantityIDTextBox.Text = String.Empty;
            discountIDTextBox.Text = String.Empty;
        }

        private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(selected == -1)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa");
                return;
            } else
            {
                addOrders.RemoveAt(selected);
                productOrderDataGrid.ItemsSource = addOrders;
                productOrderDataGrid.Items.Refresh();
            }
        }

        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            discountIDTextBox.Text = String.Empty;
            productIDTextBox.Text = String.Empty;
            quantityIDTextBox.Text = String.Empty;
        }

        private void productIDTextBox_DropDownClosed(object sender, EventArgs e)
        {
            List<int> listDiscountID = new List<int>();

            int x = 0;

            Int32.TryParse(productIDTextBox.Text, out x);

            listDiscountID = discountDAO.getListDiscountID(x);

            discountIDTextBox.ItemsSource = listDiscountID;
        }
    }
}
