using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class InvoicesItemsManager : IInvoicesItemsManager
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

        #region IInvoicesItemsManager Members

        /// <summary>
        /// Gets all invoices item.
        /// </summary>
        /// <returns>All item</returns>
        public IList<IE_InvoicesItem> GetAll()
        {
           return (from invoicesItem in DataContext.IE_InvoicesItems select invoicesItem).ToList<IE_InvoicesItem>();
        }

        public IList<IE_InvoicesItem> GetByInvoiceId(int invoiceId)
        {
            return (from invoicesItem in DataContext.IE_InvoicesItems
                    where invoicesItem.IE_IN_ID == invoiceId
                    select invoicesItem).ToList<IE_InvoicesItem>();
        }

        /// <summary>
        /// Gets the specified item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A invoices item with specified id</returns>
        public IE_InvoicesItem Get(int id)
        {
            List<IE_InvoicesItem> invoicesItemList = (from invoicesItem in DataContext.IE_InvoicesItems
                                                      where invoicesItem.IE_ID == id
                                                      select invoicesItem).ToList<IE_InvoicesItem>();

            if (invoicesItemList.Count > 0)
            {
                return invoicesItemList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="invoicesItem">The invoices item.</param>
        public void Add(IE_InvoicesItem invoicesItem)
        {
            DataContext.IE_InvoicesItems.InsertOnSubmit(invoicesItem);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="invoicesItem">The invoices item.</param>
        public void Delete(DataLayer.IE_InvoicesItem invoicesItem)
        {
            DataContext.IE_InvoicesItems.DeleteOnSubmit(invoicesItem);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to IE_InvoicesItem objects.
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
