using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IProductCategoriesManager : IDisposable
    {
        IList<PC_ProductCategory> GetAll();
        PC_ProductCategory Get(int id);
        void Add(PC_ProductCategory product);
        void Delete(PC_ProductCategory product);
        void Update();
    }
}
