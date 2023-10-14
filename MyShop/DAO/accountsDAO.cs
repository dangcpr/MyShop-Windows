using MyShop.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using static MyShop.Classes.Accounts;
using static MyShop.DAO.connectDatabaseDAO;

namespace MyShop.DAO
{
    class accountsDAO: INotifyPropertyChanged
    {
        public static Accounts userAccount { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<bool> checkLoginAccount(string username, string password)
        {
            try
            {
                NpgsqlConnection connection = connectDB();

                var dataTable = getDataTable(connection, "select * from \"account\"");

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
                        userAccount = new Accounts();
                        userAccount.username = username;
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                MessageBox.Show("Something wrong :(", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
