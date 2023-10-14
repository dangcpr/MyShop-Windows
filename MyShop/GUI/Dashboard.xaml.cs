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

        private void handleDashboardSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Mobile view
            if (e.PreviousSize != new Size() && e.PreviousSize.Width < 864)
            {
                dashboardNavTop.Visibility = Visibility.Collapsed;
                dashboardNavTopMobile.Visibility = Visibility.Visible;

                tab1Name.Visibility = Visibility.Collapsed;
                tab2Name.Visibility = Visibility.Collapsed;
                tab3Name.Visibility = Visibility.Collapsed;
                tab4Name.Visibility = Visibility.Collapsed;

                tab1.Width = 100;
                tab2.Width = 100;
                tab3.Width = 100;
                tab4.Width = 100;
            }
            else
            {
                dashboardNavTop.Visibility = Visibility.Visible;
                dashboardNavTopMobile.Visibility = Visibility.Collapsed;

                tab1Name.Visibility = Visibility.Visible;
                tab2Name.Visibility = Visibility.Visible;
                tab3Name.Visibility = Visibility.Visible;
                tab4Name.Visibility = Visibility.Visible;

                tab1.Width = 250;
                tab2.Width = 250;
                tab3.Width = 250;
                tab4.Width = 250;
            }
        }

        private void handleChange(object sender, RoutedEventArgs e)
        {
            _user.username = "Nguyễn Văn A";
        }

        private void handleLogout(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Do you can to log out?", "Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (confirm == MessageBoxResult.Yes)
            {
                _user = null;
                var loginScreen = new Login();
                loginScreen.Show();
                this.Hide();
            }
        }
    }
}
