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
    /// Interaction logic for CashReturn.xaml
    /// </summary>
    public partial class CashReturn : Page
    {
        private static CashReturn _instance;

        private CashReturn()
        {
            InitializeComponent();
        }

        public static CashReturn Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CashReturn();
                }
                return _instance;
            }

        }
    }
}
