using LiveChartsCore.Themes;
using MyShop.Classes;
using Npgsql;
using System;
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
