   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyShop.GUI
MyShop.GUI  5   	   #    MyShop.GUI.CustomerManagement   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyShop.GUI     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  CustomerManagement��5   	   $	��600��6$   $
��1200��61   .��5      � .+�5   
   � $��Center=�6   $
��1200��6,    � RowDefinitions 5      �� 5       � ColumnDefinitions 5      �� 5      $
��1.5* �6#   �� 5      $
��3.0* �6#   .+�5      �� $��0��6   $	��250��6&    �� Orientation$ Vertical=�62   $��Center=�6I   .+�5      �� $��Center=�6   $��0 30 0 0q�68   .+�5      �� $��Customer Management��6    $
��20px(�6;   $
��Bold"�6K    �� TextAlignment$ Center=�6]   �� 5      $ Vertical=�6!   $��0 20 0 0q�68   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��Customer ID��6(   $��14(�6;   $��0��6I   $��0��6Y   $��Right=�6f   $��Bottom=�6�   $��80��6�   $��	0 0 30 10q�6�   �� 5      -   5         ��Name$ CustomerIDTextBox��5   8    �� 
IsReadOnly$
 True��6&   $��1��6S   $��0��6c   $	��160��6p   $
��left=�6|   $��	0 0 30 10q�6�   $��Bottom=�6�   5      �� 5       $ 
Horizontal=�6%   .+�5!      �� $
��Name��6(   $��14(�64   $��0��6B   $��0��6R   $��Right=�6_   $��Bottom=�6{   $��80��6�   $��	0 0 30 10q�6�   �� 5"      -   5        $ CustomerNameTextBox��5"   &   $��1��6C   $��0��6S   $	��160��6`   $
��left=�6l   $��	0 0 30 10q�6�   $��Bottom=�6�   5#      �� 5$      $ 
Horizontal=�6%   .+�5%      �� $��Address��6(   $��14(�67   $��0��6E   $��0��6U   $��Right=�6b   $��Bottom=�6~   $��80��6�   $��	0 0 30 10q�6�   �� 5&      -   5        $ CustomerAddressTextBox��5&   &   $��1��6F   $��0��6V   $	��160��6c   $
��left=�6o   $��	0 0 30 10q�6�   $��Bottom=�6�   5'      �� 5(      $ 
Horizontal=�6%   .+�5)      �� $��Phone��6(   $��14(�65   $��0��6C   $��0��6S   $��Right=�6`   $��Bottom=�6|   $��80��6�   $��	0 0 30 10q�6�   �� 5*      -   5        $ CustomerPhoneTextBox��5*   &   $��1��6D   $��0��6T   $	��160��6a   $
��left=�6m   $��	0 0 30 10q�6�   $��Bottom=�6�   5+      �� 5,      $ 
Horizontal=�6%   $��0 30 0 0q�6>   .+�5-      �� -   5        $	��Add��5-   %   $��0 0 10 0q�63   6E   �� 5.      -   5        $
��Edit��5.   %   $��0 0 10 0q�64   6F   �� 5/      -   5        $��Remove��5/   %   66   50      51      52      53      �� 54      $��0��6   $��1��6&   $ Vertical=�66   $��0 0 70 0q�6M   $��Right=�55      $	��Top=�6-   .+�56      �� $��Customer List��6   ���#79747E$
��20px(�6F   $
��Bold"�6V   $��0 30 0 0q�6h   &   System.Windows.Controls.DataGrid  57      -	   5          Name$ CustomerDataGrid��57      $	��700��65   $	��250��6A   $��0 30 0 0q�6N   $
��Auto=�58      $
��Auto=�59         CanUserDeleteRows$
 True��5;      	   CanUserAddRows$	 False��65   
   SelectionMode$
 Extended=�5<         SelectionUnit$
 Cell=�64   ��5:      �� *customerData+5=      5>      5?      5@      5A      