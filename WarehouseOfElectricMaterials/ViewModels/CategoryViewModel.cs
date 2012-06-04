using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.ViewModels
{
    class CategoryViewModel : ViewModelBase
    {
        #region "Fields

        private PC_ProductCategory _productCategory;
        private IList<CategoryViewModel> _childern;
        private bool _isSelected;
        private bool _isExpanded;

        #endregion

        #region "Properties

        public PC_ProductCategory ProductCategory
        {
            get
            {
                return _productCategory;
            }
            set
            {
                _productCategory = value;
                OnPropertyChanged("ProductCategory");
            }
        }

        public IList<CategoryViewModel> Children
        {
            get
            {
                return _childern;
            }
            set
            {
                _childern = value;
                OnPropertyChanged("Children");
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded= value;
                OnPropertyChanged("IsExpanded");
            }
        }

        #endregion

    }
}
