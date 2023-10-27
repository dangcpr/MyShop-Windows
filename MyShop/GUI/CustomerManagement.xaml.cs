using MyShop.API;
using MyShop.Classes;
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

        List<MyShop.Classes.Customer> customers = new List<Classes.Customer>();
        List<MyShop.Classes.CustomerQuery> customersQuery = new List<Classes.CustomerQuery>();

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

                customersQuery.Add(newCustomer);
            }

            CustomerDataGrid.ItemsSource = customersQuery;
        }

        private void CustomerDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void CustomerDataGrid_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
