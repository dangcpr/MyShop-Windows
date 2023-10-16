using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

using static MyShop.Classes.MyModel;

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for DashboardUC.xaml
    /// </summary>
    public partial class DashboardUC : UserControl
    {
        public DashboardUC()
        {
            InitializeComponent();
        }

        List<MyShop.Classes.ProductSpeedStats> _speedStatsTable;

        MyShop.Classes.MyModel _myModel = new MyShop.Classes.MyModel();

        private void handleDashboardUCLoaded(object sender, RoutedEventArgs e)
        {
            _speedStatsTable = MyShop.DAO.productDAO.getSpeedStats();

            string[] colorList = { "Hotpink", "Turquoise", "Gold" };
            var colorIndex = 0;

            myPieChart.Series.Clear();

            foreach (var speed in _speedStatsTable)
            {
                SolidColorBrush color = (SolidColorBrush)new BrushConverter()
                    .ConvertFromString(colorList[colorIndex]);

                myPieChart.Series.Add(new PieSeries
                {
                    Title = speed.name.ToString(),
                    Fill = color,
                    StrokeThickness = 0,
                    Values = new ChartValues<double> { Convert.ToDouble(speed.in_num_cat) }
                });

                colorIndex++;
            }

            colorIndex = 0;

            _myModel.productTopLimit = MyShop.DAO.productDAO.getTopProductLimit(5, 5);

            productTopLimitDatagrid.ItemsSource = _myModel.productTopLimit;
        }

        private void SearchBoxUC_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("===> SearchBoxUC_Loaded Check");
        }
    }
}
