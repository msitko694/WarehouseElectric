using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface ISpeditionManager : IDisposable
    {
        IList<SP_Spedition> GetAll();
        SP_Spedition Get(int id);
        void Add(SP_Spedition spedition);
        void Delete(SP_Spedition spedition);
        void Update();
    }
}
