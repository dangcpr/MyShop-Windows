using LiveChartsCore.Themes;
using MyShop.Classes;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyShop.Classes.Category;
using static MyShop.DAO.connectDatabaseDAO;

namespace MyShop.DAO
{
    internal class categoryDAO
    {
        public static List<MyShop.Classes.Category> listCategories()
        {
            List<MyShop.Classes.Category> listCategories = new List<Classes.Category> ();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT * FROM \"category\"", npgsqlConnection);

            List<MyShop.Classes.Category> ListCategory = new List<MyShop.Classes.Category>();

            var reader1 = query1.ExecuteReader();
            while (reader1.Read())
            {
                listCategories.Add(new Classes.Category
                {
                    category_id = (int)reader1.GetValue(0),
                    name = (string)reader1.GetValue(1),
                    create_at = (DateTime)reader1.GetValue(2),
                    modify_at = (DateTime)reader1.GetValue(3)
                });
            }
            reader1.Close();

            return listCategories;
        }

        public static List<String> listNameCategories()
        {
            List<String> listCategories = new List<String>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT * FROM \"category\" ORDER BY category_id;", npgsqlConnection);

            var reader1 = query1.ExecuteReader();
            while (reader1.Read())
            {
                listCategories.Add((string)reader1.GetValue(1));
            }
            reader1.Close();

