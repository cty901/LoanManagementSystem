using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfPage.Settings.Content;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for CustomerSearch.xaml
    /// </summary>
    public partial class CustomerSearch : Page
    {
       public List<PageData> PagingList { get; set; }

        private static CustomerSearch _instance;
        private string _searchText;
        List<customer> SearchedListCustomer;

        private CustomerSearch()
        {
            InitializeComponent();
        }

        public static CustomerSearch Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerSearch();
                }
                return _instance;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _searchText = SearchTextBox.Text;
            RefreshSearchedListByPage(1); 
        }

        private void RefreshSearchedListByPage(int page)
        {
            PagingCollection<customer> _PagingCollection = CustomerService.GetPaginatedQuickSearchedCustomerListByPage(_searchText, page);
            SearchedListCustomer = _PagingCollection.Collection;
            PagingList = _PagingCollection.PagesList;

            SearchList.ItemsSource = SearchedListCustomer;
            SearchList.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            RefreshSearchedListByPage(selectedpage);
        }

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                customer selected = SearchedListCustomer.Single(emp => emp.ID == lbl.Content.ToString());
                CreateASMS.Instance.SelectedCustomer = selected;
                SendAMail.Instance.SelectedCustomer = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG,"Error", MessageDialogStyle.Affirmative);
            }
        }
        public void ClearSearchResult()
        {
            if (_instance != null)
            {
                SearchList.ItemsSource=null;
                SearchTextBox.Clear();
            }
        }

        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearSearchResult();
            if (_instance != null)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButton_Click(sender, e);
        }

        private void Page_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            SearchButton_Click(sender, e);
        }
    }
}
