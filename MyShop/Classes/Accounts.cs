using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Classes
{
    class Accounts : INotifyPropertyChanged
    {
        public string uid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string created { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
