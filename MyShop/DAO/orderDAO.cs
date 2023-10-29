using Microsoft.VisualBasic;
using MyShop.Classes;
using MyShop.GUI;
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

        public static void addOrderProduct(int order_id, int customer_id, string deliver_address
            , List<MyShop.Classes.addOrder> addOrderList)
        {
            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query = new NpgsqlCommand($"INSERT INTO public.\"order\"(\r\n\torder_id, customer_id, deliver_address)\r" +
                $"\n\tVALUES (@orderID, @customerID, @deliveryAddress);", connection);

            query.Parameters.Add("@orderID", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query.Parameters.Add("@customerID", NpgsqlTypes.NpgsqlDbType.Integer).Value = customer_id;
            query.Parameters.Add("@deliveryAddress", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = deliver_address;
            query.ExecuteNonQuery();

            foreach (var order in addOrderList)
            {
                NpgsqlCommand query2 = new NpgsqlCommand("INSERT INTO public.detail_order(\r\n\torder_id, product_id, quantity, discount_id)\r\n\t" +
                    "VALUES (@order_id_new, @product_id_add, @quantity_add, @discount_id_add);", connection);
                query2.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
                query2.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query2.Parameters.Add("@quantity_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.quantity;
                query2.Parameters.Add("@discount_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.discountID != null ? order.discountID : DBNull.Value;
                query2.ExecuteNonQuery();

                //Update price
                NpgsqlCommand query3 = new NpgsqlCommand("UPDATE detail_order \r\n" +
                    "SET after_price = (SELECT price FROM \"product\" WHERE product_id = @product_id_add) \r\n\t* (1-COALESCE(cast((SELECT \"percent\" end FROM \"discount\" WHERE discount_id = @discount_id_add AND product_id = @product_id_add LIMIT 1) as decimal) / 100, 0)) \r\n\tWHERE order_id = @order_id_new and product_id = @product_id_add; ", connection);
                query3.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query3.Parameters.Add("@discount_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.discountID != null ? order.discountID : DBNull.Value;
                query3.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
                query3.ExecuteNonQuery();

                NpgsqlCommand query4 = new NpgsqlCommand("UPDATE product SET inventory_number = inventory_number - @quantity_add WHERE product_id = @product_id_add ", connection);
                query4.Parameters.Add("@quantity_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.quantity;
                query4.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query4.ExecuteNonQuery();
            }

            NpgsqlCommand query5 = new NpgsqlCommand("UPDATE public.\"order\" SET price = (SELECT SUM(quantity * after_price) FROM \"detail_order\" WHERE order_id = @order_id_new) WHERE order_id = @order_id_new; ", connection);
            query5.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query5.ExecuteNonQuery();

            //query.ExecuteNonQuery();
            //query2.ExecuteNonQuery();
            //query3.ExecuteNonQuery();
            //query4.ExecuteNonQuery();
            //query5.ExecuteNonQuery();
        }

        public static void updateOrderProduct(int order_id, int customer_id, string deliver_address, string status, List<MyShop.Classes.addOrder> addOrderList)
        {
            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query7 = new NpgsqlCommand("UPDATE \"product\" pr SET inventory_number = inventory_number + (SELECT quantity FROM detail_order deo " +
                "WHERE pr.product_id = deo.product_id and order_id = @order_id_update) \r\n\tWHERE pr.product_id in (select product_id from detail_order deo where order_id = @order_id_update);", connection);
            query7.Parameters.Add("@order_id_update", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query7.ExecuteNonQuery();

            NpgsqlCommand query8 = new NpgsqlCommand("DELETE FROM \"detail_order\" WHERE order_id = @order_id_update;", connection);
            query8.Parameters.Add("@order_id_update", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query8.ExecuteNonQuery();

            foreach (var order in addOrderList)
            {
                NpgsqlCommand query2 = new NpgsqlCommand("INSERT INTO public.detail_order(\r\n\torder_id, product_id, quantity, discount_id)\r\n\t" +
                    "VALUES (@order_id_new, @product_id_add, @quantity_add, @discount_id_add);", connection);
                query2.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
                query2.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query2.Parameters.Add("@quantity_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.quantity;
                query2.Parameters.Add("@discount_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.discountID != null ? order.discountID : DBNull.Value;
                query2.ExecuteNonQuery();

                //Update price
                NpgsqlCommand query3 = new NpgsqlCommand("UPDATE detail_order \r\n" +
                    "SET after_price = (SELECT price FROM \"product\" WHERE product_id = @product_id_add) \r\n\t* (1-COALESCE(cast((SELECT \"percent\" end FROM \"discount\" WHERE discount_id = @discount_id_add AND product_id = @product_id_add LIMIT 1) as decimal) / 100, 0)) \r\n\tWHERE order_id = @order_id_new and product_id = @product_id_add; ", connection);
                query3.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query3.Parameters.Add("@discount_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.discountID != null ? order.discountID : DBNull.Value;
                query3.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
                query3.ExecuteNonQuery();

                NpgsqlCommand query4 = new NpgsqlCommand("UPDATE product SET inventory_number = inventory_number - @quantity_add WHERE product_id = @product_id_add ", connection);
                query4.Parameters.Add("@quantity_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.quantity;
                query4.Parameters.Add("@product_id_add", NpgsqlTypes.NpgsqlDbType.Integer).Value = order.productID;
                query4.ExecuteNonQuery();
            }

            NpgsqlCommand query9 = new NpgsqlCommand("UPDATE public.\"order\"\r\n\tSET deliver_address=@deliver_address_new, status=@status_new, modify_at=NOW() WHERE order_id = @order_id_update;", connection);
            query9.Parameters.Add("@deliver_address_new", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = deliver_address;
            query9.Parameters.Add("@status_new", NpgsqlTypes.NpgsqlDbType.Varchar, 512).Value = status;
            query9.Parameters.Add("@order_id_update", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query9.ExecuteNonQuery();


            NpgsqlCommand query5 = new NpgsqlCommand("UPDATE public.\"order\" SET price = (SELECT SUM(quantity * after_price) FROM \"detail_order\" WHERE order_id = @order_id_new) WHERE order_id = @order_id_new; ", connection);
            query5.Parameters.Add("@order_id_new", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query5.ExecuteNonQuery();

            //query.ExecuteNonQuery();
            //query2.ExecuteNonQuery();
            //query3.ExecuteNonQuery();
            //query4.ExecuteNonQuery();
            //query5.ExecuteNonQuery();
        }

        public static void deleteOrderProduct(int order_id)
        {
            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("UPDATE \"product\" pr SET inventory_number = inventory_number + (SELECT quantity FROM detail_order deo WHERE pr.product_id = deo.product_id and order_id = @order_id_update) \r\n\tWHERE pr.product_id in (select product_id from detail_order deo where order_id = @order_id_update);\r\n", connection);
            query2.Parameters.Add("@order_id_update", NpgsqlTypes.NpgsqlDbType.Integer).Value = order_id;
            query2.ExecuteNonQuery();

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

        public static int getMaxOrderID()
        {
            int maxProductID = 0;

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT MAX(order_id) FROM \"order\"", connection);

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                maxProductID = (int)reader2.GetValue(0);
            }

            reader2.Close();

            return maxProductID;
        }

        public static List<addOrder> getDetailOrderShort(int orderID)
        {
            List<addOrder> listDetail = new List<addOrder>();

            NpgsqlConnection connection = connectDB();

            NpgsqlCommand query2 = new NpgsqlCommand("SELECT * FROM \"detail_order\" where order_id = @order_id", connection);
            query2.Parameters.Add("@order_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = orderID;

            var reader2 = query2.ExecuteReader();
            while (reader2.Read())
            {
                listDetail.Add(new addOrder
                {
                    productID = (int)reader2.GetValue(1),
                    quantity = (int)reader2.GetValue(2),
                    discountID = reader2.GetValue(3) == DBNull.Value ? null : (int)reader2.GetValue(3),
                });
            }

            reader2.Close();

            return listDetail;
        }
    }
}
