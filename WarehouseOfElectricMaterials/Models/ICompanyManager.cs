using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Models
{
    interface ICompanyManager : IDisposable
    {
        /// <summary>
        /// Sets the company data.
        /// </summary>
        void SetCompanyData(CI_CompanyInfo companyInfo);
        
        /// <summary>
        /// Gets the company data.
        /// </summary>
        /// <returns></returns>
        CI_CompanyInfo GetCompanyData();
    }
}
