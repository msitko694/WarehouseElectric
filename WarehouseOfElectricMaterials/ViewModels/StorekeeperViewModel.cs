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
    class StorekeeperViewModel : ViewModelBase
    {
        #region "Constructors"
        public StorekeeperViewModel(StorekeeperView storekeeperView, CategoryViewModel categoryViewModel, CategoryViewModel lackCategoryViewModel)
        {
            _storekeeperView = storekeeperView;
            CategoryViewModel = categoryViewModel;
            LackCategoryViewModel = lackCategoryViewModel;

            //ZAKŁADKA DOSTAWCY
            //lista dostawców do DataGrida
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

        private RelayCommand _goAddNewProductCommand;
        #endregion //Fields
        #region "Properties"
        public CategoryViewModel CategoryViewModel { get; set; }
        public CategoryViewModel LackCategoryViewModel { get; set; }
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
        #endregion //Properties
        #region "Methods"
        public
void LogOut(Object obj)
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
            AddNewSupplierView window = new AddNewSupplierView();
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
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
                    int selectedCategoryId = CategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
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
                int selectedCategoryId = LackCategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                IList<PR_Product> tmp = (from product in productsManager.GetAllFromCategory(selectedCategoryId)
                                         where product.PR_DEPOT_QUANTITY < 20
                                         select product).ToList<PR_Product>();
                ListLackProductsToShow = (from product in tmp
                                          where product.PR_NAME == LackProductNameToSearch
                                          select product).ToList<PR_Product>();
            }
        }

        public void ClearChoice(Object obj)
        {
            ListProductsToShow = null;
        }

        public void GoAddNewProduct(Object obj)
        {
            AddNewProductView window = new AddNewProductView();
            Application.Current.MainWindow = window;
            Application.Current.MainWindow.Show();
        }
        #endregion //Methods
    }
}
