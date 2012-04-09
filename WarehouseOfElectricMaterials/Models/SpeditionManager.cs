using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class SpeditionManager : ISpeditionManager
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

        #region ISpeditionManager Members

        /// <summary>
        /// Gets all spedition.
        /// </summary>
        /// <returns>All spedition</returns>
        public IList<SP_Spedition> GetAll()
        {
           return (from spedition in DataContext.SP_Speditions select spedition).ToList<SP_Spedition>();
        }

        /// <summary>
        /// Gets the specified spedition.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A spedition with specified id</returns>
        public SP_Spedition Get(int id)
        {
            List<SP_Spedition> speditionList = (from spedition in DataContext.SP_Speditions
                                                       where spedition.SP_ID == id
                                                       select spedition).ToList <SP_Spedition>();

            if (speditionList.Count > 0)
            {
                return speditionList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified spedition.
        /// </summary>
        /// <param name="product">The spedition.</param>
        public void Add(SP_Spedition spedition)
        {
            DataContext.SP_Speditions.InsertOnSubmit(spedition);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified spedition.
        /// </summary>
        /// <param name="product">The spedition.</param>
        public void Delete(DataLayer.SP_Spedition spedition)
        {
            DataContext.SP_Speditions.DeleteOnSubmit(spedition);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to SP_Spedition objects.
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
