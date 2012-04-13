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
    class AdminViewModel:ViewModelBase
    {
        #region "Constructors"
        public AdminViewModel(AdminView adminView)
        {
            //przycisk wyszukiwania pracowników w dodawaniu nowego użytkownika nie jest wciśnięty;
            _adminView = adminView;

            FindWorkerByNameToAddUser = "Wprowadź nazwisko pracownika";

            WorkersManager workerManager = new WorkersManager();
            ListWorkersAll = workerManager.GetAll();
            AddUserFailedVisibility = Visibility.Hidden;
            AddUserFailedUserNameVisibility = Visibility.Hidden;
            AddUserFailedUserPasswordVisibility = Visibility.Hidden;
        }

        #endregion //Constructors

        #region "Fields"

        private AdminView _adminView;
        private RelayCommand _logOutCommand;
        private RelayCommand _choosePanelCommand;
        private RelayCommand _addUserCommand;
        private RelayCommand _findWorkerToAddUserCommand;
        private IList<WO_Worker> _listWorkersAll;
        private WO_Worker _workerSelectedToAddUser; 
        private String _findWorkerByNameToAddUser;
        private String _userNameToAddNewUser;
        private String _userPasswordToAddNewUser;
        private bool _newUserIsAdmin;
        private bool _newUserIsCashier;
        private bool _newUserIsStorekeeper;
        private Visibility _addUserFailedVisibility;
        private Visibility _addUserFailedUserNameVisibility;
        private Visibility _addUserFailedUserPasswordVisibility;

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

        public RelayCommand AddUserCommand
        {
            get
            {
                _addUserCommand = new RelayCommand(AddUser);
                _addUserCommand.CanUndo = (obj) => false;

                return _addUserCommand;
            }
            set
            {
                _addUserCommand = value;
            }
        }


        public RelayCommand FindWorkerToAddUserCommand
        {
            get
            {
                _findWorkerToAddUserCommand = new RelayCommand(FindWorkerToAddUser);
                _findWorkerToAddUserCommand.CanUndo = (obj) => false;

                return _findWorkerToAddUserCommand;
            }
            set
            {
                _findWorkerToAddUserCommand = value;
            }
        }


        public IList<WO_Worker> ListWorkersAll
        {
            get
            {
                return _listWorkersAll;
            }
            set
            {
                _listWorkersAll = value;
                OnPropertyChanged("ListWorkersAll");
            }
        }


        //zaznaczony pracownik w data gridzie
        public WO_Worker WorkerSelectedToAddUser
        {
            get
            {
                return _workerSelectedToAddUser;
            }
            set
            {
                _workerSelectedToAddUser = value;
                OnPropertyChanged("WorkerSelectedToAddUser");
            }
        }


        public String FindWorkerByNameToAddUser
        {
            get
            {
                return _findWorkerByNameToAddUser;
            }
            set
            {
                _findWorkerByNameToAddUser = value;
                OnPropertyChanged("FindWorkerByNameToAddUser");
            }
        }


        public String UserNameToAddNewUser
        {
            get
            {
                return _userNameToAddNewUser;
            }
            set
            {
                _userNameToAddNewUser = value;
                OnPropertyChanged("UserNameToAddNewUser");
            }
        }

        public String UserPasswordToAddNewUser
        {
            get
            {
                return _userPasswordToAddNewUser;
            }
            set
            {
                _userPasswordToAddNewUser = value;
                OnPropertyChanged("UserPasswordToAddNewUser");
            }
        }


        public bool NewUserIsAdmin
        {
            get
            {
                return _newUserIsAdmin;
            }
            set
            {
                _newUserIsAdmin = value;
                OnPropertyChanged("NewUserIsAdmin");
            }
        }

        public bool NewUserIsCashier
        {
            get
            {
                return _newUserIsCashier;
            }
            set
            {
                _newUserIsCashier = value;
                OnPropertyChanged("NewUserIsCashier");
            }
        }

        public bool NewUserIsStorekeeper
        {
            get
            {
                return _newUserIsStorekeeper;
            }
            set
            {
                _newUserIsStorekeeper = value;
                OnPropertyChanged("NewUserIsStorekeeper");
            }
        }


        public Visibility AddUserFailedVisibility
        {
            get
            {
                return _addUserFailedVisibility;
            }
            set
            {
                _addUserFailedVisibility = value;
                OnPropertyChanged("AddUserFailedVisibility");
            }
        }

        public Visibility AddUserFailedUserNameVisibility
        {
            get
            {
                return _addUserFailedUserNameVisibility;
            }
            set
            {
                _addUserFailedUserNameVisibility = value;
                OnPropertyChanged("AddUserFailedUserNameVisibility");
            }
        }

        public Visibility AddUserFailedUserPasswordVisibility
        {
            get
            {
                return _addUserFailedUserPasswordVisibility;
            }
            set
            {
                _addUserFailedUserPasswordVisibility = value;
                OnPropertyChanged("AddUserFailedUserPasswordVisibility");
            }
        }


        #endregion //Properties

        #region "Methods"


        public void LogOut(Object obj)
        {
            SessionHelper.LogOut();
            Application.Current.MainWindow = new LoginView();
            Application.Current.MainWindow.Show();
            _adminView.Close();
            
        }

        public void ChoosePanel(Object obj)
        {

            Application.Current.MainWindow = new ChoosePanelView();
            Application.Current.MainWindow.Show();
            _adminView.Close();

        }

        public void FindWorkerToAddUser(Object obj)
        {
            WorkersManager workerManager = new WorkersManager();
            ListWorkersAll = workerManager.GetByWorkerSurname(FindWorkerByNameToAddUser);
            
            
        }

        public void AddUser(Object obj)
        {
            if (WorkerSelectedToAddUser == null)
                AddUserFailedVisibility = Visibility.Visible;
            else
                AddUserFailedVisibility = Visibility.Hidden;

            if (UserNameToAddNewUser == null)
                AddUserFailedUserNameVisibility = Visibility.Visible;
            else
                AddUserFailedUserNameVisibility = Visibility.Hidden;

            if (UserPasswordToAddNewUser == null)
                AddUserFailedUserPasswordVisibility = Visibility.Visible;
            else
                AddUserFailedUserPasswordVisibility = Visibility.Hidden;

            if (AddUserFailedVisibility != Visibility.Visible && AddUserFailedUserNameVisibility != Visibility.Visible
                && AddUserFailedUserPasswordVisibility != Visibility.Visible)
            {
                UsersManager userManager = new UsersManager();
                US_User newUser = new US_User();

                newUser.US_USERNAME = UserNameToAddNewUser;
                newUser.US_PASSWORD = UserPasswordToAddNewUser;
                newUser.US_WO_ID = WorkerSelectedToAddUser.WO_ID;
                newUser.US_IS_ADMIN = NewUserIsAdmin;
                newUser.US_IS_CASHIER = NewUserIsCashier;
                newUser.US_IS_STOREKEEPER = NewUserIsStorekeeper;
                newUser.US_ADDED = DateTime.Now;
                newUser.US_LAST_MODIFIED = DateTime.Now;

                userManager.Add(newUser);
            }
            

        }


        #endregion //Methods
    }
}
