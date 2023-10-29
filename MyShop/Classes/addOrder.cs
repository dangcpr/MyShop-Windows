using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class addOrder : INotifyPropertyChanged
    {
        public int? productID { get; set; }
        public int? quantity { get; set; }
        public int? discountID { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
