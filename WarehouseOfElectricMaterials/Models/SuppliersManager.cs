using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class SuppliersManager : ISuppliersManager
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

        #region ISuppliersManager Members


        /// <summary>
        /// Gets the supplier by using his name.
        /// </summary>
        /// <param name="supplierName">The supplier name.</param>
        /// <returns>supplier with specified name</returns>
        public SU_Supplier GetBySupplierName(String supplierName)
        {
            List<SU_Supplier> supplierList = (from supplier in DataContext.SU_Suppliers where supplier.SU_NAME == supplierName select supplier).ToList<SU_Supplier>();

            if (supplierList.Count > 0)
            {
                return supplierList[0];
            }
            else
            {
                return null;
            }
        }

        public IList<SU_Supplier> GetByName(string name)
        {
            return (from supplier in DataContext.SU_Suppliers
                    where supplier.SU_NAME.Contains(name)
                    select supplier).ToList<SU_Supplier>();
        }


        /// <summary>
        /// Gets all suppliers.
        /// </summary>
        /// <returns>All suppliers</returns>
        public IList<SU_Supplier> GetAll()
        {
           return (from supplier in DataContext.SU_Suppliers select supplier).ToList<SU_Supplier>();
        }

        /// <summary>
        /// Gets the specified supplier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A supplier with specified id</returns>
        public SU_Supplier Get(int id)
        {
            List<SU_Supplier> supplierList = (from supplier in DataContext.SU_Suppliers
                                                       where supplier.SU_ID == id
                                                       select supplier).ToList <SU_Supplier>();

            if(supplierList.Count > 0)
            {
                return supplierList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified supplier.
        /// </summary>
        /// <param name="product">The supplier.</param>
        public void Add(SU_Supplier supplier)
        {
            DataContext.SU_Suppliers.InsertOnSubmit(supplier);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified supplier.
        /// </summary>
        /// <param name="product">The supplier.</param>
        public void Delete(DataLayer.SU_Supplier supplier)
        {
            DataContext.SU_Suppliers.DeleteOnSubmit(supplier);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to SU_Suppliers objects.
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
