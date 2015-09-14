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

namespace LoanManagementSystem.View.WpfPage.Settings.Content
{
    /// <summary>
    /// Interaction logic for ContentPageSettings.xaml
    /// </summary>
    public partial class ContentPageSettings : Page
    {
       private static ContentPageSettings instance;
        public IList<string> ErrorList { get; set; }

        private ContentPageSettings()
        {
            InitializeComponent();

            EmailContentFrame.Content = SendAMail.Instance;
            AddAreaContentFrame.Content = AddArea.Instance;
        }

        public static ContentPageSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentPageSettings();
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
