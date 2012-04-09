using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Entities;

namespace WarehouseElectric.Models
{
    class ProductsManager : IProductsManager
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

        #region IProductManager Members


        /// <summary>
        /// Gets the product by using name.
        /// </summary>
        /// <param name="productname">The product name.</param>
        /// <returns>Product with specified name</returns>
        public PR_Product GetByProductName(String productname)
        {
            List<PR_Product> productsList = (from product in DataContext.PR_Products where product.PR_NAME == productname select product).ToList<PR_Product>();

            if (productsList.Count > 0)
            {
                return productsList[0];
            }
            else
            {
                return null;
            }
        }

        


        /// <summary>
        /// Gets all product.
        /// </summary>
        /// <returns>All product</returns>
        public IList<PR_Product> GetAll()
        {
           return (from product in DataContext.PR_Products select product).ToList<PR_Product>();
        }

        /// <summary>
        /// Gets the specified product.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A product with specified id</returns>
        public PR_Product Get(int id)
        {
            List<PR_Product> croductList = (from product in DataContext.PR_Products
                                                       where product.PR_ID == id
                                                       select product).ToList <PR_Product>();

            if (croductList.Count > 0)
            {
                return croductList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Add(PR_Product product)
        {
            DataContext.PR_Products.InsertOnSubmit(product);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void Delete(DataLayer.PR_Product product)
        {
            DataContext.PR_Products.DeleteOnSubmit(product);
            DataContext.SubmitChanges();
        }

        /// <summary>
        /// Updates changes made to PR_Product objects.
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
