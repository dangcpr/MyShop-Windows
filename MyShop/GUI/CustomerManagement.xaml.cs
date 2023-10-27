using MyShop.API;
using MyShop.BUS;
using MyShop.Classes;
using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using static MyShop.API.MyShopApi;
using static MyShop.Classes.Customer;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Window
    {
        public CustomerManagement()
        {
            InitializeComponent();
        }

        //List<MyShop.Classes.Customer> customers = new List<Classes.Customer>();
        List<MyShop.Classes.CustomerQuery> customersQuery = new List<Classes.CustomerQuery>();
        public static MyShop.Classes.CustomerQuery customer = new MyShop.Classes.CustomerQuery();
        public static int customerSelected = -1;

        public MyShop.BUS.customerBUS customerBUS = new BUS.customerBUS();

        private async void CustomerMangementLoaded(object sender, RoutedEventArgs e)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // API Calling
            //string jsonStrRes = await GetCustomerData();

            //var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

            //customers.Clear();
            //foreach (var customer in res.customerList)
            //{
            //    MyShop.Classes.Customer newCustomer = new MyShop.Classes.Customer();

            //    newCustomer.customer_id = customer.customer_id;
            //    newCustomer.name = customer.name;
            //    newCustomer.address = customer.address;
            //    newCustomer.phone = customer.phone;
            //    newCustomer.create_at = customer.create_at;
            //    newCustomer.modify_at = customer.modify_at;

            //    customers.Add(newCustomer);
            //}

            // GraphQl query
            string responseString = await GetCustomerQueryData();

            var res2 = System.Text.Json.JsonSerializer.Deserialize<RootObject>(responseString, options);

            customersQuery.Clear();
            foreach (var customer in res2.customers)
            {
                MyShop.Classes.CustomerQuery newCustomer = new MyShop.Classes.CustomerQuery();

                newCustomer.customer_id = customer.customer_id;
                newCustomer.name = customer.name;
                newCustomer.address = customer.address;
                newCustomer.phone = customer.phone;
                newCustomer.create_at = customer.create_at;
                newCustomer.modify_at = customer.modify_at;

                customersQuery.Add(newCustomer);
            }

            CustomerDataGrid.ItemsSource = customersQuery;
        }

        private void CustomerDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            IList<DataGridCellInfo> selectedcells = e.AddedCells;

            foreach (DataGridCellInfo di in selectedcells)
            {
                MyShop.Classes.CustomerQuery dvr = (MyShop.Classes.CustomerQuery)di.Item;

                customer.customer_id = dvr.customer_id;
                customerSelected = dvr.customer_id;
            }

            Debug.WriteLine(customerSelected.ToString() + ' ' + customer.customer_id);
        }

        private async void handleAddCustomer(object sender, RoutedEventArgs e)
        {
            if(CustomerNameTextBox.Text == "" || CustomerAddressTextBox.Text == "" || CustomerPhoneTextBox.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ dữ liệu", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // API Calling
            string responseString = await GetCustomerQueryData();

            var res2 = System.Text.Json.JsonSerializer.Deserialize<RootObject>(responseString, options);

            customersQuery.Clear();
            foreach (var customer in res2.customers)
            {
                MyShop.Classes.CustomerQuery newCustomer = new MyShop.Classes.CustomerQuery();

                newCustomer.customer_id = customer.customer_id;
                newCustomer.name = customer.name;
                newCustomer.address = customer.address;
                newCustomer.phone = customer.phone;
                newCustomer.create_at = customer.create_at;
                newCustomer.modify_at = customer.modify_at;

                customersQuery.Add(newCustomer);
            }

            CustomerDataGrid.ItemsSource = customersQuery;

            var lastCustomer = customersQuery[customersQuery.Count - 1];
            int newCustomerId = lastCustomer.customer_id + 1;

            var newAddCustomer = new MyShop.Classes.Customer();
            newAddCustomer.customer_id = newCustomerId;
            newAddCustomer.name = CustomerNameTextBox.Text;
            newAddCustomer.address = CustomerAddressTextBox.Text;
            newAddCustomer.phone = CustomerPhoneTextBox.Text;

            bool checkCustomerBUS = customerBUS.checkCustomerBUS();

            if(checkCustomerBUS == true)
            {
                bool checkCreateNewCustomer = MyShop.DAO.customerDAO.createNewCustomer(
                    newAddCustomer.name, newAddCustomer.address, newAddCustomer.phone);

                if (checkCreateNewCustomer == true)
                {
                    MessageBox.Show("Create Customer Successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh Datagrid
                    string strCustomerRefresh = await GetCustomerQueryData();

                    var resRefresh = System.Text.Json.JsonSerializer.Deserialize<RootObject>(strCustomerRefresh, options);

                    customersQuery.Clear();
                    foreach (var customer in resRefresh.customers)
                    {
                        MyShop.Classes.CustomerQuery newCustomer = new MyShop.Classes.CustomerQuery();

                        newCustomer.customer_id = customer.customer_id;
                        newCustomer.name = customer.name;
                        newCustomer.address = customer.address;
                        newCustomer.phone = customer.phone;
                        newCustomer.create_at = customer.create_at;
                        newCustomer.modify_at = customer.modify_at;

                        customersQuery.Add(newCustomer);
                    }

                    CustomerNameTextBox.Text = "";
                    CustomerAddressTextBox.Text = "";
                    CustomerPhoneTextBox.Text = "";

                    CustomerDataGrid.ItemsSource = customersQuery;
                    CustomerDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Create Customer Failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void handleEditCustomer(object sender, RoutedEventArgs e)
        {
            if (customerSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }
        }

        private async void handleRemoveCustomer(object sender, RoutedEventArgs e)
        {
            if (customerSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            var targetCustomerId = customer.customer_id;

            if (targetCustomerId != 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to remove customer " + targetCustomerId, "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool checkCustomerBUS = customerBUS.checkCustomerBUS();

                        if (checkCustomerBUS == true)
                        {
                            customerDAO.deleteCustomer(targetCustomerId);

                            MessageBox.Show("Remove Customer Successfully", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                            };

                            var strCustomerRefresh = await GetCustomerData();

                            // Refresh Datagrid
                            string responseString = await GetCustomerQueryData();

                            var res2 = System.Text.Json.JsonSerializer.Deserialize<RootObject>(responseString, options);

                            customersQuery.Clear();
                            foreach (var customer in res2.customers)
                            {
                                MyShop.Classes.CustomerQuery newCustomer = new MyShop.Classes.CustomerQuery();

                                newCustomer.customer_id = customer.customer_id;
                                newCustomer.name = customer.name;
                                newCustomer.address = customer.address;
                                newCustomer.phone = customer.phone;
                                newCustomer.create_at = customer.create_at;
                                newCustomer.modify_at = customer.modify_at;

                                customersQuery.Add(newCustomer);
                            }

                            CustomerDataGrid.ItemsSource = customersQuery;
                            CustomerDataGrid.Items.Refresh();

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message); return;
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
        }
    }
}
