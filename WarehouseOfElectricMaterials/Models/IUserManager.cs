using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Models
{
    interface IUsersManager : IDisposable
    {
        IList<US_User> GetAll();
        US_User Get(int id);
        US_User GetByUserName(String username);
        void Add(US_User user);
        void Delete(US_User user);
        void Update();
    }
}
