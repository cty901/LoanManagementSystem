using LoanManagementSystem.DBModel;
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

namespace LoanManagementSystem.View.WpfPage.SMS.Content
{
    /// <summary>
    /// Interaction logic for MakeASMS.xaml
    /// </summary>
    public partial class CreateASMS : Page
    {
        private static CreateASMS _instance;
        private customer _selectedCustomer;

        private CreateASMS()
        {
            InitializeComponent();
        }

        public static CreateASMS Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CreateASMS();
                }
                return _instance;
            }
        }

        public customer SelectedCustomer
        {
            set
            {
               _selectedCustomer=value;
               if (_selectedCustomer != null)
               {
                   CustomerNameTextBox.Text = _selectedCustomer.FULLNAME;
                   CustomerCodeTextBox.Text = _selectedCustomer.CUSTOMER_ID;
                   PhoneNumberTextBox.Text = _selectedCustomer.PHONE_HP1;
               }
            }

        }
    }
}
