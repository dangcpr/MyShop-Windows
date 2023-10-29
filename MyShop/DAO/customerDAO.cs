using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyShop.DAO.connectDatabaseDAO;


namespace MyShop.DAO
{
    public static class customerDAO
    {
        public static List<int> getListCustomerID()
        {
            List<int> listCustomerID = new List<int>();

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT customer_id FROM \"customer\" order by customer_id ASC", connection);

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                listCustomerID.Add((int)reader2.GetValue(0));
            }

            reader2.Close();

            return listCustomerID;
        }
    }
}
