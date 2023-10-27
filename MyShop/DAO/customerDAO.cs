using Microsoft.Office.Interop.Excel;
using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static MyShop.Classes.Customer;
using static MyShop.DAO.connectDatabaseDAO;

namespace MyShop.DAO
{
    public class customerDAO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static bool createNewCustomer(string name, string address, string phone)
        {
            try
            {
                string queryStr = $"INSERT INTO public.customer(\r\n\t\"name\", \"address\", \"phone\")\r" +
                    $"\n\tVALUES ('{name}', '{address}', '{phone}');";

                ExecutePSQLQuery(queryStr);

                return true;
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.ToString());
                return false;
            }
        }

        public static MyShop.Classes.Customer getCustomerById(int customer_id)
        {
            NpgsqlConnection connection = connectDB();

            var targetCustomer = new MyShop.Classes.Customer();

            var dataTable = getDataTable(connection, $"select * from customer where customer_id = {customer_id}");

            var customerList = new List<MyShop.Classes.Customer>();

            customerList = (from DataRow dr in dataTable.Rows
                           select new MyShop.Classes.Customer()
                           {
                               customer_id = (int)dr["customer_id"],
                               name = dr["name"].ToString(),
                               phone = dr["phone"].ToString(),
                               address = dr["address"].ToString(),
                               create_at = DateTime.Parse(dr["create_at"].ToString()),
                               modify_at = DateTime.Parse(dr["modify_at"].ToString())
                           }).ToList();

            foreach (var customer in customerList)
            {
                if (customer.customer_id == customer_id)
                {
                    targetCustomer = customer; break;
                }
            }

            return targetCustomer;
        }

        public static bool deleteCustomer(int customer_id)
        {
            try
            {
                string queryStr = $"DELETE FROM public.customer\r\n\tWHERE \"customer_id\"={customer_id};";
                ExecutePSQLQuery(queryStr);

                return true;
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.ToString());
                return false;
            }
        }

        public static bool editCustomer(int customer_id, string name, string address, string phone)
        {
            try
            {
                string queryStr = $"UPDATE public.customer\r\n\tSET \"name\"='{name}', \"address\"='{address}', \"phone\"='{phone}', modify_at=NOW()\r\n\tWHERE \"customer_id\"={customer_id};";
                ExecutePSQLQuery(queryStr);

                return true;
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.ToString());
                return false;
            }
        }
    }
}
