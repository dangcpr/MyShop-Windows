﻿#pragma checksum "..\..\..\..\UserControls\OrdersUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "01B5EAB75CCC65AF942F5D8CAD649F3A8A8163AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using MyShop.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MyShop.UserControls {
    
    
    /// <summary>
    /// OrdersUC
    /// </summary>
    public partial class OrdersUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\UserControls\OrdersUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sortOders;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\UserControls\OrdersUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker orderFromDatePicker;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\UserControls\OrdersUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker orderToDatePicker;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\UserControls\OrdersUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid orderManageDataGrid;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\UserControls\OrdersUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid orderDataGridPag;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyShop;component/usercontrols/ordersuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\OrdersUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((MyShop.UserControls.OrdersUC)(target)).Loaded += new System.Windows.RoutedEventHandler(this.handleOrdersUCLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 47 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleAddOrder);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 51 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleEditOrder);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 55 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleDeleteOrder);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 59 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleViewDetailOrder);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sortOders = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.orderFromDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.orderToDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 9:
            
            #line 79 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleSortDate);
            
            #line default
            #line hidden
            return;
            case 10:
            this.orderManageDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 90 "..\..\..\..\UserControls\OrdersUC.xaml"
            this.orderManageDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.orderManageDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 91 "..\..\..\..\UserControls\OrdersUC.xaml"
            this.orderManageDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.orderManageDataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.orderDataGridPag = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            
            #line 104 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handlePrevDataGrid);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 114 "..\..\..\..\UserControls\OrdersUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleNextDataGrid);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

