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
    class CashierViewModel:ViewModelBase
    {
        #region "Contructors"
        public CashierViewModel(CashierView cashierView)
        {
            _cashierView = cashierView;
        }

        #endregion //Constructors

        #region "Fields"
        private CashierView _cashierView;
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
        #endregion //Properties
        
        #region "Methods"
        public void LogOut(Object obj)
        {
            SessionHelper.LogOut();
            Application.Current.MainWindow = new LoginView();
            Application.Current.MainWindow.Show();

            _cashierView.Close();

        }
        #endregion //Methods
    }
}
