using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class Order : INotifyPropertyChanged
    {
        public int order_id { get; set; }

		public int customer_id { get; set; }
	
		public int price { get; set; }
	
		public string deliver_address { get; set; }

		public string status { get; set; }
	
		public DateTime order_date { get; set; }

		public DateTime modify_at { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

	public class OrderProduct : INotifyPropertyChanged
	{
        public int order_id { get; set; }

        public int customer_id { get; set; }

        public string customer_name { get; set; }

        public int price { get; set; }

        public string deliver_address { get; set; }

        public string status { get; set; }

        public DateTime order_date { get; set; }

        public DateTime modify_at { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class DetailOrderProduct : INotifyPropertyChanged
    {
        public int order_id { get; set; }

        public int customer_id { get; set; }

        public string name { get; set; }

        public int quantity { get; set; }

        public int discount_id { get; set; }

        public int after_price { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
