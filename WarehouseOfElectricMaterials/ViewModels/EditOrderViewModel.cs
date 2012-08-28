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
    class EditOrderViewModel : ViewModelBase
    {
        #region "Constructors"
        public EditOrderViewModel(EditOrderView editOrderView, CategoryViewModel categoryViewModel, Int32 flag)
        {
            _editOrderView = editOrderView;
            CategoryViewModel = categoryViewModel;

            OrdersManager ordersManager = new OrdersManager();
            Order = ordersManager.Get(flag);
            
            NameOfCompany = Order.SU_Supplier.SU_NAME;
            StreetOfCompany = Order.SU_Supplier.SU_STREET;
            TownOfCompany = Order.SU_Supplier.SU_TOWN;
            PostCodeOfCompany = Order.SU_Supplier.SU_POST_CODE;
            PhoneOfCompany = Order.SU_Supplier.SU_PHONE;

            OrderItemsManager ordersItemsManager = new OrderItemsManager();
            OrderItem = ordersItemsManager.GetAllByOrderID(Order.OR_ID);
            Order.OR_LAST_MODIFIED = DateTime.Now;
            ordersManager.Update();
        }
        #endregion //constructors

        #region "Fields"
        private EditOrderView _editOrderView;
        private ProductCategoriesManager _productCategoriesManager;
        private RelayCommand _showProductsCommand;
        private ObservableCollection<ProductOnOrderViewModel> _productsCollection;
        private String _nameOfCompany;
        private String _streetOfCompany;
        private String _townOfCompany;
        private String _postCodeOfCompany;
        private String _phoneOfCompany;
        private IList<OE_OrderItem> _orderItem;
        private RelayCommand _addProductToOrderCommand;
        private OR_Order _order;
        private RelayCommand _closeCommand;
        private const decimal _VAT_PERCENTAGE_VALUE = 0.22m;
        #endregion //Fields

        #region "Properties"
        public RelayCommand CloseCommand
        {
            get
            {
                _closeCommand = new RelayCommand(Close);
                _closeCommand.CanUndo = (obj) => false;

                return _closeCommand;
            }
            set
            {
                _closeCommand = value;
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

        public String NameOfCompany
        {
            get
            {
                return _nameOfCompany;
            }
            set
            {
                _nameOfCompany = value;
                OnPropertyChanged("NameOfCompany");
            }
        }

        public String StreetOfCompany
        {
            get
            {
                return _streetOfCompany;
            }
            set
            {
                _streetOfCompany = value;
                OnPropertyChanged("StreetOfCompany");
            }
        }

        public String TownOfCompany
        {
            get
            {
                return _townOfCompany;
            }
            set
            {
                _townOfCompany = value;
                OnPropertyChanged("TownOfCompany");
            }
        }
        public String PostCodeOfCompany
        {
            get
            {
                return _postCodeOfCompany;
            }
            set
            {
                _postCodeOfCompany = value;
                OnPropertyChanged("PostCodeOfCompany");
            }
        }
        public String PhoneOfCompany
        {
            get
            {
                return _phoneOfCompany;
            }
            set
            {
                _phoneOfCompany = value;
                OnPropertyChanged("PhoneOfCompany");
            }
        }
        public IList<OE_OrderItem> OrderItem
        {
            get
            {
                return _orderItem;
            }
            set
            {
                _orderItem = value;
                OnPropertyChanged("OrderItem");
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
        #endregion //Properties

        #region "Methods"
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

        public void AddProductToOrder(Object obj)
        {
            

            ProductsManager productsManager = new ProductsManager();
            OrderItemsManager orderItemsManager = new OrderItemsManager();
            ProductsCollection = new ObservableCollection<ProductOnOrderViewModel>(
                from product in ProductsCollection
                where product.QuantityOnOrder > 0
                select product
            );
            foreach (var product in ProductsCollection)
            {
                OE_OrderItem newOrderItem = new OE_OrderItem()
                {
                    OE_ADDED = DateTime.Now,
                    OE_LAST_MODIFIED = DateTime.Now,
                    OE_OR_ID = Order.OR_ID,
                    OE_PR_ID = product.Product.PR_ID,
                    OE_QUANTITY = product.QuantityOnOrder,
                    OE_UNIT_PRICE = product.Product.PR_UNIT_PRICE,
                    OE_TOTAL_NETTO = product.QuantityOnOrder * product.Product.PR_UNIT_PRICE,
                    OE_VAT_RATE = _VAT_PERCENTAGE_VALUE
                };
                newOrderItem.OE_TOTAL_VAT = _VAT_PERCENTAGE_VALUE * newOrderItem.OE_TOTAL_NETTO;
                newOrderItem.OE_TOTAL_BRUTTO = newOrderItem.OE_TOTAL_NETTO + newOrderItem.OE_TOTAL_VAT;
                orderItemsManager.Add(newOrderItem);
                
                
                
            }
            ProductsCollection.Clear();
            OrderItem = orderItemsManager.GetAllByOrderID(Order.OR_ID);
            
            
        }

        public void Close(Object obj)
        {
            _editOrderView.Close();
        }
        #endregion //Methods
    }
}
