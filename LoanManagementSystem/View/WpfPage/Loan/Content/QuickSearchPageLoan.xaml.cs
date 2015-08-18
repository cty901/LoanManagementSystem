using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using LoanManagementSystem.Util;
using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls.Dialogs;


namespace LoanManagementSystem.View.WpfPage.Loan.Content
{
    /// <summary>
    /// Interaction logic for Employee QuickSearchPageLoan.xaml
    /// </summary>
    public partial class QuickSearchPageLoan : Page
    {
        private static QuickSearchPageLoan instance;
        //public IList<string> ErrorList { get; set; }
        public List<loan> LoanList { get; set; }
        public List<PageData> PagingList { get; set; }

        private PagingCollection<loan> _PagingCollection { get; set; }

        private bool _isSearchedPerformed = false;
        private string _searchText = "";

        private QuickSearchPageLoan()
        {
            InitializeComponent();
            RefreshLoanListByPage(1);
        }

        public static QuickSearchPageLoan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuickSearchPageLoan();
                }
                return instance;
            }
        }


        private void QuickSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _isSearchedPerformed = true;
            _searchText = QuickSearchTextBox.Text;
            RefreshLoanListByPage(1);
        }
        
        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            RefreshLoanListByPage(selectedpage);
        }


        private async void LoanDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                loan selected = LoanList.Single(c => c.ID == lbl.Content.ToString());
                Session.SelectedLoan = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_LOAN, MessageDialogStyle.Affirmative);
            }
        }

        private void RefreshLoanListByPage(int page)
        {
            if (_isSearchedPerformed)
            {
                if (_searchText != "")
                {
                    _PagingCollection = LoanService.GetPaginatedQuickSearchedLoanListByPage(_searchText, page);
                }
                else
                {
                    _PagingCollection = LoanService.GetPaginatedLoanListByPage(page);
                }
            }
            else
            {
                _PagingCollection = LoanService.GetPaginatedLoanListByPage(page);
            }

            LoanList = _PagingCollection.Collection;
            PagingList = _PagingCollection.PagesList;

            LoanListView.ItemsSource = LoanList;
            LoanListView.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        public void RefreshPage()
        {
            RefreshLoanListByPage(1);
        }

    }
}
