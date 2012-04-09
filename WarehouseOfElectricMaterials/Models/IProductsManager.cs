using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IProductsManager : IDisposable
    {
        IList<PR_Product> GetAll();
        PR_Product Get(int id);
        PR_Product GetByProductName(String productname);
       
        void Add(PR_Product product);
        void Delete(PR_Product product);
        void Update();
    }
}
