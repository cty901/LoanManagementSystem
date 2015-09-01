using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Model.SMSModel;
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
    /// Interaction logic for IssueLoan.xaml
    /// </summary>
    public partial class IssueLoan : Page
    {
        private static IssueLoan _instance;
        private employee _selectedEmployee;
        private customer _selectedCustomer;
        private loan_type _selectedLoanType;
        private Mode _viewmode;
        private loan _selectedLoan;
        private sm _sms;

        public Mode ViewMode
        {
            get { return _viewmode; }
            set
            {
                _viewmode = value;
                changeMode(_viewmode);
            }
        }

        private void changeMode(Mode _viewmode)
        {
            if (_viewmode.Equals(Mode.EDIT))
            {
                clearLoanIssuePage();
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, true, true, true, true, true);

                SelectedEmployee = Session.SelectedLoan.employee;
                SelectedCustomer = Session.SelectedLoan.customer;
                SelectedLoan = Session.SelectedLoan;
                SelectedLoan_Type = Session.SelectedLoan.loan_type;
            }
            if (_viewmode.Equals(Mode.VIEW))
            {
                clearLoanIssuePage();
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, false, false, false, false, false);

                SelectedEmployee = Session.SelectedLoan.employee;
                SelectedCustomer = Session.SelectedLoan.customer;
                SelectedLoan = Session.SelectedLoan;
                SelectedLoan_Type = Session.SelectedLoan.loan_type;
            }
            if (_viewmode.Equals(Mode.NEW))
            {
                clearLoanIssuePage();
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, true, true, true, true, true);
            }
        }
        
        public employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                if (_selectedEmployee != null)
                {
                    EmployeeTextBox.Text = value.FULLNAME;
                }
            }
        }
        public customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                if (_selectedCustomer != null)
                {
                    CustomerTextBox.Text = value.FULLNAME;
                }
            }
        }
        public loan_type SelectedLoan_Type
        {
            get
            {
                return _selectedLoanType;
            }
            set
            {
                _selectedLoanType = value;
                if (_selectedLoanType != null)
                {
                    if (_viewmode.Equals(Mode.NEW))
                    {
                        LoanTypeTextBox.Text = value.LOAN_TYPE_ID;
                        InstalmentTextBox.Text = value.INSTALLMENT.ToString();
                        AmountTextBox.Text = value.AMOUNT.ToString();
                        EndDateDatePicker.SelectedDate = System.DateTime.Now.Date.AddDays(Convert.ToInt32(value.DAYS));
                    }
                }
            }
        }

        private IssueLoan()
        {
            InitializeComponent();
        }

        public static IssueLoan Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IssueLoan();
                }
                return _instance;
            }
        }
        public sm SMS
        {
            get
            {
                return _sms;
            }
            set
            {
                _sms = value;
            }
        }

        private void clearData()
        {
            _selectedCustomer = null;
            _selectedEmployee = null;
            _selectedLoanType = null;
            _selectedLoan = null;
        }

        private loan getLoanData()
        {
            try
            {
                loan _loan = new loan();
                if(_viewmode.Equals(Mode.NEW))
                {
                    _loan.ID = IDHandller.generateID("loan");
                    _loan.STATUS = true;
                    _loan.INSERT_USER_ID = Session.LoggedEmployee.ID;
                    _loan.INSERT_DATETIME = System.DateTime.Now;
                }
                else if(_viewmode.Equals(Mode.EDIT))
                {
                    _loan.ID = IDHandller.generateID("loan");
                    _loan.STATUS = true;
                    _loan.UPDATE_USER_ID = Session.LoggedEmployee.ID;
                    _loan.UPDATE_DATETIME = System.DateTime.Now;
                }
                _loan.FK_EMPLOYEE_ID = SelectedEmployee.ID;
                _loan.FK_CUSTOMER_ID = SelectedCustomer.ID;
                _loan.FK_LOAN_TYPE_ID = SelectedLoan_Type.ID;
                _loan.FK_BRANCH_ID = "1";

                _loan.LOAN_ID = LoanCodeTextBox.Text;
                _loan.AMOUNT = Convert.ToDecimal(AmountTextBox.Text);
                _loan.INSTALLMENT = Convert.ToDecimal(InstalmentTextBox.Text);
                _loan.START_DATE = Convert.ToDateTime(StartDateDatePicker.SelectedDate);
                _loan.END_DATE = Convert.ToDateTime(EndDateDatePicker.SelectedDate);

                _loan.REMARK = RemarkTextBox.Text;
                _loan.LOAN_STATUS = true;

                

                return _loan;
            }
            catch
            {
                return null;
            }
        }

        private async void LoanSaveButton_Click(object sender, RoutedEventArgs e)
        {
            loan _loan = getLoanData();
            if(_viewmode.Equals(Mode.NEW))
            {
                if (LoanService.InsertLoan(_loan) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Added Success!", MessageDialogStyle.Affirmative);
                    SendConfirmationSMS();
                    clearLoanIssuePage();
                    MultiSearch.Instance.ClearSearchResult();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
            if(_viewmode.Equals(Mode.EDIT))
            {

                _loan.ID = Session.SelectedLoan.ID;

                if (LoanService.UpdateLoan(_loan) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Edit Success!", MessageDialogStyle.Affirmative);
                    SendConfirmationSMS();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
        }

        private void clearLoanIssuePage()
        {
            clearData();
            CustomerTextBox.Clear();
            EmployeeTextBox.Clear();
            LoanTypeTextBox.Clear();
            AmountTextBox.Clear();
            InstalmentTextBox.Clear();
            LoanCodeTextBox.Clear();
            StartDateDatePicker.SelectedDate = System.DateTime.Now;
            EndDateDatePicker.SelectedDate = System.DateTime.Now;
            RemarkTextBox.Clear();
        }

        private void LoanCodeGenButton_Click(object sender, RoutedEventArgs e)
        {
            LoanCodeTextBox.Text = IDHandller.generateCode("loan");
        }

        private void LoanCancelButton_Click(object sender, RoutedEventArgs e)
        {
            clearLoanIssuePage();
        }

        public loan SelectedLoan {
            get
            {
                return _selectedLoan;
            }
            set
            {
                _selectedLoan = value;
                if (_selectedLoan != null)
                {
                    LoanTypeTextBox.Text=LoanTypeService.getLoanTypeByID(_selectedLoan.FK_LOAN_TYPE_ID).LOAN_TYPE_ID;
                    InstalmentTextBox.Text = _selectedLoan.INSTALLMENT.ToString();
                    AmountTextBox.Text = _selectedLoan.AMOUNT.ToString();
                    EndDateDatePicker.SelectedDate = _selectedLoan.END_DATE;
                    LoanCodeTextBox.Text = _selectedLoan.LOAN_ID;
                }
            }        
        }
        private void SendConfirmationSMS()
        {
            createSMS();
            if (SMS != null)
            {
                int result = SMSManager.Instance.SendASMS(SMS.PHONE_NUMBER, SMS.CONTENT);
                if (result == 1)
                {
                    SMS.SENDING_STATUS = true;
                    SMS.SEND_DATE_TIME = System.DateTime.Now;
                    SMSService.InsertSMS(SMS);
                    SMS = null;
                }
                else
                {
                    SMS.SENDING_STATUS = false;
                    SMS.SEND_DATE_TIME = System.DateTime.Now;
                    SMSService.InsertSMS(SMS);
                }
            }
        }

        private void createSMS()
        {
            sm _nsms = new sm();

            try
            {
                _nsms.ID = IDHandller.generateID("sms");

                _nsms.PHONE_NUMBER = SelectedCustomer.PHONE_HP1;
                _nsms.CONTENT = "Dear "+LetterHandller.Uppercase(SelectedCustomer.FULLNAME)+", We confirm loan of Rs."+AmountTextBox.Text+" has been granted."+Environment.NewLine+Messages.TTL_MSG;
                _nsms.TYPE = "send";
                _nsms.SENDING_STATUS = true;

                _nsms.STATUS = true;
                _nsms.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _nsms.INSERT_DATETIME = System.DateTime.Now;

                _nsms.FK_EMPLOYEE_ID = Session.LoggedEmployee.ID;
                _nsms.FK_CUSTOMER_ID = SelectedCustomer.ID;
                SMS = _nsms;
            }
            catch
            {
                SMS = null;
            }
        }
    }
}
