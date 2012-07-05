using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using WarehouseElectric.Helpers;
using Microsoft.Win32;


namespace WarehouseElectric.ViewModels
{
    class InvoiceFullViewModel : ViewModelBase
    {

        public delegate void CloseEvent();
        public CloseEvent CloseEventHandler;

        #region "Constructors"
        public InvoiceFullViewModel(int invoiceId)
        {
            InvoicesManager invoicesManager = new InvoicesManager();
            _invoice = invoicesManager.Get(invoiceId);
            TotalVat = 0;
            TotalNetto = 0;
            TotalBrutto = 0;
            CustomersManager customersManager = new CustomersManager();
            Customers = customersManager.GetAll();

            if(_invoice != null)
            {
                CustomerNameToShow = _invoice.CU_Customer.CU_NAME;
                CustomerPhoneToShow = _invoice.CU_Customer.CU_PHONE;
                CustomerPostCodeToShow = _invoice.CU_Customer.CU_POST_CODE;
                CustomerStreetToShow = _invoice.CU_Customer.CU_STREET;
                CustomerTownToShow = _invoice.CU_Customer.CU_TOWN;
                IssueDate = _invoice.IN_ADDED.GetDateTimeFormats('D')[1];
                SaleDate = _invoice.IN_SELL_DATE.GetDateTimeFormats('D')[1];
                InvoiceNumber = _invoice.IN_INVOICE_NUMBER;

                InvoiceItemsToShow = _invoice.IE_InvoicesItems;

                IssuedByWorker = _invoice.WO_Worker.PO_Position.PO_NAME + " " + _invoice.WO_Worker.WO_NAME + " " + _invoice.WO_Worker.WO_SURNAME;
                TotalBrutto = _invoice.IN_TOTAL;
                SpeditionType = _invoice.SP_Spedition.SP_NAME;
                IsReadOnly = true;
            }
            else
            {
                //fill products data grid
                ProductsManager productsManager = new ProductsManager();
                ProductsCollection = new ObservableCollection<ProductOnInvoiceViewModel>(
                    from product in productsManager.GetAll()
                    where product.PR_DEPOT_QUANTITY > 0
                    select new ProductOnInvoiceViewModel()
                            {
                                Product = product,
                            }
                );
                foreach (var product in ProductsCollection)
                {
                    product.OnAmmountChangeHandler += UpdatePrices;
                }

                SpeditionManager speditionManager = new SpeditionManager();
                SpeditionTypes =  speditionManager.GetAll();
            }
        }
        #endregion  //Constructors
        #region "Fields"

        private CU_Customer _selectedCustomer;
        private IList<CU_Customer> _customers;
        private IList<SP_Spedition> _speditionTypes;
        private SP_Spedition _selectedSpedition;
        private const decimal _VAT_PERCENTAGE_VALUE = 0.22m;
        private IN_Invoice _invoice;
        private String _customerNameToShow;
        private String _customerPhoneToShow;
        private String _customerStreetToShow;
        private String _customerTownToShow;
        private String _customerPostCodeToShow;
        private String _issueDate;
        private String _invoiceNumber;
        private String _saleDate;
        private String _issuedByWorker;
        private IList<IE_InvoicesItem> _invoiceItemsToShow;
        private String _productNameToShow;
        private String _speditionType;
        private decimal? _totalBrutto;
        private decimal? _totalNetto;
        private decimal? _totalVat;
        private ObservableCollection<ProductOnInvoiceViewModel> _productsCollection;
        private bool _isReadOnly;
        private RelayCommand _saveInvoiceCommand;
        private RelayCommand _generateReportCommand;

