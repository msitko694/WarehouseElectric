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
    class AddNewProductViewModel : ViewModelBase
    {
         #region "Constructors"
        public AddNewProductViewModel(AddNewProductView addNewProductView)
        {
            _addNewProductView = addNewProductView;
            //ukrycie etykiet informujących o niepoprawnym uzupełnieniu formularza
            AddProductFailedNameVisibilityLabel = Visibility.Hidden;
            AddProductFailedCategoryVisibilityLabel = Visibility.Hidden;
            AddProductFailedUnitPriceVisibilityLabel = Visibility.Hidden;
            AddProductFailedDepotQuantityVisibilityLabel = Visibility.Hidden;
            AddProductFailedQuantityTypeVisibilityLabel = Visibility.Hidden;

            //wypełnienie listy kategori
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoryToAddComboBox = productCategoriesManager.GetAllName();

            //wypełnienie listy jednostek
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            ProductQuantityTypeToAddComboBox = quantityTypesManager.GetAllName();
           }
        #endregion //Constructors
        #region "Fields"
        private AddNewProductView _addNewProductView;
        private RelayCommand _addProductCommand;
        private String _ProductNameToAddTextBox;
        private String _ProductDepotQuantityToAddTextBox;
        private String _ProductUnitPriceToAddTextBox;
        private Visibility _AddProductFailedQuantityTypeVisibilityLabel;
        private Visibility _AddProductFailedCategoryVisibilityLabel;
        private Visibility _AddProductFailedDepotQuantityVisibilityLabel;
        private Visibility _AddProductFailedUnitPriceVisibilityLabel;
        private Visibility _addProductFailedNameVisibilityLabel;
        private IList<PR_Product> _listProductsToShow;
        private List<String> _ProductQuantityTypeToAddComboBox;
        private List<String> _ProductCategoryToAddComboBox;
        private String _SelectedProductQuantityTypeComboBox;
        #endregion //Fields
        #region "Properties"
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
        #endregion //Properties
        #region "Methods"
        public void AddProduct ( Object obj)
        {
            Int32 result,result2;
          //sprawdzenie poprawności wpisania nazwy produktu
            if (ProductNameToAddTextBox == null || ProductNameToAddTextBox == "" || ProductNameToAddTextBox == " ")
                       AddProductFailedNameVisibilityLabel = Visibility.Visible;
                  else
                        AddProductFailedNameVisibilityLabel = Visibility.Hidden;
            if (!Int32.TryParse(ProductDepotQuantityToAddTextBox, out result))
                AddProductFailedDepotQuantityVisibilityLabel = Visibility.Visible;
            else
                AddProductFailedDepotQuantityVisibilityLabel = Visibility.Hidden;

            if(!Int32.TryParse(ProductUnitPriceToAddTextBox,out result2))
                AddProductFailedUnitPriceVisibilityLabel=Visibility.Visible;
            else
                AddProductFailedUnitPriceVisibilityLabel=Visibility.Hidden;
            if (_SelectedProductQuantityTypeComboBox != null)
                AddProductFailedQuantityTypeVisibilityLabel = Visibility.Hidden;
            else
                AddProductFailedQuantityTypeVisibilityLabel = Visibility.Visible;

           if (AddProductFailedNameVisibilityLabel == Visibility.Hidden && AddProductFailedDepotQuantityVisibilityLabel ==Visibility.Hidden 
               && AddProductFailedUnitPriceVisibilityLabel == Visibility.Hidden && AddProductFailedQuantityTypeVisibilityLabel ==Visibility.Hidden)
            {
                QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
                QT_QuantityType QuantityType = quantityTypesManager.GetByName(SelectedProductQuantityTypeComboBox);
                ProductsManager ProductsManager = new ProductsManager();
                PR_Product newProduct = new PR_Product();
                newProduct.PR_NAME = ProductNameToAddTextBox;
                newProduct.PR_DEPOT_QUANTITY = result;
                newProduct.PR_UNIT_PRICE = result2;
                newProduct.PR_IS_ACTIVE = true;
                newProduct.PR_USED = true;
                newProduct.PR_QT_ID = QuantityType.QT_ID;
                newProduct.PR_PC_ID = 2;
                newProduct.PR_ADDED = DateTime.Now;
                newProduct.PR_LAST_MODIFIED = DateTime.Now;

                ProductsManager.Add(newProduct);

                MessageBox.Show("Produkt został dodany.");
                _addNewProductView.Close();
         }
        }
        #endregion //Methods
    }
}
