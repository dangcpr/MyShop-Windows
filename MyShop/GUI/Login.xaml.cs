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
using System.Configuration;

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

        private async void handleLoginLoaded(object sender, RoutedEventArgs e)
        {
            MyShop.BUS.accountsBUS accBUS = new MyShop.BUS.accountsBUS();

            var checkAccountsBUS = accBUS.checkAccountsBUS();

            if (checkAccountsBUS == true)
            {
                bool isExist = checkExistAccount("minhtrifit");
                bool isExist2 = checkExistAccount("dangcpr");


                // Auto create account with hashed password
                if (isExist == false && isExist2 == false)
                {
                    createAccount("minhtrifit", "123", "Lê Minh Trí");
                    createAccount("dangcpr", "456", "Nguyễn Hải Đăng");
                }

                // Check user is login already
                var usernameSave = ConfigurationManager.AppSettings["Username"];
                var passwordSave = ConfigurationManager.AppSettings["Password"];


                // API Calling
                string jsonStrRes = await GetAccountData();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

                // Password check
                foreach (var account in res.accountList)
                {
                    if (account.username == usernameSave)
                    {
                        userAccount = new Accounts();
                        userAccount.username = usernameSave;
                        var dashboardScreen = new Dashboard();
                        dashboardScreen.Show();
                        this.Close();
                    }
                }
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
                    MyShop.Classes.Accounts targetAccount = new MyShop.Classes.Accounts();
                    bool checkLogin = false;

                    // API Calling
                    string jsonStrRes = await GetAccountData();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var res = System.Text.Json.JsonSerializer.Deserialize<RootObject>(jsonStrRes, options);

                    // Password check
                    foreach (var account in res.accountList)
                    {
                        if (account.username == username && BCrypt.Net.BCrypt.Verify(password, account.password))
                        {
                            checkLogin = true;
                            targetAccount.username = account.username;
                            targetAccount.password = account.password;
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

                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        config.AppSettings.Settings["Username"].Value = targetAccount.username;
                        config.AppSettings.Settings["Password"].Value = targetAccount.password;

                        config.Save(ConfigurationSaveMode.Minimal);

                        ConfigurationManager.RefreshSection("appSettings");
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
