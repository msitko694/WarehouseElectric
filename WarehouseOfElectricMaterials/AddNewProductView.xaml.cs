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

namespace WarehouseElectric.ViewModels
{
    /// <summary>
    /// Interaction logic for AddNewProductView.xaml
    /// </summary>
    public partial class AddNewProductView : Window
    {
        public AddNewProductView()
        {
            InitializeComponent();
            CategoryViewModel categoryViewModel = new CategoryViewModel(true);
            treeViewChooseProductCategory.DataContext = categoryViewModel;
            AddNewProductViewModel addNewProductViewModel = new AddNewProductViewModel(this,categoryViewModel);
            DataContext = addNewProductViewModel;
        }
    }
}
