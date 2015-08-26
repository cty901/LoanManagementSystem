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
    /// Interaction logic for ContentPageSMS.xaml
    /// </summary>
    public partial class ContentPageSMS : Page
    {
       private static ContentPageSMS instance;
        public IList<string> ErrorList { get; set; }

        private ContentPageSMS()
        {
            InitializeComponent();
            SearchContentFrame.Content = CustomerSearch.Instance;
            SMSContentFrame.Content = CreateASMS.Instance;
        }

        public static ContentPageSMS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentPageSMS();
                }
               return instance;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var h = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight;
            ScrollBar.Height = h - 150;
        }
    }
}
