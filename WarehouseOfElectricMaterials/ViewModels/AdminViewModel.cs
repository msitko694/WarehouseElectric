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
            _adminView = adminView;

            FindWorkerByNameToAddUser = "Wprowadź nazwisko pracownika";
            
            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.Get(SessionHelper.userId);
            UsernameText = user.US_USERNAME; //Nazwa zalogowanego użytkownika


            WorkersManager workerManager = new WorkersManager();
            ListWorkersAll = workerManager.GetAll();

            AddUserFailedVisibility = Visibility.Hidden;
            AddUserFailedUserNameVisibility = Visibility.Hidden;
            AddUserFailedUserPasswordVisibility = Visibility.Hidden;


            //zakładka użytkownicy/zarządzanie
            FindUserToModificationTextBox = "Wprowadź nazwę użytkownika";

            ListUserToModificationDataGrid = usersManager.GetAll().ToList();

            SelectedUserToModifiedFailedVisibilityLabel = Visibility.Hidden;

            //zakładka pracownicy/nowy
            PositionsManager positionsManager = new PositionsManager();
            ListPositionsToAddWorkerComboBox = positionsManager.GetAllName();

            AddWorkerFailedWorkerPhoneVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerBirthDateVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerPeselVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerEmploymentDateVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerPositionSelectedVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerNameVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerSurnameVisibilityLabel = Visibility.Hidden;
        }

        #endregion //Constructors

        #region "Fields"

        private AdminView _adminView;
        private RelayCommand _logOutCommand;
        private RelayCommand _choosePanelCommand;
        private RelayCommand _addUserCommand;
        private RelayCommand _findWorkerToAddUserCommand;
        private RelayCommand _findUserToModificationCommand; //wyszukanie konkretnego uzytkownika
        private RelayCommand _modifyUserCommond; //modyfikacja użytkownika
        private RelayCommand _deleteUserCommond; //usuwanie użytkownika
        private RelayCommand _addWorkerCommond; //dodawanie nowego pracownika
        private IList<WO_Worker> _listWorkersAll;
        private List<US_User> _listUserToModificationDataGrid; //wyświetlenie w datagrid listy userów do modyfikacji(zakładka użytkownicy/zarządzanie)
        private List<String> _listPositionsToAddWorkerComboBox; //dodanie do combobox listy pozycji pracowników (zakładka pracownicy/nowy)
        private WO_Worker _workerSelectedToAddUser;
        private String _positionSelectedToAddWorker; //wybrana pozycja z comboboxa zakładka pracownicy/nowy 
        private US_User _userSelectedToModificationInDataGrid;
        private String _findWorkerByNameToAddUser;
        private String _userNameToAddNewUser;
        private String _userPasswordToAddNewUser;
        private String _usernameText; //nazwa zalogowanego użytkownika
        private String _findUserToModificationTextBox; //pobieranie nazwy do wyszukania użytkownika w zakładce modyfikacji użytkowników
        private String _newUsernameToModificationTextBox; //pobieranie nowej nazwy użytkownika
        private String _newUserPasswordToModificationTextBox; //pobieranie nowego hasła użytkownika
        private String _workerNameToAddTextBox; //pobieranie imienia pracownika (zakładka pracownicy/nowy)
        private String _workerSurnameToAddTextBox; //pobieranie nazwiska pracownik (zakładka pracownicy/nowy)
        private String _workerPhoneToAddTextBox; //pobieranei numeru telefonu prcaownika (zakładka pracownicy/nowy)
        private String _workerYearOfBirthdayToAddTextBox; //pobieranie roku urodzenia pracownik (zakładka pracownicy/nowy)
        private String _workerMonthOfBirthdayToAddTextBox; //pobieranie miesiąca urodzenia pracownik (zakładka pracownicy/nowy)
        private String _workerDayOfBirthdayToAddTextBox; //pobieranie miesiąca urodzenia pracownik (zakładka pracownicy/nowy)
        private String _workerPeselToAddTextBox; //pobieranie numeru pesel pracownik (zakładka pracownicy/nowy)
        private String _workerYearOfEmploymentToAddTextBox; //pobieranie roku zatrudnienia pracownika (zakładka pracownicy/nowy)
        private String _workerMonthOfEmploymentToAddTextBox; //pobieranie miesiąca zatrudnienia pracownika (zakładka pracownicy/nowy)
        private String _workerDayOfEmploymentToAddTextBox; //pobieranie dnia zatrudnienia pracownika (zakładka pracownicy/nowy)
        private bool _newUserIsAdmin;
        private bool _newUserIsCashier;
        private bool _newUserIsStorekeeper;
        private bool _newUserIsWorkerCheckBox; //checkbox odblokowujący możliwość wyboru pracownik przy dodawaniu nowego użytkownika
        private bool _unlockUserToModificationCheckBox; //checkbox odblokowywujacy możliwość modyfikowania użytkowników
        private bool _modifiedUserIsAdminCheckBox; //modyfikowany użytkownik będzie administratorem
        private bool _modifiedUserIsCashierCheckBox; //modyfikowany użytkownik będzie kasjerem
        private bool _modifiedUserIsStorekeeperCheckBox; //modyfikowany użytkownik będzie magazynierem
        private Visibility _addUserFailedVisibility;
        private Visibility _addUserFailedUserNameVisibility;
        private Visibility _addUserFailedUserPasswordVisibility;
        private Visibility _selectedUserToModifiedFailedVisibilityLabel; //wyświetlenie informacji o konieczności wyboru użytkownika przed przystąpieniem do modyfikacji
        private Visibility _addWorkerFailedWorkerPhoneVisibilityLabel; // wyświetlenie informacji o błędnym wprowadzeniu numeru telefonu przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerBirthDateVisibilityLabel; //wyświetlenie informacji o błędnym wprowadzeniu daty urodzenia przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerPeselVisibilityLabel; //wyświetlenie informacji o błędnym wprowadzeniu numeru pesel przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerEmploymentDateVisibilityLabel; //wyświetlenie informacji o błędnym wprowadzeniu daty zatrudnienia przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerPositionSelectedVisibilityLabel; //wyświetlenie informacji o błędnym wyborze pozycji przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerNameVisibilityLabel; //wyświetlenie informacji o błędnym wprowadzeniu imienia przy dodawaniu pracownika
        private Visibility _addWorkerFailedWorkerSurnameVisibilityLabel; //wyświetlenie informacji o błędnym wprowadzeniu nazwiska przy dodawaniu pracownika


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

        public RelayCommand FindUserToModificationCommand
        {
            get
            {
                _findUserToModificationCommand = new RelayCommand(FindUserToModification);
                _findUserToModificationCommand.CanUndo = (obj) => false;

                return _findUserToModificationCommand;
            }
            set
            {
                _findUserToModificationCommand = value;
            }
        }


        public RelayCommand ModifyUserCommond
        {
            get
            {
                _modifyUserCommond = new RelayCommand(ModifyUser);
                _modifyUserCommond.CanUndo = (obj) => false;

                return _modifyUserCommond;
            }
            set
            {
                _modifyUserCommond = value;
            }
        }

        public RelayCommand DeleteUserCommond
        {
            get
            {
                _deleteUserCommond = new RelayCommand(DeleteUser);
                _deleteUserCommond.CanUndo = (obj) => false;

                return _deleteUserCommond;
            }
            set
            {
                _deleteUserCommond = value;
            }
        }

        public RelayCommand AddWorkerCommond
        {
            get
            {
                _addWorkerCommond = new RelayCommand(AddWorker);
                _addWorkerCommond.CanUndo = (obj) => false;

                return _addWorkerCommond;
            }
            set
            {
                _addWorkerCommond = value;
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

        
        public List<US_User> ListUserToModificationDataGrid
        {
            get
            {
                return _listUserToModificationDataGrid;
            }
            set
            {
                _listUserToModificationDataGrid = value;
                OnPropertyChanged("ListUserToModificationDataGrid");
            }
        }

        public List<String> ListPositionsToAddWorkerComboBox
        {
            get
            {
                return _listPositionsToAddWorkerComboBox;
            }
            set
            {
                _listPositionsToAddWorkerComboBox = value;
                OnPropertyChanged("ListPositionsToAddWorkerComboBox");
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

        //zaznaczony użytkownik do modyfikacji w data grid
        public US_User UserSelectedToModificationInDataGrid
        {
            get
            {
                return _userSelectedToModificationInDataGrid;
            }
            set
            {
                _userSelectedToModificationInDataGrid = value;
                OnPropertyChanged("UserSelectedToModificationInDataGrid");
            }
        }

        public String PositionSelectedToAddWorker
        {
            get
            {
                return _positionSelectedToAddWorker;
            }
            set
            {
                _positionSelectedToAddWorker = value;
                OnPropertyChanged("PositionSelectedToAddWorker");
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

        //nazwa użytkownik do modyfikacji (zakładka użytkownicy/zarządzanie)
        public String FindUserToModificationTextBox
        {
            get
            {
                return _findUserToModificationTextBox;
            }
            set
            {
                _findUserToModificationTextBox = value;
                OnPropertyChanged("FindUserToModificationTextBox");
            }
        }

        // nowa nazwa użytkownik (zakładka użytkownicy/zarządzanie)
        public String NewUsernameToModificationTextBox
        {
            get
            {
                return _newUsernameToModificationTextBox;
            }
            set
            {
                _newUsernameToModificationTextBox = value;
                OnPropertyChanged("NewUsernameToModificationTextBox");
            }
        }

        public String NewUserPasswordToModificationTextBox
        {
            get
            {
                return _newUserPasswordToModificationTextBox;
            }
            set
            {
                _newUserPasswordToModificationTextBox = value;
                OnPropertyChanged("NewUserPasswordToModificationTextBox");
            }
        }

        public String WorkerNameToAddTextBox
        {
            get
            {
                return _workerNameToAddTextBox;
            }
            set
            {
                _workerNameToAddTextBox = value;
                OnPropertyChanged("WorkerNameToAddTextBox");
            }
        }

        public String WorkerSurnameToAddTextBox
        {
            get
            {
                return _workerSurnameToAddTextBox;
            }
            set
            {
                _workerSurnameToAddTextBox = value;
                OnPropertyChanged("WorkerSurnameToAddTextBox");
            }

        }

        public String WorkerPhoneToAddTextBox
        {
            get
            {
                return _workerPhoneToAddTextBox;
            }
            set
            {
                _workerPhoneToAddTextBox = value;
                OnPropertyChanged("WorkerPhoneToAddTextBox");
            }
        }

        public String WorkerYearOfBirthdayToAddTextBox
        {
            get
            {
                return _workerYearOfBirthdayToAddTextBox;
            }
            set
            {
                _workerYearOfBirthdayToAddTextBox = value;
                OnPropertyChanged("WorkerYearOfBirthdayToAddTextBox");
            }
        }

        public String WorkerMonthOfBirthdayToAddTextBox
        {
            get
            {
                return _workerMonthOfBirthdayToAddTextBox;
            }
            set
            {
                _workerMonthOfBirthdayToAddTextBox = value;
                OnPropertyChanged("WorkerMonthOfBirthdayToAddTextBox");
            }
        }

        public String WorkerDayOfBirthdayToAddTextBox
        {
            get
            {
                return _workerDayOfBirthdayToAddTextBox;
            }
            set
            {
                _workerDayOfBirthdayToAddTextBox = value;
                OnPropertyChanged("WorkerDayOfBirthdayToAddTextBox");
            }
        }

        public String WorkerPeselToAddTextBox
        {
            get
            {
                return _workerPeselToAddTextBox;
            }
            set
            {
                _workerPeselToAddTextBox = value;
                OnPropertyChanged("WorkerPeselToAddTextBox");
            }
        }

        public String WorkerYearOfEmploymentToAddTextBox
        {
            get
            {
                return _workerYearOfEmploymentToAddTextBox;
            }
            set
            {
                _workerYearOfEmploymentToAddTextBox = value;
                OnPropertyChanged("WorkerYearOfEmploymentToAddTextBox");
            }
        }

        public String WorkerMonthOfEmploymentToAddTextBox
        {
            get
            {
                return _workerMonthOfEmploymentToAddTextBox;
            }
            set
            {
                _workerMonthOfEmploymentToAddTextBox = value;
                OnPropertyChanged("WorkerMonthOfEmploymentToAddTextBox");
            }
        }

        public String WorkerDayOfEmploymentToAddTextBox
        {
            get
            {
                return _workerDayOfEmploymentToAddTextBox;
            }
            set
            {
                _workerDayOfEmploymentToAddTextBox = value;
                OnPropertyChanged("WorkerDayOfEmploymentToAddTextBox");
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

        public bool NewUserIsWorkerCheckBox
        {
            get
            {
                return _newUserIsWorkerCheckBox;
            }
            set
            {
                _newUserIsWorkerCheckBox = value;
                OnPropertyChanged("NewUserIsWorkerCheckBox");
            }
        }

        public bool UnlockUserToModificationCheckBox
        {
            get
            {
                return _unlockUserToModificationCheckBox;
            }
            set
            {
                _unlockUserToModificationCheckBox = value;
                OnPropertyChanged("UnlockUserToModificationCheckBox");
            }
        }

        public bool ModifiedUserIsAdminCheckBox
        {
            get
            {
                return _modifiedUserIsAdminCheckBox;
            }
            set
            {
                _modifiedUserIsAdminCheckBox = value;
                OnPropertyChanged("ModifiedUserIsAdminCheckBox");
            }
        }

        public bool ModifiedUserIsCashierCheckBox
        {
            get
            {
                return _modifiedUserIsCashierCheckBox;
            }
            set
            {
                _modifiedUserIsCashierCheckBox = value;
                OnPropertyChanged("ModifiedUserIsCashierCheckBox");
            }
        }

        public bool ModifiedUserIsStorekeeperCheckBox
        {
            get
            {
                return _modifiedUserIsStorekeeperCheckBox;
            }
            set
            {
                _modifiedUserIsStorekeeperCheckBox = value;
                OnPropertyChanged("ModifiedUserIsStorekeeperCheckBox");
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

        public Visibility SelectedUserToModifiedFailedVisibilityLabel
        {
            get
            {
                return _selectedUserToModifiedFailedVisibilityLabel;
            }
            set
            {
                _selectedUserToModifiedFailedVisibilityLabel = value;
                OnPropertyChanged("SelectedUserToModifiedFailedVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerPhoneVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerPhoneVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerPhoneVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerPhoneVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerBirthDateVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerBirthDateVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerBirthDateVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerBirthDateVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerPeselVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerPeselVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerPeselVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerPeselVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerEmploymentDateVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerEmploymentDateVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerEmploymentDateVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerEmploymentDateVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerPositionSelectedVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerPositionSelectedVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerPositionSelectedVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerPositionSelectedVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerNameVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerNameVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerNameVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerNameVisibilityLabel");
            }
        }

        public Visibility AddWorkerFailedWorkerSurnameVisibilityLabel
        {
            get
            {
                return _addWorkerFailedWorkerSurnameVisibilityLabel;
            }
            set
            {
                _addWorkerFailedWorkerSurnameVisibilityLabel = value;
                OnPropertyChanged("AddWorkerFailedWorkerSurnameVisibilityLabel");
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
            if (NewUserIsWorkerCheckBox == true)
            {
                if (WorkerSelectedToAddUser == null)
                    AddUserFailedVisibility = Visibility.Visible;
                else
                    AddUserFailedVisibility = Visibility.Hidden;
            }
            else
                AddUserFailedVisibility = Visibility.Hidden;

            if (UserNameToAddNewUser == null || UserNameToAddNewUser == "")
                AddUserFailedUserNameVisibility = Visibility.Visible;
            else
                AddUserFailedUserNameVisibility = Visibility.Hidden;

            if (UserPasswordToAddNewUser == null || UserPasswordToAddNewUser == "")
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
                
                if(NewUserIsWorkerCheckBox == true)
                    newUser.US_WO_ID = WorkerSelectedToAddUser.WO_ID;

                newUser.US_IS_ADMIN = NewUserIsAdmin;
                newUser.US_IS_CASHIER = NewUserIsCashier;
                newUser.US_IS_STOREKEEPER = NewUserIsStorekeeper;
                newUser.US_ADDED = DateTime.Now;
                newUser.US_LAST_MODIFIED = DateTime.Now;

                userManager.Add(newUser);

                //pobranie aktualnej listy uzytkowników do zakładki zarządzania użytkownikami
                ListUserToModificationDataGrid = userManager.GetAll().ToList();
            }
            


        }
        /////////////////////////////////////
        //zakładka uzytkownicy/zarządzanie//
        public void FindUserToModification(Object obj)
        {
            //powinno być tak ale jest problem z rzutowniem typów muszę jeszcze otym poczytać
            //ListUserToModificationDataGrid = userManager.GetByUserName(FindUserToModificationTextBox);
            UsersManager userManager = new UsersManager();
            ListUserToModificationDataGrid = (from user in userManager.GetAll()
                                              where user.US_USERNAME == FindUserToModificationTextBox 
                                              select user).ToList<US_User>();
            
        }

        public void ModifyUser(Object obj)
        {
            if (UserSelectedToModificationInDataGrid == null)
            {
                SelectedUserToModifiedFailedVisibilityLabel = Visibility.Visible;
            }
            else
                SelectedUserToModifiedFailedVisibilityLabel = Visibility.Hidden;

            if (SelectedUserToModifiedFailedVisibilityLabel != Visibility.Visible )
            {
                UsersManager userManager = new UsersManager();
                US_User user = userManager.GetByUserName(UserSelectedToModificationInDataGrid.US_USERNAME);
                
                if (NewUsernameToModificationTextBox != null && NewUsernameToModificationTextBox != "")
                    user.US_USERNAME = NewUsernameToModificationTextBox;

                if(NewUserPasswordToModificationTextBox != null && NewUserPasswordToModificationTextBox != "")
                    user.US_PASSWORD = NewUserPasswordToModificationTextBox;
                
                user.US_IS_ADMIN = ModifiedUserIsAdminCheckBox;
                user.US_IS_CASHIER = ModifiedUserIsCashierCheckBox;
                user.US_IS_STOREKEEPER = ModifiedUserIsStorekeeperCheckBox;
                user.US_LAST_MODIFIED = DateTime.Now;

                userManager.Update();


                ListUserToModificationDataGrid = userManager.GetAll().ToList();
            }
        }

        public void DeleteUser(Object obj)
        { 
            if (UserSelectedToModificationInDataGrid != null)
            {
                SelectedUserToModifiedFailedVisibilityLabel = Visibility.Hidden;
                UsersManager userManager = new UsersManager();
                US_User user = userManager.GetByUserName(UserSelectedToModificationInDataGrid.US_USERNAME);
                userManager.Delete(user);
                ListUserToModificationDataGrid = userManager.GetAll().ToList();
            }
            else
                SelectedUserToModifiedFailedVisibilityLabel = Visibility.Visible;

        }


        //zakładka pracwonicy/nowy
        public void AddWorker(Object obj)
        {
            Int32 result;
            Int64 resultInt64;
            String birthDate = null;
            String employmentDate = null;
            DateTime dateOfBirthday;
            DateTime dateOfEmployment;
            //sprawdzanie poprawności wprowadzeia numeru telefonu przy dodawaniu pracownika
            if (!Int32.TryParse(WorkerPhoneToAddTextBox, out result))
                AddWorkerFailedWorkerPhoneVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerPhoneVisibilityLabel = Visibility.Hidden;
            //sprawdzenie poprawnośći wprowadzenia daty urodzenia przy dodawaniu pracownika
            if (!Int32.TryParse(WorkerYearOfBirthdayToAddTextBox, out result) || WorkerYearOfBirthdayToAddTextBox.Length != 4
                || !Int32.TryParse(WorkerMonthOfBirthdayToAddTextBox, out result) || WorkerMonthOfBirthdayToAddTextBox.Length != 2
                || !Int32.TryParse(WorkerDayOfBirthdayToAddTextBox, out result) || WorkerDayOfBirthdayToAddTextBox.Length != 2)
               
                AddWorkerFailedWorkerBirthDateVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerBirthDateVisibilityLabel = Visibility.Hidden;

            //sprawdzenie poprawnośći wprowadzenia numeru pesel przy dodawaniu pracownika
            if (!Int64.TryParse(WorkerPeselToAddTextBox, out resultInt64) || WorkerPeselToAddTextBox.Length != 11)
                AddWorkerFailedWorkerPeselVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerPeselVisibilityLabel = Visibility.Hidden;
           
            //sprawdzenie poprawnośći wprowadzneia daty zatrudnienia 
            if (!Int32.TryParse(WorkerYearOfEmploymentToAddTextBox, out result) || WorkerYearOfEmploymentToAddTextBox.Length != 4
                || !Int32.TryParse(WorkerMonthOfEmploymentToAddTextBox, out result) || WorkerMonthOfEmploymentToAddTextBox.Length != 2
                || !Int32.TryParse(WorkerDayOfEmploymentToAddTextBox, out result) || WorkerDayOfEmploymentToAddTextBox.Length != 2)
                AddWorkerFailedWorkerEmploymentDateVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerEmploymentDateVisibilityLabel = Visibility.Hidden;

            //sprawdzenie poprawnośći wyboru stanowiska pracy
            if (PositionSelectedToAddWorker != null)
                AddWorkerFailedWorkerPositionSelectedVisibilityLabel = Visibility.Hidden;
            else
                AddWorkerFailedWorkerPositionSelectedVisibilityLabel = Visibility.Visible;
           
            //sprawdzenie poprawności wpisania imienia pracownika
            if (WorkerNameToAddTextBox == null || WorkerNameToAddTextBox == "" || WorkerNameToAddTextBox == " ")
                AddWorkerFailedWorkerNameVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerNameVisibilityLabel = Visibility.Hidden;

            //sprawdzenie poprawności wpisania nazwiska pracownika
            if (WorkerSurnameToAddTextBox == null || WorkerSurnameToAddTextBox == "" || WorkerSurnameToAddTextBox == " ")
                AddWorkerFailedWorkerSurnameVisibilityLabel = Visibility.Visible;
            else
                AddWorkerFailedWorkerSurnameVisibilityLabel = Visibility.Hidden;


            if (AddWorkerFailedWorkerPhoneVisibilityLabel == Visibility.Hidden && AddWorkerFailedWorkerBirthDateVisibilityLabel == Visibility.Hidden
                && AddWorkerFailedWorkerPeselVisibilityLabel == Visibility.Hidden && AddWorkerFailedWorkerEmploymentDateVisibilityLabel == Visibility.Hidden
                && AddWorkerFailedWorkerPositionSelectedVisibilityLabel == Visibility.Hidden && AddWorkerFailedWorkerNameVisibilityLabel == Visibility.Hidden
                && AddWorkerFailedWorkerSurnameVisibilityLabel == Visibility.Hidden)
            {
                birthDate = WorkerDayOfBirthdayToAddTextBox + "/" + WorkerMonthOfBirthdayToAddTextBox + "/" + WorkerYearOfBirthdayToAddTextBox;
                employmentDate = WorkerDayOfEmploymentToAddTextBox + "/" + WorkerMonthOfEmploymentToAddTextBox + "/" + WorkerYearOfEmploymentToAddTextBox;
                dateOfBirthday = Convert.ToDateTime(birthDate);
                dateOfEmployment = Convert.ToDateTime(employmentDate);
                PositionsManager positionManager = new PositionsManager();
                PO_Position position = positionManager.GetByPositionName(PositionSelectedToAddWorker);

                WorkersManager workerManager = new WorkersManager();
                WO_Worker newWorker = new WO_Worker();

                newWorker.WO_PO_ID = position.PO_ID;
                newWorker.WO_NAME = WorkerNameToAddTextBox;
                newWorker.WO_SURNAME = WorkerSurnameToAddTextBox;
                newWorker.WO_PHONE = WorkerPhoneToAddTextBox;
                newWorker.WO_PESEL = WorkerPeselToAddTextBox;
                newWorker.WO_BIRTH_DATE = dateOfBirthday;
                newWorker.WO_WORKING_SINCE = dateOfEmployment;
                newWorker.WO_ADDED = DateTime.Now;
                newWorker.WO_LAST_MODIFIED = DateTime.Now;

                birthDate = null;
                workerManager.Add(newWorker);
                MessageBox.Show("Pracownik został dodany");
            }
            
        }

        #endregion //Methods
    }
}
