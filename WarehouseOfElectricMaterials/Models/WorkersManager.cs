using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class WorkersManager : IWorkersManager
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

        #region IWorkersManager Members


        /// <summary>
        /// Gets the worker by using his worker name.
        /// </summary>
        /// <param name="workername","workersurname">The worker name.</param>
        /// <returns>Worker with specified name</returns>
        public WO_Worker GetByWorkerName(String workername, String workersurname)
        {
            List<WO_Worker> workerList = (from worker in DataContext.WO_Workers where (worker.WO_NAME == workername 
                                                                                        && worker.WO_SURNAME == workersurname) 
                                          select worker).ToList<WO_Worker>();

            if (workerList.Count > 0)
            {
                return workerList[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the worker by using his worker surname.
        /// </summary>
        /// <param name="workersurname">The worker name.</param>
        /// <returns>Worker with specified name</returns>
        public IList<WO_Worker> GetByWorkerSurname(String workersurname)
        {
            return (from worker in DataContext.WO_Workers
                    where worker.WO_SURNAME.Contains(workersurname)
                    select worker).ToList<WO_Worker>();
            
        }

        /// <summary>
        /// Gets the worker by using his worker pesel.
        /// </summary>
        /// <param name="workerpesel">The worker pesel.</param>
        /// <returns>Worker with specified pesel</returns>
        public WO_Worker GetByWorkerPesel(String workerpesel)
        {
            List<WO_Worker> workerList = (from worker in DataContext.WO_Workers where worker.WO_PESEL == workerpesel select worker).ToList<WO_Worker>();

            if (workerList.Count > 0)
            {
                return workerList[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Gets all workers.
        /// </summary>
        /// <returns>All workers</returns>
        public IList<WO_Worker> GetAll()
        {
           return (from worker in DataContext.WO_Workers select worker).ToList<WO_Worker>();
        }

        /// <summary>
        /// Gets the specified worker.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A worker with specified id</returns>
        public WO_Worker Get(int id)
        {
            List<WO_Worker> workersList = (from worker in DataContext.WO_Workers
                                                       where worker.WO_ID == id
                                                       select worker).ToList <WO_Worker>();

            if (workersList.Count > 0)
            {
                return workersList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified worker.
        /// </summary>
        /// <param name="worker">The worker.</param>
        public void Add(WO_Worker worker)
        {
            DataContext.WO_Workers.InsertOnSubmit(worker);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified worker.
        /// </summary>
        /// <param name="worker">The worker.</param>
        public void Delete(DataLayer.WO_Worker worker)
        {
            DataContext.WO_Workers.DeleteOnSubmit(worker);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to WO_Worker objects.
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
