   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyShop.GUI
MyShop.GUI  5   	   9!clr-namespace:MyShop.UserControlsMyShop.UserControls  5   	       MyShop.GUI.Dashboard   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:MyShop.GUI     5   	   0uc!clr-namespace:MyShop.UserControls     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Icon$  /assets/logo-mobile.png��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � Title$ MyShop Dashboard��5   	   $	��800��6"   $
��1280��6/   .��5      � .+�5   
   ]� .+�5      #  MaterialDesignThemes.Wpf.Card  .��5      �� 3  -MaterialDesignThemes.Wpf.NavigationRailAssist   ShowSelectionBackground$
 True��6    J� SnapsToDevicePixels$
 True��5       *  &MaterialDesignNavigationRailTabControl#��[  5       �� TabStripPlacement$
 Left=�5         FloatingContent 5      �� 5       .+�5!      �� -   5        $��dashboardNavTop��5!   )   $��0 50px 0 50pxq�6@   .+�5"   "   �� -   5        $��dashboardLogo��5"   (   $��/assets/logo.png��6=   $��160px��6W   �� 5#   "   $��
0 30px 0 0q�6-   .+�5$   &   �� $��
0 10px 0 0q�61    �� Orientation$ Vertical=�6E   .+�5%   *   �� $	��200��65   $ Vertical=�6A   .+�5&   .   �� $
��80px��64   $��/assets/user.png��6A   �� 5'   .   $��Center=�6h   $
��20px(�5(   ,   $
��Bold"�6<   $��
0 10px 0 0q�6N   $��Center=�6b   ��5'   8   �� *
username+5(   b   5)   +   �� 5*   *   $ Vertical=�65   $��Center=�6L   $��
0 30px 0 0q�6i   .+�5+   .   �� -   5        $	��150��5+   5   $
��40px��6A   ���#8E8CFF$��0q�5,   )   .��5-   2   �� $ 
Horizontal=�6=   $��Center=�5.   -   $
��auto��6J   $
��auto��6W   .+�5/   6   '  !MaterialDesignThemes.Wpf.PackIcon  $
��20px��6N   $
��20px��6[   $
��Bold"�6i   $��Center=�50   >   	   Kind$	 Logout=�6Y   �� 51   6   $��Log out��6@   $
��20px(�6O   $
��Bold"�6_   $��
10px 0 0 0q�6q   52   3   53   /   54   +   55   '   56   #   57      �� 58      -   5        $��dashboardNavTopMobile��58   )   $��0 50px 0 50pxq�6F   .+�59   "   �� $��/assets/logo-mobile.png��6(   $��48��6I   �� 5:   "   $
��50px��6(   $��0 50px 0 30pxq�65   $��/assets/user.png��6L   �� 5;   "   -   5        $��Center=�5;   )    * &MaterialDesignFloatingActionMiniButton#��[ 5<   )   
 � ToolTip$,
 &MaterialDesignFloatingActionMiniButton��5=   )   {�5;   F   �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType
 Window.��5>   &     $	 Logout=�6>   $��24��6L   $��24��6X   5?   #   5@      5A      .@�5C      �� -   5        $
��tab1��5C      $	��250��6*   ���#8E8CFF��5D      �� 5E      -   5        $��tab1Icon��5E   )   $��
30px 0 0 0q�69   $ 
Horizontal=�6M   $	��200��6f   $
��auto��6r   .+�5F   "     $
��20px��6:   $
��20px��6G   $��Center=�5G      $	 ViewDashboard=�69   �� 5H   "   -	   5        $��tab1Name��5H   ,   $��	Dashboard��6<   $
��20px(�6M   $��
10px 0 0 0q�6]   5I      .��5K      �� ���#ededed.+�5L      %   MyShop.UserControls.DashboardUC  %   MyShop.UserControls.DashboardUC   Loaded$ DashboardUC_Loaded��6-   $��Center=�6I   5M      5N      �� 5O      -
   5        $
��tab2��5O      $	��250��6*   ���#8E8CFF��5P      �� 5Q      -   5        $��tab2Icon��5Q   )   $��
30px 0 0 0q�69   $ 
Horizontal=�6M   $	��200��6f   $
��auto��6r   .+�5R   "     $
��20px��6:   $
��20px��6G   $��Center=�5S      $	 ArchiveSettings=�69   �� 5T   "   -   5        $��tab2Name��5T   ,   $��Manage Items��6<   $
��20px(�6P   $��
10px 0 0 0q�6`   5U      .��5W      �� ���#ededed.+�5X      '   !MyShop.UserControls.ManageItemsUC  5Y      5Z      �� 5[      -   5        $
��tab3��5[      $	��250��6*   ���#8E8CFF��5\      �� 5]      -   5        $��tab3Icon��5]   )   $��
30px 0 0 0q�69   $ 
Horizontal=�6M   $	��200��6f   $
��auto��6r   .+�5^   "     $
��20px��6:   $
��20px��6G   $��Center=�5_      $
	 Cart=�69   �� 5`   "   -   5        $��tab3Name��5`   ,   $��Orders��6<   $
��20px(�6J   $��
10px 0 0 0q�6Z   5a      .��5c      �� ���#ededed.+�5d      "   MyShop.UserControls.OrdersUC  5e      5f      �� 5g      -   5        $
��tab4��5g      $	��250��6*   ���#8E8CFF��5h      �� 5i      -   5        $��tab4Icon��5i   )   $��
30px 0 0 0q�69   $ 
Horizontal=�6M   $	��200��6f   $
��auto��6r   .+�5j   "     $
��20px��6:   $
��20px��6G   $��Center=�5k      $	 ChartBarStacked=�69   �� 5l   "   -   5        $��tab4Name��5l   ,   $��Reports��6<   $
��20px(�6K   $��
10px 0 0 0q�6[   5m      .��5o      �� ���#ededed.+�5p      #   MyShop.UserControls.ReportsUC  5q      5r      5s      5t      5u      5v      5w      