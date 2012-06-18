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
    class AddNewProductViewModel
    {
         #region "Constructors"
        public AddNewProductViewModel(AddNewProductView addNewProductView)
        {
            _addNewProductView = addNewProductView;
            //ukrycie etykiet informujących o niepoprawnym uzupełnieniu formularza
           // AddProductFailedNameVisibilityLabel = Visibility.Hidden;
           }
        #endregion //Constructors
        #region "Fields"
        private AddNewProductView _addNewProductView;
        private RelayCommand _addProductCommand;
        private String _ProductNameToAddTextBox;
        private Visibility _addProductFailedNameVisibilityLabel;
        private IList<PR_Product> _listProductsToShow;

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
                //OnPropertyChanged("ProductNameToAddTextBox");
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
                //OnPropertyChanged("AddProductFailedNameVisibilityLabel");
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
                //OnPropertyChanged("ListProductsToShow");
            }
        }
        #endregion //Properties
        #region "Methods"
        public void AddProduct ( Object obj)
        {
            Int32 result;
          //sprawdzenie poprawności wpisania nazwy dostawcy
            if (ProductNameToAddTextBox == null || ProductNameToAddTextBox == "" || ProductNameToAddTextBox == " ")
                       AddProductFailedNameVisibilityLabel = Visibility.Visible;
                  else
                        AddProductFailedNameVisibilityLabel = Visibility.Hidden;
           if (AddProductFailedNameVisibilityLabel == Visibility.Hidden )
            {

                ProductsManager ProductsManager = new ProductsManager();
                PR_Product newProduct = new PR_Product();
                newProduct.PR_NAME = ProductNameToAddTextBox;
                newProduct.PR_ADDED = DateTime.Now;
                newProduct.PR_LAST_MODIFIED = DateTime.Now;

                ProductsManager.Add(newProduct);

                MessageBox.Show("Dostawca został dodany");
                _addNewProductView.Close();
         }
        }
        #endregion //Methods
    }
}
