using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
    }
}
