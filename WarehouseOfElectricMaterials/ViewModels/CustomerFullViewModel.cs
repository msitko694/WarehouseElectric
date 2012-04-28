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
    class CustomerFullViewModel:ViewModelBase
    {
        #region "Constructors"
        public CustomerFullViewModel(CustomerFullView customerFullView, int customerId)
        {
            IsEnabledButtonOpenInvoiceFullView = false;
            _customerFullView = customerFullView;
            CustomersManager customersManager = new CustomersManager();
            _customer = customersManager.Get(customerId);

            CustomerNameToShow = _customer.CU_NAME;
            CustomerPhoneToShow = _customer.CU_PHONE;
            CustomerStreetToShow = _customer.CU_STREET;
            CustomerTownToShow = _customer.CU_TOWN;
            CustomerPostCodeToShow = _customer.CU_POST_CODE;

            InvoicesManager invoicesManager = new InvoicesManager();
            InvoicesListToShow = invoicesManager.GetByCustomerId(_customer.CU_ID);
            
        }
        #endregion //Constructors
        #region "Fields"
        private CustomerFullView _customerFullView;
        private CU_Customer _customer;
        private String _customerNameToShow;
        private String _customerPhoneToShow;
        private String _customerStreetToShow;
        private String _customerTownToShow;
        private String _customerPostCodeToShow;
        private IList<IN_Invoice> _invoicesListToShow;
        private Boolean _isEnabledButtonOpenInvoiceFullView;
        private RelayCommand _openInvoiceFullViewCommand;
        private IN_Invoice _selectedInvoice;
        #endregion //Fields
        #region "Properties"
        public String CustomerNameToShow
        {
            get
            {
                return _customerNameToShow;
            }
            set
            {
                _customerNameToShow = value;
                OnPropertyChanged("CustomerNameToShow");
            }
        }
        public String CustomerPhoneToShow
        {
            get
            {
                return _customerPhoneToShow;
            }
            set
            {
                _customerPhoneToShow = value;
                OnPropertyChanged("CustomerPhoneToShow");
            }
        }
        public String CustomerPostCodeToShow
        {
            get
            {
                return _customerPostCodeToShow;
            }
            set
            {
                _customerPostCodeToShow = value;
                OnPropertyChanged("CustomerPostCodeToShow");
            }

        }
        public String CustomerStreetToShow
        {
            get
            {
                return _customerStreetToShow;
            }
            set
            {
                _customerStreetToShow = value;
                OnPropertyChanged("CustomerStreetToShow");
            }
        }
        public String CustomerTownToShow
        {
            get
            {
                return _customerTownToShow;
            }
            set
            {
                _customerTownToShow = value;
                OnPropertyChanged("CustomerTownToShow");
            }
        }
        public IList<IN_Invoice> InvoicesListToShow
        {
            get
            {
                return _invoicesListToShow;
            }
            set
            {
                _invoicesListToShow = value;
                OnPropertyChanged("InvoicesListToShow");
            }
        }
        public Boolean IsEnabledButtonOpenInvoiceFullView
        {
            get
            {
                return _isEnabledButtonOpenInvoiceFullView;
            }
            set
            {
                _isEnabledButtonOpenInvoiceFullView = value;
                OnPropertyChanged("IsEnabledButtonOpenInvoiceFullView");
            }
        }
        public RelayCommand OpenInvoiceFullViewCommand
        {
            get
            {
                if (_openInvoiceFullViewCommand == null)
                {
                    _openInvoiceFullViewCommand = new RelayCommand(OpenInvoiceFullView);
                    _openInvoiceFullViewCommand.CanUndo = (obj) => false;
                }
                return _openInvoiceFullViewCommand;
            }
            set
            {
                _openInvoiceFullViewCommand = value;
            }

        }
        public IN_Invoice SelectedInvoice
        {
            get
            {
                return _selectedInvoice;
            }
            set
            {
                IsEnabledButtonOpenInvoiceFullView = true;
                _selectedInvoice = value;
                OnPropertyChanged("SelectedInvoice");
            }

        }
        #endregion //Properties
        #region "Methods"
        public void OpenInvoiceFullView(Object obj)
        {
            InvoiceFullView invoiceFullView = new InvoiceFullView(SelectedInvoice.IN_ID);
            invoiceFullView.Show();
        }
        #endregion //Methods
    }
}
