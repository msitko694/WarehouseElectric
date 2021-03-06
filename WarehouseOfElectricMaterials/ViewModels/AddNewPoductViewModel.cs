﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;

namespace WarehouseElectric.ViewModels
{
    public delegate void OnProductAddedHandler();
    class AddNewProductViewModel : ViewModelBase
    {
        #region events
        public event OnProductAddedHandler onProductAddedHandler;
        #endregion events
        #region "Constructors"
        public AddNewProductViewModel(AddNewProductView addNewProductView, CategoryViewModel categoryViewModel)
        {
            _addNewProductView = addNewProductView;
            CategoryViewModel = categoryViewModel;
            //ukrycie etykiet informujących o niepoprawnym uzupełnieniu formularza
            AddProductFailedNameVisibilityLabel = Visibility.Hidden;
            AddProductFailedCategoryVisibilityLabel = Visibility.Hidden;
            AddProductFailedUnitPriceVisibilityLabel = Visibility.Hidden;
            AddProductFailedDepotQuantityVisibilityLabel = Visibility.Hidden;
            AddProductFailedQuantityTypeVisibilityLabel = Visibility.Hidden;
            AddProductFailedVATVisibilityLabel = Visibility.Hidden;

            //wypełnienie listy kategori
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoryToAddComboBox = productCategoriesManager.GetAllName();

            //wypełnienie listy jednostek
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            ProductQuantityTypeToAddComboBox = quantityTypesManager.GetAllName();
            
            
           }
        public AddNewProductViewModel(AddNewProductView addNewProductView, CategoryViewModel categoryViewModel, Int32 flag)
        {
            _addNewProductView = addNewProductView;
            CategoryViewModel = categoryViewModel;
            //ukrycie etykiet informujących o niepoprawnym uzupełnieniu formularza
            AddProductFailedNameVisibilityLabel = Visibility.Hidden;
            AddProductFailedCategoryVisibilityLabel = Visibility.Hidden;
            AddProductFailedUnitPriceVisibilityLabel = Visibility.Hidden;
            AddProductFailedDepotQuantityVisibilityLabel = Visibility.Hidden;
            AddProductFailedQuantityTypeVisibilityLabel = Visibility.Hidden;
            AddProductFailedVATVisibilityLabel = Visibility.Hidden;

            //wypełnienie listy kategori
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoryToAddComboBox = productCategoriesManager.GetAllName();

            //wypełnienie listy jednostek
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            ProductQuantityTypeToAddComboBox = quantityTypesManager.GetAllName();
            /*
            InvoicesItemsManager invoicesItemsManager = new InvoicesItemsManager();
            IE_InvoicesItem item=invoicesItemsManager.Get(flag);

           PR_Product product = item.PR_Product;
              */
            ProductsManager productsManager = new ProductsManager();
            //PR_Product product = productsManager.Get(flag);
            Product = productsManager.Get(flag);
            ProductNameToAddTextBox = Product.PR_NAME;
            SelectedProductQuantityTypeComboBox = Product.QT_QuantityType.QT_NAME.ToString();
            //CategoryViewModel.RootCategories
            ProductDepotQuantityToAddTextBox="0";
            
            ProductUnitPriceToAddTextBox=Product.PR_UNIT_PRICE.ToString();
            
            //ProductVATToAddTextBox = item.IE_VAT_RATE.ToString();

        }
        #endregion //Constructors
        #region "Fields"
        private AddNewProductView _addNewProductView;
        private RelayCommand _addProductCommand;
        private String _ProductNameToAddTextBox;
        private String _ProductDepotQuantityToAddTextBox;
        private String _ProductUnitPriceToAddTextBox;
        private String _ProductVATToAddTextBox;
        private Visibility _AddProductFailedQuantityTypeVisibilityLabel;
        private Visibility _AddProductFailedCategoryVisibilityLabel;
        private Visibility _AddProductFailedDepotQuantityVisibilityLabel;
        private Visibility _AddProductFailedUnitPriceVisibilityLabel;
        private Visibility _addProductFailedNameVisibilityLabel;
        private Visibility _addProductFailedVATVisibilityLabel;
        private IList<PR_Product> _listProductsToShow;
        private List<String> _ProductQuantityTypeToAddComboBox;
        private List<String> _ProductCategoryToAddComboBox;
        private String _SelectedProductQuantityTypeComboBox;
        private PR_Product _product;
        #endregion //Fields
        #region "Properties"
        public PR_Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public RelayCommand AddProductCommand
        {
            get
            {
                _addProductCommand = new RelayCommand(AddProduct);
                _addProductCommand.CanUndo = (obj) => false;

                return _addProductCommand;
            }
            set
            {
                _addProductCommand = value;
            }
        }
        public String ProductNameToAddTextBox
        {
            get
            {
                return _ProductNameToAddTextBox;
            }
            set
            {
                _ProductNameToAddTextBox = value;
                OnPropertyChanged("ProductNameToAddTextBox");
            }
        }
        public String ProductDepotQuantityToAddTextBox
        {
            get
            {
                return _ProductDepotQuantityToAddTextBox;
            }
            set
            {
                _ProductDepotQuantityToAddTextBox = value;
                OnPropertyChanged("ProductDepotQuantityToAddTextBox");
            }
        }
        public String ProductUnitPriceToAddTextBox
        {
            get
            {
                return _ProductUnitPriceToAddTextBox;
            }
            set
            {
                _ProductUnitPriceToAddTextBox = value;
                OnPropertyChanged("ProductUnitPriceToAddTextBox");
            }
        }
        public String ProductVATToAddTextBox
        {
            get
            {
                return _ProductVATToAddTextBox;
            }
            set
            {
                _ProductVATToAddTextBox = value;
                OnPropertyChanged("ProductVATToAddTextBox");
            }
        }
        public Visibility AddProductFailedNameVisibilityLabel
        {
            get
            {
                return _addProductFailedNameVisibilityLabel;
            }
            set
            {
                _addProductFailedNameVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedNameVisibilityLabel");
            }
        }
        public Visibility AddProductFailedQuantityTypeVisibilityLabel
        {
            get
            {
                return _AddProductFailedQuantityTypeVisibilityLabel;
            }
            set
            {
                _AddProductFailedQuantityTypeVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedQuantityTypeVisibilityLabel");
            }
        }
        public Visibility AddProductFailedCategoryVisibilityLabel
        {
            get
            {
                return _AddProductFailedCategoryVisibilityLabel;
            }
            set
            {
                _AddProductFailedCategoryVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedCategoryVisibilityLabel");
            }
        }
        public Visibility AddProductFailedDepotQuantityVisibilityLabel
        {
            get
            {
                return _AddProductFailedDepotQuantityVisibilityLabel;
            }
            set
            {
                _AddProductFailedDepotQuantityVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedDepotQuantityVisibilityLabel");
            }
        }
        public Visibility AddProductFailedUnitPriceVisibilityLabel
        {
            get
            {
                return _AddProductFailedUnitPriceVisibilityLabel;
            }
            set
            {
                _AddProductFailedUnitPriceVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedUnitPriceVisibilityLabel");
            }
        }
        public Visibility AddProductFailedVATVisibilityLabel
        {
            get
            {
                return _addProductFailedVATVisibilityLabel;
            }
            set
            {
                _addProductFailedVATVisibilityLabel = value;
                OnPropertyChanged("AddProductFailedVATVisibilityLabel");
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
        public List<String> ProductQuantityTypeToAddComboBox {
            get
            {
        return _ProductQuantityTypeToAddComboBox;
    }
    set
{
    _ProductQuantityTypeToAddComboBox=value;
    OnPropertyChanged("ProductQuantityTypeToAddComboBox");
}
}
        public List<String> ProductCategoryToAddComboBox
        {
            get
            {
                return _ProductCategoryToAddComboBox;
            }
            set
            {
                _ProductCategoryToAddComboBox = value;
                OnPropertyChanged("ProductCategoryToAddComboBox");
            }
        }
        public String SelectedProductQuantityTypeComboBox
        {
            get
            {
                return _SelectedProductQuantityTypeComboBox;
            }
            set
            {
                _SelectedProductQuantityTypeComboBox = value;
                OnPropertyChanged("SelectedProductQuantityTypeComboBox");
            }
        }

        public CategoryViewModel CategoryViewModel { get; set; }

        #endregion //Properties
        #region "Methods"
        public void AddProduct ( Object obj)
        {
            Int32 result;//,result3;
            Double result2;
            int selectedCategoryId=2;
          //sprawdzenie poprawności wpisania nazwy produktu
            if (ProductNameToAddTextBox == null || ProductNameToAddTextBox == "" || ProductNameToAddTextBox == " ")
                       AddProductFailedNameVisibilityLabel = Visibility.Visible;
                  else
                        AddProductFailedNameVisibilityLabel = Visibility.Hidden;

            if (!Int32.TryParse(ProductDepotQuantityToAddTextBox, out result))
                AddProductFailedDepotQuantityVisibilityLabel = Visibility.Visible;
            else
                AddProductFailedDepotQuantityVisibilityLabel = Visibility.Hidden;

            if(!Double.TryParse(ProductUnitPriceToAddTextBox,out result2))
                AddProductFailedUnitPriceVisibilityLabel=Visibility.Visible;
            else
                AddProductFailedUnitPriceVisibilityLabel=Visibility.Hidden;

            if (SelectedProductQuantityTypeComboBox != null)
                AddProductFailedQuantityTypeVisibilityLabel = Visibility.Hidden;
            else
                AddProductFailedQuantityTypeVisibilityLabel = Visibility.Visible;
            if(Product == null)
            if (CategoryViewModel.GetSelectedCategory().ProductCategory != null)
            {
                selectedCategoryId = CategoryViewModel.GetSelectedCategory().ProductCategory.PC_ID;
                AddProductFailedCategoryVisibilityLabel = Visibility.Hidden;
            }
            else
                AddProductFailedCategoryVisibilityLabel = Visibility.Visible;
           /* if (!Int32.TryParse(ProductVATToAddTextBox, out result3))
                AddProductFailedVATVisibilityLabel = Visibility.Visible;
            else
                AddProductFailedVATVisibilityLabel = Visibility.Hidden;
            */
           if (AddProductFailedNameVisibilityLabel == Visibility.Hidden && AddProductFailedDepotQuantityVisibilityLabel ==Visibility.Hidden 
               && AddProductFailedUnitPriceVisibilityLabel == Visibility.Hidden && AddProductFailedQuantityTypeVisibilityLabel ==Visibility.Hidden
               && AddProductFailedCategoryVisibilityLabel==Visibility.Hidden && AddProductFailedVATVisibilityLabel==Visibility.Hidden)
            {
                QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
                QT_QuantityType QuantityType = quantityTypesManager.GetByName(SelectedProductQuantityTypeComboBox);
                ProductsManager productsManager = new ProductsManager();
                //InvoicesItemsManager InvoicesItemsManager = new InvoicesItemsManager();
                if (Product == null)
                {
                    Product = new PR_Product();
                    Product.PR_NAME = ProductNameToAddTextBox;
                    Product.PR_DEPOT_QUANTITY = result;
                    Product.PR_UNIT_PRICE = (decimal)result2;
                    Product.PR_IS_ACTIVE = true;
                    Product.PR_USED = true;
                    Product.PR_QT_ID = QuantityType.QT_ID;
                    Product.PR_PC_ID = selectedCategoryId;
                    Product.PR_ADDED = DateTime.Now;
                    Product.PR_LAST_MODIFIED = DateTime.Now;
                    productsManager.Add(Product);

                    MessageBox.Show("Produkt został dodany. ");
                }
                else
                {
                    PR_Product newProduct = new PR_Product();
                    newProduct = productsManager.Get(Product.PR_ID);
                    selectedCategoryId = Product.PR_PC_ID;

                    newProduct.PR_NAME = Product.PR_NAME;
                    newProduct.PR_DEPOT_QUANTITY = Product.PR_DEPOT_QUANTITY + result;
                    newProduct.PR_UNIT_PRICE = Product.PR_UNIT_PRICE;
                    newProduct.PR_IS_ACTIVE = true;
                    newProduct.PR_USED = true;
                    newProduct.PR_QT_ID = Product.PR_QT_ID;
                    newProduct.PR_PC_ID = Product.PR_PC_ID;
                    newProduct.PR_LAST_MODIFIED = DateTime.Now;
                    
                    productsManager.Update();
                    
                    MessageBox.Show("Produkt został dodany. ");
                }
               onProductAddedHandler();
                _addNewProductView.Close();
         }
        }
        #endregion //Methods
    }
}
