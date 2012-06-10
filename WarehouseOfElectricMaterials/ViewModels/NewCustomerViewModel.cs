using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Models;

namespace WarehouseElectric.ViewModels
{
    class NewCustomerViewModel : ViewModelBase
    {
        #region "Constructors"

        public NewCustomerViewModel(ICustomersManager customersManager, NewCustomerView newCustomerView, CashierViewModel cashierViewModel)
        {
            _customersManager = customersManager;
            _newCustomerView = newCustomerView;
            _cashierViewModel = cashierViewModel;
            _customer = new CU_Customer();
        }

        public NewCustomerViewModel(ICustomersManager customersManager, NewCustomerView newCustomerView, CashierViewModel cashierViewModel, int customerId)
        {
            _customersManager = customersManager;
            _newCustomerView = newCustomerView;
            _cashierViewModel = cashierViewModel;
            Customer = _customersManager.Get(customerId);
            _isUpdating = true;
        }

        #endregion 

        #region "Fields"

        private CU_Customer _customer;
        private bool _isUpdating = false;
        private ICustomersManager _customersManager;
        private RelayCommand _saveCustomerCommand;
        private NewCustomerView _newCustomerView;
        private CashierViewModel _cashierViewModel;

        #endregion "Fields

        #region "Properties"

        public CU_Customer Customer
        {
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
            }
            get
            {
                return _customer;
            }
        }

        public RelayCommand SaveCustomerCommand
        {
            get
            {
                if(_saveCustomerCommand == null)
                {
                    _saveCustomerCommand = new RelayCommand(SaveCustomer);
                    _saveCustomerCommand.CanUndo = (obj) => false;
                }
                return _saveCustomerCommand;
            }
            set
            {
                _saveCustomerCommand = value;
            }

        }

        #endregion 

        #region "Methods"

        public void SaveCustomer(object obj)
        {
            if(!_isUpdating)
            {
                Customer.CU_ADDED = DateTime.Now;
                Customer.CU_LAST_MODIFIED = DateTime.Now;
                _customersManager.Add(Customer);
            }
            else
            {
                Customer.CU_LAST_MODIFIED = DateTime.Now;
                _customersManager.Update();
            }
            _cashierViewModel.LoadCustomersList();
            _newCustomerView.Close();
        }

        #endregion

        #region "IDisposable members"

        protected override void OnDispose()
        {
            if(_customersManager != null)
            {
                _customersManager.Dispose();
                _customersManager = null;
            }
            base.OnDispose();
        }

        #endregion
    }
}
