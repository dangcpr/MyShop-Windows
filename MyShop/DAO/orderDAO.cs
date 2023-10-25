using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using static MyShop.DAO.connectDatabaseDAO;


namespace MyShop.DAO
{
    public class orderDAO : INotifyPropertyChanged
    {
        public static List<MyShop.Classes.Order> _monthOrderList;

        public static List<MyShop.Classes.Order> _weekOrderList;

        public static MyShop.Classes.Order _order;

        public event PropertyChangedEventHandler? PropertyChanged;


        public static List<Order> getOrderList(string type)
        {
            NpgsqlConnection connection = connectDB();
            var queryStr = "";

            if (type == "month")
            {
                queryStr = "SELECT * FROM \"order\" WHERE EXTRACT('MONTH' FROM \"order_date\") = EXTRACT('MONTH' FROM NOW())";
            } 
            else if (type == "preMonth")
            {
                queryStr = "SELECT * FROM \"order\" WHERE EXTRACT('MONTH' FROM \"order_date\") = EXTRACT('MONTH' FROM NOW()) - 1";
            }
            else if (type == "week")
            {
                queryStr = "SELECT * FROM \"order\" WHERE EXTRACT('WEEK' FROM \"order_date\") = EXTRACT('WEEK' FROM NOW())";
            }
            else if (type == "preWeek")
            {
                queryStr = "SELECT * FROM \"order\" WHERE EXTRACT('WEEK' FROM \"order_date\") = EXTRACT('WEEK' FROM NOW()) - 1";
            }

            var dataTable = getDataTable(connection, queryStr);

            var monthOrderList = new List<MyShop.Classes.Order>();

            monthOrderList = (from DataRow dr in dataTable.Rows
                              select new MyShop.Classes.Order()
                              {
                                  order_id = (int)dr["order_id"],
                                  customer_id = (int)dr["customer_id"],
                                  price = (int)dr["price"],
                                  deliver_address = dr["deliver_address"].ToString(),
                                  status = dr["status"].ToString(),
                                  order_date = DateTime.Parse(dr["order_date"].ToString()),
                                  modify_at = DateTime.Parse(dr["modify_at"].ToString())
                              }).ToList();

            return monthOrderList;
        }

        public static List<MyShop.Classes.OrderProduct> getOrderProductList()
        {
            NpgsqlConnection connection = connectDB();
            var queryStr = "SELECT o.order_id, o.customer_id, c.name as customer_name, o.price, o.deliver_address, o.status, o.order_date, o.modify_at\r\n\tFROM public.\"order\" o JOIN \"customer\" c on o.customer_id = c.customer_id;";

            var dataTable = getDataTable(connection, queryStr);

            var orderProductList = new List<MyShop.Classes.OrderProduct>();

            orderProductList = (from DataRow dr in dataTable.Rows
                              select new MyShop.Classes.OrderProduct()
                              {
                                  order_id = (int)dr["order_id"],
                                  customer_id = (int)dr["customer_id"],
                                  customer_name = dr["customer_name"].ToString(),
                                  price = (int)dr["price"],
                                  deliver_address = dr["deliver_address"].ToString(),
                                  status = dr["status"].ToString(),
                                  order_date = DateTime.Parse(dr["order_date"].ToString()),
                                  modify_at = DateTime.Parse(dr["modify_at"].ToString())
                              }).ToList();

            return orderProductList;
        }
    }
}
