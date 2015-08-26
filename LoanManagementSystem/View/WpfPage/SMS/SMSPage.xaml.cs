using LoanManagementSystem.View.WpfPage.SMS.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoanManagementSystem.View.WpfPage.SMS
{
    /// <summary>
    /// Interaction logic for SMSPage.xaml
    /// </summary>
    public partial class SMSPage : Page
    {
        private static SMSPage instance;

         public SMSPage()
        {
            InitializeComponent();
            hideMenu();
            ContentFrame.Content = ContentPageSMS.Instance;
        }

        public static SMSPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SMSPage();
                }
                return instance;
            }
        }

      

        private void collapseAllMenuItems()
        {
            foreach (Button child in MenuBar.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;

            }
        }

        private void hideMenu()
        {
            collapseAllMenuItems();
            SelectedLoanPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
  
    }
}
