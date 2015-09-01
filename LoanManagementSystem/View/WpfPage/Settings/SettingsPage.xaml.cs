using LoanManagementSystem.View.WpfPage.Settings.Content;
using System.Windows.Controls;

namespace LoanManagementSystem.View.WpfPage.Settings
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private static SettingsPage instance;

         public SettingsPage()
        {
            InitializeComponent();
            hideMenu();
            ContentFrame.Content = ContentPageSettings.Instance;
        }

        public static SettingsPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsPage();
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
