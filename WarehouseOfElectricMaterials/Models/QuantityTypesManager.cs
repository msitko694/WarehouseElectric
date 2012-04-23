using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class QuantityTypesManager : IQuantityTypesManager
    {
        #region "Fields"

        private LinqDataLayerDataContext _dataContext;

        #endregion //fields

        #region "Properties"

        public LinqDataLayerDataContext DataContext
        {
            get
            {
                if(_dataContext == null)
                {
                    _dataContext = new LinqDataLayerDataContext();
                }
                return _dataContext;
            }
            set
            {
                _dataContext = value;
            }
        }

        #endregion //properties

        #region IQuantityTypeMenager Members

        /// <summary>
        /// Gets all quantity type
        /// </summary>
        /// <returns>All quantity type</returns>
        public IList<QT_QuantityType> GetAll()
        {
           return (from quantityT in DataContext.QT_QuantityTypes select quantityT).ToList<QT_QuantityType>();
        }

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A quantity type with specified id</returns>
        public QT_QuantityType Get(int id)
        {
            List<QT_QuantityType> QuantityList = (from quantityT in DataContext.QT_QuantityTypes
                                                    where quantityT.QT_ID== id
                                                    select quantityT).ToList<QT_QuantityType>();

            if (QuantityList.Count > 0)
            {
                return QuantityList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A quantity type with specified name</returns>
        public QT_QuantityType GetByName(String name)
        {
            List<QT_QuantityType> QuantityList = (from quantityT in DataContext.QT_QuantityTypes
                                                  where quantityT.QT_NAME == name
                                                  select quantityT).ToList<QT_QuantityType>();

            if (QuantityList.Count > 0)
            {
                return QuantityList[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="product">The quantity type.</param>
        public void Add(QT_QuantityType quantityType)
        {
            DataContext.QT_QuantityTypes.InsertOnSubmit(quantityType);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified type.
        /// </summary>
        /// <param name="product">The quantity type.</param>
        public void Delete(DataLayer.QT_QuantityType quantityType)
        {
            DataContext.QT_QuantityTypes.DeleteOnSubmit(quantityType);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to QT_QuantityType objects.
        /// </summary>
        public void Update()
        {
            DataContext.SubmitChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_dataContext != null)
                {
                    _dataContext.Dispose();
                }
                _dataContext = null;
            }
        }

        #endregion
    }
}
