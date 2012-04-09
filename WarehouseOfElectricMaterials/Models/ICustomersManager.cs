using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface ICustumersManager : IDisposable
    {
        IList<CU_Customer> GetAll();
        CU_Customer Get(int id);
        CU_Customer GetByCustomerName(String customerName);
        void Add(CU_Customer customer);
        void Delete(CU_Customer customer);
        void Update();
    }
}
