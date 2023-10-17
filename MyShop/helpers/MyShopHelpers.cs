using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

using static MyShop.Classes.MyModel;
using static MyShop.DAO.connectDatabaseDAO;

using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using Microsoft.Office.Interop.Excel;
using Npgsql;

namespace MyShop.helpers
{
    public class MyShopHelpers
    {
        public static System.Data.DataTable productTable { get; set; }

        public static System.Data.DataTable categoryTable { get; set; }
     

        class ExcelProduct
        {
            string product_id { get; set; }

            string name { get; set; }

            string inventory_number { get; set; }

            string import_price { get; set; }

            string price { get; set; }

            string image { get; set; }

            string manufacture { get; set; }

            string category_id { get; set; }
        }

        class ExcelCategory
        {
            string category_id { get; set; }

            string name { get; set; }
        }

        public static bool readExcelData()
        {
            string filePath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == true)
            {               
                try
                {
                    List<ExcelProduct> productList = new List<ExcelProduct>();
                    List<ExcelCategory> categoryList = new List<ExcelCategory>();


                    System.Data.DataTable dtP = new System.Data.DataTable();
                    System.Data.DataTable dtC = new System.Data.DataTable();


                    dtP.Columns.Add("product_id");
                    dtP.Columns.Add("name");
                    dtP.Columns.Add("inventory_number");
                    dtP.Columns.Add("import_price");
                    dtP.Columns.Add("price");
                    dtP.Columns.Add("image");
                    dtP.Columns.Add("detail");
                    dtP.Columns.Add("manufacture");
                    dtP.Columns.Add("category_id");

                    dtC.Columns.Add("category_id");
                    dtC.Columns.Add("name");

                    filePath = openFileDialog.FileName;

                    // Open excel file
                    var package = new ExcelPackage(new FileInfo(filePath));

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Get first sheet (sheet1)
                    ExcelWorksheet productWS = package.Workbook.Worksheets[0];
                    ExcelWorksheet productWS2 = package.Workbook.Worksheets[1];

                    excelProductList = new List<Classes.Product>();
                    excelCategoryList = new List<Classes.Category>();
                    excelCategoryProductList = new List<Classes.CategoryProduct>();

                    // Loop from row 2 (Excel file start from 1)
                    for (var row = productWS.Dimension.Start.Row + 1; row < productWS.Dimension.End.Row + 2; row++)
                    {
                        var cols = 1;

                        if (productWS.Cells[row, cols].Value != null)
                        {
                            string product_id = productWS.Cells[row, cols++].Value.ToString();
                            string name = productWS.Cells[row, cols++].Value.ToString();
                            string inventory_number = productWS.Cells[row, cols++].Value.ToString();
                            string import_price = productWS.Cells[row, cols++].Value.ToString();
                            string price = productWS.Cells[row, cols++].Value.ToString();
                            string image = productWS.Cells[row, cols++].Value.ToString();
                            string detail = productWS.Cells[row, cols++].Value.ToString();
                            string manufacture = productWS.Cells[row, cols++].Value.ToString();
                            string category_id = productWS.Cells[row, cols++].Value.ToString();

                            dtP.Rows.Add(product_id, name, inventory_number, import_price,
                                price, image, detail, manufacture, category_id);

                            // product table

                            var newProduct = new MyShop.Classes.Product
                            {
                                product_id = Int32.Parse(product_id),
                                name = name,
                                inventory_number = Int32.Parse(inventory_number),
                                import_price = Int32.Parse(import_price),
                                price = Int32.Parse(price),
                                image = image,
                                detail = detail,
                                manufacture = manufacture,
                            };

                            excelProductList.Add(newProduct);

                            // Category table

                            var newCategory = new MyShop.Classes.Category
                            {
                                category_id = Int32.Parse(category_id),
                                name = name,
                            };

                            excelCategoryList.Add(newCategory);

                            // Category_product table

                            var newCategoryProduct = new MyShop.Classes.CategoryProduct
                            {
                                category_id = Int32.Parse(category_id),
                                product_id = Int32.Parse(product_id),
                            };

                            excelCategoryProductList.Add(newCategoryProduct);
                        }
                    }

                    for (var row = productWS2.Dimension.Start.Row + 1; row < productWS2.Dimension.End.Row + 2; row++)
                    {
                        var cols = 1;

                        if (productWS2.Cells[row, cols].Value != null)
                        {
                            string category_id = productWS2.Cells[row, cols++].Value.ToString();
                            string name = productWS2.Cells[row, cols++].Value.ToString();

                            dtC.Rows.Add(category_id, name);                     
                        }
                    }

                    productTable = dtP;
                    categoryTable = dtC;

                    // Save to database
                    NpgsqlConnection connection = connectDB();

                    foreach (var product in excelProductList)
                    {
                        string productStr = $"INSERT INTO product(\r\n\tproduct_id, name, inventory_number, import_price, price, image, detail, manufacture)\r\n\t" +
                            $"VALUES ({product.product_id}, '{product.name}', {product.inventory_number}, {product.import_price}, {product.price}, '{product.image}', '{product.detail}', '{product.manufacture}');";

                        ExecutePSQLQuery(productStr);
                    }

                    foreach (var category in excelCategoryList)
                    {
                        string cetegoryStr = $"INSERT INTO category(\r\n\tcategory_id, name)\r\n\t" +
                            $"VALUES ({category.category_id}, '{category.name}');";

                        ExecutePSQLQuery(cetegoryStr);
                    }

                    foreach (var categoryProduct in excelCategoryProductList)
                    {
                        string cetegoryProductStr = $"INSERT INTO category_product(\r\n\tproduct_id, category_id)\r\n\t" +
                            $"VALUES ({categoryProduct.product_id}, {categoryProduct.category_id});";

                        ExecutePSQLQuery(cetegoryProductStr);
                    }

                    return true;
                }
                catch
                {
                    MessageBox.Show("Import data failed", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return false;
        }
    }
}
