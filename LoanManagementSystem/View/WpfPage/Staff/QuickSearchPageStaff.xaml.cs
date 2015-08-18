using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfPage.Staff.Content;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
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

namespace LoanManagementSystem.View.WpfPage.Staff
{
    /// <summary>
    /// Interaction logic for QuickSearchPageStaff.xaml
    /// </summary>
    public partial class QuickSearchPageStaff: Page
    {
        private static QuickSearchPageStaff instance;
        //public IList<string> ErrorList { get; set; }
        public List<employee> EmployeeList { get; set; }
        public List<PageData> PagingList { get; set; }

        private PagingCollection<employee> _PagingCollection { get; set; }

        private bool _isSearchedPerformed = false;
        private string _searchText = "";

        private QuickSearchPageStaff()
        {
            InitializeComponent();
            RefreshEmployeeListByPage(1);
        }

        public static QuickSearchPageStaff Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuickSearchPageStaff();
                }

                return instance;
            }
        }

        private void QuickSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _isSearchedPerformed = true;
            _searchText = QuickSearchTextBox.Text;
            RefreshEmployeeListByPage(1);
        }

        private void RefreshEmployeeListByPage(int page)
        {
            if (_isSearchedPerformed)
            {
                if (_searchText != "")
                {
                    _PagingCollection = EmployeeService.GetPaginatedQuickSearchedEmployeeListByPage(_searchText, page);
                }
                else 
                {
                    _PagingCollection = EmployeeService.GetPaginatedEmployeeListByPage(page);
                }
            }
            else
            {
                _PagingCollection = EmployeeService.GetPaginatedEmployeeListByPage(page);
            }

            EmployeeList = _PagingCollection.Collection;
            PagingList = _PagingCollection.PagesList;

            StaffListView.ItemsSource = EmployeeList;
            StaffListView.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            RefreshEmployeeListByPage(selectedpage);
        }

        private async void StaffDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                employee selected = EmployeeList.Single(c => c.ID == lbl.Content.ToString());
                Session.SelectedEmployee = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE,MessageDialogStyle.Affirmative);
                
            }
        }

        public void RefreshPage(){
            RefreshEmployeeListByPage(1);
        }

    }
}
