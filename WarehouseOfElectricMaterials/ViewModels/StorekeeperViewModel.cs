using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;
using System.Data.SqlClient;
using WarehouseElectric.Helpers;
using Microsoft.Win32;
using System.Windows.Documents;


namespace WarehouseElectric.ViewModels
{
    class StorekeeperViewModel : ViewModelBase
    {
        #region "Constructors"
        public StorekeeperViewModel(StorekeeperView storekeeperView, CategoryViewModel categoryViewModel, CategoryViewModel lackCategoryViewModel)
        {
            _storekeeperView = storekeeperView;
            CategoryViewModel = categoryViewModel;
            LackCategoryViewModel = lackCategoryViewModel;

            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.Get(SessionHelper.userId);
            UsernameText = user.US_USERNAME; //Nazwa zalogowanego użytkownika

            //ZAKŁADKA DOSTAWCY
            //lista dostawców do DataGrida
            SuppliersManager suppliersManager = new SuppliersManager();
            ListSuppliersToShow = suppliersManager.GetAll().ToList();

            OrdersManager orderManager = new OrdersManager();
            ShowAllOrder = orderManager.GetAll().ToList();
            

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
        private ProductCategoriesManager _productCategoriesManager;

        private IList<PR_Product> _listProductsToShow;
        private RelayCommand _showProductsCommand;
        private RelayCommand _productSearchCommand;
        private String _productNameToSearch;

        private IList<PR_Product> _listLackProductsToShow;
        private RelayCommand _showLackProductsCommand;
        private RelayCommand _lackProductSearchCommand;
        private String _lackProductNameToSearch;

        private RelayCommand _clearChoiceCommand;
        private RelayCommand _ClearLackChoiceCommand;

        private RelayCommand _goAddNewProductCommand;
        private RelayCommand _goEditProductCommand;
        private RelayCommand _goDeleteProductCommand;
        private PR_Product _productSelectedToDelete;
        private RelayCommand _goAddNewOrderCommand;
        private IList<OR_Order> _showAllOrder;
        private OR_Order _selectedOrderToEdit;
        private RelayCommand _editOrderCommand;
        private RelayCommand _refreshOrderListCommand;
        private RelayCommand _printOrderCommand;
        private String _findOrder;
        private RelayCommand _findOrderBySupplierCommand;
        private String _usernameText; //nazwa zalogowanego użytkownika

        #endregion //Fields
        #region "Properties"

        public RelayCommand FindOrderBySupplierCommand
        {
            get
            {
                _findOrderBySupplierCommand = new RelayCommand(FindOrderBySupplier);
                _findOrderBySupplierCommand.CanUndo = (obj) => false;
                return _findOrderBySupplierCommand;
            }
            set
            {
                _findOrderBySupplierCommand = value;
            }
        }
        public String FindOrder
        {
            get
            {
                return _findOrder;
            }
            set
            {
                _findOrder = value;
                OnPropertyChanged("FindOrder");
            }

        }

        public RelayCommand PrintOrderCommand
        {
            get
            {
                if (_printOrderCommand == null)
                {
                    _printOrderCommand = new RelayCommand(new System.Action<object>((obj) =>
                    {
                        try
                        {
                            IExporter exporter = new HtmlExporter();
                            String filePath = "forPrint.html";
                            if (!String.IsNullOrWhiteSpace(filePath))
                            {
                                filePath += ".html";
                                exporter.ExportOrder(SelectedOrderToEdit, filePath);
                            }
                            System.Windows.Forms.WebBrowser webBrowserForPrinting = new System.Windows.Forms.WebBrowser();
                            webBrowserForPrinting.DocumentCompleted += (sender, e) =>
                            {
                                ((System.Windows.Forms.WebBrowser)sender).Print();
                                ((System.Windows.Forms.WebBrowser)sender).Dispose();
                            };
                            string absoulteUri = System.Environment.CurrentDirectory + "\\" + filePath;
                            webBrowserForPrinting.Url = new Uri(absoulteUri);
                        }
                        catch(Exception)
                        {

                        }

                    }));
                    _printOrderCommand.CanUndo = (obj) => false;
                }
                return _printOrderCommand;
            }
        }

        public RelayCommand GoAddNewOrderCommand
        {
            get
            {
                _goAddNewOrderCommand = new RelayCommand(AddNewOrder);
                _goAddNewOrderCommand.CanUndo = (obj) => false;

                return _goAddNewOrderCommand;
            }
            set
            {
                _goAddNewOrderCommand = value;
            }
        }
        public RelayCommand EditOrderCommand
        {
            get
            {
                _editOrderCommand = new RelayCommand(EditOrder);
                _editOrderCommand.CanUndo = (obj) => false;

                return _editOrderCommand;
            }
            set
            {
                _editOrderCommand = value;
            }
        }
        public RelayCommand RefreshOrderListCommand
        {
            get
            {
                _refreshOrderListCommand = new RelayCommand(RefreshOrderList);
                _refreshOrderListCommand.CanUndo = (obj) => false;

                return _refreshOrderListCommand;
            }
            set
            {
                _refreshOrderListCommand = value;
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

        public ProductCategoriesManager ProductCategoriesManager
        {
            get
            {
                if (_productCategoriesManager == null)
                {
                    _productCategoriesManager = new ProductCategoriesManager();
                }
                return _productCategoriesManager;
            }
            set
            {
                _productCategoriesManager = value;
            }
        }
        public CategoryViewModel CategoryViewModel { get; set; }
        public CategoryViewModel LackCategoryViewModel { get; set; }

        public IList<PR_Product> ListProductsToShow
        {
            get
            {
                return _listProductsToShow;
            }
            set
            {
                _listProductsToShow = value;
                OnPropertyChanged("ListProductsToShow");
            }
        }
        public RelayCommand ShowProductsCommand
        {
            get
            {
                _showProductsCommand = new RelayCommand(ShowProducts);
                _showProductsCommand.CanUndo = (obj) => false;

                return _showProductsCommand;
            }
            set
            {
                _showProductsCommand = value;
            }
        }
        public RelayCommand ProductSearchCommand
        {
            get
            {
                _productSearchCommand = new RelayCommand(ProductSearch);
                _productSearchCommand.CanUndo = (obj) => false;

                return _productSearchCommand;
            }
            set
            {
                _productSearchCommand = value;
            }
        }
        public String ProductNameToSearch
        {
            get
            {
                return _productNameToSearch;
            }
            set
            {
                _productNameToSearch = value;
                OnPropertyChanged("ProductNameToSearch");
            }
        }

        public IList<PR_Product> ListLackProductsToShow
        {
            get
            {
                return _listLackProductsToShow;
            }
            set
            {
                _listLackProductsToShow = value;
                OnPropertyChanged("ListLackProductsToShow");
            }
        }
        public RelayCommand ShowLackProductsCommand
        {
            get
            {
                _showLackProductsCommand = new RelayCommand(ShowLackProducts);
                _showLackProductsCommand.CanUndo = (obj) => false;

                return _showLackProductsCommand;
            }
            set
            {
                _showLackProductsCommand = value;
            }
        }
        public RelayCommand LackProductSearchCommand
        {
            get
            {
                _lackProductSearchCommand = new RelayCommand(LackProductSearch);
                _lackProductSearchCommand.CanUndo = (obj) => false;

                return _lackProductSearchCommand;
            }
            set
            {
                _lackProductSearchCommand = value;
            }
        }
        public String LackProductNameToSearch
        {
            get
            {
                return _lackProductNameToSearch;
            }
            set
            {
                _lackProductNameToSearch = value;
                OnPropertyChanged("LackProductNameToSearch");
            }
        }

        public RelayCommand ClearChoiceCommand
        {
            get
            {
                _clearChoiceCommand = new RelayCommand(ClearChoice);
                _clearChoiceCommand.CanUndo = (obj) => (false);

                return _clearChoiceCommand;
            }
            set
            {
                _clearChoiceCommand = value;
            }
        }
        public RelayCommand ClearLackChoiceCommand
        {
            get
            {
                _ClearLackChoiceCommand = new RelayCommand(ClearLackChoice);
                _ClearLackChoiceCommand.CanUndo = (obj) => (false);

                return _ClearLackChoiceCommand;
            }
            set
            {
                _ClearLackChoiceCommand = value;
            }
        }
        public RelayCommand GoAddNewProductCommand
        {
            get
            {
                if (_goAddNewProductCommand == null)
                {
                    _goAddNewProductCommand = new RelayCommand(GoAddNewProduct);
                    _goAddNewProductCommand.CanUndo = (obj) => false;
                }
                return _goAddNewProductCommand;
            }
            set
            {
                _goAddNewProductCommand = value;
            }
        }
        public RelayCommand GoEditProductCommand
        {
            get
            {
                if (_goEditProductCommand == null)
                {
                    _goEditProductCommand = new RelayCommand(GoEditProduct);
                    _goEditProductCommand.CanUndo = (obj) => false;
                }
                return _goEditProductCommand;
            }
            set
            {
                _goEditProductCommand = value;
            }
        }
        public RelayCommand GoDeleteProductCommand
        {
            get
            {
                if (_goDeleteProductCommand == null)
                {
                    _goDeleteProductCommand = new RelayCommand(GoDeleteProduct);
                    _goDeleteProductCommand.CanUndo = (obj) => false;
                }
                return _goDeleteProductCommand;
            }
            set
            {
                _goDeleteProductCommand = value;
            }
        }
        public PR_Product ProductSelectedToDelete
        {
            get
            {
                return _productSelectedToDelete;
            }
            set
            {
                _productSelectedToDelete = value;
                OnPropertyChanged("ProductSelectedToDelete");
            }
        }
        public IList<OR_Order> ShowAllOrder
        {
            get
            {
                return _showAllOrder;
            }
            set
            {
                _showAllOrder = value;
                OnPropertyChanged("ShowAllOrder");
            }
        }
        public OR_Order SelectedOrderToEdit
        {
            get
            {
                return _selectedOrderToEdit;
            }
            set
            {
                _selectedOrderToEdit = value;
                OnPropertyChanged("SelectedOrderToEdit");
            }
        }
        public String UsernameText
        {
            get
            {
                return _usernameText;
            }
            set
            {
                _usernameText = value;
                OnPropertyChanged("UsernameText");
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
            AddNewSupplierView window = new AddNewSupplierView(window_OnSupplierAdded);
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
        }

        public void window_OnSupplierAdded()
        {
            SuppliersManager suppliersManager = new SuppliersManager();
            ListSuppliersToShow = (from supplier in suppliersManager.GetAll()
                                   select supplier).ToList<SU_Supplier>();
        }

        public void ShowProducts(Object obj)
        {
            using (ProductsManager productsManager = new ProductsManager())
            {
                if (CategoryViewModel.GetSelectedCategory().ProductCategory != null)
                {
                    int selectedCategoryId = CategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                    ListProductsToShow = productsManager.GetAllFromCategory(selectedCategoryId).ToList();
                }
            }
        }
        public void ProductSearch(Object obj)
        {
            using (ProductsManager productsManager = new ProductsManager())
            {
                ListProductsToShow = (from product in productsManager.GetAll()
                                      where product.PR_NAME == ProductNameToSearch
                                      select product).ToList<PR_Product>();
            }
        }

        public void ShowLackProducts(Object obj)
        {
            using (ProductsManager productsManager = new ProductsManager())
            {
                if (LackCategoryViewModel.GetSelectedCategory().ProductCategory != null)
                {
                    int selectedCategoryId = LackCategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                    ListLackProductsToShow = (from product in productsManager.GetAllFromCategory(selectedCategoryId)
                                              where product.PR_DEPOT_QUANTITY < 20
                                              select product).ToList<PR_Product>();
                }
            }
        }
        public void LackProductSearch(Object obj)
        {
            using (ProductsManager productsManager = new ProductsManager())
            {
                if (LackCategoryViewModel.GetSelectedCategory().ProductCategory != null)
                {
                    int selectedCategoryId = LackCategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                    IList<PR_Product> tmp = (from product in productsManager.GetAllFromCategory(selectedCategoryId)
                                             where product.PR_DEPOT_QUANTITY < 20
                                             select product).ToList<PR_Product>();
                    ListLackProductsToShow = (from product in tmp
                                              where product.PR_NAME == LackProductNameToSearch
                                              select product).ToList<PR_Product>();
                }
            }
        }

        public void ClearChoice(Object obj)
        {
            ListProductsToShow = null;
        }
        public void ClearLackChoice(Object obj)
        {
            ListLackProductsToShow = null;
        }

        public void GoAddNewProduct(Object obj)
        {
            AddNewProductView window = new AddNewProductView(-1,window_OnProductAdded);
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
        }
        public void window_OnProductAdded()
        {
            ProductsManager productsManager = new ProductsManager();
            ListProductsToShow = (from product in productsManager.GetAll() select product).ToList<PR_Product>();
        }
        public void GoEditProduct(Object obj)
        {
             if (ProductSelectedToDelete != null)
            {
                ProductsManager productsManager = new ProductsManager();
                PR_Product product = productsManager.Get(ProductSelectedToDelete.PR_ID);
                
               /* IE_InvoicesItem item = new IE_InvoicesItem();
                
                item.IE_PR_ID = product.PR_ID;
                item.IE_QUANTITY = 1;
                item.IE_TOTAL_BRUTTO = 0;
                item.IE_TOTAL_NETTO = 0;
                item.IE_TOTAL_VAT = 0;
                item.IE_UNIT_PRICE = 0;
                item.IE_VAT_RATE = 0;
                item.IE_IN_ID = 1;
                 item.IE_ADDED = DateTime.Now;
                item.IE_LAST_MODIFIED = DateTime.Now;
               
                
                 InvoicesItemsManager invoicesItemsManager = new InvoicesItemsManager();
                invoicesItemsManager.Add(item);
                */
             
            AddNewProductView window = new AddNewProductView(product.PR_ID,window_OnProductAdded);
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
            }
             else
                 MessageBox.Show("Nie można dodać ilości do produktu, ponieważ nie zaznaczyłeś żadnego.");
                   
 
        }
        public void GoDeleteProduct(Object obj)
        {
            if (ProductSelectedToDelete != null)
            {
                ProductsManager productsManager = new ProductsManager();
                PR_Product product = productsManager.Get(ProductSelectedToDelete.PR_ID);
                try
                {
                    productsManager.Delete(product);
                    MessageBox.Show("Produkt został usunięty.");
                    window_OnProductAdded();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Nie można usunąć produktu, ponieważ jest używany w bazie danych.");
                }
            }
                else
                    MessageBox.Show("Nie można usunąć produktu, ponieważ nie zaznaczyłeś żadnego.");
                   
 

        }

        public void AddNewOrder(Object obj)
        {
            AddNewOrderView window = new AddNewOrderView();
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();

        }
        public void EditOrder(Object obj)
        {
            if (SelectedOrderToEdit == null)
            {
                MessageBox.Show("Wybierz zamówienie");
                return;
            }
            EditOrderView window = new EditOrderView(SelectedOrderToEdit.OR_ID);
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
            
        }
        public void RefreshOrderList(Object obj)
        {
            OrdersManager orderManager = new OrdersManager();
            ShowAllOrder = orderManager.GetAll().ToList();
        }

        public void FindOrderBySupplier(Object obj)
        {
            if (FindOrder != null)
            {
                OrdersManager ordersManager = new OrdersManager();
                ShowAllOrder = ordersManager.GetBySupplierName(FindOrder);
            }
        }

        #endregion //Methods
    }
}
