using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MyShop.DAO.connectDatabaseDAO;
using static MyShop.Classes.Discount;
using System.Collections;

namespace MyShop.DAO
{
    public class discountDAO : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public static List<MyShop.Classes.Discount> GetDiscountList()
        {
            NpgsqlConnection connection = connectDB();
            var queryStr = "SELECT * from discount";

            var dataTable = getDataTable(connection, queryStr);

            var discountList = new List<MyShop.Classes.Discount>();

            discountList = (from DataRow dr in dataTable.Rows
                                select new MyShop.Classes.Discount()
                                {
                                    discount_id = (int)dr["discount_id"],
                                    product_id = (int)dr["product_id"],
                                    percent = (int)dr["percent"],
                                    create_at = DateTime.Parse(dr["create_at"].ToString()),
                                    modify_at = DateTime.Parse(dr["modify_at"].ToString())
                                }).ToList();

            return discountList;
        }

        public static List<int> getListDiscountID(int productID)
        {
            List<int> listDiscountID = new List<int>();

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT discount_id FROM \"discount\" where product_id = @productID order by discount_id ASC", connection);
            query2.Parameters.Add("@productID", NpgsqlTypes.NpgsqlDbType.Integer).Value = productID;

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                listDiscountID.Add((int)reader2.GetValue(0));
            }

            reader2.Close();

            return listDiscountID;
        }
    }
}
