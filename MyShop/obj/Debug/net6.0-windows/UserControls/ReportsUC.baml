   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MyShop.UserControlsMyShop.UserControls  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      #    MyShop.UserControls.ReportsUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MyShop.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5      -   5        .��5      � $��12q�6   .+�5   
   ��   �� Orientation$  Vertical=�6   $	��720��6,   .+�5      � $��Center=�6   $��0 0 0 0q�60   $	��900��6B   $	��400��6N    � RowDefinitions 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $��* �6'   .+�5      �� $��0��6   $��0��6*   $	��200��6:   $  Vertical=�6F   $
��Left=�6]   .+�5      �� $��Reports��6    $
��30px(�6/   $
��Bold"�6?   �� 5      ���#79747E$
��16px(�6O   ��6    �� *
username+6O   5      �� 5       �� CornerRadius$ 15��6   
�������$��0 12 0 0q�6>   $��1��6P   $��2��6]   ._�5      �� $	��320��6!   .+�5      �� $��14 12 14 10q�6$   $
��Wrap=�69   $��18(�6M   $<��6Biểu đồ doanh thu của từng loại mặt hàng��5    $   $&�� Segoe UI Variable Small Semibold)�6T   $
��Bold"�6�   �� 5!      $  
Horizontal=�6%   .+�5"      �� $��Tìm kiếm theo ngày��6(   $��15 0 0 0q�6B   $��Center=�6T   (  "System.Windows.Controls.DatePicker  5#      -   5        $��RevenueFromDayTextBox��5#   )   $��50 0 0 0q�6F   $	��100��6X   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 From��6d     5$      -   5        $��RevenueToDayTextBox��5$   )   $��50 0 0 0q�6D   $	��100��6V   $ To��6b   �� 5%      -   5        $��searchDayRevenueButton��5%   %   $��Search��6C   $��50 0 0 0q�6T   $��25��6f   6r   5&      �� 5'      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�5(      �� $��Tìm kiếm theo tuần��6(   $��15 0 0 0q�6B   $��Center=�6T   �� 5)      -   5        $��RevenueWeekTextBox��5)   &   $��50 0 0 0q�6@   $	��100��6R   $
 Week��6^   $��Center=�6�   �� 5*      -   5        $��RevenueYearWeekTextBox��5*   &   $��50 0 0 0q�6D   $	��100��6V   $
 Year��6b   $��Center=�6�   �� 5+      -   5        $��searchWeekRevenueButton��5+   %   $��Search��6k   $��50 0 0 0q�6|   $��Center=�6�   
���  ��$��25��6�   5,      �� 5-      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�5.      �� $��Tìm kiếm theo tháng��6(   $��15 0 0 0q�6C   $��Center=�6U   �� 5/      -   5        $��RevenueMonthTextBox��5/   &   $��44 0 0 0q�6A   $	��100��6S   $ Month��6_   $��Center=�6�   �� 50      -	   5        $��RevenueYearMonthTextBox��50   &   $��50 0 0 0q�6E   $	��100��6W   $
 Year��6c   $��Center=�6�   �� 51      -
   5        $��searchMonthRevenueButton��51   %   $��Search��6l   $��50 0 0 0q�6}   $��Center=�6�   
��� ���$��25��6�   52      �� 53      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�54      �� $��Tìm kiếm theo năm��6(   $��15 0 0 0q�6A   $��Center=�6S   �� 55      -   5        $��RevenueYearTextBox��55   &   $��50 0 0 0q�6@   $	��100��6R   $
 Year��6^   $��Center=�6�   �� 56      -   5        $��searchYearRevenueButton��56   %   $��Search��6k   $��	200 0 0 0q�6|   $��Center=�6�   
���   �$��25��6�   57      #  LiveCharts.Wpf.CartesianChart  58      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�6X   $��5 5 5 5q�6p   $	��154��6�      Series 6-   �� *RevenueSeriesCollection+   AxisX 5:   "     LiveCharts.Wpf.Axis     Title$ Category��6+   	   Labels	 6<   �� *RevenueLabels+6_   
   AxisY
 5=   "     $ Price��6+      LabelFormatter 69   �� *	Formatter+6`   5?      5@      5A      �� 5B      $ 15��6   
�������$��2��6>   $��2��6K   $��0 10 0 0q�6_   ._�5C      �� $	��320��6!   .+�5D      �� $��14 12 14 10q�6$   $
��Wrap=�69   $��18(�6M   $H��BBiểu đồ số lượng bán ra của từng loại mặt hàng��5E      $&�� Segoe UI Variable Small Semibold)�6J   $
��Bold"�6x   �� 5F      $  
Horizontal=�6%   .+�5G      �� $��Tìm kiếm theo ngày��6(   $��15 0 0 0q�6B   $��Center=�6T     5H      -   5        $��QuantityFromDayTextBox��5H   )   $��50 0 0 0q�6G   $	��100��6Y   $
 From��6e     5I      -   5        $��QuantityToDayTextBox��5I   )   $��50 0 0 0q�6E   $	��100��6W   $ To��6c   �� 5J      -   5        $��searchDayQuantityButton��5J   %   $��Search��6D   $��50 0 0 0q�6U   $��25��6g   6s   5K      �� 5L      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�5M      �� $��Tìm kiếm theo tuần��6(   $��15 0 0 0q�6B   $��Center=�6T   �� 5N      -   5        $��QuantityWeekTextBox��5N   &   $��50 0 0 0q�6A   $	��100��6S   $
 Week��6_   $��Center=�6�   �� 5O      -   5        $��QuantityYearWeekTextBox��5O   &   $��50 0 0 0q�6E   $	��100��6W   $
 Year��6c   $��Center=�6�   �� 5P      -   5        $��searchWeekQuantityButton��5P   %   $��Search��6l   $��50 0 0 0q�6}   $��Center=�6�   
���  ��$��25��6�   5Q      �� 5R      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�5S      �� $��Tìm kiếm theo tháng��6(   $��15 0 0 0q�6C   $��Center=�6U   �� 5T      -   5        $��QuantityMonthTextBox��5T   &   $��44 0 0 0q�6B   $	��100��6T   $ Month��6`   $��Center=�6�   �� 5U      -   5        $��QuantityYearMonthTextBox��5U   &   $��50 0 0 0q�6F   $	��100��6X   $
 Year��6d   $��Center=�6�   �� 5V      -   5        $��searchMonthQunatityButton��5V   %   $��Search��6n   $��50 0 0 0q�6   $��Center=�6�   
��� ���$��25��6�   5W      �� 5X      $  
Horizontal=�6%   $��0 5 0 0q�6>   .+�5Y      �� $��Tìm kiếm theo năm��6(   $��15 0 0 0q�6A   $��Center=�6S   �� 5Z      -   5        $��QuantityYearTextBox��5Z   &   $��50 0 0 0q�6A   $	��100��6S   $
 Year��6_   $��Center=�6�   �� 5[      -   5        $��searchYearQunatityButton��5[   %   $��Search��6m   $��	200 0 0 0q�6~   $��Center=�6�   
���   �$��25��6�   5\        5]      $ Bottom=�6V   $��5 5 5 5q�6n   $	��154��6    6-   �� *CountSeriesCollection+ 5_   "     $ Product��6+   	 6;   �� *CountLabels+6\   
 5b   "     $ Ouantity��6+    6<   �� *CountFormatter+6h   5d      5e      5f      5g      5h      5i      5j      