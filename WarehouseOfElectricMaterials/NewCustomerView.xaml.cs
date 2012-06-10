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
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric
{
    /// <summary>
    /// Interaction logic for NewCustomerView.xaml
    /// </summary>
    public partial class NewCustomerView : Window
    {
        public NewCustomerView(CashierViewModel cashierViewModel)
        {
            InitializeComponent();
                DataContext = new NewCustomerViewModel(new CustomersManager(), this, cashierViewModel);
        }

        public NewCustomerView(CashierViewModel cashierViewModel,  int customerId)
        {
            InitializeComponent();
            DataContext = new NewCustomerViewModel(new CustomersManager(), this, cashierViewModel, customerId);   
        }
    }
}
