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
    }
}
