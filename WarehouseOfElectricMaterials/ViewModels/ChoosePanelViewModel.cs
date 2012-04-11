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
        private Boolean _CashierPanelIsEnabled;
        private Boolean _StorekeeperPanelIsEnabled;
        private Boolean _AdminPanelIsEnabled;
        private String _UsernameText;
        private RelayCommand _GoCashierPanelCommand;
        private RelayCommand _GoAdminPanelCommand;
        private RelayCommand _LogOutCommand;

        #endregion //Fields

        #region "Properties"

        public RelayCommand LogOutCommand
        {
            get
            {
                if (_LogOutCommand == null)
                {
                    _LogOutCommand = new RelayCommand(LogOut);
                    _LogOutCommand.CanUndo = (obj) => false;
                }
                return _LogOutCommand;
            }
            set
            {
                _GoAdminPanelCommand = value;
            }
        }

        public RelayCommand GoCashierPanelCommand
        {
            get
            {
                if (_GoCashierPanelCommand == null)
                {
                    _GoCashierPanelCommand = new RelayCommand(GoCashierPanel);
                    _GoCashierPanelCommand.CanUndo = (obj) => false;
                }
                return _GoCashierPanelCommand;
            }
            set
            {
                _GoCashierPanelCommand = value;
            }
        }

        public RelayCommand GoAdminPanelCommand
        {
            get
            {
                if (_GoAdminPanelCommand == null)
                {
                    _GoAdminPanelCommand = new RelayCommand(GoAdminPanel);
                    _GoAdminPanelCommand.CanUndo = (obj) => false;
                }
                return _GoAdminPanelCommand;
            }
            set
            {
                _GoAdminPanelCommand = value;
            }
        }

        public Boolean CashierPanelIsEnabled
        {
            get
            {
                return _CashierPanelIsEnabled;
            }
            set
            {
                _CashierPanelIsEnabled = value;
                OnPropertyChanged("CashierPanelIsEnabled");
            }
        }

        public Boolean StorekeeperPanelIsEnabled
        {
            get
            {
                return _StorekeeperPanelIsEnabled;
            }
            set
            {
                _StorekeeperPanelIsEnabled = value;
                OnPropertyChanged("StorekeeperPanelIsEnabled");
            }
        }

        public Boolean AdminPanelIsEnabled
        {
            get
            {
                return _AdminPanelIsEnabled;
            }
            set
            {
                _AdminPanelIsEnabled = value;
                OnPropertyChanged("AdminPanelIsEnabled");
            }
        }

        public String UsernameText
        {
            get
            {
                return _UsernameText;
            }
            set
            {
                _UsernameText = value;
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
