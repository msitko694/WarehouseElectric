using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class InvoicesManager : IInvoicesManager
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

        #region IInvoicesManager Members

        /// <summary>
        /// Gets the Invoice by using customer ID.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        /// <returns>Invoice with specified customer</returns>
        public IList<IN_Invoice> GetByCustomerId(int customerId)
        {
            return (from invoice in DataContext.IN_Invoices where invoice.IN_CU_ID == customerId select invoice).ToList<IN_Invoice>();
        }


        /// <summary>
        /// Gets the Invoice by using worker ID.
        /// </summary>
        /// <param name="workerId">The workerId.</param>
        /// <returns>Invoice with specified worker</returns>
        public IList<IN_Invoice> GetByWorkerId(int workerId)
        {
            return (from invoice in DataContext.IN_Invoices where invoice.IN_WO_ID == workerId select invoice).ToList<IN_Invoice>();
        }



        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns>All invoices</returns>
        public IList<IN_Invoice> GetAll()
        {
           return (from invoice in DataContext.IN_Invoices select invoice).ToList<IN_Invoice>();
        }

        /// <summary>
        /// Gets the specified invoice.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An invoice with specified id</returns>
        public IN_Invoice Get(int id)
        {
            List<IN_Invoice> invoiceList = (from invoice in DataContext.IN_Invoices
                                                       where invoice.IN_ID == id
                                                       select invoice).ToList <IN_Invoice>();

            if (invoiceList.Count > 0)
            {
                return invoiceList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        public void Add(IN_Invoice invoice)
        {
            DataContext.IN_Invoices.InsertOnSubmit(invoice);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        public void Delete(DataLayer.IN_Invoice invoice)
        {
            DataContext.IN_Invoices.DeleteOnSubmit(invoice);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to IN_Invoice objects.
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
