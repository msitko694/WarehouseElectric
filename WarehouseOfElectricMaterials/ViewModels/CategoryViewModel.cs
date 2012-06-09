using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Models;
using System.Windows;
using System.Data.SqlClient;

namespace WarehouseElectric.ViewModels
{
    class CategoryViewModel : ViewModelBase
    {

        #region "Constructor"

        public CategoryViewModel(bool shouldBeLoadedFromDb = false)
        {
            if(shouldBeLoadedFromDb)
            {
                ReadCategoriesFromDbIntoList();
            }
        }

        #endregion 

        #region "Fields

        private PC_ProductCategory _productCategory;
        private IList<CategoryViewModel> _childern;
        private bool _isSelected;
        private bool _isExpanded;
        private ProductCategoriesManager _productCategoriesManager;
        private String _newCategoryName;
        private bool _isRootCategory;
        private IList<CategoryViewModel> _rootCategories;
        private RelayCommand _deleteCategoryCommand;
        private RelayCommand _addNewCategoryCommand;

        #endregion

        #region "Properties

        public String NewCategoryName
        {
            get
            {
                return _newCategoryName;
            }
            set
            {
                _newCategoryName = value;
                OnPropertyChanged("NewCategoryName");
            }
        }

        public IList<CategoryViewModel> RootCategories
        {
            get
            {
                return _rootCategories;
            }
            set
            {
                _rootCategories = value;
                OnPropertyChanged("RootCategories");
            }
        }

        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                if(_deleteCategoryCommand == null)
                {
                    _deleteCategoryCommand = new RelayCommand(DeleteCategory);
                    _deleteCategoryCommand.CanUndo = (obj) => false;
                }
                return _deleteCategoryCommand;
            }
            set
            {
                _deleteCategoryCommand = value;
            }
        }

        public RelayCommand AddNewCategoryCommand
        {
            get
            {
                if(_addNewCategoryCommand == null)
                {
                    _addNewCategoryCommand = new RelayCommand(AddNewCategory);
                    _addNewCategoryCommand.CanUndo = (obj) => false;
                }
                return _addNewCategoryCommand;
            }
            set
            {
                _addNewCategoryCommand = value;
            }
        }

        public ProductCategoriesManager ProductCategoriesManager
        {
            get
            {
                if(_productCategoriesManager == null)
                {
                    _productCategoriesManager = new ProductCategoriesManager();
                }
                return _productCategoriesManager;
            }
            set
            {
                _productCategoriesManager = value;
            }
        }

        public bool IsRootCategory
        {
            get
            {
                return _isRootCategory;
            }
            set
            {
                _isRootCategory = value;
                OnPropertyChanged("IsRootCategory");
            }
        }

        public PC_ProductCategory ProductCategory
        {
            get
            {
                return _productCategory;
            }
            set
            {
                _productCategory = value;
                OnPropertyChanged("ProductCategory");
            }
        }

        public IList<CategoryViewModel> Children
        {
            get
            {
                return _childern;
            }
            set
            {
                _childern = value;
                OnPropertyChanged("Children");
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded= value;
                OnPropertyChanged("IsExpanded");
            }
        }

        #endregion

        #region "Methods"

        public void DeleteCategory(object obj)
        {
            CategoryViewModel selectedCategory = GetSelectedCategory();
            if(selectedCategory.ProductCategory != null)
            {
                try
                {
                    RecursiveDeleteCategories(selectedCategory);
                    ProductCategoriesManager.Delete(selectedCategory.ProductCategory);
                    ReadCategoriesFromDbIntoList();
                }
                catch(SqlException)
                {
                    MessageBox.Show("Nie można usunąć kategorii ponieważ istnieją przypisane do niej (lub do kategorii potomnych) produkty");
                }
            }
        }

        public void RecursiveDeleteCategories(CategoryViewModel category)
        {
            foreach(var child in category.Children)
            {
                RecursiveDeleteCategories(child);
                ProductCategoriesManager.Delete(child.ProductCategory);
            }
        }

        public void AddNewCategory(object obj)
        {
            int? parentCategoryId = null;
            if(!IsRootCategory)
            {
                if(GetSelectedCategory().ProductCategory != null)
                {
                    parentCategoryId = GetSelectedCategory().ProductCategory.PC_ID;
                }
                else
                {
                    MessageBox.Show("Wybierz kategorię nadrzędną lub dodaj jako kategorię główną");
                    return;
                }
            }

            PC_ProductCategory productCategory = new PC_ProductCategory
            {
                PC_NAME = NewCategoryName,
                PC_PC_ID = parentCategoryId
            };
            ProductCategoriesManager.Add(productCategory);

            ReadCategoriesFromDbIntoList();
        }

        public CategoryViewModel GetSelectedCategory()
        {
            CategoryViewModel selectedCategory = new CategoryViewModel();
            foreach(var cat in RootCategories)
            {
                if(!cat.IsSelected)
                {
                    IList<CategoryViewModel> categoriesList = new List<CategoryViewModel>();
                    GetAllChildrenCategories(cat, categoriesList);
                    categoriesList = categoriesList.Where(x => x.IsSelected).Select(x => x).ToList();
                    if(categoriesList.Count > 0)
                    {
                        selectedCategory = categoriesList[0];
                    }
                }
                else
                {
                    selectedCategory = cat;
                    break;
                }
            }

            return selectedCategory;
        }

        public void GetAllChildrenCategories(CategoryViewModel category, IList<CategoryViewModel> childCategoriesList)
        {
            foreach(var child in category.Children)
            {
                //Execute recursive for all nested childs
                GetAllChildrenCategories(child, childCategoriesList);
                //Check if category with specified ID already exists on list. Add to the list if not exists.
                if(childCategoriesList.Where((x) => x.ProductCategory.PC_ID == child.ProductCategory.PC_ID).Select(x => x).ToList().Count == 0)
                {
                    childCategoriesList.Add(child);
                }
            }
        }

        public void ReadCategoriesFromDbIntoList()
        {
            IList<PC_ProductCategory> unorderedList;
            unorderedList = ProductCategoriesManager.GetAll();

            RootCategories = unorderedList.Where((x) => x.PC_PC_ID == null).Select((x) => new CategoryViewModel() { ProductCategory = x }).ToList();

            //get root categories
            foreach(var category in RootCategories)
            {
                //build categories tree
                BuildCategoriesTree(category, unorderedList);
            }
        }

        private void BuildCategoriesTree(CategoryViewModel categoryViewmodel, IList<PC_ProductCategory> unorderedList)
        {
            categoryViewmodel.Children = (from category in unorderedList where category.PC_PC_ID == categoryViewmodel.ProductCategory.PC_ID select new CategoryViewModel() { ProductCategory = category }).ToList();

            foreach(var category in categoryViewmodel.Children)
            {
                BuildCategoriesTree(category, unorderedList);
            }
        }


        protected override void OnDispose()
        {
            if(_productCategoriesManager != null)
            {
                _productCategoriesManager.Dispose();
            }
            base.OnDispose();
        }

        #endregion
    }
}
