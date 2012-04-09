using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IInvoicesItemsManager : IDisposable
    {
        IList<IE_InvoicesItem> GetAll();
        IE_InvoicesItem Get(int id);
        void Add(IE_InvoicesItem invoicesItem);
        void Delete(IE_InvoicesItem invoicesItem);
        void Update();
    }
}
