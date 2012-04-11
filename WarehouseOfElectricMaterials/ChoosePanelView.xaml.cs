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
    /// Interaction logic for ChoosePanelView.xaml
    /// </summary>
    public partial class ChoosePanelView : Window
    {
        public ChoosePanelView()
        {
            InitializeComponent();
            DataContext = new ChoosePanelViewModel(this);
        }
 
    }
}
