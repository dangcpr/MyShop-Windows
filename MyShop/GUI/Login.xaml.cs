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

using static MyShop.BUS.connectDatabaseBUS;
using static MyShop.BUS.accountsBUS;

using static MyShop.DAO.connectDatabaseDAO;
using static MyShop.DAO.accountsDAO;
using MyShop.DAO;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private MyShop.DAO.connectDatabaseDAO dbDAO = null;
        private MyShop.DAO.accountsDAO accountsDAO = null;

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

                    bool checkLogin = await accountsDAO.checkLoginAccount(username, password);

                    if (checkLogin == true)
                    {
                        //MessageBox.Show("Login Successfully");
                        var dashboardScreen = new Dashboard();
                        dashboardScreen.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login failed");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something wrong :(");
            }               
        }
    }
}
