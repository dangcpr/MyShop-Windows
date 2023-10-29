
﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyShop.DAO.connectDatabaseDAO;
using Microsoft.Office.Interop.Excel;
using MyShop.Classes;
using System.Data;
using System.Diagnostics;
using System.Net;
using static MyShop.Classes.Customer;


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
