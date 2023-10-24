using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using static MyShop.DAO.connectDatabaseDAO;
using static MyShop.DAO.categoryDAO;
using static MyShop.DAO.productDAO;
using static MyShop.Classes.Category;
using System.Collections.ObjectModel;
using MyShop.Classes;
using System.Diagnostics;
using LiveChartsCore.Kernel;
using MyShop.DAO;
using Microsoft.Win32;
using SkiaSharp;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        Product product = new Product();
        List<MyShop.Classes.Category> categories;
        MyShop.BUS.productBUS productBUS = new BUS.productBUS();
        string startupPath;
        OpenFileDialog openFileDialog;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string startupPath = System.IO.Directory.GetCurrentDirectory();

            startupPath = Environment.CurrentDirectory;
            Debug.WriteLine(startupPath);

            try
            {
                if (dbBUS.checkConnectDatabase() == true)
                {
                    categories = MyShop.DAO.categoryDAO.listCategories();

                    foreach (MyShop.Classes.Category category in categories)
                    {
                        categoryComboBox.Items.Add(category.name);
                    }

                    ProductIDTextBox.Text = (MyShop.DAO.productDAO.getMaxProductID() + 1).ToString();
                    imageTextBox.Text = (MyShop.DAO.productDAO.getMaxProductID() + 1).ToString() + ".jpg";
                }
            }
            catch (Exception ex)
            {
                ProductIDTextBox.Text = "0";
            }
        }

        private void SumbitButtonAdd_OnPressed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductIDTextBox.Text.Length == 0 || nameProductTextBox.Text.Length == 0 || inventoryNumberProductTextBox.Text.Length == 0 ||
                importPriceProductTextBox.Text.Length == 0 || priceProductTextBox.Text.Length == 0 || imageTextBox.Text.Length == 0 ||
                detailTextBox.Text.Length == 0 || manufactureTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Không được bỏ trống dòng nào");
                    return;
                }

                product.product_id = Int32.Parse(ProductIDTextBox.Text.ToString());
                product.name = nameProductTextBox.Text.ToString();
                product.inventory_number = Int32.Parse(inventoryNumberProductTextBox.Text.ToString());
                product.import_price = Int32.Parse(importPriceProductTextBox.Text.ToString());
                product.price = Int32.Parse(priceProductTextBox.Text.ToString());
                product.image = imageTextBox.Text.ToString();
                product.detail = detailTextBox.Text.ToString();
                product.manufacture = manufactureTextBox.Text.ToString();

                int choose = categoryComboBox.SelectedIndex;

                int categoryID = categories[choose].category_id;

                if (productBUS.checkProductInSale() == true)
                {
                    insertProduct(product, categoryID);
                }

                string path = $"{startupPath}/products/{product.image}";
                if(openFileDialog != null && openFileDialog.FileName != null && openFileDialog.FileName != "") File.Copy(openFileDialog.FileName, path, true);
                if (System.IO.File.Exists(path))
                {
                    using (var lockFile = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        lockFile.Close();
                    }
                }

                MessageBox.Show("Thêm sản phẩm thành công");
                MyShop.UserControls.ManageItemsUC.dtProduct.ItemsSource = productDAO.getProductList();

                this.Close();

                return;
            }catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        MyShop.BUS.connectDatabaseBUS dbBUS = new MyShop.BUS.connectDatabaseBUS();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if(openFileDialog.ShowDialog() == true)
            {
                imageProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
