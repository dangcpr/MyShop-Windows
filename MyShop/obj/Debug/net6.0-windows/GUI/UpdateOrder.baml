   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyShop.GUI
MyShop.GUI  5   	       MyShop.GUI.UpdateOrder   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyShop.GUI     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  UpdateOrder��5	   	   $	��500��6   $	��800��6*   .��5
      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   .+�5      �� $��Update Order��6   $��Center=�6,   $��20(�6I   $
��Bold"�6W   $��0 0 0 15q�6i   � 5      $��0 0 0 5q�6   $	��145��6$    � ColumnDefinitions 5      �� 5      �� 5       � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��0 �6$   .+�5      �� $��Order ID��6   $��14(�6,   $��0��6:   $��0��6J   $��Right=�6W   $��Bottom=�6s   $��	0 0 30 10q�6�   �� 5      -   5         ��Name$ OrderIDTextBox��5   ,    �� 
IsReadOnly$
 True��6   $��1��6D   $��0��6T   $	��160��6a   $
��left=�6m   $��	0 0 30 10q�6�   $��Bottom=�6�   �� 5      $��Customer ID��6   $��14(�6/   $��0��6=   $��1��6M   $��Right=�6Z   $��Bottom=�6v   $��	0 0 30 10q�6�   �� 5      -   5         ��Name$ customerIdCombobox��5   -    �� 
IsReadOnly$
 True��6   $��1��6J   $��1��6Z   $	��160��6g   $
��left=�6s   $��	0 0 30 10q�6�   $��Bottom=�6�   
���   �6�   �� 5!      $��Deliver Address��6   $��14(�63   $��0��6A   $��2��6Q   $��Right=�6^   $��Bottom=�6z   $��	0 0 30 10q�6�   �� 5"      -   5        $ nameDeliverAddressTextBox��5"      $��1��6=   $��2��6M   $	��160��6Z   $
��left=�6f   $��	0 0 30 10q�6�   $��Bottom=�6�   �� 5$      $��Status��6   $��14(�6*   $��0��68   $��3��6H   $��Right=�6U   $��Bottom=�6q   $��	0 0 30 10q�6�   �� 5%      -   5        $ statusComboBox��5%      $��1��64   $��3��6D   $	��160��6Q   $
��left=�6]   $��	0 0 30 10q�6x   $��Bottom=�6�   .@�5&      �� $��created��6#   �� 5'      $��
delivering��6#   �� 5(      $��	completed��6#   �� 5)      $��	cancelled��6#   5*      �� 5,      $y�Hidden=�6   $��Quantity��60   $��14(�6@   $��0��6N   $��4��6^   $��Right=�6k   $��Bottom=�6�   $��	0 0 30 10q�6�   �� 5-      -   5        $ nameQuanityTextBox��5-   .   $y�Hidden=�6   $��1��6J   $��4��6Z   $	��160��6g   $
��left=�6s   $��	0 0 30 10q�6�   $��Bottom=�6�   5/      �� 50      $ 
Horizontal=�6   $��10q�62   .+�51      &   System.Windows.Controls.DataGrid  -   5        $��productOrderDataGrid��51      $��1��67   $��4��6G   $	��300��6T   $	��152��6`   $��	0 0 30 10q�6m   $��Center=�6�      
IsReadOnly$
 True��6�   	   CanUserReorderColumns$	 False��6�   52      �� 53      $ Vertical=�6   $	��331��64   .+�54      �� -   5        $��productIDTextBox��54   K   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 
Product ID��6   6c   �� 55      -	   5        $��quantityIDTextBox��55   H   $
 Quantity��6   6H   �� 56      -
   5        $��discountIDTextBox��56   V   $
 Discount ID (if have)��6   6V   �� 57      $ 
Horizontal=�6!   $��Center=�6:   .+�58      �� -   5        $��DeleteDataButton��58   B   $��Delete Data��6!   $��5q�67   
���  ��6y   �� 59      -   5        $��AddProductButton��59   :   $	��Add��6!   $��5q�6/   6R   �� 5:      -   5        $��RemoveProductButton��5:   P   $��Remove��6!   
���   �$��5q�6E   6k   5;      5<      5=      �� 5>      -   5        $��Submit��5>      $	��100��6@   $��0 20 0 0q�6L   5?      5@      5A      