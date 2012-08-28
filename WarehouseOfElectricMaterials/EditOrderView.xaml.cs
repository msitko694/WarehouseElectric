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
    /// Interaction logic for EditOrderView.xaml
    /// </summary>
    public partial class EditOrderView : Window
    {
        public EditOrderView(Int32 flag)
        {
            /*InitializeComponent();
            CategoryViewModel categoryViewModel = new CategoryViewModel(true);

            EditOrderViewModel editOrderViewModel = new EditOrderViewModel(this, categoryViewModel);
            DataContext = editOrderViewModel;
            treeViewSelectCategory.DataContext = categoryViewModel;
            treeViewSelectCategory.SelectedItemChanged += (sender, e) =>
            {
                editOrderViewModel.ShowProducts(this);
            };*/
            InitializeComponent();
            CategoryViewModel categoryViewModel = new CategoryViewModel(true);
            treeViewSelectCategory.DataContext = categoryViewModel;
            EditOrderViewModel editOrderViewModel;
            editOrderViewModel = new EditOrderViewModel(this, categoryViewModel, flag);
            treeViewSelectCategory.SelectedItemChanged += (sender, e) =>
            {
                editOrderViewModel.ShowProducts(this);
            };
            DataContext = editOrderViewModel;
        }

       
    }
}
