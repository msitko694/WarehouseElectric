using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Models
{
    class CompanyManager : ICompanyManager
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

        #region ICompanyManager Members

        /// <summary>
        /// Sets the company data.
        /// </summary>
        /// <param name="companyInfo"></param>
        public void SetCompanyData(DataLayer.CI_CompanyInfo companyInfo)
        {
            if(companyInfo.CI_ID == 0)
            {
                DataContext.CI_CompanyInfos.InsertOnSubmit(companyInfo);
                DataContext.SubmitChanges();
            }
            else
            {
                CI_CompanyInfo existedInfo = (from info in DataContext.CI_CompanyInfos select info).ToList<CI_CompanyInfo>()[0];
                existedInfo.CI_ADDED = companyInfo.CI_ADDED;
                existedInfo.CI_LAST_MODIFIED = companyInfo.CI_LAST_MODIFIED;
                existedInfo.CI_PHONE = companyInfo.CI_PHONE;
                existedInfo.CI_POST_CODE = companyInfo.CI_POST_CODE;
                existedInfo.CI_STREET = companyInfo.CI_STREET;
                existedInfo.CI_TOWN = companyInfo.CI_TOWN;
                existedInfo.CI_NAME = companyInfo.CI_NAME;
                DataContext.SubmitChanges();
            }
        }

        /// <summary>
        /// Gets the company data.
        /// </summary>
        public CI_CompanyInfo GetCompanyData()
        {
            return (from info in DataContext.CI_CompanyInfos select info).ToList<CI_CompanyInfo>()[0];
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
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
