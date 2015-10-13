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
using System;
using System.Data;


namespace LoanManagementSystem.View.WpfPage.Reports
{
    /// <summary>
    /// Interaction logic for Employee QuickSearchPage.xaml
    /// </summary>
    public partial class EmployeeReportPage : Page, INotifyPropertyChanged
    {
        private static EmployeeReportPage instance;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isSearchedPerformed = false;
        private string _searchText = "";
        private List<employee_cash> _loanList;
        //private List<area> _areaList;
        private area _selectedArea;
        private DateTime _dateFrom=System.DateTime.Now;
        private DateTime _dateTo=System.DateTime.Now;

        public List<PageData> PagingList { get; set; }
        private PagingCollection<employee_cash> _PagingCollection { get; set; }

        private EmployeeReportPage()
        {
            InitializeComponent();
            this.DataContext = this;
            RefreshEmployeePaymentListByPage(1);
        }

        public static EmployeeReportPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmployeeReportPage();
                }
                return instance;
            }
        }

        //public List<area> AreaList
        //{
        //    get { return _areaList; }
        //    set
        //    {
        //        _areaList = value;
        //        OnPropertyChanged("AreaList");
        //    }
        //}

        //public area SelectedArea
        //{
        //    get 
        //    {
        //        if (_selectedArea == null)
        //        {
        //           _selectedArea = new area();
        //           _selectedArea.AREA_NAME = "ALL";
        //        }
        //        return _selectedArea; 
        //    }
        //    set
        //    {
        //        _selectedArea = value;
        //        OnPropertyChanged("SelectedArea");
        //    }
        //}

        public DateTime DateFrom
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }

        public DateTime DateTo
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        public List<employee_cash> EmployeePaymentList
        {
            get { return _loanList; }
            set 
            {
                _loanList = value;
                OnPropertyChanged("EmployeePaymentList");
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //AreaList = (List<area>)AreaService.getAreaCodes();
            //SelectedArea = this.SelectedArea;

            //if (AreaComboBox.SelectedIndex == -1)
            //{
            //    AreaComboBox.SelectedIndex = 0;
            //}
        }  

        //private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (EmployeePaymentListView != null)
        //    {
        //        RefreshEmployeePaymentListByPage(1);
        //    }
        //}

        private void QuickSearchButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployeePaymentListByPage(1);
        }

        private void FromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEmployeePaymentListByPage(1);
        }

        private void ToDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEmployeePaymentListByPage(1);
        }
        
        private void RefreshEmployeePaymentListByPage(int page)
        {
            //area _area = SelectedArea;
            DateTime _dateFrom = DateFrom;
            DateTime _dateTo = DateTo;

            
            _PagingCollection = Employee_CashService.GetPaginatedQuickSearchedEmployeePaymentListByPage(page,_dateFrom,_dateTo);
                

            EmployeePaymentList = _PagingCollection.Collection;
            PagingList = _PagingCollection.PagesList;

            EmployeePaymentListView.ItemsSource = EmployeePaymentList;
            EmployeePaymentListView.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            RefreshEmployeePaymentListByPage(selectedpage);
        }

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
