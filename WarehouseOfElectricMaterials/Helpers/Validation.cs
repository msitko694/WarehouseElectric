using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections;

namespace WarehouseElectric.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValid(DependencyObject parent)
        {
            if(Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for(int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if(!IsValid(child)) { return false; }
            }

            return true;
        }

        public static void ValidationOfTextBoxes(object current)
        {
            if(current.GetType() == typeof(TextBox))
            {
                ((TextBox)current).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }

            DependencyObject depObj = current as DependencyObject;

            if(depObj != null)
            {
                foreach(object logicalChild in LogicalTreeHelper.GetChildren(depObj))
                {
                    ValidationOfTextBoxes(logicalChild);
                }
            }
        }

    }
}
