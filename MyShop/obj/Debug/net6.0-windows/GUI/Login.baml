   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyShop.GUI
MyShop.GUI  5   	       MyShop.GUI.Login   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyShop.GUI     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�5	   	    � Icon$ /assets/logo-mobile.png��5   	    � Title$ MyShop Login��5   	   $	��680��6   $
��1024��6+   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��3.5* �6   �� 5      $
��3.0* �6    � RowDefinitions 5   
   �� 5      $	��680 �6   .+�5   
   �� $��0��6   $��0��6&    �� Orientation$ Vertical=�63   $	��400��6J   $��0 80px 0 100pxq�6V   .+�5      �� $��/assets/logo.png��6   $��230px��6.   $
��Left=�6<   �� 5      $��0 30px 0 30pxq�6   .+�5       �� $ 
Horizontal=�6   .+�5!      �� $��
Hey, Hello��6    $
��30px(�62   ���#706E78$
��Bold"�6W   �� 5"      $
��👋��6    $
��30px(�6*   
��� ���$
��Bold"�6L   $��
10px 0 0 0q�6^   5#      �� 5$      $!��Please log into your portal��6   $
��20px(�6?   ���#706E78$��
0 10px 0 0q�6d   5%      �� 5&      $��400px��6   $
��Left=�6'   .+�5'      �� -   5        $��usernameInput��5'      $��Center=�6/   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��6J   $
��20px(�5(      $��400px��6*   $��
0 0 0 50pxq�68    !  MaterialDesignOutlinedTextBox#��[  5)      G� 5*      -   5        $��passwordInput��5*      $
��20px(�5+      $��400px��6+   $ Password��5,       % !MaterialDesignOutlinedPasswordBox#��[ 5-      �� 5.      $��Forget Password?��6   ���#8E8CFF$
��Bold"�6I   $
��15px(�6[   $��Right=�5/      $��0 25px 0 25pxq�68   �� 50      -   5        $��400px��50      $��Login��6'   $
��20px(�67   $
��40px��51      ���#8E8CFF$��0q�6<   52      53      54      �� 56   
   $��1��6   $��0��6%   $��Right=�62   .+�57      �� $��/assets/login-cover.png��6   $	��680��65   58      59      5:      