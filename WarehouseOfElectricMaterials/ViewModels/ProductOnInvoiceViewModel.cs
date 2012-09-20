using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.ViewModels
{
    class ProductOnInvoiceViewModel : ViewModelBase
    {

        public delegate void OnAmmountChange();
        public OnAmmountChange OnAmmountChangeHandler;

        #region "Fields"

        PR_Product _product;
        decimal _ammountOnInvoice;
        decimal _vat;
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

        public decimal AmmountOnInvoice
        {
            get
            {
                return _ammountOnInvoice;
            }
            set
            {
                if(value < Product.PR_DEPOT_QUANTITY)
                {
                    _ammountOnInvoice = value;
                    OnPropertyChanged("AmmountOnInvoice");
                    if(OnAmmountChangeHandler != null)
                    {
                        OnAmmountChangeHandler();
                    }
                }
            }
        }

        public decimal Vat
        {
            get
            {
                return _vat;
            }
            set
            {
                _vat = value;
                OnPropertyChanged("Vat");
            }
        }

        #endregion 
    }
}
