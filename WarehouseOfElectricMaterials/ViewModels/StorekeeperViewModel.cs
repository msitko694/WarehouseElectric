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
            //lista kategorii produktów do TreeView
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoriesToShow = productCategoriesManager.GetAllOnBaseLevel();
            //lista produktów do DataGrid
            ReadCategoriesFromDbIntoList();
    
            ProductsManager productsManager = new ProductsManager();
            int selectedCategoryId = 7;
           // selectedCategoryId = GetSelectedCategory().ProductCategory.PC_ID;
            ListProductsToShow = productsManager.GetAllFromCategory(selectedCategoryId).ToList();

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
            private IList<PR_Product> _listProductsToShow;
            private IList<CategoryViewModel> _rootCategories;
            private ProductCategoriesManager _productCategoriesManager;
            private RelayCommand _getSelectedProductCategoryCommand;
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
            public RelayCommand GetSelectedProductCategoryCommand
            {
                get
                {
                    _getSelectedProductCategoryCommand = new RelayCommand(GetSelectedProductCategory);
                    _getSelectedProductCategoryCommand.CanUndo = (obj) => false;

                    return _getSelectedProductCategoryCommand;
                }
                set
                {
                    _getSelectedProductCategoryCommand = value;
                }
            }
            public IList<CategoryViewModel> RootCategories
            {
                get
                {
                    return _rootCategories;
                }
                set
                {
                    _rootCategories = value;
                    OnPropertyChanged("RootCategories");
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
            public void GetSelectedProductCategory(Object obj)
            {
                ProductsManager productsManager = new ProductsManager();
                   int selectedCategoryId = GetSelectedCategory().ProductCategory.PC_ID;
                ListProductsToShow = productsManager.GetAllFromCategory(selectedCategoryId).ToList();
           }
 
            public void GoAddNewSupplier(Object obj)
            {
                 Application.Current.MainWindow  = new AddNewSupplierView();
                 Application.Current.MainWindow.Show();

                 SuppliersManager supplierManager = new SuppliersManager();
                 ListSuppliersToShow = supplierManager.GetAll();
            }
            public CategoryViewModel GetSelectedCategory()
            {
                CategoryViewModel selectedCategory = new CategoryViewModel();
                foreach (var cat in RootCategories)
                {
                    if (!cat.IsSelected)
                    {
                        var list = GetAllChildrenCategories(cat).Where(x => x.IsSelected).Select(x => x).ToList();
                        if (list.Count > 0)
                        {
                            selectedCategory = list[0];
                        }
                    }
                    else
                    {
                        selectedCategory = cat;
                        break;
                    }
                }

                return selectedCategory;
            }
            public IEnumerable<CategoryViewModel> GetAllChildrenCategories(CategoryViewModel category)
            {
                foreach (var child in category.Children)
                {
                    GetAllChildrenCategories(category);
                    yield return child;
                }
            }
                   public void ReadCategoriesFromDbIntoList()
            {
                IList<PC_ProductCategory> unorderedList;
                unorderedList = ProductCategoriesManager.GetAll();

                RootCategories = unorderedList.Where((x) => x.PC_PC_ID == null).Select((x) => new CategoryViewModel() { ProductCategory = x }).ToList();

                //get root categories
                foreach (var category in RootCategories)
                {
                    //build categories tree
                    BuildCategoriesTree(category, unorderedList);
                }
            }
            private void BuildCategoriesTree(CategoryViewModel categoryViewmodel, IList<PC_ProductCategory> unorderedList)
            {
                categoryViewmodel.Children = (from category in unorderedList where category.PC_PC_ID == categoryViewmodel.ProductCategory.PC_ID select new CategoryViewModel() { ProductCategory = category }).ToList();

                foreach (var category in categoryViewmodel.Children)
                {
                    BuildCategoriesTree(category, unorderedList);
                }
            }

        #endregion //Methods
    }
}
