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
using System.ComponentModel;


namespace LoanManagementSystem.View.WpfPage.Customer
{
    /// <summary>
    /// Interaction logic for Employee QuickSearchPage.xaml
    /// </summary>
    public partial class QuickSearchPage : Page, INotifyPropertyChanged
    {
        private static QuickSearchPage instance;
        //public IList<string> ErrorList { get; set; }
        private bool _isSearchedPerformed = false;
        private string _searchText = "";
        private List<customer> _customerList;

        public List<customer> CustomerList
        {
            get { return _customerList; }
            set 
            {
                _customerList = value;
                OnPropertyChanged("CustomerList");
            }
        }
        public List<PageData> PagingList { get; set; }

        private PagingCollection<customer> _PagingCollection { get; set; }

        private QuickSearchPage()
        {
            InitializeComponent();
            RefreshCustomerListByPage(1);
        }

        public static QuickSearchPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuickSearchPage();
                }
                return instance;
            }
        }


        private void QuickSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _isSearchedPerformed = true;
            _searchText = QuickSearchTextBox.Text;
            RefreshCustomerListByPage(1);
        }
        
        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            RefreshCustomerListByPage(selectedpage);
        }


        private async void CustomerDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                customer selected = CustomerList.Single(c => c.ID == lbl.Content.ToString());
                Session.SelectedCustomer = selected;
                Session.CopySelected = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_CUSTOMER, MessageDialogStyle.Affirmative);
            }
        }

        private void RefreshCustomerListByPage(int page)
        {
            if (_isSearchedPerformed)
            {
                if (_searchText != "")
                {
                    _PagingCollection = CustomerService.GetPaginatedQuickSearchedCustomerListByPage(_searchText, page);
                }
                else
                {
                    _PagingCollection = CustomerService.GetPaginatedCustomerListByPage(page);
                }
            }
            else
            {
                _PagingCollection = CustomerService.GetPaginatedCustomerListByPage(page);
            }

            CustomerList = _PagingCollection.Collection;
            PagingList = _PagingCollection.PagesList;

            CustomerListView.ItemsSource = CustomerList;
            CustomerListView.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        private async void StaffDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                customer selected = CustomerList.Single(c => c.ID == lbl.Content.ToString());
                Session.SelectedCustomer = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE, MessageDialogStyle.Affirmative);

            }
        }

        public void RefreshPage()
        {
            RefreshCustomerListByPage(1);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
