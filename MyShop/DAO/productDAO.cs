﻿using LiveChartsCore.Themes;
using Microsoft.Office.Interop.Excel;
using MyShop.Classes;
using Npgsql;
using Npgsql.Internal;
using System;
using System.Collections;
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
using static MyShop.Classes.ProductTopLimit;
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

            var dataTable = getDataTable(connection, "select * from \"product\" ORDER BY product_id ASC");

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

        public static List<ProductTopLimit> getTopProductLimit(int top, int limit)
        {
            // top: Số lượng sản phẩm được cho là sắp hết hàng
            // limit: lấy số lượng từ danh sách của biến top

            NpgsqlConnection connection = connectDB();

            var queryStr = $@"SELECT product_id, name, inventory_number,
                import_price, price FROM ""product""
                WHERE ""inventory_number"" < {top}
                order by ""inventory_number"" ASC LIMIT {limit};";


            var dataTable = getDataTable(connection, queryStr);

            var productTopLimitList = new List<MyShop.Classes.ProductTopLimit>();

            productTopLimitList = (from DataRow dr in dataTable.Rows
                               select new MyShop.Classes.ProductTopLimit()
                               {
                                   product_id = (int)dr["product_id"],
                                   name = dr["name"].ToString(),
                                   inventory_number = (int)dr["inventory_number"],
                                   import_price = (int)dr["import_price"],
                                   price = (int)dr["price"]
                               }).ToList();

            return productTopLimitList;
        }

        public static int getMaxProductID()
        {
            int maxProductID = 0;

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT MAX(product_id) FROM \"product\"", connection);

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                maxProductID = (int)reader2.GetValue(0);
            }

            reader2.Close();

            return maxProductID;
        }

        public static void insertProduct(Product product, int categoryID)
        {
            NpgsqlConnection connection = connectDB();

            string productStr = $"INSERT INTO product(\r\n\tproduct_id, name, inventory_number, import_price, price, image, detail, manufacture)\r\n\t" +
                            $"VALUES (@id, @name, @in, @ip, @price, @image, @detail, @manu);";

            NpgsqlCommand query = new NpgsqlCommand(productStr, connection);
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.product_id;
            query.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = product.name;
            query.Parameters.Add("@in", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.inventory_number;
            query.Parameters.Add("@ip", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.import_price;
            query.Parameters.Add("@price", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.price;
            query.Parameters.Add("@image", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = product.image;
            query.Parameters.Add("@detail", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = product.detail;
            query.Parameters.Add("@manu", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = product.manufacture;

            string cateProStr = $"INSERT INTO public.category_product(\r\n\tproduct_id, category_id)\r\n\tVALUES (@id, @c_id)";

            NpgsqlCommand query2 = new NpgsqlCommand(cateProStr, connection);

            query2.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.product_id;
            query2.Parameters.Add("@c_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = categoryID;

            query.ExecuteNonQuery();
            query2.ExecuteNonQuery();
        }

        public static string getCategory(int productID)
        {
            NpgsqlConnection connection = connectDB();

            string getCategory = "";

            string getCategoryStr = $"SELECT c.name as category_name FROM public.product p " +
                $"JOIN public.category_product cp ON p.product_id = cp.product_id " +
                $"JOIN public.category c ON cp.category_id = c.category_id WHERE p.product_id = @id";

            NpgsqlCommand query = new NpgsqlCommand(getCategoryStr, connection);
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = productID;

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                getCategory = (string)reader.GetValue(0);
            }

            reader.Close();

            return getCategory;
        }

        public static void updateProduct(Product product, string categoryName)
        {
            NpgsqlConnection connection = connectDB();

            string productStr = $"UPDATE public.product SET name=@name, inventory_number = @in, import_price = @ip, " +
                $"price =@price, image =@image, detail =@detail, manufacture =@manu, status =@status, modify_at = NOW() WHERE product_id = @id; ";

            NpgsqlCommand query = new NpgsqlCommand(productStr, connection);
            query.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = product.name;
            query.Parameters.Add("@in", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.inventory_number;
            query.Parameters.Add("@ip", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.import_price;
            query.Parameters.Add("@price", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.price;
            query.Parameters.Add("@image", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = product.image;
            query.Parameters.Add("@detail", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = product.detail;
            query.Parameters.Add("@manu", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = product.manufacture;
            query.Parameters.Add("@status", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = product.status;
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.product_id;

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT category_id FROM \"category\" where name = @name", connection);
            query2.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = categoryName;

            var reader2 = query2.ExecuteReader();
            int categoryID = 1;

            while (reader2.Read())
            {
                categoryID = (int)reader2.GetValue(0);
            }

            reader2.Close();

            string cateProStr = $"UPDATE public.category_product\r\n\tSET category_id=@c_id\r\n\tWHERE product_id=@p_id";
            NpgsqlCommand query3 = new NpgsqlCommand(cateProStr, connection);

            query3.Parameters.Add("@c_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = categoryID;
            query3.Parameters.Add("@p_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = product.product_id;

            query.ExecuteNonQuery();
            query3.ExecuteNonQuery();
        }

        public static void deleteProduct(int productID)
        {
            NpgsqlConnection connection = connectDB();

            string productStr = $"DELETE FROM public.product WHERE product_id = @id";

            NpgsqlCommand query = new NpgsqlCommand(productStr, connection);
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = productID;

            query.ExecuteNonQuery();
        }

        public static List<Product> getProductListSearch(string searchString)
        {
            List<MyShop.Classes.Product> listProductSearch = new List<Classes.Product>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT * FROM \"product\" WHERE name LIKE @searchPattern ORDER BY product_id ASC", npgsqlConnection);
            query1.Parameters.Add("@searchPattern", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = '%' + searchString + '%';

            var reader1 = query1.ExecuteReader();
            while (reader1.Read())
            {
                listProductSearch.Add(new Classes.Product
                {
                    product_id = (int)reader1.GetValue(0),
                    name = (string)reader1.GetValue(1),
                    inventory_number = (int)reader1.GetValue(2),
                    import_price = (int)reader1.GetValue(3),
                    price = (int)reader1.GetValue(4),
                    image = (string)reader1.GetValue(5),
                    detail = (string)reader1.GetValue(6),
                    manufacture = (string)reader1.GetValue(7),
                    status = (string)reader1.GetValue(8),
                    create_at = (DateTime)reader1.GetValue(9),
                    modify_at = (DateTime)reader1.GetValue(10),
                });
            }
            reader1.Close();

            return listProductSearch;
        }

        public static List<Product> getProductListPrice(int fromPrice, int toPrice)
        {
            NpgsqlConnection connection = connectDB();

            string getCategoryStr = $"SELECT * from \"product\" where \"price\" >= @fromPrice and \"price\" <= @toPrice ORDER BY product_id ASC";

            NpgsqlCommand query = new NpgsqlCommand(getCategoryStr, connection);
            query.Parameters.Add("@fromPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = fromPrice;
            query.Parameters.Add("@toPrice", NpgsqlTypes.NpgsqlDbType.Integer).Value = toPrice;

            var reader = query.ExecuteReader();
 
            List<Product> listProducts = new List<Product>();

            while (reader.Read())
            {
                listProducts.Add(new Classes.Product
                {
                    product_id = (int)reader.GetValue(0),
                    name = (string)reader.GetValue(1),
                    inventory_number = (int)reader.GetValue(2),
                    import_price = (int)reader.GetValue(3),
                    price = (int)reader.GetValue(4),
                    image = (string)reader.GetValue(5),
                    detail = (string)reader.GetValue(6),
                    manufacture = (string)reader.GetValue(7),
                    status = (string)reader.GetValue(8),
                    create_at = (DateTime)reader.GetValue(9),
                    modify_at = (DateTime)reader.GetValue(10),
                });
            }

            reader.Close();

            return listProducts;
        }

        public List<String> getListNameProduct()
        {
            NpgsqlConnection connection = connectDB();

            List<String> listNameProduct = new List<String>();

            NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM \"product\" ORDER BY product_id", connection);

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                listNameProduct.Add((string)reader.GetValue(1));
            }

            reader.Close();

            return listNameProduct;
        }

        public List<long> getListQuantityProduct()
        {
            NpgsqlConnection connection = connectDB();

            List<long> istQuantityProduct = new List<long>();

            NpgsqlCommand query = new NpgsqlCommand("SELECT pr.product_id, pr.name, COALESCE(SUM(dor.quantity),0) FROM \"detail_order\" dor \r\n" +
                "RIGHT JOIN  \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "GROUP BY pr.product_id, pr.name\r\n" +
                "ORDER BY pr.product_id;", connection);

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                istQuantityProduct.Add((long)reader.GetValue(2));
            }

            reader.Close();

            return istQuantityProduct;
        }

        public List<long> getListQuantityDayProduct(DateTime from, DateTime to)
        {
            NpgsqlConnection connection = connectDB();

            List<long> istQuantityProduct = new List<long>();

            NpgsqlCommand query;

            query = new NpgsqlCommand("SELECT pr.product_id, pr.name, COALESCE(SUM(dor.quantity),0) FROM \"detail_order\" dor \r\n" +
                    "JOIN \"order\" o ON dor.order_id = o.order_id AND (o.order_date BETWEEN @from AND @to)\r\n" +
                    "RIGHT JOIN  \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                    "GROUP BY pr.product_id, pr.name\r\n" +
                    "ORDER BY coalesce DESC;", connection);


            query.Parameters.Add("@from", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = from;
            query.Parameters.Add("@to", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = to;

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                istQuantityProduct.Add((long)reader.GetValue(2));
            }

            reader.Close();

            return istQuantityProduct;
        }

        public List<long> getListQuantityWeekProduct(int week, int year)
        {
            NpgsqlConnection connection = connectDB();

            List<long> istQuantityProduct = new List<long>();

            NpgsqlCommand query;


            query = new NpgsqlCommand("SELECT pr.product_id, pr.name, COALESCE(SUM(dor.quantity),0) FROM \"detail_order\" dor \r\n" +
                    "JOIN \"order\" o ON dor.order_id = o.order_id and EXTRACT('WEEK' FROM order_date) = @week AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                    "RIGHT JOIN  \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                    "GROUP BY pr.product_id, pr.name\r\n" +
                    "ORDER BY pr.product_id;", connection);
            query.Parameters.Add("@week", NpgsqlTypes.NpgsqlDbType.Integer).Value = week;
            query.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                istQuantityProduct.Add((long)reader.GetValue(2));
            }

            reader.Close();

            return istQuantityProduct;
        }

        public List<long> getListQuantityMonthProduct(int month, int year)
        {
            NpgsqlConnection connection = connectDB();

            List<long> istQuantityProduct = new List<long>();

            NpgsqlCommand query;


            query = new NpgsqlCommand("SELECT pr.product_id, pr.name, COALESCE(SUM(dor.quantity),0) FROM \"detail_order\" dor \r\n" +
                    "JOIN \"order\" o ON dor.order_id = o.order_id and EXTRACT('MONTH' FROM order_date) = @month AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                    "RIGHT JOIN  \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                    "GROUP BY pr.product_id, pr.name\r\n" +
                    "ORDER BY pr.product_id;", connection);
            query.Parameters.Add("@month", NpgsqlTypes.NpgsqlDbType.Integer).Value = month;
            query.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                istQuantityProduct.Add((long)reader.GetValue(2));
            }

            reader.Close();

            return istQuantityProduct;
        }

        public List<long> getListQuantityYearProduct(int year)
        {
            NpgsqlConnection connection = connectDB();

            List<long> istQuantityProduct = new List<long>();

            NpgsqlCommand query;


            query = new NpgsqlCommand("SELECT pr.product_id, pr.name, COALESCE(SUM(dor.quantity),0) FROM \"detail_order\" dor \r\n" +
                    "JOIN \"order\" o ON dor.order_id = o.order_id and EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                    "RIGHT JOIN  \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                    "GROUP BY pr.product_id, pr.name\r\n" +
                    "ORDER BY pr.product_id;", connection);
            query.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader = query.ExecuteReader();
            while (reader.Read())
            {
                istQuantityProduct.Add((long)reader.GetValue(2));
            }

            reader.Close();

            return istQuantityProduct;
        }

        public static List<int> getListProductID()
        {
            List<int> listProductID = new List<int>();

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT product_id FROM \"product\" order by product_id ASC ", connection);

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                listProductID.Add((int)reader2.GetValue(0));
            }

            reader2.Close();

            return listProductID;
        }
    }
}
