using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
	public class Product : INotifyPropertyChanged
	{
		public int product_id { get; set; }

		public string name { get; set; }

		public int inventory_number { get; set; }

		public int import_price { get; set; }

		public int price { get; set; }

		public string image { get; set; }

		public string detail { get; set; }

		public string manufacture { get; set; }

		public string status { get; set; }

		public DateTime create_at { get; set; }

		public DateTime modify_at { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;
	}

	public class ProductSpeedStats : INotifyPropertyChanged
	{
		public int category_id { get; set; }

		public string name { get; set; }

		public long in_num_cat { get; set; }		

        public event PropertyChangedEventHandler? PropertyChanged;
    }

	public class ProductTopLimit : INotifyPropertyChanged
	{
		public int product_id { get; set; }

		public string name { get; set; }

		public int inventory_number { get; set; }

		public int import_price { get; set; }

		public int price { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
