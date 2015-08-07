using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LoanManagementSystem.Util
{
    class HandleControllers
    {
        private static List<Control> ControlList;

        public static List<T> GetLogicalChildCollection<T>(object parent) where T : DependencyObject
        {
            List<T> logicalCollection = new List<T>();
            GetLogicalChildCollection(parent as DependencyObject, logicalCollection);
            return logicalCollection;
        }
        private static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);                        
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        public static void enableContent(List<Control> controllist, Boolean enableTextbox, Boolean enableCombobox, Boolean enableButton, Boolean enableDateTimePicker, Boolean enableRadioButtonPicker)
        {
            ControlList = controllist;

            foreach (Control tb in ControlList)
            {               
                if (tb is TextBox)
                {
                    tb.IsEnabled = enableTextbox;
                    tb.Background= null;
                }
                else if (tb is ComboBox)
                {
                    tb.IsEnabled = enableCombobox;
                    tb.Foreground = Brushes.Black;
                }
                else if (tb is Button)
                {
                    tb.IsEnabled = enableButton;
                    tb.Foreground = Brushes.Black;
                    tb.Background = Brushes.Transparent;
                }
                else if (tb is DatePicker)
                {
                    tb.IsEnabled = enableDateTimePicker;
                }
                else if (tb is RadioButton)
                {
                    tb.IsEnabled = enableRadioButtonPicker;
                    tb.Foreground = Brushes.Black;
                }
            }
        }
    }
}
