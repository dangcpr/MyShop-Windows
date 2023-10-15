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

        private void handleDashboardUCSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Mobile view
            if (e.PreviousSize != new Size() && e.PreviousSize.Width < 864)
            {
            }
            else
            {
            }
        }

        private void PieChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBoxUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
