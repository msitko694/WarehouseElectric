using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IOrdersManager : IDisposable
    {
        IList<OR_Order> GetAll();
        OR_Order Get(int id);
        IList<OR_Order> GetBySupplierName(String name);
        void Add(OR_Order order);
        void Delete(OR_Order order);
        void Update();
    }
}
