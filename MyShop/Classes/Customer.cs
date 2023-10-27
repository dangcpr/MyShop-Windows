using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class Customer
    {
        public int customer_id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public DateTime create_at { get; set; }

        public DateTime modify_at { get; set; }
    }

    public class CustomerQuery
    {
        public int customer_id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public DateTime create_at { get; set; }

        public DateTime modify_at { get; set; }
    }
}
