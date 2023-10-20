using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using static MyShop.Classes.MyModel;

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for OrdersUC.xaml
    /// </summary>
    public partial class OrdersUC : UserControl
    {
        public OrdersUC()
        {
            InitializeComponent();
        }

        private void handleOrdersUCLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void orderManageDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void handlePrevDataGrid(object sender, RoutedEventArgs e)
        {
            
        }

        private void handleNextDataGrid(object sender, RoutedEventArgs e)
        {

        }
    }
}
