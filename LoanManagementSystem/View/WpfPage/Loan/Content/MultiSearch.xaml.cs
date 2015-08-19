using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
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

namespace LoanManagementSystem.View.WpfPage.Loan.Content
{
    /// <summary>
    /// Interaction logic for MultiSearch.xaml
    /// </summary>
    public partial class MultiSearch : Page
    {
        public List<PageData> PagingList { get; set; }

        private static MultiSearch _instance;
        private string _searchText;

        List<employee> SearchedListEmployee;
        List<customer> SearchedListCustomer;
        List<loan_type> SearchedListLoanType;

        private MultiSearch()
        {
            InitializeComponent();
        }

        public static MultiSearch Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MultiSearch();
                }
                return _instance;
            }
        }

        private int getSearchType()
        {
            return SearchTypeComboBox.SelectedIndex;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _searchText = SearchTextBox.Text;
            RefreshSearchedListByPage(1); 
        }

        private void RefreshSearchedListByPage(int page)
        {
            if (getSearchType() == 1)
            {
                PagingCollection<employee> _PagingCollection = EmployeeService.GetPaginatedQuickSearchedEmployeeListByPage(_searchText, page);
                SearchedListEmployee = _PagingCollection.Collection;
                PagingList = _PagingCollection.PagesList;

                SearchList.ItemsSource = SearchedListEmployee;
                SearchList.Items.Refresh();
            }
            else if (getSearchType() == 0)
            {
                PagingCollection<customer> _PagingCollection = CustomerService.GetPaginatedQuickSearchedCustomerListByPage(_searchText, page);
                SearchedListCustomer = _PagingCollection.Collection;
                PagingList = _PagingCollection.PagesList;

                SearchList.ItemsSource = SearchedListCustomer;
                SearchList.Items.Refresh();
            }
            else if (getSearchType() == 2)
            {
                PagingCollection<loan_type> _PagingCollection = LoanTypeService.GetPaginatedQuickSearchedLoanTypeListByPage(_searchText, page);
                SearchedListLoanType = _PagingCollection.Collection;
                PagingList = _PagingCollection.PagesList;

                SearchList.ItemsSource = SearchedListLoanType;
                SearchList.Items.Refresh();
            }
            else
            {
                PagingCollection<employee> _PagingCollection = EmployeeService.GetPaginatedQuickSearchedEmployeeListByPage(_searchText, page);
                SearchedListEmployee = _PagingCollection.Collection;
                PagingList = _PagingCollection.PagesList;

                SearchList.ItemsSource = SearchedListEmployee;
                SearchList.Items.Refresh();
            }

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
                if (getSearchType() == 1)
                {
                    employee selected = SearchedListEmployee.Single(emp => emp.ID == lbl.Content.ToString());
                    IssueLoan.Instance.SelectedEmployee = selected;
                }
                else if (getSearchType() == 0)
                {
                    customer selected = SearchedListCustomer.Single(emp => emp.ID == lbl.Content.ToString());
                    IssueLoan.Instance.SelectedCustomer = selected;
                }
                else if (getSearchType() == 2)
                {
                    loan_type selected = SearchedListLoanType.Single(emp => emp.ID == lbl.Content.ToString());
                    IssueLoan.Instance.SelectedLoan_Type = selected;
                }
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
        }
    }
}
