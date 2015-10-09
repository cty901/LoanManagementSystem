using LoanManagementSystem.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Util
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate CustomerDataTemplate { get; set; }
        public DataTemplate EmployeeDataTemplate { get; set; }
        public DataTemplate LoanTypeDataTemplate { get; set; }

        //You override this function to select your data template based in the given item
        public override System.Windows.DataTemplate SelectTemplate(object item,System.Windows.DependencyObject container)
        {
            if (item == null) return null;
            FrameworkElement frameworkElement = container as FrameworkElement;
            if (frameworkElement != null)
            {
                DataTemplate Template = null;
                if (item is customer)
                {
                    Template = frameworkElement.FindResource("customerDataTemplate") as DataTemplate;
                }
                if (item is employee)
                {
                    Template = frameworkElement.FindResource("employeeDataTemplate") as DataTemplate;
                }
                if (item is loan_type)
                {
                    Template = frameworkElement.FindResource("loanTypeDataTemplate") as DataTemplate;
                }
                return Template;
            }
            else return null;
        }
    }
}
