using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Models
{
    class UsersManager : IUsersManager
    {

        #region "Fields"

        private LinqDataLayerDataContext _dataContext;

        #endregion //fields

        #region "Properties"

        public LinqDataLayerDataContext DataContext
        {
            get
            {
                if(_dataContext == null)
                {
                    _dataContext = new LinqDataLayerDataContext();
                }
                return _dataContext;
            }
            set
            {
                _dataContext = value;
            }
        }

        #endregion //properties

        #region IUserManager Members

        /// <summary>
        /// Gets the user by using his user name.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User with specified name</returns>
        public US_User GetByUserName(String username)
        {
            List<US_User> usersList = (from user in DataContext.US_Users where user.US_USERNAME == username select user).ToList<US_User>();

            if(usersList.Count > 0)
            {
                return usersList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>A List of all users</returns>
        public IList<DataLayer.US_User> GetAll()
        {
            return (from user in DataContext.US_Users select user).ToList<US_User>();
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The user with specified id.</returns>
        public DataLayer.US_User Get(int id)
        {
            List<US_User> usersList = (from user in DataContext.US_Users where user.US_ID == id select user).ToList<US_User>();

            if(usersList.Count > 0)
            {
                return usersList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Add(DataLayer.US_User user)
        {
            DataContext.US_Users.InsertOnSubmit(user);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Delete(DataLayer.US_User user)
        {
            DataContext.US_Users.DeleteOnSubmit(user);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Persist all changes into database.
        /// </summary>
        public void Update()
        {
            DataContext.SubmitChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if(isDisposing)
            {
                if(_dataContext != null)
                {
                    _dataContext.Dispose();
                }
                _dataContext = null;
            }
        }

        #endregion
    }
}
