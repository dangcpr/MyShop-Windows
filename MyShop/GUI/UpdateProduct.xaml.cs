using Microsoft.VisualBasic;
using Microsoft.Win32;
using MyShop.DAO;
using MyShop.UserControls;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        public UpdateProduct()
        {
            InitializeComponent();
        }

        MyShop.BUS.connectDatabaseBUS dbBUS = new MyShop.BUS.connectDatabaseBUS();
        List<MyShop.Classes.Category> categories;
        MyShop.Classes.Product product = new Classes.Product();
        string startupPath;
        OpenFileDialog openFileDialog;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startupPath = Environment.CurrentDirectory;
            Debug.WriteLine(startupPath);

            if (dbBUS.checkConnectDatabase() == true)
            {
                categories = MyShop.DAO.categoryDAO.listCategories();

                foreach (MyShop.Classes.Category category in categories)
                {
                    categoryComboBox.Items.Add(category.name);
                }
            }

            ProductIDTextBox.Text = ManageItemsUC.product.product_id.ToString();
            nameProductTextBox.Text = ManageItemsUC.product.name;
            inventoryNumberProductTextBox.Text = ManageItemsUC.product.inventory_number.ToString();
            importPriceProductTextBox.Text = ManageItemsUC.product.import_price.ToString();
            priceProductTextBox.Text = ManageItemsUC.product.price.ToString();
            imageTextBox.Text = ManageItemsUC.product.image;
            categoryComboBox.Text = ManageItemsUC.categoryName;
            manufactureTextBox.Text = ManageItemsUC.product.manufacture;
            statusComboBox.Text = ManageItemsUC.product.status;
            detailTextBox.Text = ManageItemsUC.product.detail;

            string pathImageProduct = $"{startupPath}/products/{ManageItemsUC.product.image}";
            if (File.Exists(pathImageProduct))
            {
                BitmapImage image = new BitmapImage(new Uri($"{startupPath}/products/{ManageItemsUC.product.image}",
                                             UriKind.Absolute));
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.Freeze();
                imageProduct.Source = image;
            }
        }

        private async void SumbitButtonUpdate_OnPressed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductIDTextBox.Text.Length == 0 || nameProductTextBox.Text.Length == 0 || inventoryNumberProductTextBox.Text.Length == 0 ||
                    importPriceProductTextBox.Text.Length == 0 || priceProductTextBox.Text.Length == 0 || imageTextBox.Text.Length == 0 ||
                    detailTextBox.Text.Length == 0 || manufactureTextBox.Text.Length == 0 || statusComboBox.Text.Length == 0)
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
                product.status = statusComboBox.Text.ToString();

                MyShop.DAO.productDAO.updateProduct(product, categoryComboBox.Text);

                string path = $"{startupPath}/products/{product.image}";

                if (System.IO.File.Exists(path))
                {
                    using (var lockFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        lockFile.Close();
                        System.IO.File.Delete(path);
                    }
                }

                if (openFileDialog != null && openFileDialog.FileName != null && openFileDialog.FileName != "")
                {   
                    File.Copy(openFileDialog.FileName, path, true);
                }
                
                MessageBox.Show("Update thành công");
                this.Close();

                ManageItemsUC.dtProduct.ItemsSource = productDAO.getProductList();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                imageProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
        }
    }
}
