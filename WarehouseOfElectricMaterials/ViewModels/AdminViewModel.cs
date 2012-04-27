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

            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.Get(SessionHelper.userId);
            UsernameText = user.US_USERNAME; //Nazwa zalogowanego użytkownika

            /////////////////////////////
            //zakładka uzytkownicy/nowy//
            FindWorkerByNameToAddUser = "Wprowadź nazwisko pracownika";
            
            //ukrycie labelek z informacjami o błędach przy dodawaniu użytkownika
            AddUserFailedVisibility = Visibility.Hidden;
            AddUserFailedUserNameVisibility = Visibility.Hidden;
            AddUserFailedUserPasswordVisibility = Visibility.Hidden;

            
            ////////////////////////////////////
            //zakładka użytkownicy/zarządzanie//
            FindUserToModificationTextBox = "Wprowadź nazwę użytkownika";

            //ukrycie labelki z informacją o nie wybraniu użytkownika przy modyfikacji
            SelectedUserToModifiedFailedVisibilityLabel = Visibility.Hidden;

            ////////////////////////////
            //zakładka pracownicy/nowy/
            PositionsManager positionsManager = new PositionsManager();
            ListPositionsToAddWorkerComboBox = positionsManager.GetAllName();

            //ukrycie labelek z informacjami o błędach przy dodawaniu pracownika
            AddWorkerFailedWorkerPhoneVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerBirthDateVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerPeselVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerEmploymentDateVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerPositionSelectedVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerNameVisibilityLabel = Visibility.Hidden;
            AddWorkerFailedWorkerSurnameVisibilityLabel = Visibility.Hidden;

            ///////////////////////////////////
            //zakładka pracownicy/zarządzanie//
            
            //ukrycie labelek z informacjami o błędach przy modyfikacji pracowników
            ModifyWorkerFailedPhoneVisibilityLabel = Visibility.Hidden;
            ModifyWorkerFailedSelectedWorkerVisibilityLabel = Visibility.Hidden;
            ModifyWorkerFailedBirthDateVisibilityLabel = Visibility.Hidden;
            ModifyWorkerFailedEmploymentDateVisibilityLabel = Visibility.Hidden;
            ModifyWorkerFailedPeselVisibilityLabel = Visibility.Hidden;

            //zakładka typy wysyłki/ jednostki produktów
            
            //ukrycie labelek informujących i błędach przy dodawanou typu wysyłki oraz jednostki
            AddNewSpeditionTypeFailedVisibilityLabel = Visibility.Hidden;
            AddNewQuantityTypeFailedVisibilityLabel = Visibility.Hidden;
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
        private RelayCommand _findWorkerToModifyButtonCommand; //wyszukanie pracownika do modyfikacji (zakładka pracownicy/zarządzanie) 
        private RelayCommand _modifyWorkerButtonCommond; //modyfikacja pracownika (zakładka pracownicy/zarządzanie)
        private RelayCommand _addNewSpeditionTypeButtonCommand; // dodanie nowego typu wysyłki (zakładka typy wysyłki);
        private RelayCommand _addNewQuantityTypeButtonCommand; // dodanie nowej jednostki(zakładka jednostki produktów);
        private RelayCommand _refreshListOfWorkerToAddUserButtonCommand; // odświeżanie listy pracowników (zkładka użytkownicy/nowy)
        private RelayCommand _refreshListOfUserToModifyButtonCommand; //odświeżenie listy użytkowników do modyfikacji(zakładka użytkownicy/zarządzanie)
        private RelayCommand _refreshListOfWorkerToModifyButtonCommand; //odświeżenie listy pracowników do modyfikacji(zakładka pracownicy/zarządzanie)
        private RelayCommand _refreshListOfSpeditionTypesButtonCommand; //odświeżenie listy z typami wysyłki(zakładka typy wysyłki)
        private RelayCommand _refreshListOfQuantityTypesButtonCommand; //odświeżenie listy z jednostkami produktów(zakładka jednostki produktów)
        private IList<WO_Worker> _listWorkersAll;
        private IList<WO_Worker> _listAllWorkersToModifyDataGrid; //wyświetlenie w datagrid listy pracowników (zakładka pracownicy/zarządzanie)
        private List<US_User> _listUserToModificationDataGrid; //wyświetlenie w datagrid listy userów do modyfikacji(zakładka użytkownicy/zarządzanie)
        private List<String> _listPositionsToAddWorkerComboBox; //dodanie do combobox listy pozycji pracowników (zakładka pracownicy/nowy)
        private List<SP_Spedition> _listSpeditionsTypeDataGrid; // wyświetlenie listy typów wysyłki w zakładce (typy wysyłki/ jednostki produktów)
        private List<QT_QuantityType> _listQuantityTypeDataGrid; // wyświetlenie listy jednostek produktów w zakładce (typy wysyłki/ jednostki produktów)
        private WO_Worker _workerSelectedToAddUser;
        private WO_Worker _workerSelectedToModifyInDataGrid; //wybrany pracownik do modyfikacji (zakładka pracownicy/zarządzanie)
        private String _positionSelectedToAddWorker; //wybrana pozycja z comboboxa zakładka pracownicy/nowy
        private String _positionSelectedToModifyWorker; //wybrana pozycja z comboboxa zakładka pracownicy/zarządzanie 
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
        private String _findWorkerByNameToModifyTextBox; //wyszukiwanie pracaownik do modyfikacji(zakładka pracownicy/zarządzanie)
        private String _newWorkerNameTextBox; //pobranie nowego imienia pracownika (zakładka pracownicy/modyfikacja)
        private String _newWorkerSurnameTextBox; //pobranie nowego nazwiska pracownika (zakładka pracownicy/modyfikacja)
        private String _newWorkerPhoneTextBox; //pobranie nowego numeru telefonu pracownika (zakładka pracownicy/modyfikacja)
        private String _newWorkerDayOfBirthdayTextBox; //pobieranie dnia urodzenia pracownida(zakładka pracownicy/modyfikacja)
        private String _newWorkerYearOfBirthdayTextBox; //pobieranie roku urodzenia pracownida(zakładka pracownicy/modyfikacja)
        private String _newWorkerMonthOfBirthdayTextBox; //pobieranie roku urodzenia pracownida(zakładka pracownicy/modyfikacja)
        private String _newWorkerYearOfEmploymentTextBox; //pobieranie roku zatrudnienia pracownika(zakładka pracownicy/modyfikacja)
        private String _newWorkerMonthOfEmploymentTextBox; //pobieranie miesiąca zatrudnienia pracownika(zakładka pracownicy/modyfikacja)
        private String _newWorkerDayOfEmploymentTextBox; //pobieranie miesiąca zatrudnienia pracownika(zakładka pracownicy/modyfikacja)
        private String _newWorkerPeselTextBox; //pobieranie nowego numeru pesel pracownika(zakładka pracownicy/modyfikacja)
        private String _newSpeditionTypeTextBox; // pobieranie nowego typu wysyłki (zakładka typy wysyłki)
        private String _newQuantityTypeTextBox; // pobieranie nowej jednostki (zakładka jednostki produktów)
        private bool _newUserIsAdmin;
        private bool _newUserIsCashier;
        private bool _newUserIsStorekeeper;
        private bool _newUserIsWorkerCheckBox; //checkbox odblokowujący możliwość wyboru pracownik przy dodawaniu nowego użytkownika
        private bool _unlockUserToModificationCheckBox; //checkbox odblokowywujacy możliwość modyfikowania użytkowników
        private bool _modifiedUserIsAdminCheckBox; //modyfikowany użytkownik będzie administratorem
        private bool _modifiedUserIsCashierCheckBox; //modyfikowany użytkownik będzie kasjerem
        private bool _modifiedUserIsStorekeeperCheckBox; //modyfikowany użytkownik będzie magazynierem
        private bool _unlockWorkerToModificationCheckBox; //checkbox odblokowywujący możliwość modyfikacji pracownika
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
        private Visibility _modifyWorkerFailedPhoneVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu numeru telefonu
        private Visibility _modifyWorkerFailedSelectedWorkerVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu numeru telefonu
        private Visibility _modifyWorkerFailedBirthDateVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu daty urodzenia
        private Visibility _modifyWorkerFailedEmploymentDateVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu daty zatrudnienia(zakładka pracownicy/zarządzanie)
        private Visibility _modifyWorkerFailedPeselVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu numeru pesel(zakładka pracownicy/zarządzanie)
        private Visibility _addNewSpeditionTypeFailedVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu nazwy nowego typu wysyłki(zakładka typy wysyłki)
        private Visibility _addNewQuantityTypeFailedVisibilityLabel; //Wyświetlenie informacji o błędnym wprowadzeniu nazwy nowej jednostki produktu(zakładka jednostki produktów)



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

        public RelayCommand FindWorkerToModifyButtonCommand
        {
            get
            {
                _findWorkerToModifyButtonCommand = new RelayCommand(FindWorkerToModifyButton);
                _findWorkerToModifyButtonCommand.CanUndo = (obj) => false;

                return _findWorkerToModifyButtonCommand;

            }
            set
            {
                _findWorkerToModifyButtonCommand = value;
            }
        }

        public RelayCommand ModifyWorkerButtonCommond
        {
            get
            {
                _modifyWorkerButtonCommond = new RelayCommand(ModifyWorkerButton);
                _modifyWorkerButtonCommond.CanUndo = (obj) => false;

                return _modifyWorkerButtonCommond;
            }
            set
            {
                _modifyWorkerButtonCommond = value;
            }
        }

        public RelayCommand AddNewSpeditionTypeButtonCommand
        {
            get
            {
                _addNewSpeditionTypeButtonCommand = new RelayCommand(AddNewSpeditionTypeButton);
                _addNewSpeditionTypeButtonCommand.CanUndo = (obj) => false;

                return _addNewSpeditionTypeButtonCommand;
            }
            set
            {
                _addNewSpeditionTypeButtonCommand = value;
            }
        }

        public RelayCommand AddNewQuantityTypeButtonCommand
        {
            get
            {
                _addNewQuantityTypeButtonCommand = new RelayCommand(AddNewQuantityTypeButton);
                _addNewQuantityTypeButtonCommand.CanUndo = (obj) => false;

                return _addNewQuantityTypeButtonCommand;
            }
            set
            {
                _addNewQuantityTypeButtonCommand = value;
            }
        }

        public RelayCommand RefreshListOfWorkerToAddUserButtonCommand
        {
            get
            {
                _refreshListOfWorkerToAddUserButtonCommand = new RelayCommand(RefreshListOfWorkerToAddUserButton);
                _refreshListOfWorkerToAddUserButtonCommand.CanUndo = (obj) => false;

                return _refreshListOfWorkerToAddUserButtonCommand;
            }
            set
            {
                _refreshListOfWorkerToAddUserButtonCommand = value;
            }
        }

        public RelayCommand RefreshListOfUserToModifyButtonCommand
        {
            get
            {
                _refreshListOfUserToModifyButtonCommand = new RelayCommand(RefreshListOfUserToModifyButton);
                _refreshListOfUserToModifyButtonCommand.CanUndo = (obj) => false;
                
                return _refreshListOfUserToModifyButtonCommand;
            }
            set
            {
                _refreshListOfUserToModifyButtonCommand = value;
            }
        }

        public RelayCommand RefreshListOfWorkerToModifyButtonCommand
        {
            get
            {
                _refreshListOfWorkerToModifyButtonCommand = new RelayCommand(RefreshListOfWorkerToModifyButton);
                _refreshListOfWorkerToModifyButtonCommand.CanUndo = (obj) => false;

                return _refreshListOfWorkerToModifyButtonCommand;
            }
            set
            {
                _refreshListOfWorkerToModifyButtonCommand = value;
            }
        }

        public RelayCommand RefreshListOfSpeditionTypesButtonCommand
        {
            get
            {
                _refreshListOfSpeditionTypesButtonCommand = new RelayCommand(RefreshListOfSpeditionTypesButton);
                _refreshListOfSpeditionTypesButtonCommand.CanUndo = (obj) => false;

                return _refreshListOfSpeditionTypesButtonCommand;
            }
            set
            {
                _refreshListOfSpeditionTypesButtonCommand = value;
            }
        }

        public RelayCommand RefreshListOfQuantityTypesButtonCommand
        {
            get
            {
                _refreshListOfQuantityTypesButtonCommand = new RelayCommand(RefreshListOfQuantityTypesButton);
                _refreshListOfQuantityTypesButtonCommand.CanUndo = (obj) => false;

                return _refreshListOfQuantityTypesButtonCommand;
            }
            set
            {
                _refreshListOfQuantityTypesButtonCommand = value;
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

        public IList<WO_Worker> ListAllWorkersToModifyDataGrid
        {
            get
            {
                return _listAllWorkersToModifyDataGrid;
            }
            set
            {
                _listAllWorkersToModifyDataGrid = value;
                OnPropertyChanged("ListAllWorkersToModifyDataGrid");
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

        public List<SP_Spedition> ListSpeditionsTypeDataGrid
        {
            get
            {
                return _listSpeditionsTypeDataGrid;
            }
            set
            {
                _listSpeditionsTypeDataGrid = value;
                OnPropertyChanged("ListSpeditionsTypeDataGrid");
            }
        }

        public List<QT_QuantityType> ListQuantityTypeDataGrid
        {
            get
            {
                return _listQuantityTypeDataGrid;
            }
            set
            {
                _listQuantityTypeDataGrid = value;
                OnPropertyChanged("ListQuantityTypeDataGrid");
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

        public WO_Worker WorkerSelectedToModifyInDataGrid
        {
            get
            {
                return _workerSelectedToModifyInDataGrid;
            }
            set
            {
                _workerSelectedToModifyInDataGrid = value;
                OnPropertyChanged("WorkerSelectedToModifyInDataGrid");
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

        public String FindWorkerByNameToModifyTextBox
        {
            get
            {
                return _findWorkerByNameToModifyTextBox;
            }
            set
            {
                _findWorkerByNameToModifyTextBox = value;
                OnPropertyChanged("FindWorkerByNameToModifyTextBox");
            }
        }

        public String NewWorkerNameTextBox
        {
            get
            {
                return _newWorkerNameTextBox;
            }
            set
            {
                _newWorkerNameTextBox = value;
                OnPropertyChanged("NewWorkerNameTextBox");
            }
        }

        public String NewWorkerSurnameTextBox
        {
            get
            {
                return _newWorkerSurnameTextBox;
            }
            set
            {
                _newWorkerSurnameTextBox = value;
                OnPropertyChanged("NewWorkerSurnameTextBox");
            }
        }

        public String NewWorkerPhoneTextBox
        {
            get
            {
                return _newWorkerPhoneTextBox;
            }
            set
            {
                _newWorkerPhoneTextBox = value;
                OnPropertyChanged("NewWorkerPhoneTextBox");
            }
        }

        public String NewWorkerDayOfBirthdayTextBox
        {
            get
            {
                return _newWorkerDayOfBirthdayTextBox;
            }
            set
            {
                _newWorkerDayOfBirthdayTextBox = value;
                OnPropertyChanged("NewWorkerDayOfBirthdayTextBox");
            }
        }

        public String NewWorkerYearOfBirthdayTextBox
        {
            get 
            {
                return _newWorkerYearOfBirthdayTextBox;
            }
            set
            {
                _newWorkerYearOfBirthdayTextBox = value;
                OnPropertyChanged("NewWorkerYearOfBirthdayTextBox");
            }
        }

        public String NewWorkerMonthOfBirthdayTextBox
        {
            get
            {
                return _newWorkerMonthOfBirthdayTextBox;
            }
            set
            {
                _newWorkerMonthOfBirthdayTextBox = value;
                OnPropertyChanged("NewWorkerMonthOfBirthdayTextBox");
            }
        }

        public String NewWorkerYearOfEmploymentTextBox
        {
            get
            {
                return _newWorkerYearOfEmploymentTextBox;
            }
            set
            {
                _newWorkerYearOfEmploymentTextBox = value;
                OnPropertyChanged("NewWorkerYearOfEmploymentTextBox");
            }
        }

        public String NewWorkerMonthOfEmploymentTextBox
        {
            get
            {
                return _newWorkerMonthOfEmploymentTextBox;
            }
            set
            {
                _newWorkerMonthOfEmploymentTextBox = value;
                OnPropertyChanged("NewWorkerMonthOfEmploymentTextBox");
            }
        }

        public String NewWorkerDayOfEmploymentTextBox
        {
            get
            {
                return _newWorkerDayOfEmploymentTextBox;
            }
            set
            {
                _newWorkerDayOfEmploymentTextBox = value;
                OnPropertyChanged("NewWorkerDayOfEmploymentTextBox");
            }
        }

        public String PositionSelectedToModifyWorker
        {
            get
            {
                return _positionSelectedToModifyWorker;
            }
            set
            {
                _positionSelectedToModifyWorker = value;
                OnPropertyChanged("PositionSelectedToModifyWorker");
            }
        }

        public String NewWorkerPeselTextBox
        {
            get
            {
                return _newWorkerPeselTextBox;
            }
            set
            {
                _newWorkerPeselTextBox = value;
                OnPropertyChanged("NewWorkerPeselTextBox");
            }
        }

        public String NewSpeditionTypeTextBox
        {
            get
            {
                return _newSpeditionTypeTextBox;
            }
            set
            {
                _newSpeditionTypeTextBox = value;
                OnPropertyChanged("NewSpeditionTypeTextBox");
            }
        }

        public String NewQuantityTypeTextBox
        {
            get
            {
                return _newQuantityTypeTextBox;
            }
            set
            {
                _newQuantityTypeTextBox = value;
                OnPropertyChanged("NewQuantityTypeTextBox");
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

        public bool UnlockWorkerToModificationCheckBox
        {
            get
            {
                return _unlockWorkerToModificationCheckBox;
            }
            set
            {
                _unlockWorkerToModificationCheckBox = value;
                OnPropertyChanged("UnlockWorkerToModificationCheckBox");
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

        public Visibility ModifyWorkerFailedPhoneVisibilityLabel
        {
            get
            {
                return _modifyWorkerFailedPhoneVisibilityLabel;
            }
            set
            {
                _modifyWorkerFailedPhoneVisibilityLabel = value;
                OnPropertyChanged("ModifyWorkerFailedPhoneVisibilityLabel");
            }
        }

        public Visibility ModifyWorkerFailedSelectedWorkerVisibilityLabel
        {
            get
            {
                return _modifyWorkerFailedSelectedWorkerVisibilityLabel;
            }
            set
            {
                _modifyWorkerFailedSelectedWorkerVisibilityLabel = value;
                OnPropertyChanged("ModifyWorkerFailedSelectedWorkerVisibilityLabel");
            }
        }

        public Visibility ModifyWorkerFailedBirthDateVisibilityLabel
        {
            get
            {
                return _modifyWorkerFailedBirthDateVisibilityLabel;
            }
            set
            {
                _modifyWorkerFailedBirthDateVisibilityLabel = value;
                OnPropertyChanged("ModifyWorkerFailedBirthDateVisibilityLabel");
            }
        }

        public Visibility ModifyWorkerFailedEmploymentDateVisibilityLabel
        {
            get
            {
                return _modifyWorkerFailedEmploymentDateVisibilityLabel;
            }
            set
            {
                _modifyWorkerFailedEmploymentDateVisibilityLabel = value;
                OnPropertyChanged("ModifyWorkerFailedEmploymentDateVisibilityLabel");
            }
        }

        public Visibility ModifyWorkerFailedPeselVisibilityLabel
        {
            get
            {
                return _modifyWorkerFailedPeselVisibilityLabel;
            }
            set
            {
                _modifyWorkerFailedPeselVisibilityLabel = value;
                OnPropertyChanged("ModifyWorkerFailedPeselVisibilityLabel");
            }
        }

        public Visibility AddNewSpeditionTypeFailedVisibilityLabel
        {
            get
            {
                return _addNewSpeditionTypeFailedVisibilityLabel;
            }
            set
            {
                _addNewSpeditionTypeFailedVisibilityLabel = value;
                OnPropertyChanged("AddNewSpeditionTypeFailedVisibilityLabel");
            }
        }

        public Visibility AddNewQuantityTypeFailedVisibilityLabel
        {
            get
            {
                return _addNewQuantityTypeFailedVisibilityLabel;
            }
            set
            {
                _addNewQuantityTypeFailedVisibilityLabel = value;
                OnPropertyChanged("AddNewQuantityTypeFailedVisibilityLabel");
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

                if (userManager.GetByUserName(UserNameToAddNewUser) == null)
                {

                    newUser.US_USERNAME = UserNameToAddNewUser;
                    newUser.US_PASSWORD = UserPasswordToAddNewUser;

                    if (NewUserIsWorkerCheckBox == true)
                        newUser.US_WO_ID = WorkerSelectedToAddUser.WO_ID;

                    newUser.US_IS_ADMIN = NewUserIsAdmin;
                    newUser.US_IS_CASHIER = NewUserIsCashier;
                    newUser.US_IS_STOREKEEPER = NewUserIsStorekeeper;
                    newUser.US_ADDED = DateTime.Now;
                    newUser.US_LAST_MODIFIED = DateTime.Now;

                    userManager.Add(newUser);

                    MessageBox.Show("Dodano użytkownika");

                    //pobranie aktualnej listy uzytkowników do zakładki zarządzania użytkownikami
                    //ListUserToModificationDataGrid = userManager.GetAll().ToList();
                }
                else
                    MessageBox.Show("Użytkownik o podanej nazwie już istnieje");
            }
            


        }

        public void RefreshListOfWorkerToAddUserButton(Object obj)
        {
            WorkersManager workerManager = new WorkersManager();
            ListWorkersAll = workerManager.GetAll();
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
                MessageBox.Show("Użytkownik został zmodyfikowany");

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
                MessageBox.Show("Użytkownik został usunięty");
            }
            else
                SelectedUserToModifiedFailedVisibilityLabel = Visibility.Visible;

        }

        public void RefreshListOfUserToModifyButton(Object obj)
        {
            UsersManager userManager = new UsersManager();
            ListUserToModificationDataGrid = userManager.GetAll().ToList();
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
                
                birthDate = WorkerMonthOfBirthdayToAddTextBox + "/" + WorkerDayOfBirthdayToAddTextBox + "/" + WorkerYearOfBirthdayToAddTextBox;
                employmentDate = WorkerMonthOfEmploymentToAddTextBox + "/" + WorkerDayOfEmploymentToAddTextBox + "/" + WorkerYearOfEmploymentToAddTextBox;
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
        //zakładka pracownicy/zarządzanie
        public void FindWorkerToModifyButton(Object obj)
        {
            WorkersManager workerManager = new WorkersManager();
            ListAllWorkersToModifyDataGrid = workerManager.GetByWorkerSurname(FindWorkerByNameToModifyTextBox);
        }

        public void ModifyWorkerButton(Object obj)
        {
            Int32 result;
            Int64 resultInt64;
            String birthDate = null;
            String employmentDate = null;
            DateTime dateOfBirthday;
            DateTime dateOfEmployment;
            

            if (WorkerSelectedToModifyInDataGrid != null)
                ModifyWorkerFailedSelectedWorkerVisibilityLabel = Visibility.Hidden;
            else
                ModifyWorkerFailedSelectedWorkerVisibilityLabel = Visibility.Visible;
            if (ModifyWorkerFailedSelectedWorkerVisibilityLabel == Visibility.Hidden)
            {
                WorkersManager workerManager = new WorkersManager();
                WO_Worker newWorker = workerManager.Get(WorkerSelectedToModifyInDataGrid.WO_ID);
                //sprawdzenie poprawnośći wprowadzenia daty urodzenia przy modyfikacji pracownika
                if (NewWorkerYearOfBirthdayTextBox != null && NewWorkerYearOfBirthdayTextBox != ""
                    || NewWorkerMonthOfBirthdayTextBox != null && NewWorkerMonthOfBirthdayTextBox != ""
                    || NewWorkerDayOfBirthdayTextBox != null && NewWorkerDayOfBirthdayTextBox != "")
                {
                    if (!Int32.TryParse(NewWorkerYearOfBirthdayTextBox, out result) || NewWorkerYearOfBirthdayTextBox.Length != 4
                         || !Int32.TryParse(NewWorkerMonthOfBirthdayTextBox, out result) || NewWorkerMonthOfBirthdayTextBox.Length != 2
                         || !Int32.TryParse(NewWorkerDayOfBirthdayTextBox, out result) || NewWorkerDayOfBirthdayTextBox.Length != 2)

                        ModifyWorkerFailedBirthDateVisibilityLabel = Visibility.Visible;
                    else
                    {
                        ModifyWorkerFailedBirthDateVisibilityLabel = Visibility.Hidden;
                        birthDate = NewWorkerYearOfBirthdayTextBox + "/" + NewWorkerMonthOfBirthdayTextBox + "/" + NewWorkerDayOfBirthdayTextBox;
                        dateOfBirthday = Convert.ToDateTime(birthDate);
                        newWorker.WO_BIRTH_DATE = dateOfBirthday;
                    }
                }
                else
                    ModifyWorkerFailedBirthDateVisibilityLabel = Visibility.Hidden;

                //sprawdzenie poprawnośći wprowadzenia numeru pesel przy dodawaniu pracownika
                if (NewWorkerPeselTextBox != null && NewWorkerPeselTextBox != "")
                {
                    if (!Int64.TryParse(NewWorkerPeselTextBox, out resultInt64) || NewWorkerPeselTextBox.Length != 11)
                        ModifyWorkerFailedPeselVisibilityLabel = Visibility.Visible;
                    else
                    {
                        ModifyWorkerFailedPeselVisibilityLabel = Visibility.Hidden;
                        newWorker.WO_PESEL = NewWorkerPeselTextBox;
                    }
                }
                else
                    ModifyWorkerFailedPeselVisibilityLabel = Visibility.Hidden;

                //sprawdzenie poprawnośći wprowadzenia daty zatrudnienia przy modyfikacji pracownika
                if (NewWorkerYearOfEmploymentTextBox != null && NewWorkerYearOfEmploymentTextBox != ""
                    || NewWorkerMonthOfEmploymentTextBox != null && NewWorkerMonthOfEmploymentTextBox != ""
                    || NewWorkerDayOfEmploymentTextBox != null && NewWorkerDayOfEmploymentTextBox != "")
                {

                    if (!Int32.TryParse(NewWorkerYearOfEmploymentTextBox, out result) || NewWorkerYearOfEmploymentTextBox.Length != 4
                        || !Int32.TryParse(NewWorkerMonthOfEmploymentTextBox, out result) || NewWorkerMonthOfEmploymentTextBox.Length != 2
                        || !Int32.TryParse(NewWorkerDayOfEmploymentTextBox, out result) || NewWorkerDayOfEmploymentTextBox.Length != 2)

                        ModifyWorkerFailedEmploymentDateVisibilityLabel = Visibility.Visible;
                    else
                    {
                        ModifyWorkerFailedEmploymentDateVisibilityLabel = Visibility.Hidden;
                        employmentDate = NewWorkerYearOfEmploymentTextBox + "/" + NewWorkerMonthOfEmploymentTextBox + "/" + NewWorkerDayOfEmploymentTextBox;
                        dateOfEmployment = Convert.ToDateTime(employmentDate);
                        newWorker.WO_WORKING_SINCE = dateOfEmployment;
                    }
                }
                else
                    ModifyWorkerFailedEmploymentDateVisibilityLabel = Visibility.Hidden;


                if (NewWorkerNameTextBox != null && NewWorkerNameTextBox != "")
                    newWorker.WO_NAME = NewWorkerNameTextBox;

                if (NewWorkerSurnameTextBox != null && NewWorkerSurnameTextBox != "" && NewWorkerSurnameTextBox != " ")
                    newWorker.WO_SURNAME = NewWorkerSurnameTextBox;

                if (NewWorkerPhoneTextBox != null && NewWorkerPhoneTextBox != "")
                    if (!Int32.TryParse(NewWorkerPhoneTextBox, out result))
                        ModifyWorkerFailedPhoneVisibilityLabel = Visibility.Visible;
                    else
                    {
                        ModifyWorkerFailedPhoneVisibilityLabel = Visibility.Hidden;
                        newWorker.WO_PHONE = NewWorkerPhoneTextBox;
                    }

                if (ModifyWorkerFailedSelectedWorkerVisibilityLabel == Visibility.Hidden && ModifyWorkerFailedBirthDateVisibilityLabel == Visibility.Hidden
                    && ModifyWorkerFailedPeselVisibilityLabel == Visibility.Hidden && ModifyWorkerFailedEmploymentDateVisibilityLabel == Visibility.Hidden)
                {

                    if (PositionSelectedToModifyWorker != null)
                    {
                        PositionsManager positionManager = new PositionsManager();
                        PO_Position position = positionManager.GetByPositionName(PositionSelectedToModifyWorker);
                        newWorker.WO_PO_ID = position.PO_ID;
                    }

                    newWorker.WO_LAST_MODIFIED = DateTime.Now;

                    workerManager.Update();
                    MessageBox.Show("Zmodyfikowano użytkownika");
                }
            }

        }

        public void RefreshListOfWorkerToModifyButton(Object obj)
        {
            WorkersManager workerManager = new WorkersManager();
            ListAllWorkersToModifyDataGrid = workerManager.GetAll();
        }

        /////////////////////////
        //zakładka typy wysyłki
        public void AddNewSpeditionTypeButton(Object obj)
        {
            SpeditionManager speditionManager = new SpeditionManager();
            SP_Spedition newSpedition = new SP_Spedition();

            if (NewSpeditionTypeTextBox != null && NewSpeditionTypeTextBox != "")
            {
                AddNewSpeditionTypeFailedVisibilityLabel = Visibility.Hidden;
                if (speditionManager.GetByName(NewSpeditionTypeTextBox) == null)
                {
                    newSpedition.SP_NAME = NewSpeditionTypeTextBox;
                    speditionManager.Add(newSpedition);
                    MessageBox.Show("Dodano nowy typ wysyłki");
                    
                }
                else
                    MessageBox.Show("Istnieje już taki typ wysyłki");
            }
            else
                AddNewSpeditionTypeFailedVisibilityLabel = Visibility.Visible;
        }

        public void RefreshListOfSpeditionTypesButton(Object obj)
        {
            SpeditionManager speditionManager = new SpeditionManager();
            ListSpeditionsTypeDataGrid = speditionManager.GetAll().ToList();
        }

        public void AddNewQuantityTypeButton(Object obj)
        {
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            QT_QuantityType quantityType = new QT_QuantityType();
            if(NewQuantityTypeTextBox != null && NewQuantityTypeTextBox != "")
            {
                AddNewQuantityTypeFailedVisibilityLabel = Visibility.Hidden;
                
                if ((quantityTypesManager.GetByName(NewQuantityTypeTextBox)) == null)
                {
                    quantityType.QT_NAME = NewQuantityTypeTextBox;
                    quantityTypesManager.Add(quantityType);
                    MessageBox.Show("Dodano nową jednostkę");

                    ListQuantityTypeDataGrid = quantityTypesManager.GetAll().ToList();
                }
                else
                    MessageBox.Show("Istnieje już taka jednostka");
            }
            else
                AddNewQuantityTypeFailedVisibilityLabel = Visibility.Visible;
        }

        public void RefreshListOfQuantityTypesButton(Object obj)
        {
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            ListQuantityTypeDataGrid = quantityTypesManager.GetAll().ToList();
        }

        #endregion //Methods
    }
}
