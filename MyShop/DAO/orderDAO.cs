using Microsoft.VisualBasic;
using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        public static void addOrderProduct(string order_id, string customer_id, string deliver_address
            , string discount_id, string product_id, string quantity)
        {
            NpgsqlConnection connection = connectDB();

            var queryStr = $"INSERT INTO public.\"order\"(\r\n\torder_id, customer_id, deliver_address)\r" +
                $"\n\tVALUES ({order_id}, {customer_id}, {deliver_address});";

            NpgsqlCommand query = new NpgsqlCommand(queryStr, connection);

            var queryStr2 = $"INSERT INTO public.detail_order(\r" +
                $"\n\torder_id, product_id, quantity, discount_id)\r" +
                $"\n\tVALUES ({order_id}, product_id_add, quantity_add, discount_id_add);";

            NpgsqlCommand query2 = new NpgsqlCommand(queryStr2, connection);

            var queryStr3 = $"UPDATE detail_order \r\nSET after_price = " +
                $"(SELECT price FROM \"product\" WHERE product_id = {product_id}) \r" +
                $"\n\t* (1-COALESCE(cast((SELECT \"percent\" end FROM \"discount\" WHERE discount_id = {discount_id} AND product_id = {product_id} LIMIT 1) as decimal) / 100, 0)) \r" +
                $"\n\tWHERE order_id = {order_id} and product_id = {product_id};";

            NpgsqlCommand query3 = new NpgsqlCommand(queryStr3, connection);

            var queryStr4 = $"UPDATE product SET inventory_number = inventory_number - quantity_add WHERE product_id = {product_id};";

            NpgsqlCommand query4 = new NpgsqlCommand(queryStr4, connection);

            var queryStr5 = $"UPDATE public.\"order\" SET price = (SELECT SUM(quantity * after_price) FROM \"detail_order\" WHERE order_id = {order_id}) WHERE order_id = {order_id};";

            NpgsqlCommand query5 = new NpgsqlCommand(queryStr5, connection);

            query.ExecuteNonQuery();
            query2.ExecuteNonQuery();
            query3.ExecuteNonQuery();
            query4.ExecuteNonQuery();
            query5.ExecuteNonQuery();
        }

        public static void deleteOrderProduct(string order_id)
        {
            NpgsqlConnection connection = connectDB();

            string categoryStr = $"DELETE FROM \"order\" WHERE order_id = {order_id};";

            NpgsqlCommand query = new NpgsqlCommand(categoryStr, connection);

            query.ExecuteNonQuery();
        }

        public static List<DetailOrderProduct> getDetailOrder(int order_id)
        {
            NpgsqlConnection connection = connectDB();

            var queryStr = $"SELECT deo.order_id, deo.product_id, p.name, deo.quantity, deo.discount_id, deo.after_price\r\n\tFROM public.detail_order deo JOIN \"product\" p ON deo.product_id = p.product_id WHERE deo.order_id = {order_id};";

            var dataTable = getDataTable(connection, queryStr);

            var detailOrderList = new List<MyShop.Classes.DetailOrderProduct>();

            detailOrderList = (from DataRow dr in dataTable.Rows
                                select new MyShop.Classes.DetailOrderProduct()
                                {
                                    order_id = (int)dr["order_id"],
                                    customer_id = (int)dr["customer_id"],
                                    name = dr["name"].ToString(),
                                    quantity = (int)dr["quantity"],
                                    after_price = (int)dr["after_price"],
                                }).ToList();

            return detailOrderList;
        }
    }
}
