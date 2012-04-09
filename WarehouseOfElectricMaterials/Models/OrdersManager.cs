using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class OrdersManager : IOrdersManager
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

        #region IOrdersManager Members

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>All orders</returns>
        public IList<OR_Order> GetAll()
        {
           return (from order in DataContext.OR_Orders select order).ToList<OR_Order>();
        }

        /// <summary>
        /// Gets the specified order.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A order with specified id</returns>
        public OR_Order Get(int id)
        {
            List<OR_Order> orderList = (from order in DataContext.OR_Orders
                                                       where order.OR_ID == id
                                                       select order).ToList <OR_Order>();

            if(orderList.Count > 0)
            {
                return orderList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified order.
        /// </summary>
        /// <param name="product">The order.</param>
        public void Add(OR_Order order)
        {
            DataContext.OR_Orders.InsertOnSubmit(order);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified order.
        /// </summary>
        /// <param name="product">The order.</param>
        public void Delete(DataLayer.OR_Order order)
        {
            DataContext.OR_Orders.DeleteOnSubmit(order);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to OR_Order objects.
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
