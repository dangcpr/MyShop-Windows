using MyShop.BUS;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MyShop.BUS.connectDatabaseBUS;
using static MyShop.Classes.Accounts;

// https://michaelscodingspot.com/postgres-in-csharp/
// https://stackoverflow.com/questions/43084599/fill-a-listview-from-postgres-database-in-wpf-c-sharp
// https://support.syncfusion.com/kb/article/11107/how-to-perform-the-crud-operations-in-wpf-scheduler-calendar-using-postgresql-database
// https://github.com/SyncfusionExamples/crud-operations-in-wpf-scheduler-using-postgresql/blob/main/SchedulerWPF/Helper/ConnectPSQL.cs

namespace MyShop.DAO
{
    class connectDatabaseDAO
    {
        public static string cntStr = @"Server=localhost;Port=5432;User id=postgres;Password=123;Database=MyShop";

        public static NpgsqlConnection iConnect { get; set; }

        public static NpgsqlConnection connectDB()
        {
            NpgsqlConnection connection = new NpgsqlConnection(cntStr);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public static DataTable getDataTable(NpgsqlConnection connection, string PSQL_Text)
        {
            //NpgsqlConnection connection = connectDB();

            DataTable table = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(PSQL_Text, connection);
            adapter.Fill(table);

            return table;
        }

        public static void ExecutePSQLQuery(string PSQLQuery)
        {
            NpgsqlCommand cmd_Command = new NpgsqlCommand(PSQLQuery, connectDB());
            cmd_Command.ExecuteNonQuery();
        }

        public static void CloseDBConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(cntStr);
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }      
    }
}
