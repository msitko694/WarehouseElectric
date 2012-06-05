﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;


namespace WarehouseElectric.ViewModels
{
    class StorekeeperViewModel:ViewModelBase
    {
        #region "Constructors"
        public StorekeeperViewModel(StorekeeperView storekeeperView)
        {
            _storekeeperView = storekeeperView;

            //podpinanie listy dostawców do dataGrida z dostawcami
            SuppliersManager suppliersManager = new SuppliersManager();
            ListSuppliersToShow = suppliersManager.GetAll().ToList();
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoriesToShow = productCategoriesManager.GetAllOnBaseLevel();

        }
        #endregion //Constructors
        #region "Fields"
            private StorekeeperView _storekeeperView;
            private RelayCommand _logOutCommand;
            private RelayCommand _choosePanelCommand;
            private RelayCommand _supplierSearchCommand;
            private IList<SU_Supplier> _listSuppliersToShow;
            private String _supplierNameToSearch;
            private RelayCommand _goAddNewSupplierCommand;
            private IList<PC_ProductCategory> _productCategoriesToShow;
            private PC_ProductCategory _selectedProductCategory;
        #endregion //Fields
        #region "Properties"
            public RelayCommand LogOutCommand
            {
                get
                {
                    if (_logOutCommand == null)
                    {
                        _logOutCommand = new RelayCommand(LogOut);
                        _logOutCommand.CanUndo = (obj) => false;
                    }
                    return _logOutCommand;
                }
                set
                {
                    _logOutCommand = value;
                }
            }
            public RelayCommand ChoosePanelCommand
            {
                get
                {
                    if (_choosePanelCommand == null)
                    {
                        _choosePanelCommand = new RelayCommand(ChoosePanel);
                        _choosePanelCommand.CanUndo = (obj) => false;
                    }
                    return _choosePanelCommand;
                }
                set
                {
                    _choosePanelCommand = value;
                }

            }
            public RelayCommand SupplierSearchCommand
            {
                get
                {
                    _supplierSearchCommand = new RelayCommand(SupplierSearch);
                    _supplierSearchCommand.CanUndo = (obj) => false;

                    return _supplierSearchCommand;
                }
                set
                {
                    _supplierSearchCommand = value;
                }
            }
            public IList<SU_Supplier> ListSuppliersToShow
            {
                get
                {
                    return _listSuppliersToShow;
                }
                set
                {
                    _listSuppliersToShow = value;
                    OnPropertyChanged("ListSuppliersToShow");
                }
            }
            public String SupplierNameToSearch
            {
                get
                {
                    return _supplierNameToSearch;
                }
                set
                {
                    _supplierNameToSearch = value;
                    OnPropertyChanged("SupplierNameToSearch");
                }
            }
            public RelayCommand GoAddNewSupplierCommand
            {
                get
                {
                    if (_goAddNewSupplierCommand == null)
                    {
                        _goAddNewSupplierCommand = new RelayCommand(GoAddNewSupplier);
                        _goAddNewSupplierCommand.CanUndo = (obj) => false;
                    }
                    return _goAddNewSupplierCommand;
                }
                set
                {
                    _goAddNewSupplierCommand = value;
                }
            }
            public IList<PC_ProductCategory> ProductCategoriesToShow
            {
                get
                {
                    return _productCategoriesToShow;
                }
                set
                {
                    _productCategoriesToShow = value;
                    OnPropertyChanged("ProductCategoriesToShow");
                }
            }
            public PC_ProductCategory SelectedProductCategory 
            {
                get
                {
                    return _selectedProductCategory;
                }
                set
                {
                    _selectedProductCategory = value;
                    OnPropertyChanged("SelectedProductCategory");
                }
            }
          #endregion //Properties
        #region "Methods"
            public void LogOut(Object obj)
            {
                SessionHelper.LogOut();
                Application.Current.MainWindow = new LoginView();
                Application.Current.MainWindow.Show();
                _storekeeperView.Close();

            }
            public void ChoosePanel(Object obj)
            {

                Application.Current.MainWindow = new ChoosePanelView();
                Application.Current.MainWindow.Show();
                _storekeeperView.Close();

            }
            public void SupplierSearch(Object obj)
            {
                SuppliersManager suppliersManager = new SuppliersManager();
                ListSuppliersToShow = (from supplier in suppliersManager.GetAll()
                                       where supplier.SU_NAME == SupplierNameToSearch
                                       select supplier).ToList<SU_Supplier>();
            }
            public void GoAddNewSupplier(Object obj)
            {
                 Application.Current.MainWindow  = new AddNewSupplierView();
                 Application.Current.MainWindow.Show();

                 SuppliersManager supplierManager = new SuppliersManager();
                 ListSuppliersToShow = supplierManager.GetAll();
            }     
        #endregion //Methods
    }
}
