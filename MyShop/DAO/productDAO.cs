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
using static MyShop.Classes.Product;
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
    }
}
