using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class OrderItemsManager : IOrderItemsManager
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

        #region IOrderItemsManager Members

        /// <summary>
        /// Gets all Order items.
        /// </summary>
        /// <returns>All Order items</returns>
        public IList<OE_OrderItem> GetAll()
        {
           return (from orderItem in DataContext.OE_OrderItems select orderItem).ToList<OE_OrderItem>();
        }
        /// <summary>
        /// Gets all Order items by order Id.
        /// </summary>
        /// <returns>All Order items</returns>
        public IList<OE_OrderItem> GetAllByOrderID(int id)
        {
            return (from orderItem in DataContext.OE_OrderItems where orderItem.OE_OR_ID == id select orderItem).ToList<OE_OrderItem>();
        }

        /// <summary>
        /// Gets the specified order item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>An order item with specified id</returns>
        public OE_OrderItem Get(int id)
        {
            List<OE_OrderItem> orderItemList = (from orderItem in DataContext.OE_OrderItems
                                                       where orderItem.OE_ID == id
                                                       select orderItem).ToList <OE_OrderItem>();

            if (orderItemList.Count > 0)
            {
                return orderItemList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified order item.
        /// </summary>
        /// <param name="orderItem">The order item.</param>
        public void Add(OE_OrderItem orderItem)
        {
            DataContext.OE_OrderItems.InsertOnSubmit(orderItem);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified order item.
        /// </summary>
        /// <param name="orderItem">The order item.</param>
        public void Delete(DataLayer.OE_OrderItem orderItem)
        {
            DataContext.OE_OrderItems.DeleteOnSubmit(orderItem);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to OE_OrderItem objects.
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
