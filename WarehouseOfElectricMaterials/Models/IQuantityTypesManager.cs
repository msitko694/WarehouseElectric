using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    interface IQuantityTypesManager: IDisposable
    {
        IList<QT_QuantityType> GetAll();
        QT_QuantityType Get(int id);
        QT_QuantityType GetByName(String name);
        void Add(QT_QuantityType quantitytype);
        void Delete(QT_QuantityType quantitytype);
        void Update();
    }
}
