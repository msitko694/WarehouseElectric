using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface ISuppliersManager : IDisposable
    {
        IList<SU_Supplier> GetAll();
        SU_Supplier Get(int id);
        SU_Supplier GetBySupplierName(String supplierName);
        void Add(SU_Supplier supplier);
        void Delete(SU_Supplier supplier);
        void Update();
    }
}
