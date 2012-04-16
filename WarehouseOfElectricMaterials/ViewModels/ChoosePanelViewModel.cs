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
    class ChoosePanelViewModel:ViewModelBase
    {
        #region "Constructors"
        public ChoosePanelViewModel(ChoosePanelView choosePanelView)
        {
            _choosePanelView = choosePanelView;
           
            //przyciski są najpierw wyłączane - zapytanie do bazy trwa określoną ilość czasu, w dodatku idzie je uniemożliwić
            CashierPanelIsEnabled = false;
            StorekeeperPanelIsEnabled = false;
            AdminPanelIsEnabled = false;

            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.Get(SessionHelper.userId);

            if (user.US_IS_CASHIER)
                CashierPanelIsEnabled = true;

            if (user.US_IS_STOREKEEPER)
                StorekeeperPanelIsEnabled = true;
            
            if (user.US_IS_ADMIN)
                AdminPanelIsEnabled = true;

            UsernameText = user.US_USERNAME;
        }

        #endregion //Constructors

        #region "Fields"

        private ChoosePanelView _choosePanelView;
        private Boolean _cashierPanelIsEnabled;
        private Boolean _storekeeperPanelIsEnabled;
        private Boolean _adminPanelIsEnabled;
        private String _usernameText;
        private RelayCommand _goCashierPanelCommand;
        private RelayCommand _goStorekeeperPanelCommand;        
        private RelayCommand _goAdminPanelCommand;
        private RelayCommand _logOutCommand;

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

        public RelayCommand GoCashierPanelCommand
        {
            get
            {
                if (_goCashierPanelCommand == null)
                {
                    _goCashierPanelCommand = new RelayCommand(GoCashierPanel);
                    _goCashierPanelCommand.CanUndo = (obj) => false;
                }
                return _goCashierPanelCommand;
            }
            set
            {
                _goCashierPanelCommand = value;
            }
        }

        public RelayCommand GoStorekeeperPanelCommand
        {
            get
            {
                if (_goStorekeeperPanelCommand == null)
                {
                    _goStorekeeperPanelCommand = new RelayCommand(GoStorekeeperPanel);
                    _goStorekeeperPanelCommand.CanUndo = (obj) => false;
                }
                return _goStorekeeperPanelCommand;
            }
            set
            {
                _goStorekeeperPanelCommand = value;
            }
        }

        public RelayCommand GoAdminPanelCommand
        {
            get
            {
                if (_goAdminPanelCommand == null)
                {
                    _goAdminPanelCommand = new RelayCommand(GoAdminPanel);
                    _goAdminPanelCommand.CanUndo = (obj) => false;
                }
                return _goAdminPanelCommand;
            }
            set
            {
                _goAdminPanelCommand = value;
            }
        }

        public Boolean CashierPanelIsEnabled
        {
            get
            {
                return _cashierPanelIsEnabled;
            }
            set
            {
                _cashierPanelIsEnabled = value;
                OnPropertyChanged("CashierPanelIsEnabled");
            }
        }

        public Boolean StorekeeperPanelIsEnabled
        {
            get
            {
                return _storekeeperPanelIsEnabled;
            }
            set
            {
                _storekeeperPanelIsEnabled = value;
                OnPropertyChanged("StorekeeperPanelIsEnabled");
            }
        }

        public Boolean AdminPanelIsEnabled
        {
            get
            {
                return _adminPanelIsEnabled;
            }
            set
            {
                _adminPanelIsEnabled = value;
                OnPropertyChanged("AdminPanelIsEnabled");
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
        public void GoCashierPanel(Object obj)
        {
            Application.Current.MainWindow = new CashierView();
            Application.Current.MainWindow.Show();
            _choosePanelView.Close();
        }
        public void GoStorekeeperPanel(Object obj)
        {
            Application.Current.MainWindow = new StorekeeperView();
            Application.Current.MainWindow.Show();
            _choosePanelView.Close();
        }
        public void GoAdminPanel(Object obj)
        {
            Application.Current.MainWindow = new AdminView();
            Application.Current.MainWindow.Show();
            _choosePanelView.Close();
        }
        public void LogOut(Object obj)
        {
            SessionHelper.LogOut();

            Application.Current.MainWindow = new LoginView();
            Application.Current.MainWindow.Show();
            _choosePanelView.Close();
        }

        #endregion //Methods
    }
}
