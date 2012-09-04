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
    public delegate void OnSupplierAddedHandler();

    class AddNewSupplierViewModel : ViewModelBase
    {
        #region "events"

        public event OnSupplierAddedHandler OnSupplierAdded;

        #endregion

        #region "Constructors"
        public AddNewSupplierViewModel(AddNewSupplierView addNewSupplierView)
        {
            _addNewSupplierView = addNewSupplierView;
            //ukrycie etykiet informujących o niepoprawnym uzupełnieniu formularza
            AddSupplierFailedNameVisibilityLabel = Visibility.Hidden;
            AddSupplierFailedStreetVisibilityLabel = Visibility.Hidden;
            AddSupplierFailedCodeVisibilityLabel = Visibility.Hidden;
            AddSupplierFailedTownVisibilityLabel = Visibility.Hidden;
            AddSupplierFailedPhoneVisibilityLabel = Visibility.Hidden;
         }
        #endregion //Constructors
        #region "Fields"
        private AddNewSupplierView _addNewSupplierView;
        private RelayCommand _addSupplierCommand;
        private String _supplierNameToAddTextBox;
        private String _supplierStreetToAddTextBox;
        private String _supplierCode1ToAddTextBox;
        private String _supplierCode2ToAddTextBox;
        private String _supplierTownToAddTextBox;
        private String _supplierPhoneToAddTextBox;
        private Visibility _addSupplierFailedNameVisibilityLabel;
        private Visibility _addSupplierFailedStreetVisibilityLabel;
        private Visibility _addSupplierFailedCodeVisibilityLabel;
        private Visibility _addSupplierFailedTownVisibilityLabel;
        private Visibility _addSupplierFailedPhoneVisibilityLabel;
        private IList<SU_Supplier> _listSuppliersToShow;

        #endregion //Fields
        #region "Properties"
        public RelayCommand AddSupplierCommand
        {
            get
            {
                _addSupplierCommand = new RelayCommand(AddSupplier);
                _addSupplierCommand.CanUndo = (obj) => false;

                return _addSupplierCommand;
            }
            set
            {
                _addSupplierCommand = value;
            }
        }
        public String SupplierNameToAddTextBox
        {
            get
            {
                return _supplierNameToAddTextBox;
            }
            set
            {
                _supplierNameToAddTextBox = value;
                OnPropertyChanged("SupplierNameToAddTextBox");
            }
        }
        public String SupplierStreetToAddTextBox
        {
            get
            {
                return _supplierStreetToAddTextBox;
            }
            set
            {
                _supplierStreetToAddTextBox = value;
                OnPropertyChanged("SupplierStreetToAddTextBox");
            }
        }
        public String SupplierCode1ToAddTextBox
        {
            get
            {
                return _supplierCode1ToAddTextBox;
            }
            set
            {
                _supplierCode1ToAddTextBox = value;
                OnPropertyChanged("SupplierCode1ToAddTextBox");
            }
        }
        public String SupplierCode2ToAddTextBox
        {
            get
            {
                return _supplierCode2ToAddTextBox;
            }
            set
            {
                _supplierCode2ToAddTextBox = value;
                OnPropertyChanged("SupplierCode2ToAddTextBox");
            }
        }
        public String SupplierTownToAddTextBox
        {
            get
            {
                return _supplierTownToAddTextBox;
            }
            set
            {
                _supplierTownToAddTextBox = value;
                OnPropertyChanged("SupplierTownToAddTextBox");
            }
        }
        public String SupplierPhoneToAddTextBox
        {
            get
            {
                return _supplierPhoneToAddTextBox;
            }
            set
            {
                _supplierPhoneToAddTextBox = value;
                OnPropertyChanged("SupplierPhoneToAddTextBox");
            }
        }
        public Visibility AddSupplierFailedNameVisibilityLabel
        {
            get
            {
                return _addSupplierFailedNameVisibilityLabel;
            }
            set
            {
                _addSupplierFailedNameVisibilityLabel = value;
                OnPropertyChanged("AddSupplierFailedNameVisibilityLabel");
            }
        }
        public Visibility AddSupplierFailedStreetVisibilityLabel
        {
            get
            {
                return _addSupplierFailedStreetVisibilityLabel;
            }
            set
            {
                _addSupplierFailedStreetVisibilityLabel = value;
                OnPropertyChanged("AddSupplierFailedStreetVisibilityLabel");
            }
        }
        public Visibility AddSupplierFailedCodeVisibilityLabel
        {
            get
            {
                return _addSupplierFailedCodeVisibilityLabel;
            }
            set
            {
                _addSupplierFailedCodeVisibilityLabel = value;
                OnPropertyChanged("AddSupplierFailedCodeVisibilityLabel");
            }
        }
        public Visibility AddSupplierFailedTownVisibilityLabel
        {
            get
            {
                return _addSupplierFailedTownVisibilityLabel;
            }
            set
            {
                _addSupplierFailedTownVisibilityLabel = value;
                OnPropertyChanged("AddSupplierFailedTownVisibilityLabel");
            }
        }
        public Visibility AddSupplierFailedPhoneVisibilityLabel
        {
            get
            {
                return _addSupplierFailedPhoneVisibilityLabel;
            }
            set
            {
                _addSupplierFailedPhoneVisibilityLabel = value;
                OnPropertyChanged("AddSupplierFailedPhoneVisibilityLabel");
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
        #endregion //Properties
        #region "Methods"
        public void AddSupplier ( Object obj)
        {
            Int32 result;
          //sprawdzenie poprawności wpisania nazwy dostawcy
            if (SupplierNameToAddTextBox == null || SupplierNameToAddTextBox == "" || SupplierNameToAddTextBox == " ")
                       AddSupplierFailedNameVisibilityLabel = Visibility.Visible;
                  else
                        AddSupplierFailedNameVisibilityLabel = Visibility.Hidden;
          //sprawdzenie poprawności wpisania ulicy
                if (SupplierStreetToAddTextBox == null || SupplierStreetToAddTextBox == "" || SupplierStreetToAddTextBox == " ")
                        AddSupplierFailedStreetVisibilityLabel = Visibility.Visible;
                  else
                        AddSupplierFailedStreetVisibilityLabel = Visibility.Hidden;
           //sprawdzanie poprawności wprowadzenia kodu pocztowego
            if (!Int32.TryParse(SupplierCode1ToAddTextBox, out result) || (!Int32.TryParse(SupplierCode2ToAddTextBox, out result)))
                        AddSupplierFailedCodeVisibilityLabel = Visibility.Visible;
                  else
                        AddSupplierFailedCodeVisibilityLabel = Visibility.Hidden;
           //sprawdzenie poprawności wpisania miejscowości
             if (SupplierTownToAddTextBox == null || SupplierTownToAddTextBox == "" || SupplierTownToAddTextBox == " ")
                       AddSupplierFailedTownVisibilityLabel = Visibility.Visible;
                  else
                        AddSupplierFailedTownVisibilityLabel = Visibility.Hidden;       
            //sprawdzanie poprawności wprowadzenia numeru telefonu przy dodawaniu pracownika
            if (!Int32.TryParse(SupplierPhoneToAddTextBox, out result))
                       AddSupplierFailedPhoneVisibilityLabel = Visibility.Visible;
                  else
                        AddSupplierFailedPhoneVisibilityLabel = Visibility.Hidden;
            if (AddSupplierFailedNameVisibilityLabel == Visibility.Hidden && AddSupplierFailedStreetVisibilityLabel == Visibility.Hidden
                && AddSupplierFailedCodeVisibilityLabel == Visibility.Hidden && AddSupplierFailedTownVisibilityLabel == Visibility.Hidden 
                && AddSupplierFailedPhoneVisibilityLabel == Visibility.Hidden)
            {

                SuppliersManager suppliersManager = new SuppliersManager();
                SU_Supplier newSupplier = new SU_Supplier();
                newSupplier.SU_NAME = SupplierNameToAddTextBox;
                newSupplier.SU_STREET = SupplierStreetToAddTextBox;
                newSupplier.SU_POST_CODE = SupplierCode1ToAddTextBox+"-"+SupplierCode2ToAddTextBox;
                newSupplier.SU_TOWN = SupplierTownToAddTextBox;
                newSupplier.SU_PHONE = SupplierPhoneToAddTextBox;
                newSupplier.SU_ADDED = DateTime.Now;
                newSupplier.SU_LAST_MODIFIED = DateTime.Now;

                suppliersManager.Add(newSupplier);

                MessageBox.Show("Dostawca został dodany");
                OnSupplierAdded();
                _addNewSupplierView.Close();
         }
        }
        #endregion //Methods
    }
}