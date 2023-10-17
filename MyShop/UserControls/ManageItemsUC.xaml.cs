using MyShop.helpers;
using System;
using System.Collections.Generic;
using System.Data;
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

using static MyShop.helpers.MyShopHelpers;
using static MyShop.Classes.MyModel;

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for ManageItemsUC.xaml
    /// </summary>
    public partial class ManageItemsUC : UserControl
    {
        public ManageItemsUC()
        {
            InitializeComponent();
        }
      
        private void handleManageItemsUCLoaded(object sender, RoutedEventArgs e)
        {
            // Code here..
        }

        private void handleSheetSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Code here..
        }

        private void handleProductImportExcel(object sender, RoutedEventArgs e)
        {
            var checkOpenExcelData = MyShop.helpers.MyShopHelpers.readExcelData();

            if (checkOpenExcelData == true)
            {
                productManageDataGrid.ItemsSource = productTable.DefaultView;
                categoryManageDataGrid.ItemsSource = categoryTable.DefaultView;

                MessageBox.Show("Import data successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SearchBoxUC_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("===> SearchBoxUC_Loaded Check");
        }       
    }
}
