using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class PositionsManager : IPositionsManager
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

        #region IPositionsManager Members


        /// <summary>
        /// Gets the position by using position name.
        /// </summary>
        /// <param name="positionName">The position name.</param>
        /// <returns>User with specified name</returns>
        public PO_Position GetByPositionName(String positionName)
        {
            List<PO_Position> positionsList = (from position in DataContext.PO_Positions where position.PO_NAME == positionName select position).ToList<PO_Position>();

            if (positionsList.Count > 0)
            {
                return positionsList[0];
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// Gets all positions.
        /// </summary>
        /// <returns>All positions</returns>
        public IList<PO_Position> GetAll()
        {
           return (from position in DataContext.PO_Positions select position).ToList<PO_Position>();
        }

        /// <summary>
        /// Gets all positions name.
        /// </summary>
        /// <returns>All positions name</returns>
        public List<String> GetAllName()
        {
            return (from position in DataContext.PO_Positions select position.PO_NAME).ToList();
        }

        /// <summary>
        /// Gets the specified position.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A position with specified id</returns>
        public PO_Position Get(int id)
        {
            List<PO_Position> positionsList = (from position in DataContext.PO_Positions
                                                       where position.PO_ID == id
                                                       select position).ToList <PO_Position>();

            if (positionsList.Count > 0)
            {
                return positionsList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the position.
        /// </summary>
        /// <param name="product">The position.</param>
        public void Add(PO_Position position)
        {
            DataContext.PO_Positions.InsertOnSubmit(position);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified position.
        /// </summary>
        /// <param name="product">The position.</param>
        public void Delete(DataLayer.PO_Position position)
        {
            DataContext.PO_Positions.DeleteOnSubmit(position);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to PO_Position objects.
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
