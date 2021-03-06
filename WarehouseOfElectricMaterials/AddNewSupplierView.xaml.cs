﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WarehouseElectric.ViewModels;

namespace WarehouseElectric
{
    /// <summary>
    /// Interaction logic for AddNewSupplierView.xaml
    /// </summary>
    public partial class AddNewSupplierView : Window
    {
        public AddNewSupplierView(OnSupplierAddedHandler OnSupplierAdded)
        {
            InitializeComponent();
            AddNewSupplierViewModel addNewSupplierViewModel = new AddNewSupplierViewModel(this);
            DataContext = addNewSupplierViewModel;
            addNewSupplierViewModel.OnSupplierAdded += OnSupplierAdded;
        }
    }
}