        #endregion  //Fields
        #region "Properties"

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
            }
        }

        public IList<CU_Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        public IList<SP_Spedition> SpeditionTypes
        {
            get
            {
                return _speditionTypes;
            }
            set
            {
                _speditionTypes = value;
                OnPropertyChanged("SpeditionTypes");
            }
        }

        public SP_Spedition SelectedSpedition
        {
            get
            {
                return _selectedSpedition;
            }
            set
            {
                _selectedSpedition = value;
                OnPropertyChanged("SelectedSpedition");
            }
        }

        public IN_Invoice Invoice
        {
            get
            {
                return _invoice;
            }
            set
            {
                _invoice = value;
                OnPropertyChanged("Invoice");
            }
        }

        public RelayCommand SaveInvoiceCommand
        {
            get
            {
                if (_saveInvoiceCommand == null)
                {
                    _saveInvoiceCommand = new RelayCommand(SaveInvoice);
                    _saveInvoiceCommand.CanUndo = (obj) => false;
                }
                return _saveInvoiceCommand;
            }
        }


        public RelayCommand GenerateReportCommand
        {
            get
            {
                if (_generateReportCommand == null)
                {
                    _generateReportCommand = new RelayCommand(new System.Action<object>((obj) =>{
                        IExporter exporter = new HtmlExporter();
                        String filePath;
                        SaveFileDialog fileDialog = new SaveFileDialog();
                        fileDialog.DefaultExt = "*.html";
                        fileDialog.Filter = "Html document (*.html) | *.html";
                        fileDialog.AddExtension = true;
                        fileDialog.ShowDialog();
                        filePath = fileDialog.FileName;
                        if (!String.IsNullOrWhiteSpace(filePath))
                        {
                            filePath += ".html";
                            exporter.ExportInvoice(Invoice, filePath);
                        }
                    }));
                    _generateReportCommand.CanUndo = (obj) => false;
                }
                return _generateReportCommand;
            }
        }

        public ObservableCollection<ProductOnInvoiceViewModel> ProductsCollection
        {
            get
            {
                return _productsCollection;
            }
            set
            {
                _productsCollection = value;
                OnPropertyChanged("ProductsCollection");
            }
        }

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
        public String InvoiceNumber
        {
            get
            {
                return _invoiceNumber;
            }
            set
            {
                _invoiceNumber = value;
                OnPropertyChanged("InvoiceNumber");
            }
        }
        public String SaleDate
        {
            get
            {
                return _saleDate;
            }
            set
            {
                _saleDate = value;
                OnPropertyChanged("SaleDate");
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
        public String ProductNameToShow
        {
            get
            {
                return _productNameToShow;
            }
            set
            {
                _productNameToShow = value;
                OnPropertyChanged("ProductNameToShow");
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
        public decimal? TotalBrutto
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
        public decimal? TotalNetto
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
        public decimal? TotalVat
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

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        /// <summary>
        /// Updates the prices.
        /// </summary>
        public void UpdatePrices()
        {
            TotalNetto = 0;
            foreach (var product in ProductsCollection)
            {                
                TotalNetto += product.AmmountOnInvoice * product.Product.PR_UNIT_PRICE;
            }
            TotalVat =  TotalNetto * _VAT_PERCENTAGE_VALUE;
            TotalBrutto = TotalVat + TotalNetto;
            TotalBrutto = Decimal.Round(TotalBrutto.Value, 2);
            TotalNetto = Decimal.Round(TotalNetto.Value, 2);
            TotalVat = Decimal.Round(TotalVat.Value, 2);

        }

        public void SaveInvoice(object obj)
        {
            WO_Worker worker = null;
            WorkersManager workersManager = new WorkersManager();

            foreach(var currentWorker in workersManager.GetAll())
            {
                if (currentWorker.US_Users[0].US_ID == SessionHelper.userId)
                {
                    worker = currentWorker;
                    break;
                }
            }

            if(worker == null)
            {
                MessageBox.Show("Tylko pracownicy mogą dodawać faktury, zaloguj się jako pracownik", "Brak uprawnień", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_selectedCustomer == null)
            {
                MessageBox.Show("Nie wybrano klienta, dla którego jest wystawiana fakutra", "Nie wybrano klienta", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_selectedSpedition == null)
            {
                MessageBox.Show("Nie wybrano rodzaju spedycji dla tej faktury", "Nie wybrano rodzaju spedycji", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            InvoicesManager invoicesManager = new InvoicesManager();
            IN_Invoice invoice = new IN_Invoice()
            {
                IN_ADDED = DateTime.Now,
                IN_LAST_MODIFIED = DateTime.Now,
                IN_SELL_DATE = DateTime.Now,
                IN_TOTAL = TotalBrutto,
                IN_TOTAL_NETTO = TotalNetto.Value,
                IN_TOTAL_VAT = TotalVat.Value,
                IN_SP_ID = SelectedSpedition.SP_ID,
                IN_WO_ID = worker.WO_ID,
                IN_CU_ID = SelectedCustomer.CU_ID   
            };
            invoicesManager.Add(invoice);
            invoice.IN_INVOICE_NUMBER = String.Format("{0}/{1}/{2}:{3}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, invoice.IN_ID);
            invoicesManager.Update() ;
            ProductsManager productsManager = new ProductsManager();
            InvoicesItemsManager invoicesItemsManager = new InvoicesItemsManager();
            foreach (var product in ProductsCollection)
            {
                if (product.AmmountOnInvoice > 0)
                {
                    IE_InvoicesItem invoiceItem = new IE_InvoicesItem()
                    {
                        IE_ADDED = DateTime.Now,
                        IE_LAST_MODIFIED = DateTime.Now,
                        IE_TOTAL_NETTO = product.AmmountOnInvoice * product.Product.PR_UNIT_PRICE,
                        IE_UNIT_PRICE = product.Product.PR_UNIT_PRICE,
                        IE_VAT_RATE = _VAT_PERCENTAGE_VALUE,
                        IE_PR_ID = product.Product.PR_ID,
                        IE_IN_ID = invoice.IN_ID,
                        IE_QUANTITY = product.AmmountOnInvoice
                    };
                    invoiceItem.IE_TOTAL_VAT = _VAT_PERCENTAGE_VALUE * invoiceItem.IE_TOTAL_NETTO;
                    invoiceItem.IE_TOTAL_BRUTTO = invoiceItem.IE_TOTAL_NETTO + invoiceItem.IE_TOTAL_VAT;
                    invoicesItemsManager.Add(invoiceItem);

                    var productToChange = productsManager.Get(product.Product.PR_ID);
                    productToChange.PR_DEPOT_QUANTITY = product.Product.PR_DEPOT_QUANTITY.Value - product.AmmountOnInvoice;
                }
            }
            productsManager.Update();
            CloseEventHandler();
        }

        #endregion  //Methods
    }
}
