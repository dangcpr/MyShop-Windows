using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using static MyShop.DAO.connectDatabaseDAO;

namespace MyShop.DAO
{
    class accountsDAO
    {
        public async Task<bool> checkLoginAccount(string username, string password)
        {
            try
            {
                NpgsqlConnection connection = connectDB();

                var dataTable = getDataTable(connection, "select * from accounts");

                var accountList = new List<MyShop.Classes.Accounts>();

                accountList = (from DataRow dr in dataTable.Rows
                               select new MyShop.Classes.Accounts()
                               {
                                   username = dr["username"].ToString(),
                                   password = dr["password"].ToString()
                               }).ToList();

                foreach (var account in accountList)
                {
                    if (account.username == username && account.password == password)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                MessageBox.Show("Something wrong :(");
                return false;
            }
        }
    }
}
