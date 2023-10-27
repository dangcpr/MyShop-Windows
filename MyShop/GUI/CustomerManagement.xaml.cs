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

        private async void CustomerMangementLoaded(object sender, RoutedEventArgs e)
        {
            // API Calling
            string jsonStrRes = await GetCustomerData();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

            customers.Clear();
            foreach (var cusomter in res.customerList)
            {
                MyShop.Classes.Customer newCustomer = new MyShop.Classes.Customer();

                newCustomer.customer_id = cusomter.customer_id;
                newCustomer.name = cusomter.name;
                newCustomer.address = cusomter.address;
                newCustomer.phone = cusomter.phone;
                newCustomer.create_at = cusomter.create_at;
                newCustomer.modify_at = cusomter.modify_at;

                customers.Add(newCustomer);
            }

            CustomerDataGrid.ItemsSource = customers;
        }

        private void CustomerDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void CustomerDataGrid_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        {

        }
    }
}
