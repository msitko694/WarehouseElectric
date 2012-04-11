using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseElectric
{
    class SessionHelper
    {
        public static Boolean isLogged = false;
        private static int _userId;
        public static int userId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                isLogged = true;
            }
            
        }
        static void LogiIn(int loggedUserId)
        {
            isLogged = true;
            userId = loggedUserId;
        }
        internal static void LogOut()
        {
            isLogged = false;
        }
    }
}
