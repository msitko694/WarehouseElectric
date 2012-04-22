using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IPositionsManager : IDisposable
    {
        IList<PO_Position> GetAll();
        List<String> GetAllName();
        PO_Position Get(int id);
        PO_Position GetByPositionName(String positionName);
        void Add(PO_Position position);
        void Delete(PO_Position position);
        void Update();
    }
}
