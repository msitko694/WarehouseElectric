using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IInvoicesManager : IDisposable
    {
        IList<IN_Invoice> GetAll();
        IN_Invoice Get(int id);
        IList<IN_Invoice> GetByCustomerId(int customerId);
        IList<IN_Invoice> GetByWorkerId(int workerId);
        void Add(IN_Invoice invoice);
        void Delete(IN_Invoice invoice);
        void Update();
    }
}
