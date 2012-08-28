using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WarehouseElectric.ViewModels;

namespace WarehouseElectric
{
    /// <summary>
    /// Interaction logic for AddNewOrderView.xaml
    /// </summary>
    public partial class AddNewOrderView : Window
    {
        public AddNewOrderView()
        {
            InitializeComponent();
            //DataContext = new AddNewOrderViewModel(this);
            ///CategoryViewModel categoryViewModel = new CategoryViewModel(true);
            //treeViewSelectProductCategory.DataContext = categoryViewModel;

            CategoryViewModel categoryViewModel = new CategoryViewModel(true);
            CategoryViewModel lackCategoryViewModel = new CategoryViewModel(true);
            AddNewOrderViewModel addNewOrderViewModel = new AddNewOrderViewModel(this, categoryViewModel);
            DataContext = addNewOrderViewModel;
            treeViewSelectProductCategory.DataContext = categoryViewModel;
            treeViewSelectProductCategory.SelectedItemChanged += (sender, e) =>
            {
                addNewOrderViewModel.ShowProducts(this);
            };

        }
    }
}
