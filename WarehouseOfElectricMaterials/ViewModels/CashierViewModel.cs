using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;

using System.Collections.ObjectModel;

namespace WarehouseElectric.ViewModels
{
    public class CashierViewModel : ViewModelBase
    {
        #region "Contructors"
        public CashierViewModel(CashierView cashierView)
        {
            _customersManager = new CustomersManager();
            // część "Klienci" - docelowo do przeniesienia w dogodniejsze miejsce   
            IsEnabledButtonOpenCustomerFullView = false;
            _cashierView = cashierView;
            LoadCustomersList();

            // część "Magazyn"
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            ProductCategoriesToShow = productCategoriesManager.GetAllOnBaseLevel();
        }

        #endregion //Constructors

        #region "Fields"
        private CashierView _cashierView;
        private String _customerNameToSearch;
        private RelayCommand _userDeleteCommand;
        private RelayCommand _customerSearchCommand;
        private RelayCommand _addNewCustomerCommand;
        private RelayCommand _editCustomerCommand;
        private Boolean _isEnabledButtonOpenCustomerFullView;
        private ObservableCollection<CU_Customer> _listCustomersToShow;
        private RelayCommand _logOutCommand;
        private RelayCommand _openCustomerFullViewCommand;
        private RelayCommand _choosePanelCommand;
        private CU_Customer _selectedCustomer;
        private CustomersManager _customersManager;
        private IList<PC_ProductCategory> _productCategoriesToShow;
        #endregion //Fields

        #region "Properties"
        public Boolean IsEnabledButtonOpenCustomerFullView
        {
            get
            {
                return _isEnabledButtonOpenCustomerFullView;
            }
            set
            {
                _isEnabledButtonOpenCustomerFullView = value;
                OnPropertyChanged("IsEnabledButtonOpenCustomerFullView");
            }
        }
        public RelayCommand EditCustomerCommand
        {
            get
            {
                if(_editCustomerCommand == null)
                {
                    _editCustomerCommand = new RelayCommand((x) =>
                    {
                        if(SelectedCustomer != null)
                        {
                            Window newCustomerView = new NewCustomerView(this, SelectedCustomer.CU_ID);
                            newCustomerView.Show();
                        }
                    });
                    _editCustomerCommand.CanUndo = (obj) => false;
                }
                return _editCustomerCommand;
            }
            set
            {
                _choosePanelCommand = value;
            }

        }
        public RelayCommand ChoosePanelCommand
        {
            get
            {
                if(_choosePanelCommand == null)
                {
                    _choosePanelCommand = new RelayCommand((x) =>
                    {
                        Application.Current.MainWindow = new ChoosePanelView();
                        Application.Current.MainWindow.Show();
                        _cashierView.Close();
                    });
                    _choosePanelCommand.CanUndo = (obj) => false;
                }
                return _choosePanelCommand;
            }
            set
            {
                _choosePanelCommand = value;
            }

        }
        public RelayCommand AddNewCustomerCommand
        {
            get
            {
                if(_addNewCustomerCommand == null)
                {
                    _addNewCustomerCommand = new RelayCommand((x) =>
                    {
                        Window newCustomerView = new NewCustomerView(this);
                        newCustomerView.Show();
                    });
                    _addNewCustomerCommand.CanUndo = (obj) => false;
                }
                return _addNewCustomerCommand;
            }
            set
            {
                _addNewCustomerCommand = value;
            }

        }
        public RelayCommand UserDeleteCommand
        {
            get
            {
                if(_userDeleteCommand == null)
                {
                    _userDeleteCommand = new RelayCommand((x) =>
                    {
                        if(SelectedCustomer != null)
                        {
                            _customersManager.Delete(SelectedCustomer);
                            LoadCustomersList();
                        }
                    });
                    _userDeleteCommand.CanUndo = (obj) => false;
                }
                return _userDeleteCommand;
            }
            set
            {
                _userDeleteCommand = value;
            }
        }
        public RelayCommand OpenCustomerFullViewCommand
        {
            get
            {
                if(_openCustomerFullViewCommand == null)
                {
                    _openCustomerFullViewCommand = new RelayCommand(OpenCustomerFullView);
                    _openCustomerFullViewCommand.CanUndo = (obj) => false;
                }
                return _openCustomerFullViewCommand;
            }
            set
            {
                _openCustomerFullViewCommand = value;
            }
        }
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
                if(value != null)
                    IsEnabledButtonOpenCustomerFullView = true;
                else
                    IsEnabledButtonOpenCustomerFullView = false;
            }
        }
        public RelayCommand LogOutCommand
        {
            get
            {
                if(_logOutCommand == null)
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
        public RelayCommand CustomerSearchCommand
        {
            get
            {
                if(_customerSearchCommand == null)
                {
                    _customerSearchCommand = new RelayCommand(CustomerSearch);
                    _customerSearchCommand.CanUndo = (obj) => false;
                }
                return _customerSearchCommand;
            }
            set
            {
                _customerSearchCommand = value;
            }
        }
        public ObservableCollection<CU_Customer> ListCustomersToShow
        {
            get
            {
                return _listCustomersToShow;
            }
            set
            {
                _listCustomersToShow = value;
                OnPropertyChanged("ListCustomersToShow");
            }
        }
        public String CustomerNameToSearch
        {
            get
            {
                return _customerNameToSearch;
            }
            set
            {
                _customerNameToSearch = value;
                OnPropertyChanged("CustomerNameToSearch");
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
        #endregion //Properties

        #region "Methods"
        public void CustomerSearch(Object obj)
        {
            ListCustomersToShow.Clear();
            ListCustomersToShow = new ObservableCollection<CU_Customer>(_customersManager.GetByName(CustomerNameToSearch));
        }
        public void OpenCustomerFullView(Object obj)
        {
            CustomerFullView customerFullView = new CustomerFullView(SelectedCustomer.CU_ID);
            customerFullView.Show();

        }
        public void LogOut(Object obj)
        {
            SessionHelper.LogOut();
            Application.Current.MainWindow = new LoginView();
            Application.Current.MainWindow.Show();

            _cashierView.Close();

        }
        public void LoadCustomersList()
        {
            ListCustomersToShow = new ObservableCollection<CU_Customer>(_customersManager.GetAll());
            var bindingExpression = _cashierView.dataGridCustomers.GetBindingExpression(DataGrid.ItemsSourceProperty);
            bindingExpression.UpdateTarget();
        }
        #endregion //Methods
    }
}
