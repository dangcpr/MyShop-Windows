using MyShop.BUS;
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

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using BCrypt.Net;

using static MyShop.BUS.connectDatabaseBUS;
using static MyShop.BUS.accountsBUS;

using static MyShop.DAO.connectDatabaseDAO;
using static MyShop.DAO.accountsDAO;
using static MyShop.Classes.Product;
using static MyShop.Classes.MyModel;
using MyShop.DAO;
using MyShop.Classes;
using System.Security.Policy;

using static MyShop.API.MyShopApi;
using MyShop.API;
using System.Collections;
using System.Text.Json;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MyShop.DAO.connectDatabaseDAO dbDAO;
        private MyShop.DAO.accountsDAO accountsDAO;
        MyShop.API.MyShopApi api;


        public Login()
        {
            InitializeComponent();

            MyShop.BUS.connectDatabaseBUS dbBUS = new MyShop.BUS.connectDatabaseBUS();
            accountsDAO = new MyShop.DAO.accountsDAO();

            var checkCntDb = dbBUS.checkConnectDatabase();

            if (checkCntDb == true)
            {
                dbDAO = new MyShop.DAO.connectDatabaseDAO();
                connectDB();
            }           
        }

        private void handleLoginLoaded(object sender, RoutedEventArgs e)
        {
            MyShop.BUS.accountsBUS accBUS = new MyShop.BUS.accountsBUS();

            var checkAccountsBUS = accBUS.checkAccountsBUS();

            if (checkAccountsBUS == true)
            {
                bool isExist = checkExistAccount("minhtrifit");

                Debug.WriteLine(isExist);

                // Auto create account with hashed password
                if (isExist == false) createAccount("minhtrifit", "123", "Lê Minh Trí");
            }           
        }

        private async void handleLoginAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                MyShop.BUS.accountsBUS accBUS = new MyShop.BUS.accountsBUS();

                var checkAccountsBUS = accBUS.checkAccountsBUS();

                if (checkAccountsBUS == true)
                {
                    var username = usernameInput.Text.ToString();
                    var password = passwordInput.Password.ToString();
                    bool checkLogin = false;

                    // API Calling
                    string jsonStrRes = await GetAccountData();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

                    // Password check
                    foreach (var acccount in res.accountList)
                    {
                        if (acccount.username == username && BCrypt.Net.BCrypt.Verify(password, acccount.password))
                        {
                            checkLogin = true;
                        }
                    }

                    if (checkLogin == true)
                    {
                        //MessageBox.Show("Login Successfully");
                        userAccount = new Accounts();
                        userAccount.username = username;
                        var dashboardScreen = new Dashboard();
                        dashboardScreen.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.ToString());
                MessageBox.Show("Something wrong with API :(", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private async void handleLoginAccount(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MyShop.BUS.accountsBUS accBUS = new MyShop.BUS.accountsBUS();

        //        var checkAccountsBUS = accBUS.checkAccountsBUS();

        //        if (checkAccountsBUS == true)
        //        {
        //            var username = usernameInput.Text.ToString();
        //            var password = passwordInput.Password.ToString();

        //            bool checkLogin = await accountsDAO.checkLoginAccount(username, password);

        //            if (checkLogin == true)
        //            {
        //                //MessageBox.Show("Login Successfully");
        //                var dashboardScreen = new Dashboard();
        //                dashboardScreen.Show();
        //                this.Close();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Login failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Something wrong :(", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}      
    }
}
