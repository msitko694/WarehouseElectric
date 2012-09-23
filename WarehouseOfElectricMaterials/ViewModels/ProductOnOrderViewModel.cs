using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.ViewModels
{
    class ProductOnOrderViewModel : ViewModelBase
    {

        #region "Fields"

        PR_Product _product;
        decimal _quantityOnOrder;

        #endregion

        #region "Properties"

        public PR_Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public decimal QuantityOnOrder
        {
            get
            {
                return _quantityOnOrder;
            }
            set
            {
                
                    _quantityOnOrder = value;
                    OnPropertyChanged("QuantityOnOrder");
                
            }
        }

        #endregion 
    }
}
