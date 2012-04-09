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
    class LoginViewModel : ViewModelBase
    {
        #region "Constructors"

        public LoginViewModel(LoginView loginView, PasswordBox passwordBox)
        {
            _loginView = loginView;
            _userPassword = passwordBox;
            LoginFailedVisibility = Visibility.Hidden;
        }

        #endregion //Constructors

        #region "Fields"

        private RelayCommand _logInCommand;
        private RelayCommand _exitCommand;
        private String _userName;
        private PasswordBox _userPassword;
        private Visibility _loginFailedVisibility;
        private LoginView _loginView;
        
        #endregion //Fields

        #region "Properties"

        public RelayCommand LogInCommand
        {
            get
            {
                if(_logInCommand == null)
                {
                    _logInCommand = new RelayCommand(LogIn);
                    _logInCommand.CanUndo = (obj) => false;
                }
                return _logInCommand;
            }
            set
            {
                _logInCommand = value;
            }
        }
        public RelayCommand ExitCommand
        {
            get
            {
                if(_exitCommand == null)
                {
                    _exitCommand = new RelayCommand((obj) => Application.Current.Shutdown(1));
                    _exitCommand.CanUndo = (obj) => false;
                }
                return _exitCommand;
            }
            set
            {
                _exitCommand = value;
            }
        }
        public String UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        public Visibility LoginFailedVisibility
        {
            get
            {
                return _loginFailedVisibility;
            }
            set
            {
                _loginFailedVisibility = value;
                OnPropertyChanged("LoginFailedVisibility");
            }
        }

        #endregion //Properties

        #region "Methods"
        public  void LogIn(Object obj)
        {
            UsersManager usersManager = new UsersManager();
            US_User user = usersManager.GetByUserName(UserName);
            if(user != null)
            {
                if(_userPassword.Password == user.US_PASSWORD)
                {
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                    _loginView.Close();         
                }
                else
                {
                    LoginFailedVisibility = Visibility.Visible;
                }
            }
            else
            {
                LoginFailedVisibility = Visibility.Visible;
            }
        }
        #endregion //Methods
    }
}
