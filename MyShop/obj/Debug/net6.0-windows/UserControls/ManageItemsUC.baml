   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MyShop.UserControlsMyShop.UserControls  5      9!clr-namespace:MyShop.UserControlsMyShop.UserControls  5
      '    !MyShop.UserControls.ManageItemsUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MyShop.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      *uc!clr-namespace:MyShop.UserControls  5
      -   5        .��5      � .+�5   
   ��   �� Orientation$  Vertical=�6   $	��720��6,   .+�5      � $��Center=�6   $��0 20 0 0q�60   $	��900��6C   $	��400��6O    � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6*   $	��200��6:   $  Vertical=�6F   $
��Left=�6]   .+�5      �� $��Manage Items��6    $
��30px(�64   $
��Bold"�6D   �� 5      ���#79747E$
��20px(�6O   ��6    �� *
username+6O   5      �� 5       $��0��6   $��1��6*   $  
Horizontal=�6:   $��Right=�5!      $��Center=�69   .+�5"      �� -   5        $	��120��5"      $��Import Data��6)      MaterialDesignRaisedButton#��[  5$       � ToolTip$/ )Resource name: MaterialDesignRaisedButton��5%      {�5#      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window5%   Q   5&      5'      � 5)      $��Center=�6   $��	0 20 0 20q�60   $	��900��6D   $	��400��6P    5*      �� 5+       5-      �� 5.      $��* �6'   .+�51      ��  �� CornerRadius$ 15��6   $	��900��6+   
�������$��Center=�6J   $��0 5 0 0q�6g   ._�52      �� $��20 20 20 20q�6!   $  Vertical=�66   $��Center=�6M   .+�53      �� $  Vertical=�6%   .+�54      �� $  
Horizontal=�6)   .+�55   "   �� $
��Wrap=�6,   $��15(�6@   ���#79747E$��Manage Products��56   A   $&�� Segoe UI Variable Small Semibold)�57   ,   $
��Bold"�6Z   �� 58   "   -   5        $��SearchProductTextBox��5G   &   $��	40 0 0 15q�59   &   $	��230��5:   &   $��50��5;   &   $	��Top=�5<   &   
������� �� AcceptsReturn$
 True��5>   &   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search Something...��5?   &   .  (MaterialDesignThemes.Wpf.TextFieldAssist	   HasLeadingIcon$
	 True��5@   &   
   LeadingIcon$
 ChevronTripleDown=�5A   &      HasTrailingIcon$
 True��5B   &      TrailingIcon$ Search=�5C   &      
IsFloating$ False��5D   &    ! MaterialDesignOutlinedTextBox#��[ 5F   &   {�5E   &   ��  �� Path$ 	IsChecked� �� ElementName$2 ,MaterialDesignOutlinedTextBoxEnabledComboBox��5G   &   �� 5I   "   -   5        $��
10 0 50 15q�5I   )   $��Search��6=   
���� ��6t   �� 5J   "   -   5        $��	10 0 0 15q�5J   )   $	��Add��6<   6J   �� 5K   "   -   5        $��	10 0 0 15q�5K   )   $��Remove��6<   6M   �� 5L   "   -   5        $��	10 0 0 15q�5L   )   $��Update��6<   6M   5M      �� 5N      $  
Horizontal=�6)   .+�5O   "   �� $
��Wrap=�6,   $��13(�6@   ���#79747E$��Tìm kiếm khoảng giá��5P   A   $��Segoe UI Variable Small)�5Q   ,   $��Normal"�6Q   $��Center=�6e   $��0 0 50 0q�6�   �� 5R   "   -   5        $��searchFromPriceTextBox��5R   N   $
 Từ��6*   $	��100��6l   $��0 0 20 0q�6x   $��Bottom=�6�   �� 5S   "   -	   5        $��searchToPriceTextBox��5S   O   $ Tới��6*   $	��100��6k   $��Bottom=�6w   �� 5T   "   -
   5        $��15 0 0 0q�5T   )   $��Search��6;   
���� ��6p   �� 5U   "   -   5        $��10 0 0 0q�5U   )   $��
Remove Day��6;   
���� ��6t   5V      &   System.Windows.Controls.DataGrid  5W      -   5        $��productManageDataGrid��5W   '   $	��900��6D   $	��200��6P   $
��Auto=�5X   '   $
��Auto=�5Y   '      CanUserDeleteRows$ False��5[   '      CanUserAddRows$ False��6B      SelectionMode$ Extended=�6Y      SelectionUnit$
 Cell=�6r   ��5Z   '   �� *productExcelData+5[   �   �� 5\      $  
Horizontal=�6)   .+�5]   "   �� $��Trang��6,   $��14(�69   $��0 0 20 0q�6G   $��Center=�6Y   �� 5^   "   -   5        $��pageNumProductTextBox��5^   *   $��20��6S   $��Center=�6^   $��Center=�6{   6�   �� 5_   "   $	�� / ��6,   $��Center=�67   �� 5`   "   -   5        $��numberPageProductText��5`   ,   $��Center=�6U   �� 5a   "   -   5        $��setPageNumProduct��5a   )   $	��Set��6C   $��15 0 0 0q�6q   
���� ��6�   �� 5b   "   $��Số dòng mỗi trang��6,   $��14(�6E   $��	50 0 20 0q�6S   $��Center=�6f   �� 5c   "   -   5        $ ��numOfPerPageProductTextBox��5c   *   $��20��6X   $��Center=�6c   $��Center=�6�   �� 5d   "   -   5        $��setProductPerOfPage��5d   )   $	��Set��6E   $��15 0 0 0q�6u   5e      5f      5h      5i      5j      � 5l      $��Center=�6   $��0 0 0 20q�60   $	��900��6C   $	��400��6O    5m      �� 5n       5p      �� 5q      $��* �6'   .+�5t      �� $ 15��6   $	��900��6+   
�������$��Center=�6J   $��0 5 0 10q�6g   ._�5u      �� $��20 20 20 20q�6!   $  Vertical=�66   $��Center=�6M   .+�5v      �� $  
Horizontal=�6%   .+�5w      �� $
��Wrap=�6(   $��15(�6<   ���#79747E$��Manage Categories��5x   =   $&�� Segoe UI Variable Small Semibold)�5y   (   $
��Bold"�6V   %   MyShop.UserControls.SearchBoxUC  5z      -   5          Name$ categoryManageSearchBox��5z   -   %   MyShop.UserControls.SearchBoxUC   Loaded$ SearchBoxUC_Loaded��6m   $	��230��6N   $��	40 0 0 15q�6Z   6m   �� 5{      -   5        $��	10 0 0 15q�5{   %   $	��Add��68   6F   �� 5|      -   5        $��	10 0 0 15q�5|   %   $��Remove��68   6I   �� 5}      -   5        $��	10 0 0 15q�5}   %   $��Update��68   6I   5~        5      -   5        $��categoryManageDataGrid��5   #   $	��900��6A   $	��130��6M   $
��Auto=�5�   #   $
��Auto=�5�   #   $
 True��5�   #   $ False��6=   $ Extended=�6U   $
 Cell=�6n   ��5�   #   �� *productExcelData+5�   �   5�      5�      5�      5�      5�      5�      