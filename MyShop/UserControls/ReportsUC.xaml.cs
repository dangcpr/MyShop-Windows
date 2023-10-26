using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MyShop.UserControls
{
    /// <summary>
    /// Interaction logic for ReportsUC.xaml
    /// </summary>
    public partial class ReportsUC : UserControl
    {
        List<String> listNameCategory = new List<String>();
        List<String> listNameProduct = new List<String>();
        MyShop.BUS.productBUS productBUS = new MyShop.BUS.productBUS();
        MyShop.DAO.productDAO productDAO = new MyShop.DAO.productDAO();

        List<long> listRevenue = new List<long>();
        ChartValues<long> listRevenueChart = new ChartValues<long>();

        List<long> listProfit = new List<long>();
        ChartValues<long> listProfitChart = new ChartValues<long>();

        List<long> listQuantityProduct = new List<long>();
        ChartValues<long> listQuantityProductChart = new ChartValues<long>();

        public ReportsUC()
        {
            InitializeComponent();
        }

        public SeriesCollection RevenueSeriesCollection { get; set; }
        public SeriesCollection CountSeriesCollection { get; set; }

        public List<String> RevenueLabels { get; set; }
        public List<String> CountLabels { get; set; }

        public Func<double, string> Formatter { get; set; }
        public Func<double, string> CountFormatter { get; set; }

        private void UserControlReport_Loaded(object sender, RoutedEventArgs e)
        {
            listRevenueChart.Clear();
            listProfitChart.Clear();
            listQuantityProductChart.Clear();

            if (MyShop.BUS.categoryBUS.checkCategoryBUS() == true)
            {
                listNameCategory = MyShop.DAO.categoryDAO.listNameCategories();

                listRevenue = MyShop.DAO.categoryDAO.revenueCategories();
                listProfit = MyShop.DAO.categoryDAO.profitCategories();
            }

            for (int i = 0; i < listRevenue.Count; i++)
            {
                listRevenueChart.Add(listRevenue[i]);
            }

            for (int i = 0; i < listProfit.Count; i++)
            {
                listProfitChart.Add(listProfit[i]);
            }

            RevenueSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = listRevenueChart,
                }
            };

            //adding series will update and animate the chart automatically
            RevenueSeriesCollection.Add(new ColumnSeries
            {
                Title = "Profit",
                Values = listProfitChart,
            });

            RevenueLabels = listNameCategory;

            Formatter = value => value.ToString("N");

            /*-------------------QUANTITY PRODUCT --------------------------*/
            if (productBUS.checkProductInSale() == true)
            {
                listNameProduct = productDAO.getListNameProduct();
                listRevenue = productDAO.getListQuantityProduct();
            }

            
            for (int i = 0; i < listRevenue.Count; i++)
            {
                listQuantityProductChart.Add(listRevenue[i]);
            }


            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quantity",
                    Values = listQuantityProductChart,
                }
            };

            CountLabels = listNameProduct;

            DataContext = this;
        }

        private void searchDayRevenueButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyShop.BUS.categoryBUS.checkCategoryBUS() == true)
            {
                listRevenue = MyShop.DAO.categoryDAO.revenueDayCategories(DateTime.Parse(RevenueFromDayTextBox.Text), DateTime.Parse(RevenueToDayTextBox.Text));
                listProfit = MyShop.DAO.categoryDAO.profitDayCategories(DateTime.Parse(RevenueFromDayTextBox.Text), DateTime.Parse(RevenueToDayTextBox.Text));
            }

            listRevenueChart.Clear();
            listProfitChart.Clear();

            for (int i = 0; i < listRevenue.Count; i++)
            {
                listRevenueChart.Add(listRevenue[i]);
            }

            for (int i = 0; i < listProfit.Count; i++)
            {
                listProfitChart.Add(listProfit[i]);
            }

            RevenueSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = listRevenueChart,
                }
            };

            //adding series will update and animate the chart automatically
            RevenueSeriesCollection.Add(new ColumnSeries
            {
                Title = "Profit",
                Values = listProfitChart,
            });

            //DataContext = this;

        }

        private void searchWeekRevenueButton_Click(object sender, RoutedEventArgs e)
        {
            int week, year;

            bool weekIsNum = int.TryParse(RevenueWeekTextBox.Text, out week);
            bool yearIsNum = int.TryParse(RevenueYearWeekTextBox.Text, out year);

            if (!weekIsNum || !yearIsNum)
            {
                MessageBox.Show("Vui lòng nhập tuần và năm là số nguyên");
                return;
            }

            if (MyShop.BUS.categoryBUS.checkCategoryBUS() == true)
            {
                listRevenue = MyShop.DAO.categoryDAO.revenueWeekCategories(week, year);
                listProfit = MyShop.DAO.categoryDAO.profitWeekCategories(week, year);
            }

            listRevenueChart.Clear();
            listProfitChart.Clear();

            for (int i = 0; i < listRevenue.Count; i++)
            {
                listRevenueChart.Add(listRevenue[i]);
            }

            for (int i = 0; i < listProfit.Count; i++)
            {
                listProfitChart.Add(listProfit[i]);
            }

            RevenueSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = listRevenueChart,
                }
            };

            //adding series will update and animate the chart automatically
            RevenueSeriesCollection.Add(new ColumnSeries
            {
                Title = "Profit",
                Values = listProfitChart,
            });

            //DataContext = this;
        }

        private void searchMonthRevenueButton_Click(object sender, RoutedEventArgs e)
        {
            int month, year;

            bool monthIsNum = int.TryParse(RevenueMonthTextBox.Text, out month);
            bool yearIsNum = int.TryParse(RevenueYearMonthTextBox.Text, out year);

            if (!monthIsNum || !yearIsNum)
            {
                MessageBox.Show("Vui lòng nhập tháng và năm là số nguyên");
                return;
            }

            if (MyShop.BUS.categoryBUS.checkCategoryBUS() == true)
            {
                listRevenue = MyShop.DAO.categoryDAO.revenueMonthCategories(month, year);
                listProfit = MyShop.DAO.categoryDAO.profitMonthCategories(month, year);
            }

            listRevenueChart.Clear();
            listProfitChart.Clear();

            for (int i = 0; i < listRevenue.Count; i++)
            {
                listRevenueChart.Add(listRevenue[i]);
            }

            for (int i = 0; i < listProfit.Count; i++)
            {
                listProfitChart.Add(listProfit[i]);
            }

            RevenueSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = listRevenueChart,
                }
            };

            //adding series will update and animate the chart automatically
            RevenueSeriesCollection.Add(new ColumnSeries
            {
                Title = "Profit",
                Values = listProfitChart,
            });

            //DataContext = this;
        }

        private void searchYearRevenueButton_Click(object sender, RoutedEventArgs e)
        {
            int year;

            bool yearIsNum = int.TryParse(RevenueYearTextBox.Text, out year);

            if (!yearIsNum)
            {
                MessageBox.Show("Vui lòng năm là số nguyên");
                return;
            }

            if (MyShop.BUS.categoryBUS.checkCategoryBUS() == true)
            {
                listRevenue = MyShop.DAO.categoryDAO.revenueYearCategories(year);
                listProfit = MyShop.DAO.categoryDAO.profitYearCategories(year);
            }

            listRevenueChart.Clear();
            listProfitChart.Clear();

            for (int i = 0; i < listRevenue.Count; i++)
            {
                listRevenueChart.Add(listRevenue[i]);
            }

            for (int i = 0; i < listProfit.Count; i++)
            {
                listProfitChart.Add(listProfit[i]);
            }

            RevenueSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = listRevenueChart,
                }
            };

            //adding series will update and animate the chart automatically
            RevenueSeriesCollection.Add(new ColumnSeries
            {
                Title = "Profit",
                Values = listProfitChart,
            });
        }
        private void searchDayQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            if (productBUS.checkProductInSale() == true)
            {
                listQuantityProduct = productDAO.getListQuantityDayProduct(DateTime.Parse(QuantityFromDayTextBox.Text), DateTime.Parse(QuantityToDayTextBox.Text));
            }

            listQuantityProductChart.Clear();

            for (int i = 0; i < listQuantityProduct.Count; i++)
            {
                listQuantityProductChart.Add(listQuantityProduct[i]);
            }

            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quantity",
                    Values = listQuantityProductChart,
                }
            };
        }

        private void searchWeekQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            int week, year;

            bool weekIsNum = int.TryParse(QuantityWeekTextBox.Text, out week);
            bool yearIsNum = int.TryParse(QuantityYearWeekTextBox.Text, out year);

            if (!weekIsNum || !yearIsNum)
            {
                MessageBox.Show("Vui lòng nhập tuần và năm là số nguyên");
                return;
            }


            if (productBUS.checkProductInSale() == true)
            {
                listQuantityProduct = productDAO.getListQuantityWeekProduct(week, year);
            }

            listQuantityProductChart.Clear();

            for (int i = 0; i < listQuantityProduct.Count; i++)
            {
                listQuantityProductChart.Add(listQuantityProduct[i]);
            }

            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quantity",
                    Values = listQuantityProductChart,
                }
            };
        }

        private void searchMonthQunatityButton_Click(object sender, RoutedEventArgs e)
        {
            int month, year;

            bool monthIsNum = int.TryParse(QuantityMonthTextBox.Text, out month);
            bool yearIsNum = int.TryParse(QuantityYearMonthTextBox.Text, out year);

            if (!monthIsNum || !yearIsNum)
            {
                MessageBox.Show("Vui lòng nhập tháng và năm là số nguyên");
                return;
            }


            if (productBUS.checkProductInSale() == true)
            {
                listQuantityProduct = productDAO.getListQuantityMonthProduct(month, year);
            }

            listQuantityProductChart.Clear();

            for (int i = 0; i < listQuantityProduct.Count; i++)
            {
                listQuantityProductChart.Add(listQuantityProduct[i]);
            }

            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quantity",
                    Values = listQuantityProductChart,
                }
            };
        }

        private void searchYearQunatityButton_Click(object sender, RoutedEventArgs e)
        {
            int year;

            bool yearIsNum = int.TryParse(QuantityYearTextBox.Text, out year);

            if (!yearIsNum)
            {
                MessageBox.Show("Vui lòng nhập năm là số nguyên");
                return;
            }


            if (productBUS.checkProductInSale() == true)
            {
                listQuantityProduct = productDAO.getListQuantityYearProduct(year);
            }

            listQuantityProductChart.Clear();

            for (int i = 0; i < listQuantityProduct.Count; i++)
            {
                listQuantityProductChart.Add(listQuantityProduct[i]);
            }

            CountSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Quantity",
                    Values = listQuantityProductChart,
                }
            };
        }
    }
}
