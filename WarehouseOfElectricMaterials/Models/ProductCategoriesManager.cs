using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class ProductCategoriesManager : IProductCategoriesManager
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

        #region IProductCategoriesManager Members

        /// <summary>
        /// Gets all product categories.
        /// </summary>
        /// <returns>All product categories</returns>
        public IList<PC_ProductCategory> GetAll()
        {
           return (from category in DataContext.PC_ProductCategories 
                   select category).ToList<PC_ProductCategory>();
        }

        public IList<PC_ProductCategory> GetAllOnBaseLevel()
        {
            return (from category in DataContext.PC_ProductCategories
                    where category.PC_PC_ID == null
                    select category).ToList<PC_ProductCategory>();
        }

        /// <summary>
        /// Gets the specified category.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A product category with specified id</returns>
        public PC_ProductCategory Get(int id)
        {
            List<PC_ProductCategory> categoriesList = (from category in DataContext.PC_ProductCategories
                                                       where category.PC_ID == id
                                                       select category).ToList <PC_ProductCategory>();

            if(categoriesList.Count > 0)
            {
                return categoriesList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the specified category.
        /// </summary>
        /// <param name="product">The product category.</param>
        public void Add(PC_ProductCategory productCategory)
        {
            DataContext.PC_ProductCategories.InsertOnSubmit(productCategory);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="product">The product category.</param>
        public void Delete(DataLayer.PC_ProductCategory productCategory)
        {
            DataContext.PC_ProductCategories.DeleteOnSubmit(productCategory);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to PC_ProductCategory objects.
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
