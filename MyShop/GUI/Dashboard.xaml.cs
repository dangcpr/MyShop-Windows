using MyShop.DAO;
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

using static MyShop.Classes.Accounts;
using static MyShop.DAO.accountsDAO;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();      
        }

        MyShop.Classes.Accounts _user;

        private void handleDashboardLoaded(object sender, RoutedEventArgs e)
        {
            var username = accountsDAO.userAccount.username;

            _user = new MyShop.Classes.Accounts
            {
                username = username,
            };

            this.DataContext = _user;
        }

        private void handleChange(object sender, RoutedEventArgs e)
        {
            _user.username = "Nguyễn Văn A";
        }

        private void handleLogout(object sender, RoutedEventArgs e)
        {
            _user = null;
            var loginScreen = new Login();
            loginScreen.Show();
            this.Hide();
        }
    }
}
