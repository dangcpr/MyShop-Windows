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

using BCrypt.Net;

using static MyShop.Classes.Accounts;
using static MyShop.DAO.connectDatabaseDAO;
using System.Diagnostics;

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

        public static bool checkExistAccount(string username)
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
                               }).ToList();

                foreach (var account in accountList)
                {
                    if (account.username == username) return true;
                }

                return false;
            }
            catch
            {
                MessageBox.Show("Something wrong :(", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return false;
        }

        public static bool createAccount(string username, string password, string name)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                //bool isPasswordMatch = BCrypt.Net.BCrypt.Verify("123", hashedPassword);

                string queryStr = $"INSERT INTO account(username, password, fullname, role,avatar)\r\n\tVALUES ('{username}', '{hashedPassword}', '{name}', 'admin', null);";

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
