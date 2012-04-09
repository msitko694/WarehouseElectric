using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IWorkersManager : IDisposable
    {
        IList<WO_Worker> GetAll();
        WO_Worker Get(int id);
        WO_Worker GetByWorkerName(String workerName, String workerSurname);
        WO_Worker GetByWorkerPesel(String workerPesel);
        void Add(WO_Worker worker);
        void Delete(WO_Worker worker);
        void Update();
    }
}