            return listCategories;
        }

        public static List<long> revenueCategories()
        {
            List<long> listRevenueCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum(dor.after_price * dor.quantity),0) " +
                "as \"doanh_thu\" FROM \"order\" o\r\nJOIN \"detail_order\" dor ON o.order_id = dor.order_id\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\n" +
                "GROUP BY c.category_id, c.name\r\n" +
                "ORDER BY c.category_id;", npgsqlConnection);

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listRevenueCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listRevenueCategories;
        }

        public static List<long> revenueDayCategories(DateTime from, DateTime to)
        {
            List<long> listRevenueCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum(dor.after_price * dor.quantity),0) " +
                "as \"doanh_thu\" FROM \"order\" o\r\nJOIN \"detail_order\" dor ON o.order_id = dor.order_id and (o.order_date BETWEEN @from AND @to)\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\n" +
                "GROUP BY c.category_id, c.name\r\n" +
                "ORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@from", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = from;
            query1.Parameters.Add("@to", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = to;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listRevenueCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listRevenueCategories;
        }

        public static List<long> revenueWeekCategories(int week, int year)
        {
            List<long> listRevenueCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum(dor.after_price * dor.quantity),0) " +
                "as \"doanh_thu\" FROM \"order\" o\r\nJOIN \"detail_order\" dor ON EXTRACT('WEEK' FROM order_date) = @week AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\n" +
                "GROUP BY c.category_id, c.name\r\n" +
                "ORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@week", NpgsqlTypes.NpgsqlDbType.Integer).Value = week;
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listRevenueCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listRevenueCategories;
        }

        public static List<long> revenueMonthCategories(int month, int year)
        {
            List<long> listRevenueCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum(dor.after_price * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id and EXTRACT('MONTH' FROM order_date) = @month AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nRIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\n" +
                "GROUP BY c.category_id, c.name\r\n" +
                "ORDER BY c.category_id", npgsqlConnection);
            query1.Parameters.Add("@month", NpgsqlTypes.NpgsqlDbType.Integer).Value = month;
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listRevenueCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listRevenueCategories;
        }

        public static List<long> revenueYearCategories(int year)
        {
            List<long> listRevenueCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum(dor.after_price * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nRIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\n" +
                "GROUP BY c.category_id, c.name\r\n" +
                "ORDER BY c.category_id", npgsqlConnection);
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listRevenueCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listRevenueCategories;
        }

        public static List<long> profitCategories()
        {
            List<long> listProfitCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum((dor.after_price - pr.import_price) * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nLEFT JOIN \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\nGROUP BY c.category_id, c.name\r\nORDER BY c.category_id;", npgsqlConnection);


            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listProfitCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listProfitCategories;
        }

        public static List<long> profitDayCategories(DateTime from, DateTime to)
        {
            List<long> listProfitCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum((dor.after_price - pr.import_price) * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id  and (o.order_date BETWEEN @from AND @to)\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nLEFT JOIN \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\nGROUP BY c.category_id, c.name\r\nORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@from", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = from;
            query1.Parameters.Add("@to", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = to;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listProfitCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listProfitCategories;
        }

        public static List<long> profitWeekCategories(int week, int year)
        {
            List<long> listProfitCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum((dor.after_price - pr.import_price) * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id  and EXTRACT('WEEK' FROM order_date) = @week AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nLEFT JOIN \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\nGROUP BY c.category_id, c.name\r\nORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@week", NpgsqlTypes.NpgsqlDbType.Integer).Value = week;
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listProfitCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listProfitCategories;
        }

        public static List<long> profitMonthCategories(int month, int year)
        {
            List<long> listProfitCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum((dor.after_price - pr.import_price) * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id  and EXTRACT('MONTH' FROM order_date) = @month AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nLEFT JOIN \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\nGROUP BY c.category_id, c.name\r\nORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@month", NpgsqlTypes.NpgsqlDbType.Integer).Value = month;
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listProfitCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listProfitCategories;
        }

        public static List<long> profitYearCategories(int year)
        {
            List<long> listProfitCategories = new List<long>();

            NpgsqlConnection npgsqlConnection = MyShop.DAO.connectDatabaseDAO.connectDB();

            NpgsqlCommand query1 = new NpgsqlCommand("SELECT c.category_id, c.name, COALESCE(sum((dor.after_price - pr.import_price) * dor.quantity),0) as \"doanh_thu\" FROM \"order\" o\r\n" +
                "JOIN \"detail_order\" dor ON o.order_id = dor.order_id AND EXTRACT('YEAR' FROM order_date) = @year\r\n" +
                "LEFT JOIN \"category_product\" cp ON cp.product_id = dor.product_id \r\nLEFT JOIN \"product\" pr ON dor.product_id = pr.product_id\r\n" +
                "RIGHT JOIN \"category\" c ON c.category_id = cp.category_id\r\nGROUP BY c.category_id, c.name\r\nORDER BY c.category_id;", npgsqlConnection);
            query1.Parameters.Add("@year", NpgsqlTypes.NpgsqlDbType.Integer).Value = year;

            var reader1 = query1.ExecuteReader();

            while (reader1.Read())
            {
                listProfitCategories.Add((long)reader1.GetValue(2));
            }
            reader1.Close();

            return listProfitCategories;
        }

        public static void insertCategory(Category category)
        {
            NpgsqlConnection connection = connectDB();

            string productStr = $"INSERT INTO public.category(\r\n\tcategory_id, name)\r\n\tVALUES (@id, @name);";

            NpgsqlCommand query = new NpgsqlCommand(productStr, connection);
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = category.category_id;
            query.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = category.name;

            query.ExecuteNonQuery();
        }

        public static int getMaxCategoryID()
        {
            int maxCategoryID = 0;

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT MAX(category_id) FROM \"category\"", connection);

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                maxCategoryID = (int)reader2.GetValue(0);
            }

            reader2.Close();

            return maxCategoryID;
        }

        public static void updateCategory(Category category)
        {
            NpgsqlConnection connection = connectDB();

            string categoryStr = $"UPDATE public.category SET name = @name, modify_at = NOW() WHERE category_id =@id ";

            NpgsqlCommand query = new NpgsqlCommand(categoryStr, connection);
            query.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar, 128).Value = category.name;
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = category.category_id;

            query.ExecuteNonQuery();
        }

        public static void deleteCategory(int categoryID) 
        { 
            NpgsqlConnection connection = connectDB();

            string categoryStr = $"DELETE FROM public.category WHERE category_id =@id ";

            NpgsqlCommand query = new NpgsqlCommand(categoryStr, connection);
            query.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = categoryID;

            query.ExecuteNonQuery();
        }
    }
}
