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
    class StorekeeperViewModel:ViewModelBase
    {
        #region "Constructors"
        public StorekeeperViewModel(StorekeeperView storekeeperView)
        {
            _storekeeperView = storekeeperView;

            //podpinanie listy dostawców do dataGrida z dostawcami
            SuppliersManager suppliersManager = new SuppliersManager();
            ListSuppliersToShow = suppliersManager.GetAll();

        }
        #endregion //Constructors
        #region "Fields"
            private StorekeeperView _storekeeperView;
            private IList<SU_Supplier> _listSuppliersToShow;
        #endregion //Fields
        #region "Properties"
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
        #endregion //Methods
    }
}
