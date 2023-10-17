using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Net.Http;
using System.Data;

using static MyShop.Classes.Product;
using static MyShop.Classes.Category;
using static MyShop.Classes.CategoryProduct;


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

        public string image_change_order_week { get; set; }

        //===== % Product Types Sold Implement
        public List<MyShop.Classes.ProductSpeedStats> productTypeSold { get; set; }

        //===== SpeedSale Implement
        public ChartValues<double> speedStats { get; set; }

        public long productInventorySum { get; set; }

        public List<MyShop.Classes.ProductTopLimit> productTopLimit { get; set; }

        public static HttpClient client = new HttpClient();

        public static List<MyShop.Classes.Product> excelProductList { get; set; }

        public static List<MyShop.Classes.Category> excelCategoryList { get; set; }

        public static List<MyShop.Classes.CategoryProduct> excelCategoryProductList { get; set; }

        //===== PropertyChangedEventHandler
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
