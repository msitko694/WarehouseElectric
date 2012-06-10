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
    /// Interaction logic for CashierView.xaml
    /// </summary>
    public partial class CashierView : Window
    {
        public CashierView()
        {
            InitializeComponent();
            DataContext = new CashierViewModel(this);
            treeViewChooseProductCategory.DataContext = new CategoryViewModel(true);
            ApplyScrollForCustomerGrid();
        }

        private void ApplyScrollForCustomerGrid()
        {
            object item = null;
            DataGridColumn column = null;
            if(dataGridCustomers.Items.Count > 0)
            {
                item = dataGridCustomers.Items[0];
            }
            if(dataGridCustomers.Columns.Count > 0)
            {
                column = dataGridCustomers.Columns[0];
            }
            dataGridCustomers.ScrollIntoView(item, column);
        }
    }
}
