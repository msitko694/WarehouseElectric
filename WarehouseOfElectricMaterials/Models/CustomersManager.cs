using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class CustomersManager : ICustumersManager
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

        #region ICustumersManager Members


        /// <summary>
        /// Gets the customer by using his name.
        /// </summary>
        /// <param name="customername">The Customername.</param>
        /// <returns>Customer with specified name</returns>
        public CU_Customer GetByCustomerName(String customerName)
        {
            List<CU_Customer> customerList = (from customer in DataContext.CU_Customers where customer.CU_NAME == customerName select customer).ToList<CU_Customer>();

            if (customerList.Count > 0)
            {
                return customerList[0];
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>All customers</returns>
        public IList<CU_Customer> GetAll()
        {
           return (from customer in DataContext.CU_Customers select customer).ToList<CU_Customer>();
        }

        public IList<CU_Customer> GetByName(string name)
        {
            return (from customer in DataContext.CU_Customers
                    where customer.CU_NAME.Contains(name)
                    select customer).ToList<CU_Customer>();
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A customer with specified id</returns>
        public CU_Customer Get(int id)
        {
            List<CU_Customer> customersList = (from customer in DataContext.CU_Customers
                                                       where customer.CU_ID == id
                                                       select customer).ToList <CU_Customer>();

            if (customersList.Count > 0)
            {
                return customersList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void Add(CU_Customer customer)
        {
            DataContext.CU_Customers.InsertOnSubmit(customer);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void Delete(DataLayer.CU_Customer customer)
        {
            DataContext.CU_Customers.DeleteOnSubmit(customer);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to CU_Customer objects.
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
