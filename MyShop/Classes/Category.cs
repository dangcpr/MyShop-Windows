using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    public class Category : INotifyPropertyChanged
    {
        public int category_id { get; set; }

        public string name { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
