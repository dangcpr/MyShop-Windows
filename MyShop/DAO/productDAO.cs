using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static MyShop.Classes.Product;
using static MyShop.Classes.ProductSpeedStats;
using static MyShop.DAO.connectDatabaseDAO;

namespace MyShop.DAO
{
    public class productDAO : INotifyPropertyChanged
    {
        public static Product _productList { get; set; }

        public static Product _product { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public productDAO()
        {
            _product = new Product();
        }

        public static List<Product> getProductList()
        {
            NpgsqlConnection connection = connectDB();

            var dataTable = getDataTable(connection, "select * from \"product\"");

            var productList = new List<MyShop.Classes.Product>();

            productList = (from DataRow dr in dataTable.Rows
                           select new MyShop.Classes.Product()
                           {
                               product_id = (int)dr["product_id"],
                               name = dr["name"].ToString(),
                               inventory_number = (int)dr["inventory_number"],
                               import_price = (int)dr["import_price"],
                               price = (int)dr["price"],
                               image = dr["image"].ToString(),
                               detail = dr["detail"].ToString(),
                               manufacture = dr["manufacture"].ToString(),
                               status = dr["status"].ToString(),
                               create_at = DateTime.Parse(dr["create_at"].ToString()),
                               modify_at = DateTime.Parse(dr["modify_at"].ToString())
                           }).ToList();

            return productList;
        }

        public static List<MyShop.Classes.ProductSpeedStats> getSpeedStats()
        {
            NpgsqlConnection connection = connectDB();

            string queryStr = "SELECT c.\"category_id\", c.\"name\", SUM(p.\"inventory_number\") as \"in_num_cat\" FROM \"category_product\" ct\r\nJOIN \"product\" p ON ct.product_id = p.product_id\r\nJOIN \"category\" c ON ct.\"category_id\" = c.\"category_id\"\r\nGROUP BY c.\"category_id\";";

            var dataTable = getDataTable(connection, queryStr);

            var speedStatsTable = new List<MyShop.Classes.ProductSpeedStats>();

            speedStatsTable = (from DataRow dr in dataTable.Rows
                           select new MyShop.Classes.ProductSpeedStats()
                           {
                               category_id = (int)dr["category_id"],
                               name = dr["name"].ToString(),
                               in_num_cat = (long)dr["in_num_cat"]
                           }).ToList();

            return speedStatsTable;
        }

        public static long getProductInventorySum()
        {
            NpgsqlConnection connection = connectDB();

            string queryStr = "SELECT SUM(\"inventory_number\") FROM \"product\";";

            var dataTable = getDataTable(connection, queryStr);

            long productInventorySum = dataTable.Rows.Count;

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    object value = row[col.ColumnName];
                    productInventorySum = (long)value;
                }
            }

            return productInventorySum;
        }
    }
}
