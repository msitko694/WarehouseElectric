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
    class InvoiceFullViewModel:ViewModelBase
    {
        #region "Constructors"
        public InvoiceFullViewModel(InvoiceFullView invoiceFullView, int invoiceId)
        {
            _invoiceFullView = invoiceFullView;
            InvoicesManager invoicesManager = new InvoicesManager();
            _invoice = invoicesManager.Get(invoiceId);

            CustomerNameToShow = _invoice.CU_Customer.CU_NAME;
            CustomerPhoneToShow = _invoice.CU_Customer.CU_PHONE;
            CustomerPostCodeToShow = _invoice.CU_Customer.CU_POST_CODE;
            CustomerStreetToShow = _invoice.CU_Customer.CU_STREET;
            CustomerTownToShow = _invoice.CU_Customer.CU_TOWN;
            IssueDate = _invoice.IN_ADDED.GetDateTimeFormats('D')[1];
            InvoiceItemsToShow = _invoice.IE_InvoicesItems;
            IssuedByWorker = _invoice.WO_Worker.PO_Position.PO_NAME + " " + _invoice.WO_Worker.WO_NAME + " " + _invoice.WO_Worker.WO_SURNAME;
            TotalBrutto = _invoice.IN_TOTAL.ToString();
            SpeditionType = _invoice.SP_Spedition.SP_NAME;


        }
        #endregion  //Constructors
        #region "Fields"
        private IN_Invoice _invoice;
        private InvoiceFullView _invoiceFullView;
        private String _customerNameToShow;
        private String _customerPhoneToShow;
        private String _customerStreetToShow;
        private String _customerTownToShow;
        private String _customerPostCodeToShow;
        private String _issueDate;
        private String _issuedByWorker;
        private IList<IE_InvoicesItem> _invoiceItemsToShow;
        private String _speditionType;
        private String _totalBrutto;
        private String _totalNetto;
        private String _totalVat;
        
        #endregion  //Fields
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
        public IList<IE_InvoicesItem> InvoiceItemsToShow
        {
            get
            {
                return _invoiceItemsToShow;
            }
            set
            {
                _invoiceItemsToShow = value;
                OnPropertyChanged("InvoiceItemsToShow");
            }
        }
        public String IssueDate
        {
            get
            {
                return _issueDate;
            }
            set
            {
                _issueDate = value;
                OnPropertyChanged("IssueDate");
            }
        }
        public String IssuedByWorker
        {
            get
            {
                return _issuedByWorker;
            }
            set
            {
                _issuedByWorker = value;
                OnPropertyChanged("IssuedByWorker");
            }
        }
        public String SpeditionType
        {
            get
            {
                return _speditionType;
            }
            set
            {
                _speditionType = value;
                OnPropertyChanged("SpeditionType");
            }
        }
        public String TotalBrutto
        {
            get
            {
                return _totalBrutto;
            }
            set
            {
                _totalBrutto = value;
                OnPropertyChanged("TotalBrutto");
            }
        }
        public String TotalNetto
        {
            get
            {
                return _totalNetto;
            }
            set
            {
                _totalNetto = value;
                OnPropertyChanged("TotalNetto");
            }
        }
        public String TotalVat
        {
            get
            {
                return _totalVat;
            }
            set
            {
                _totalVat = value;
                OnPropertyChanged("TotalVat");
            }
        }
        #endregion  //Properties
        #region "Methods"
        #endregion  //Methods
    }
}
