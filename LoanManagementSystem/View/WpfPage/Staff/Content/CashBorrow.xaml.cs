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

namespace LoanManagementSystem.View.WpfPage.Staff.Content
{
    /// <summary>
    /// Interaction logic for CashBorrow.xaml
    /// </summary>
    public partial class CashBorrow : Page
    {
        private static CashBorrow _instance;

        private CashBorrow()
        {
            InitializeComponent();
        }

        public static CashBorrow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CashBorrow();
                }
                return _instance;
            }

        }

        private void Amount5000Button_Click(object sender, RoutedEventArgs e)
        {
            setAmount("5000.00");
        }

        private void Amount10000Button_Click(object sender, RoutedEventArgs e)
        {
            setAmount("10000.00");
        }

        private void Amount20000Button_Click(object sender, RoutedEventArgs e)
        {
            setAmount("20000.00");
        }

        private void setAmount(String _amount)
        {
            AmountTextBox.Text = _amount;
        }
    }
}
