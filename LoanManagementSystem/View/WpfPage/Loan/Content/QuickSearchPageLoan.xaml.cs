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


namespace LoanManagementSystem.View.WpfPage.Loan.Content
{
    /// <summary>
    /// Interaction logic for Employee QuickSearchPageLoan.xaml
    /// </summary>
    public partial class QuickSearchPageLoan : Page, INotifyPropertyChanged
    {
        private static QuickSearchPageLoan instance;
        public List<PageData> PagingList { get; set; }

        private PagingCollection<loan> _PagingCollection { get; set; }

        private bool _isSearchedPerformed = false;
        private string _searchText = "";
        bool _loanStatusActive = true;

        private List<loan> _loanList;
        private List<area> _areaList;
        private area _selectedArea;

        private QuickSearchPageLoan()
        {
            InitializeComponent();
            this.DataContext = this;
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

        public List<area> AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged("AreaList");
            }
        }

        public area SelectedArea
        {
            get
            {
                if (_selectedArea == null)
                {
                    _selectedArea = new area();
                    _selectedArea.AREA_NAME = "ALL";
                }
                return _selectedArea;
            }
            set
            {
                _selectedArea = value;
                OnPropertyChanged("SelectedArea");
            }
        }

        public List<loan> LoanList
        {
            get { return _loanList; }
            set
            {
                _loanList = value;
                OnPropertyChanged("LoanList");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AreaList = (List<area>)AreaService.getAreaCodes();
            SelectedArea = this.SelectedArea;

            if (AreaComboBox.SelectedIndex == -1)
            {
                AreaComboBox.SelectedIndex = 0;
            }
        }

        private void QuickSearchButton_Click(object sender, RoutedEventArgs e)
        {
            _isSearchedPerformed = true;
            _searchText = QuickSearchTextBox.Text;
            RefreshLoanListByPage(1);
        }

        private void QuickSearchButton_Click(object sender, TextChangedEventArgs e)
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

        private async void LoginByLoanID(loan _loan)
        {
            if (_loan!=null)
            {
                loan selected = LoanList.Single(ln => ln.ID == _loan.ID );
                Session.SelectedLoan = selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_LOAN, MessageDialogStyle.Affirmative);
            }
        }


        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuickSearchTextBox != null)
            {
                QuickSearchTextBox.Text = "";
                RefreshLoanListByPage(1);
            }
        }

        private void RefreshLoanListByPage(int page)
        {
            area _area = SelectedArea;

            if (_isSearchedPerformed)
            {
                _PagingCollection = LoanService.GetPaginatedQuickSearchedLoanListByPage(_searchText, page, _loanStatusActive, _area.AREA_NAME);
            }
            else
            {
                _PagingCollection = LoanService.GetPaginatedLoanListByPage(page, _area.AREA_NAME);
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

        private void ActiveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _loanStatusActive = true;
        }

        private void ActiveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _loanStatusActive = false;
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
