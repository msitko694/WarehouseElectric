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
    class CashierViewModel:ViewModelBase
    {
        #region "Contructors"
        public CashierViewModel(CashierView cashierView)
        {
            IsEnabledButtonOpenCustomerFullView = false;
            _cashierView = cashierView;

            CustomersManager customersManager = new CustomersManager();
            ListCustomersToShow = customersManager.GetAll().ToList();
            
        }

        #endregion //Constructors

        #region "Fields"
        private CashierView _cashierView;
        private String _customerNameToSearch;
        private RelayCommand _customerSearchCommand;
        private Boolean _isEnabledButtonOpenCustomerFullView;
        private IList<CU_Customer> _listCustomersToShow;
        private RelayCommand _logOutCommand;
        private RelayCommand _openCustomerFullViewCommand;
        private CU_Customer _selectedCustomer;
        #endregion //Fields

        #region "Properties"
        public Boolean IsEnabledButtonOpenCustomerFullView
        {
            get
            {
                return _isEnabledButtonOpenCustomerFullView;
            }
            set
            {
                _isEnabledButtonOpenCustomerFullView = value;
                OnPropertyChanged("IsEnabledButtonOpenCustomerFullView");
            }
        }
        public RelayCommand OpenCustomerFullViewCommand
        {
            get
            {
                if (_openCustomerFullViewCommand == null)
                {
                    _openCustomerFullViewCommand = new RelayCommand(OpenCustomerFullView);
                    _openCustomerFullViewCommand.CanUndo = (obj) => false;
                }
                return _openCustomerFullViewCommand;
            }
            set
            {
                _openCustomerFullViewCommand = value;
            }
        }
        public CU_Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
                if (value != null)
                    IsEnabledButtonOpenCustomerFullView = true;
                else
                    IsEnabledButtonOpenCustomerFullView = false;
            }
        }
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
        public RelayCommand CustomerSearchCommand
        {
            get
            {
                if (_customerSearchCommand == null)
                {
                    _customerSearchCommand = new RelayCommand(CustomerSearch);
                    _customerSearchCommand.CanUndo = (obj) => false;
                }
                return _customerSearchCommand; 
            }
            set
            {
                _customerSearchCommand = value;   
            }
        }
        public IList<CU_Customer> ListCustomersToShow
        {
            get
            {
                return _listCustomersToShow;
            }
            set
            {
                _listCustomersToShow = value;
                OnPropertyChanged("ListCustomersToShow");
            }
        }
        public String CustomerNameToSearch
        {
            get
            {
                return _customerNameToSearch;
            }
            set
            {
                _customerNameToSearch = value;
                OnPropertyChanged("CustomerNameToSearch");
            }
        }
        #endregion //Properties
        
        #region "Methods"
        public void CustomerSearch(Object obj)
        {
            CustomersManager customersManager = new CustomersManager();
            ListCustomersToShow.Clear();
            ListCustomersToShow = customersManager.GetByName(CustomerNameToSearch);
          
        }
        public void OpenCustomerFullView(Object obj)
        {
            CustomerFullView customerFullView = new CustomerFullView(SelectedCustomer.CU_ID);
            customerFullView.Show();

        }
        public void LogOut(Object obj)
        {
            SessionHelper.LogOut();
            Application.Current.MainWindow = new LoginView();
            Application.Current.MainWindow.Show();

            _cashierView.Close();

        }
        #endregion //Methods
    }
}
