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
    /// Interaction logic for StorekeeperView.xaml
    /// </summary>
    public partial class StorekeeperView : Window
    {
        public StorekeeperView()
        {
            InitializeComponent();
            CategoryViewModel categoryViewModel = new CategoryViewModel(true);
            StorekeeperViewModel storekeeperViewModel = new StorekeeperViewModel(this, categoryViewModel);
            DataContext = storekeeperViewModel;
            treeViewChooseProductCategory.DataContext = categoryViewModel;
            treeViewChooseProductCategory.SelectedItemChanged += (sender, e) =>
            {
                storekeeperViewModel.ShowProducts();
            };
        }
    }
}
