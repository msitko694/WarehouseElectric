using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;
using System.Windows.Controls;
using System.Data.SqlClient;

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

            //zakładka stanowiska pracowników
            //ukrycie labelki informującej o błędnym wprowadzeniu nazwy nowego stanowiskoa
            AddNewPositionFailedVisibilityLabel = Visibility.Hidden;
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
        private RelayCommand _generateUserNameButtonCommand; //wygenerowanie nazwy użytkownika na podstawie danych pracownika(zakładka użytkownicy/nowy)
        private RelayCommand _generateUserPasswordCommand; //wygenerowanei hasła użytkownika(zakładka użytkownicy/nowy)
        private RelayCommand _findWorkerToModifyButtonCommand; //wyszukanie pracownika do modyfikacji (zakładka pracownicy/zarządzanie) 
        private RelayCommand _modifyWorkerButtonCommond; //modyfikacja pracownika (zakładka pracownicy/zarządzanie)
        private RelayCommand _deleteWorkerCommand; //usunięcie pracownika(zakładka pracownicy/zarządzanie)
        private RelayCommand _addNewSpeditionTypeButtonCommand; // dodanie nowego typu wysyłki (zakładka typy wysyłki);
        private RelayCommand _deleteSpeditionTypeCommand; // usuwanie typu wysyłki
        private RelayCommand _addNewQuantityTypeButtonCommand; // dodanie nowej jednostki(zakładka jednostki produktów);
        private RelayCommand _deleteQuantityTypeCommand; // usunięcie jednosti produktów
        private RelayCommand _refreshListOfWorkerToAddUserButtonCommand; // odświeżanie listy pracowników (zkładka użytkownicy/nowy)
        private RelayCommand _refreshListOfUserToModifyButtonCommand; //odświeżenie listy użytkowników do modyfikacji(zakładka użytkownicy/zarządzanie)
        private RelayCommand _refreshListOfWorkerToModifyButtonCommand; //odświeżenie listy pracowników do modyfikacji(zakładka pracownicy/zarządzanie)
        private RelayCommand _refreshListOfSpeditionTypesButtonCommand; //odświeżenie listy z typami wysyłki(zakładka typy wysyłki)
        private RelayCommand _refreshListOfQuantityTypesButtonCommand; //odświeżenie listy z jednostkami produktów(zakładka jednostki produktów)
        private RelayCommand _refreshListOfPositionButtonCommand; //odświeżenie listy z stanowiskami (zakładka stanowska pracowników)
        private RelayCommand _addNewPositionButtonCommand; //dodane nowego stanowsika pracy (zakładka stanowska pracowników)
        private RelayCommand _deletePositionCommand; //usunięcie stanowiska pracy
        private RelayCommand _refreshInformationOfCompanyCommand; //wczytanie danych firmy(zakładka dane firmy)
        private RelayCommand _editCompanyInformationCommand; // edycja danych firmy(zakładka dane firmy)
        private IList<WO_Worker> _listWorkersAll;
        private IList<WO_Worker> _listAllWorkersToModifyDataGrid; //wyświetlenie w datagrid listy pracowników (zakładka pracownicy/zarządzanie)
        private List<US_User> _listUserToModificationDataGrid; //wyświetlenie w datagrid listy userów do modyfikacji(zakładka użytkownicy/zarządzanie)
        private List<String> _listPositionsToAddWorkerComboBox; //dodanie do combobox listy pozycji pracowników (zakładka pracownicy/nowy)
        private List<SP_Spedition> _listSpeditionsTypeDataGrid; // wyświetlenie listy typów wysyłki w zakładce (typy wysyłki/ jednostki produktów)
        private List<QT_QuantityType> _listQuantityTypeDataGrid; // wyświetlenie listy jednostek produktów w zakładce (typy wysyłki/ jednostki produktów)
        private IList<PO_Position> _listAllPositionsDataGrid; //wyświetlenie listy stanowisk(zakładka stanowiska pracy )
        private WO_Worker _workerSelectedToAddUser;
        private WO_Worker _workerSelectedToModifyInDataGrid; //wybrany pracownik do modyfikacji (zakładka pracownicy/zarządzanie)
        private SP_Spedition _speditionSelectedToDeleteDataGrid; //wybrany typ wysyłki w data grid(zakładka typy wysyłki)
        private QT_QuantityType _quantityTypesSelectedToDeleteDataGrid; //wybrana jednostak w data grid(zakładka jednostki produktów)
        private PO_Position _positionSelectedToDeleteInDataGrid; //wybrane stanowisko w data grid(zakładka stanowiska pracy)
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
        private String _newPositionTextBox; //pobierane nazwy nowego stanowiska pracy(zakładaka stanowiska pracay)
        private String _nameOfCompanyTextBox; //nazwa firmy (zakładka dane firmy)
        private String _streetCompanyTextBox; //nazwa ulicy (zakładka dane firmy)
        private String _postCodeOfCompanyTextBox; // kod pocztowy firmy(zakładka dane firmy)
        private String _townOfCompanyTextBox; // nazwa miasta (zakładka dane firmy)
        private String _phoneOfCompanyTextBox; // numer telefonu firmy(zakładka dane firmy)
        private String _newNameOfCompanyTextBox; //nowa nazwa firmy
        private String _newStreetCompanyTextBox; //nowa nazwa ulicy do adresu firmy
        private String _newPostCodeOfCompanyTextBox; //nowu kod pocztowy firmy
        private String _newTownOfCompanyTextBox; //nowa nazwa miasta do adresu firmy
        private String _newPhoneOfCompanyTextBox; //nowy numer tel. firmy
        private Boolean _newUserIsAdmin;
        private Boolean _newUserIsCashier;
        private Boolean _newUserIsStorekeeper;
        private Boolean _newUserIsWorkerCheckBox; //checkbox odblokowujący możliwość wyboru pracownik przy dodawaniu nowego użytkownika
        private Boolean _unlockUserToModificationCheckBox; //checkbox odblokowywujacy możliwość modyfikowania użytkowników
        private Boolean _modifiedUserIsAdminCheckBox; //modyfikowany użytkownik będzie administratorem
        private Boolean _modifiedUserIsCashierCheckBox; //modyfikowany użytkownik będzie kasjerem
        private Boolean _modifiedUserIsStorekeeperCheckBox; //modyfikowany użytkownik będzie magazynierem
        private Boolean _unlockWorkerToModificationCheckBox; //checkbox odblokowywujący możliwość modyfikacji pracownika
        private Boolean _unlockEditingCompanyInformationCheckBox; // checkbox odblokowywujący możliwość modyfikacji o formie
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
        private Visibility _addNewPositionFailedVisibilityLabel; //wyświetlenia informacji o błędnym wprowadzeniu nazwy nowego stanowiska(zakładka stanowsika pracowników)
        


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

        public RelayCommand RefreshListOfPositionButtonCommand
        {
            get
            {
                _refreshListOfPositionButtonCommand = new RelayCommand(RefreshListOfPositionButton);
                _refreshListOfPositionButtonCommand.CanUndo = (obj) => false;

                return _refreshListOfPositionButtonCommand;
            }
            set
            {
                _refreshListOfPositionButtonCommand = value;
            }
        }

        public RelayCommand AddNewPositionButtonCommand
        {
            get
            {
                _addNewPositionButtonCommand = new RelayCommand(AddNewPositionButton);
                _addNewPositionButtonCommand.CanUndo = (obj) => false;

                return _addNewPositionButtonCommand;
            }
            set
            {
                _addNewPositionButtonCommand = value;
            }
        }

        public RelayCommand GenerateUserNameButtonCommand
        {
            get
            {
                _generateUserNameButtonCommand = new RelayCommand(GenerateUserNameButton);
                _generateUserNameButtonCommand.CanUndo = (obj) => false;

                return _generateUserNameButtonCommand;
            }
            set
            {
                _generateUserNameButtonCommand = value;
            }
        }

        public RelayCommand GenerateUserPasswordCommand
        {
            get
            {
                _generateUserPasswordCommand = new RelayCommand(GenerateUserPassword);
                _generateUserPasswordCommand.CanUndo = (obj) => false;

                return _generateUserPasswordCommand;
            }
            set
            {
                _generateUserPasswordCommand = value;
            }
        }

        public RelayCommand DeleteSpeditionTypeCommand
        {
            get
            {
                _deleteSpeditionTypeCommand = new RelayCommand(DeleteSpeditionType);
                _deleteSpeditionTypeCommand.CanUndo = (obj) => false;

                return _deleteSpeditionTypeCommand;
            }
            set
            {
                _deleteSpeditionTypeCommand = value;
            }
        }

        public RelayCommand DeleteQuantityTypeCommand
        {
            get
            {
                _deleteQuantityTypeCommand = new RelayCommand(DeleteQuantityType);
                _deleteQuantityTypeCommand.CanUndo = (obj) => false;

                return _deleteQuantityTypeCommand;
            }
            set
            {
                _deleteQuantityTypeCommand = value;
            }
        }

        public RelayCommand DeletePositionCommand
        {
            get
            {
                _deletePositionCommand = new RelayCommand(DeletePosition);
                _deletePositionCommand.CanUndo = (obj) => false;

                return _deletePositionCommand;
            }
            set
            {
                _deletePositionCommand = value;
            }
        }

        public RelayCommand DeleteWorkerCommand
        {
            get
            {
                _deleteWorkerCommand = new RelayCommand(DeleteWorker);
                _deleteWorkerCommand.CanUndo = (obj) => false;

                return _deleteWorkerCommand;
            }
            set
            {
                _deleteWorkerCommand = value;
            }
        }

        public RelayCommand RefreshInformationOfCompanyCommand
        {
            get
            {
                _refreshInformationOfCompanyCommand = new RelayCommand(RefreshInformationOfCompany);
                _refreshInformationOfCompanyCommand.CanUndo = (obj) => false;

                return _refreshInformationOfCompanyCommand;
            }
            set
            {
                _refreshInformationOfCompanyCommand = value;
            }
        }

        public RelayCommand EditCompanyInformationCommand
        {
            get
            {
                _editCompanyInformationCommand = new RelayCommand(EditCompanyInformation);
                _editCompanyInformationCommand.CanUndo = (obj) => false;

                return _editCompanyInformationCommand;
            }
            set
            {
                _editCompanyInformationCommand = value;
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

        public IList<PO_Position> ListAllPositionsDataGrid
        {
            get
            {
                return _listAllPositionsDataGrid;
            }
            set
            {
                _listAllPositionsDataGrid = value;
                OnPropertyChanged("ListAllPositionsDataGrid");
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

        public SP_Spedition SpeditionSelectedToDeleteDataGrid
        {
            get
            {
                return _speditionSelectedToDeleteDataGrid;
            }
            set
            {
                _speditionSelectedToDeleteDataGrid = value;
                OnPropertyChanged("SpeditionSelectedToDeleteDataGrid");
            }
        }

        public QT_QuantityType QuantityTypesSelectedToDeleteDataGrid
        {
            get
            {
                return _quantityTypesSelectedToDeleteDataGrid; 
            }
            set
            {
                _quantityTypesSelectedToDeleteDataGrid = value;
                OnPropertyChanged("QuantityTypesSelectedToDeleteDataGrid");
            }
        }

        public PO_Position PositionSelectedToDeleteInDataGrid
        {
            get
            {
                return _positionSelectedToDeleteInDataGrid;
            }
            set
            {
                _positionSelectedToDeleteInDataGrid = value;
                OnPropertyChanged("PositionSelectedToDeleteInDataGrid");
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

        public String NewPositionTextBox
        {
            get
            {
                return _newPositionTextBox;
            }
            set
            {
                _newPositionTextBox = value;
                OnPropertyChanged("NewPositionTextBox");
            }
        
        
        }

        public String NameOfCompanyTextBox
        {
            get
            {
                return _nameOfCompanyTextBox;
            }
            set
            {
                _nameOfCompanyTextBox = value;
                OnPropertyChanged("NameOfCompanyTextBox");
            }
        }

        public String StreetCompanyTextBox
        {
            get 
            {
                return _streetCompanyTextBox;
            }
            set
            {
                _streetCompanyTextBox = value;
                OnPropertyChanged("StreetCompanyTextBox");
            }
        }

        public String PostCodeOfCompanyTextBox
        {
            get
            {
                return _postCodeOfCompanyTextBox;
            }
            set
            {
                _postCodeOfCompanyTextBox = value;
                OnPropertyChanged("PostCodeOfCompanyTextBox");
            }
        }

        public String TownOfCompanyTextBox
        {
            get
            {
                return _townOfCompanyTextBox;
            }
            set 
            {
                _townOfCompanyTextBox = value;
                OnPropertyChanged("TownOfCompanyTextBox");
            }
        }

        public String PhoneOfCompanyTextBox
        {
            get
            {
                return _phoneOfCompanyTextBox;
            }
            set
            {
                _phoneOfCompanyTextBox = value;
                OnPropertyChanged("PhoneOfCompanyTextBox");
            }
        }

        public String NewNameOfCompanyTextBox
        {
            get
            {
                return _newNameOfCompanyTextBox;
            }
            set
            {
                _newNameOfCompanyTextBox = value;
                OnPropertyChanged("NewNameOfCompanyTextBox");
            }
        }

        public String NewStreetCompanyTextBox
        {
            get
            {
                return _newStreetCompanyTextBox;
            }
            set
            {
                _newStreetCompanyTextBox = value;
                OnPropertyChanged("NewStreetCompanyTextBox");
            }
        }

        public String NewPostCodeOfCompanyTextBox
        {
            get
            {
                return _newPostCodeOfCompanyTextBox;
            }
            set
            {
                _newPostCodeOfCompanyTextBox = value;
                OnPropertyChanged("NewPostCodeOfCompanyTextBox");
            }
        }

        public String NewTownOfCompanyTextBox
        {
            get
            {
                return _newTownOfCompanyTextBox;
            }
            set
            {
                _newTownOfCompanyTextBox = value;
                OnPropertyChanged("NewTownOfCompanyTextBox");
            }
        }

        public string NewPhoneOfCompanyTextBox
        {
            get
            {
                return _newPhoneOfCompanyTextBox;
            }
            set
            {
                _newPhoneOfCompanyTextBox = value;
                OnPropertyChanged("NewPhoneOfCompanyTextBox");
            }
        }

        public Boolean NewUserIsAdmin
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

        public Boolean NewUserIsCashier
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

        public Boolean NewUserIsStorekeeper
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

        public Boolean NewUserIsWorkerCheckBox
        {
            get
            {
                return _newUserIsWorkerCheckBox;
            }
            set
            {
                _newUserIsWorkerCheckBox = value;
                OnPropertyChanged("NewUserIsWorkerCheckBox");
                if (value == true)
                {
                    WorkersManager workerManager = new WorkersManager();
                    ListWorkersAll = workerManager.GetAll();
                }
            }
        }

        public Boolean UnlockUserToModificationCheckBox
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

        public Boolean ModifiedUserIsAdminCheckBox
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

        public Boolean ModifiedUserIsCashierCheckBox
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

        public Boolean ModifiedUserIsStorekeeperCheckBox
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

        public Boolean UnlockWorkerToModificationCheckBox
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

        public Boolean UnlockEditingCompanyInformationCheckBox
        {
            get
            {
                return _unlockEditingCompanyInformationCheckBox;
            }
            set
            {
                _unlockEditingCompanyInformationCheckBox = value;
                OnPropertyChanged("UnlockEditingCompanyInformationCheckBox");
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

        public Visibility AddNewPositionFailedVisibilityLabel
        {
            get
            {
                return _addNewPositionFailedVisibilityLabel;
            }
            set
            {
                _addNewPositionFailedVisibilityLabel = value;
                OnPropertyChanged("AddNewPositionFailedVisibilityLabel");
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
                    ListUserToModificationDataGrid = userManager.GetAll().ToList();
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

        public void GenerateUserNameButton(Object obj)
        {
            UsersManager userManager = new UsersManager();

            UserNameToAddNewUser = "";
            int i = 1;
            if (WorkerSelectedToAddUser != null)
            {
                if (WorkerSelectedToAddUser.WO_NAME.Length < 4)
                    UserNameToAddNewUser += WorkerSelectedToAddUser.WO_NAME.Substring(0, WorkerSelectedToAddUser.WO_NAME.Length);
                else
                    UserNameToAddNewUser += WorkerSelectedToAddUser.WO_NAME.Substring(0, 4);
                if (WorkerSelectedToAddUser.WO_SURNAME.Length < 4)
                    UserNameToAddNewUser += WorkerSelectedToAddUser.WO_SURNAME.Substring(0, WorkerSelectedToAddUser.WO_SURNAME.Length);
                else
                    UserNameToAddNewUser += WorkerSelectedToAddUser.WO_SURNAME.Substring(0, 4);
                while (userManager.GetByUserName(UserNameToAddNewUser) != null)
                {
                    UserNameToAddNewUser = UserNameToAddNewUser.Substring(0, (UserNameToAddNewUser.Length - 1)) + i.ToString();
                    i++;
                }


            }
        }

        public void GenerateUserPassword(Object obj)
        {
            Random r = new Random();
            UserPasswordToAddNewUser = "";
            for (int i = 0; i < 4; i++)
            {
                UserPasswordToAddNewUser += r.Next(10).ToString();
                UserPasswordToAddNewUser += (char)r.Next(97, 122);
            }
        }
        /////////////////////////////////////
        //zakładka uzytkownicy/zarządzanie//
        public void FindUserToModification(Object obj)
        {
            UsersManager userManager = new UsersManager();
            US_User foundUser = new US_User();
            List<US_User> listOfFoundUser = new List<US_User>();

            if (ListUserToModificationDataGrid != null)
                ListUserToModificationDataGrid = null;
            
            foundUser = userManager.GetByUserName(FindUserToModificationTextBox);
            
            if (foundUser != null)
            {
                listOfFoundUser.Add(foundUser);
                
                ListUserToModificationDataGrid = listOfFoundUser;
            }
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
                //odświeżenie listy użytkowników
                ListUserToModificationDataGrid = userManager.GetAll().ToList();

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
                try
                {
                    userManager.Delete(user);
                    ListUserToModificationDataGrid = userManager.GetAll().ToList();
                    MessageBox.Show("Użytkownik został usunięty");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Nie można usunąć użytkownika, ponieważ jest używany w bazie danych");
                }
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
                || !Int32.TryParse(WorkerMonthOfBirthdayToAddTextBox, out result) || WorkerMonthOfBirthdayToAddTextBox.Length != 2 || Int32.Parse(WorkerMonthOfBirthdayToAddTextBox) < 1 || Int32.Parse(WorkerMonthOfBirthdayToAddTextBox) > 12
                || !Int32.TryParse(WorkerDayOfBirthdayToAddTextBox, out result) || WorkerDayOfBirthdayToAddTextBox.Length != 2 || Int32.Parse(WorkerDayOfBirthdayToAddTextBox) < 1 || Int32.Parse(WorkerDayOfBirthdayToAddTextBox) > 31)
               
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
                || !Int32.TryParse(WorkerMonthOfEmploymentToAddTextBox, out result) || WorkerMonthOfEmploymentToAddTextBox.Length != 2 || Int32.Parse(WorkerMonthOfEmploymentToAddTextBox) < 1 || Int32.Parse(WorkerMonthOfEmploymentToAddTextBox) > 12
                || !Int32.TryParse(WorkerDayOfEmploymentToAddTextBox, out result) || WorkerDayOfEmploymentToAddTextBox.Length != 2 || Int32.Parse(WorkerDayOfEmploymentToAddTextBox) < 1 || Int32.Parse(WorkerDayOfEmploymentToAddTextBox) > 31)
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
                //odświeżenie listy pracowników
                ListAllWorkersToModifyDataGrid = workerManager.GetAll();
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
                         || !Int32.TryParse(NewWorkerMonthOfBirthdayTextBox, out result) || NewWorkerMonthOfBirthdayTextBox.Length != 2 || Int32.Parse(NewWorkerMonthOfBirthdayTextBox) < 1 || Int32.Parse(NewWorkerMonthOfBirthdayTextBox) > 12
                         || !Int32.TryParse(NewWorkerDayOfBirthdayTextBox, out result) || NewWorkerDayOfBirthdayTextBox.Length != 2 || Int32.Parse(NewWorkerDayOfBirthdayTextBox) < 1 || Int32.Parse(NewWorkerDayOfBirthdayTextBox) > 31)

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
                        || !Int32.TryParse(NewWorkerMonthOfEmploymentTextBox, out result) || NewWorkerMonthOfEmploymentTextBox.Length != 2 || Int32.Parse(NewWorkerMonthOfEmploymentTextBox) < 1 || Int32.Parse(NewWorkerMonthOfEmploymentTextBox) > 12
                        || !Int32.TryParse(NewWorkerDayOfEmploymentTextBox, out result) || NewWorkerDayOfEmploymentTextBox.Length != 2 || Int32.Parse(NewWorkerDayOfEmploymentTextBox) < 1 || Int32.Parse(NewWorkerDayOfEmploymentTextBox) > 31)

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
                    //odświeżenie listy pracowników
                    ListAllWorkersToModifyDataGrid = workerManager.GetAll();

                    MessageBox.Show("Zmodyfikowano Pracownika");
                    
                }
            }

        }

        public void RefreshListOfWorkerToModifyButton(Object obj)
        {
            WorkersManager workerManager = new WorkersManager();
            ListAllWorkersToModifyDataGrid = workerManager.GetAll();
        }

        public void DeleteWorker(Object obj)
        {
            WorkersManager workersManager = new WorkersManager();
            WO_Worker worker = new WO_Worker();
            if (WorkerSelectedToModifyInDataGrid != null)
            {
                worker = workersManager.Get(WorkerSelectedToModifyInDataGrid.WO_ID);
                try
                {

                    workersManager.Delete(worker);
                    //odświeżenie listy pracowników
                    ListAllWorkersToModifyDataGrid = workersManager.GetAll();
                    MessageBox.Show("Usunięto Pracownika");
                }
                catch(SqlException)
                {
                    MessageBox.Show("Nie można usunąć pracownika ponieważ jest używany w bazie danych");
                }
                
            }
            else
                MessageBox.Show("Wybierz pracownika z tabeli");
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
                    ListSpeditionsTypeDataGrid = speditionManager.GetAll().ToList();
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

        public void DeleteSpeditionType(Object obj)
        {
            SpeditionManager speditionManager = new SpeditionManager();
            SP_Spedition speditionType = new SP_Spedition();
            if (SpeditionSelectedToDeleteDataGrid != null)
            {
                try
                {
                    speditionType = speditionManager.Get(SpeditionSelectedToDeleteDataGrid.SP_ID);
                    speditionManager.Delete(speditionType);
                    ListSpeditionsTypeDataGrid = speditionManager.GetAll().ToList();
                    MessageBox.Show("Typ Wysyłki został usunięty");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Niemożna usunąc typu wysyłki ponieważ jest wykorzystywany w bazie danych");
                }
            }
            else
                MessageBox.Show("Wybierz typ wysyłki z tabeli");
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

        public void DeleteQuantityType(Object obj)
        {
            QuantityTypesManager quantityTypesManager = new QuantityTypesManager();
            QT_QuantityType quantityTypes = new QT_QuantityType();
            if (QuantityTypesSelectedToDeleteDataGrid != null)
            {
                try
                {
                    quantityTypes = quantityTypesManager.Get(QuantityTypesSelectedToDeleteDataGrid.QT_ID);
                    quantityTypesManager.Delete(quantityTypes);
                    //odświeżenie listy w datagrid
                    ListQuantityTypeDataGrid = quantityTypesManager.GetAll().ToList();
                    MessageBox.Show("Jednostak produktów została usunięta");
                }
                catch(SqlException)
                {
                    MessageBox.Show("Niemożna usunąć jednostki, ponieważ jest używana w bazie danych");
                }
            }
            else
                MessageBox.Show("Wybierz jednostkę z tabeli");
        }

        //zakładka stanowiska pracowników
        public void RefreshListOfPositionButton(Object obj)
        {
            PositionsManager positionsManager = new PositionsManager();
            ListAllPositionsDataGrid = positionsManager.GetAll();
        }

        public void AddNewPositionButton(Object obj)
        {
            PositionsManager positionsManager = new PositionsManager();
            PO_Position newPosition = new PO_Position();
            if (NewPositionTextBox != null && NewPositionTextBox != "" && NewPositionTextBox != " ")
            {
                if (positionsManager.GetByPositionName(NewPositionTextBox) == null)
                {

                    AddNewPositionFailedVisibilityLabel = Visibility.Hidden;
                    newPosition.PO_NAME = NewPositionTextBox;
                    positionsManager.Add(newPosition);
                    ListPositionsToAddWorkerComboBox = positionsManager.GetAllName();
                    ListAllPositionsDataGrid = positionsManager.GetAll();
                    MessageBox.Show("Dodano nowe stanowisko");
                }
                else
                    MessageBox.Show("Istnieje już takie stanowsiko");
            }
            else
                AddNewPositionFailedVisibilityLabel = Visibility.Visible;
        }

        public void DeletePosition(Object obj)
        {
            PositionsManager positionManager = new PositionsManager();
            PO_Position position = new PO_Position();
            if (PositionSelectedToDeleteInDataGrid != null)
            {
                try
                {
                    position = positionManager.Get(PositionSelectedToDeleteInDataGrid.PO_ID);
                    positionManager.Delete(position);
                    ListPositionsToAddWorkerComboBox = positionManager.GetAllName();
                    ListAllPositionsDataGrid = positionManager.GetAll();
                    MessageBox.Show("Stanowisko zostało usunięte");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Niemożna usunąć stanowiska ponieważ jest wykorzystywane w bazie danych");
                }
            }
            else
                MessageBox.Show("Wybierz stanowisko z tablei");
        }

        //zakładka dane firmy
        public void RefreshInformationOfCompany(Object obj)
        {

            CompanyManager companyManager = new CompanyManager();
            CI_CompanyInfo companyInfo = new CI_CompanyInfo();
            companyInfo = companyManager.GetCompanyData();
            if (companyInfo != null)
            {
                NameOfCompanyTextBox = companyInfo.CI_NAME;
                StreetCompanyTextBox = companyInfo.CI_STREET;
                PostCodeOfCompanyTextBox = companyInfo.CI_POST_CODE;
                TownOfCompanyTextBox = companyInfo.CI_TOWN;
                PhoneOfCompanyTextBox = companyInfo.CI_PHONE;
            }
            else
                MessageBox.Show("Najpierw należy wprowadzić dane");
        }

        public void EditCompanyInformation(Object obj)
        {
            CompanyManager companyManager = new CompanyManager();
            CI_CompanyInfo companyInfo = new CI_CompanyInfo();
            if (companyManager.GetCompanyData() != null)
            {
                if (NewNameOfCompanyTextBox != null && NewNameOfCompanyTextBox != "" && NewNameOfCompanyTextBox != " ")
                    companyInfo.CI_NAME = NewNameOfCompanyTextBox;
                else
                    companyInfo.CI_NAME = companyManager.GetCompanyData().CI_NAME;

                if (NewStreetCompanyTextBox != null && NewStreetCompanyTextBox != "" && NewStreetCompanyTextBox != " ")
                    companyInfo.CI_STREET = NewStreetCompanyTextBox;
                else
                    companyInfo.CI_STREET = companyManager.GetCompanyData().CI_STREET;

                if (NewPostCodeOfCompanyTextBox != null && NewPostCodeOfCompanyTextBox != "" && NewPostCodeOfCompanyTextBox != " ")
                    companyInfo.CI_POST_CODE = NewPostCodeOfCompanyTextBox;
                else
                    companyInfo.CI_POST_CODE = companyManager.GetCompanyData().CI_POST_CODE;

                if (NewTownOfCompanyTextBox != null && NewTownOfCompanyTextBox != "" && NewTownOfCompanyTextBox != " ")
                    companyInfo.CI_TOWN = NewTownOfCompanyTextBox;
                else
                    companyInfo.CI_TOWN = companyManager.GetCompanyData().CI_TOWN;

                if (NewPhoneOfCompanyTextBox != null && NewPhoneOfCompanyTextBox != "" && NewPhoneOfCompanyTextBox != " ")
                    companyInfo.CI_PHONE = NewPhoneOfCompanyTextBox;
                else
                    companyInfo.CI_PHONE = companyManager.GetCompanyData().CI_PHONE;

                companyInfo.CI_ID = companyManager.GetCompanyData().CI_ID;
                companyInfo.CI_LAST_MODIFIED = DateTime.Now;
                companyInfo.CI_ADDED = companyManager.GetCompanyData().CI_ADDED;
                companyManager.SetCompanyData(companyInfo);
                MessageBox.Show("Zmieniono dane dotyczące firmy");
            }
            else
            {
                if (NewNameOfCompanyTextBox != null && NewNameOfCompanyTextBox != "" && NewNameOfCompanyTextBox != " ")
                    companyInfo.CI_NAME = NewNameOfCompanyTextBox;
                
                if (NewStreetCompanyTextBox != null && NewStreetCompanyTextBox != "" && NewStreetCompanyTextBox != " ")
                    companyInfo.CI_STREET = NewStreetCompanyTextBox;

                if (NewPostCodeOfCompanyTextBox != null && NewPostCodeOfCompanyTextBox != "" && NewPostCodeOfCompanyTextBox != " ")
                    companyInfo.CI_POST_CODE = NewPostCodeOfCompanyTextBox;

                if (NewTownOfCompanyTextBox != null && NewTownOfCompanyTextBox != "" && NewTownOfCompanyTextBox != " ")
                    companyInfo.CI_TOWN = NewTownOfCompanyTextBox;

                if (NewPhoneOfCompanyTextBox != null && NewPhoneOfCompanyTextBox != "" && NewPhoneOfCompanyTextBox != " ")
                    companyInfo.CI_PHONE = NewPhoneOfCompanyTextBox;

                companyInfo.CI_LAST_MODIFIED = DateTime.Now;
                companyInfo.CI_ADDED = DateTime.Now;
                companyManager.SetCompanyData(companyInfo);
                MessageBox.Show("Dodano dane firmy");
            }
        }

        #endregion //Methods

        #region //Disposable mehods

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        #endregion
    }
}
