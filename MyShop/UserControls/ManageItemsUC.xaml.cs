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
using static MyShop.GUI.AddProduct;
using MyShop.DAO;
using MyShop.BUS;
using MyShop.GUI;
using MyShop.Classes;

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

        List<MyShop.Classes.Product> products;
        List<MyShop.Classes.Category> categories;
        public static DataGrid dtProduct = new DataGrid();
        public static DataGrid dtCategory = new DataGrid();

        private void handleManageItemsUCLoaded(object sender, RoutedEventArgs e)
        {
            products = productDAO.getProductList();
            productManageDataGrid.ItemsSource = products;
            dtProduct = productManageDataGrid;

            categories = categoryDAO.listCategories();
            categoryManageDataGrid.ItemsSource = categories;
            dtCategory = categoryManageDataGrid;
        }

        private void handleSheetSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Code here..
        }

        MyShop.BUS.productBUS productBUS = new productBUS();
        public static MyShop.Classes.Product product = new MyShop.Classes.Product();
        public static MyShop.Classes.Category category = new MyShop.Classes.Category();
        public static int productSelected = -1;
        public static int categorySelected = -1;
        public static string categoryName;

        private void handleProductImportExcel(object sender, RoutedEventArgs e)
        {
            var checkOpenExcelData = MyShop.helpers.MyShopHelpers.readExcelData();

            if (checkOpenExcelData == true)
            {
                products = productDAO.getProductList();
                productManageDataGrid.ItemsSource = products;

                categories = categoryDAO.listCategories();
                categoryManageDataGrid.ItemsSource = categories;

                MessageBox.Show("Import data successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SearchBoxUC_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("===> SearchBoxUC_Loaded Check");
        }

        private void productManageDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            IList<DataGridCellInfo> selectedcells = e.AddedCells;

            foreach (DataGridCellInfo di in selectedcells)
            {
                //Cast the DataGridCellInfo.Item to the source object type
                //In this case the ItemsSource is a DataTable and individual items are DataRows
                MyShop.Classes.Product dvr = (MyShop.Classes.Product)di.Item;

                product.product_id = dvr.product_id;
                productSelected = dvr.product_id;
                product.name = dvr.name;
                product.inventory_number = (int)dvr.inventory_number;
                product.import_price = (int)dvr.import_price;
                product.price = (int)dvr.price;
                product.image = (string)dvr.image;
                product.detail = (string)dvr.detail;
                product.manufacture = (string)dvr.manufacture;
                product.status = (string)dvr.status;
                categoryName = productDAO.getCategory(product.product_id);
            }
            Debug.WriteLine(productSelected.ToString() + ' ' + categoryName);
        }

        private void AddProductButton_OnPressed(object sender, RoutedEventArgs e)
        {
            MyShop.GUI.AddProduct addProduct = new MyShop.GUI.AddProduct();
            addProduct.Show();
        }

        private void RemoveProductButton_OnPressed(object sender, RoutedEventArgs e)
        {
            if (productSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to remove " + product.name, "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if(productBUS.checkProductInSale() == true)
                    {
                        productDAO.deleteProduct(productSelected);

                        MessageBox.Show("Xóa thành công");

                        dtProduct.ItemsSource = productDAO.getProductList();

                        return;
                    }
                }  
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); return;
                }
            }
            else if(result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                return;
            }
        }

        private void UpdateProductButton_OnPressed(object sender, RoutedEventArgs e)
        {
            if(productSelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }
            MyShop.GUI.UpdateProduct updateProduct = new MyShop.GUI.UpdateProduct();
            updateProduct.Show();
        }

        private void AddCategoryButton_OnPressed(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }

        private void RemoveCategoryButton_OnPressed(object sender, RoutedEventArgs e)
        {
            if (categorySelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to remove " + category.name, "Confirmation", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (categoryBUS.checkCategoryBUS() == true)
                    {
                        categoryDAO.deleteCategory(categorySelected);

                        MessageBox.Show("Xóa thành công");

                        dtProduct.ItemsSource = productDAO.getProductList();
                        dtCategory.ItemsSource = categoryDAO.listCategories();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                    return;
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

        private void UpdateCategoryButton_OnPressed(object sender, RoutedEventArgs e)
        {
            if (categorySelected < 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 dòng dữ liệu");
                return;
            }

            UpdateCategory updateCategory = new UpdateCategory();
            updateCategory.Show();
        }

        private void categoryManageDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                IList<DataGridCellInfo> selectedcells = e.AddedCells;

                foreach (DataGridCellInfo di in selectedcells)
                {
                    //Cast the DataGridCellInfo.Item to the source object type
                    //In this case the ItemsSource is a DataTable and individual items are DataRows
                    MyShop.Classes.Category dvr = (MyShop.Classes.Category)di.Item;

                    category.category_id = dvr.category_id;
                    categorySelected = dvr.category_id;
                    category.name = dvr.name;
                }

                Debug.WriteLine(categorySelected.ToString());
            } catch
            {
                categorySelected = -1;
            }
        }

        string searchProductPattern = "";

        private void searchProductButton_OnPressed(object sender, RoutedEventArgs e)
        {
            try
            {
                searchProductPattern = SearchProductTextBox.Text;
                List<Product> productSearch = new List<Product>();
                productSearch = productDAO.getProductListSearch(searchProductPattern);

                productManageDataGrid.ItemsSource = productSearch;
            }
            catch
            {
                return;
            }
        }
    }
}
