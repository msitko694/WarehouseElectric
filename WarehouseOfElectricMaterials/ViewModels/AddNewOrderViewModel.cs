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
using System.Windows.Documents;

namespace WarehouseElectric.ViewModels
{
    class AddNewOrderViewModel : ViewModelBase
    {
        #region "Constructors"
        public AddNewOrderViewModel(AddNewOrderView addNewOrderView, CategoryViewModel categoryViewModel)
        {
            _addNewOrderView = addNewOrderView;
            CategoryViewModel = categoryViewModel;
            
            SuppliersManager suppliersManager = new SuppliersManager();
            ListOfSuppliersComboBox = suppliersManager.GetAllName();

            ProductsOnOrder = new ObservableCollection<ProductOnOrderViewModel>();
        }
        #endregion //Constructors

        #region "Fields"
        private AddNewOrderView _addNewOrderView;
        private List<String> _listOfSuppliersComboBox;

        private ProductCategoriesManager _productCategoriesManager;
        private IList<PR_Product> _listProductsToShow;
        private RelayCommand _showProductsCommand;
        
        private RelayCommand _saveOrderCommand;
        private RelayCommand _addProductToOrderCommand;
        private ObservableCollection<ProductOnOrderViewModel> _productsCollection;
        private ObservableCollection<ProductOnOrderViewModel> _productsOnOrder;

        private OR_Order _order;
        private IList<OE_OrderItem> _orderItemsToShow;

        private String _selectedSuppliersName;
        private const decimal _VAT_PERCENTAGE_VALUE = 0.22m;

        #endregion //Fields

        #region "Properties"
        public RelayCommand SaveOrderCommand
        {
            get
            {
                _saveOrderCommand = new RelayCommand(SaveOrder);
                _saveOrderCommand.CanUndo = (obj) => false;

                return _saveOrderCommand;
            }
            set
            {
                _saveOrderCommand = value;
            }
        }

        public RelayCommand AddProductToOrderCommand
        {
            get
            {
                _addProductToOrderCommand = new RelayCommand(AddProductToOrder);
                _addProductToOrderCommand.CanUndo = (obj) => false;

                return _addProductToOrderCommand;
            }
            set
            {
                _addProductToOrderCommand = value;
            }
        }

        public OR_Order Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
                OnPropertyChanged("Order");
            }
        }

        public IList<OE_OrderItem> OrderItemToShow
        {
            get
            {
                return _orderItemsToShow;
            }
            set
            {
                _orderItemsToShow = value;
                OnPropertyChanged("OrderItemsToShow");
            }
        }

        public List<String> ListOfSuppliersComboBox
        {
            get
            {
                return _listOfSuppliersComboBox;
            }
            set
            {
                _listOfSuppliersComboBox = value;
                OnPropertyChanged("ListOfSuppliersComboBox");
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

        public CategoryViewModel CategoryViewModel { get; set; }

        public ObservableCollection<ProductOnOrderViewModel> ProductsCollection
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

        public ObservableCollection<ProductOnOrderViewModel> ProductsOnOrder
        {
            get
            {
                return _productsOnOrder;
            }
            set
            {
                _productsOnOrder = value;
                OnPropertyChanged("ProductsOnOrder");
            }
        }

        public String SelectedSuppliersName
        {
            get
            {
                return _selectedSuppliersName;
            }
            set
            {
                _selectedSuppliersName = value;
                OnPropertyChanged("SelectedSuppliersName");
            }
        }

        #endregion //Properties

        #region "Methods"

        public void AddProductToOrder(Object obj)
        {
            ProductsManager productsManager = new ProductsManager();

            ProductsCollection = new ObservableCollection<ProductOnOrderViewModel>(
                from product in ProductsCollection where product.QuantityOnOrder > 0
                select product
            );
            foreach (var product in ProductsCollection)
            {
                ProductsOnOrder.Add(product);
            }
            ProductsCollection.Clear();
           
        }

        public void SaveOrder(Object obj)
        {
            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.Get(SessionHelper.userId);
            

            
            if (user.US_WO_ID == null)
            {
                MessageBox.Show("Tylko pracownicy mogą dodawać zamówienia, zaloguj się jako pracownik", "Brak uprawnień", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectedSuppliersName == null)
            {
                MessageBox.Show("Wybierz Firmę przyjmującą zamówienie");
                return;
            }
            SuppliersManager suppliersManager = new SuppliersManager();
            SU_Supplier supplier = new SU_Supplier();
            foreach (var currentSupplier in suppliersManager.GetAll())
            {
                if (currentSupplier.SU_NAME == SelectedSuppliersName)
                {
                    supplier = currentSupplier;
                }
            }
            

            OrdersManager orderManager = new OrdersManager();
            OR_Order newOrder = new OR_Order()
            {
                OR_ADDED = DateTime.Now,
                OR_LAST_MODIFIED = DateTime.Now,
                OR_WO_ID = user.WO_Worker.WO_ID,//worker.WO_ID,
                OR_SU_ID = supplier.SU_ID,   
            };
            orderManager.Add(newOrder);
            
            ProductsManager productsManager = new ProductsManager();
            OrderItemsManager orderItemsManager = new OrderItemsManager();
            foreach (var product in ProductsOnOrder)
            {
                OE_OrderItem orderItem = new OE_OrderItem()
                {
                    OE_ADDED = DateTime.Now,
                    OE_LAST_MODIFIED = DateTime.Now,
                    OE_OR_ID = newOrder.OR_ID,
                    OE_PR_ID = product.Product.PR_ID,
                    OE_QUANTITY = product.QuantityOnOrder,
                    OE_UNIT_PRICE = product.Product.PR_UNIT_PRICE,
                    OE_TOTAL_NETTO = product.QuantityOnOrder * product.Product.PR_UNIT_PRICE,
                    OE_VAT_RATE = _VAT_PERCENTAGE_VALUE
                };
                orderItem.OE_TOTAL_VAT = _VAT_PERCENTAGE_VALUE * orderItem.OE_TOTAL_NETTO;
                orderItem.OE_TOTAL_BRUTTO = orderItem.OE_TOTAL_NETTO + orderItem.OE_TOTAL_VAT;
                orderItemsManager.Add(orderItem);
            }
            _addNewOrderView.Close();
        }

        public void ShowProducts(Object obj)
        {
            using (ProductsManager productsManager = new ProductsManager())
            {
                if (CategoryViewModel.GetSelectedCategory().ProductCategory != null)
                {
                    int selectedCategoryId = CategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                    //ListProductsToShow = productsManager.GetAllFromCategory(selectedCategoryId).ToList();
                    ProductsCollection = new ObservableCollection<ProductOnOrderViewModel>
                        (from product in productsManager.GetAllFromCategory(selectedCategoryId).ToList()
                        select new ProductOnOrderViewModel()
                        {
                            Product = product,
                        }  
                    );
                    

               
                }
            }
        }
        #endregion //Methods
    }
}
