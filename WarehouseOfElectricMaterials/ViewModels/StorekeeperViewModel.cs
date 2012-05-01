using System;
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

        }
        #endregion //Constructors
        #region "Fields"
            private StorekeeperView _storekeeperView;
            private RelayCommand _logOutCommand;
            private RelayCommand _choosePanelCommand;
            private RelayCommand _supplierSearchCommand;
            private IList<SU_Supplier> _listSuppliersToShow;
            private String _supplierNameToSearch;
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
     
        /*    public List<SU_Supplier> supplierDatagrid
            {
                get
                {
                    return _listUserToModificationDataGrid;
                }
                set
                {
                    _listUserToModificationDataGrid = value;
                    OnPropertyChanged("ListUserToModificationDataGrid");
                }
            }*/
           /* public RelayCommand SupplierSearchCommand
            {
                get
                {
                    if (_supplierSearchCommand == null)
                    {
                        _supplierSearchCommand = new RelayCommand(SupplierSearch);
                        _supplierSearchCommand.CanUndo = (obj) => false;
                    }
                    return _supplierSearchCommand;
                }
                set
                {
                    _supplierSearchCommand = value;
                }
            }*/
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
        #endregion //Methods
    }
}
