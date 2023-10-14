using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class MyModel : INotifyPropertyChanged
    {
        public String username { get; set; }

        public int count_product { get; set; }

        public int count_order_week { get; set; }

        public int count_order_month { get; set; }

        public int count_change_order_month { get; set; }

        public int count_change_order_week { get; set; }

        public string image_change_order_month { get; set; }

        public string iamge_change_order_week { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
