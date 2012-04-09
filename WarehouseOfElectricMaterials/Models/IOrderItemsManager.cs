using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IOrderItemsManager : IDisposable
    {
        IList<OE_OrderItem> GetAll();
        OE_OrderItem Get(int id);
        void Add(OE_OrderItem orderItem);
        void Delete(OE_OrderItem orderItem);
        void Update();
    }
}
