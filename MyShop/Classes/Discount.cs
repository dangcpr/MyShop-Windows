using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class Discount : INotifyPropertyChanged
    {
        public int discount_id { get; set; }

        public int product_id { get; set; }

        public int percent { get; set; }

        public int maximum { get; set; }

        public DateTime started { get; set; }

        public DateTime ended { get; set; }

        public DateTime create_at { get; set; }

        public DateTime modify_at { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
