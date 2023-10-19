using MyShop.BUS;
using MyShop.Classes;
using MyShop.DAO;
using System;
using System.Collections.Generic;
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

using static MyShop.DAO.categoryDAO;
using static MyShop.BUS.categoryBUS;
using System.Diagnostics;

namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        Category category = new Category();
        private void SumbitButtonAdd_OnPressed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (categoryIDTextBox.Text.Length == 0 || nameCategoryTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Không được bỏ trống dòng nào");
                    return;
                }

                category.category_id = Int32.Parse(categoryIDTextBox.Text.ToString());
                category.name = nameCategoryTextBox.Text.ToString();

                if (categoryBUS.checkCategoryBUS() == true)
                {
                    insertCategory(category);
                }

                MessageBox.Show("Thêm loại sản phẩm thành công");
                MyShop.UserControls.ManageItemsUC.dtCategory.ItemsSource = categoryDAO.listCategories();

                this.Close();

                return;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        MyShop.BUS.connectDatabaseBUS dbBUS = new MyShop.BUS.connectDatabaseBUS();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                categoryIDTextBox.Text = (MyShop.DAO.categoryDAO.getMaxCategoryID() + 1).ToString();
            }
            catch (Exception ex)
            {
                categoryIDTextBox.Text = "0";
            }
        }
    }
}
