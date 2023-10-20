﻿#pragma checksum "..\..\..\..\UserControls\ManageItemsUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "998627CD931BA180DB0787F3C7AFA01FC6D0B227"
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
    /// ManageItemsUC
    /// </summary>
    public partial class ManageItemsUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\..\..\UserControls\ManageItemsUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyShop.UserControls.SearchBoxUC productManageSearchBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\UserControls\ManageItemsUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid productManageDataGrid;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\UserControls\ManageItemsUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyShop.UserControls.SearchBoxUC categoryManageSearchBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\UserControls\ManageItemsUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid categoryManageDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MyShop;component/usercontrols/manageitemsuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((MyShop.UserControls.ManageItemsUC)(target)).Loaded += new System.Windows.RoutedEventHandler(this.handleManageItemsUCLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 37 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.handleProductImportExcel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.productManageSearchBox = ((MyShop.UserControls.SearchBoxUC)(target));
            return;
            case 4:
            
            #line 56 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddProductButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 57 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveProductButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 58 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateProductButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.productManageDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 64 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            this.productManageDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.productManageDataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.categoryManageSearchBox = ((MyShop.UserControls.SearchBoxUC)(target));
            return;
            case 9:
            
            #line 84 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCategoryButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 85 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveCategoryButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 86 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdateCategoryButton_OnPressed);
            
            #line default
            #line hidden
            return;
            case 12:
            this.categoryManageDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 92 "..\..\..\..\UserControls\ManageItemsUC.xaml"
            this.categoryManageDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.categoryManageDataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

