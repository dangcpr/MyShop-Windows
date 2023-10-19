using MyShop.BUS;
using MyShop.DAO;
using MyShop.UserControls;
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


namespace MyShop.GUI
{
    /// <summary>
    /// Interaction logic for UpdateCategory.xaml
    /// </summary>
    public partial class UpdateCategory : Window
    {
        public UpdateCategory()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categoryIDTextBox.Text = ManageItemsUC.category.category_id.ToString();
            nameCategoryTextBox.Text = ManageItemsUC.category.name;
        }

        MyShop.Classes.Category category = new Classes.Category();

        private void SumbitButtonUpdate_OnPressed(object sender, RoutedEventArgs e)
        {
            if (categoryIDTextBox.Text.Length == 0 || nameCategoryTextBox.Text.Length == 0)
            {
                MessageBox.Show("Không được bỏ trống dòng nào");
                return;
            }

            category.category_id = Int32.Parse(categoryIDTextBox.Text);
            category.name = nameCategoryTextBox.Text;

            try
            {
                if (categoryBUS.checkCategoryBUS() == true)
                {
                    updateCategory(category);
                }

                MessageBox.Show("Cập nhật loại sản phẩm thành công");
                MyShop.UserControls.ManageItemsUC.dtCategory.ItemsSource = categoryDAO.listCategories();

                this.Close();

                return;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }
    }
}
