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
    /// Interaction logic for PayLoan.xaml
    /// </summary>
    public partial class PayLoan : Page
    {
        private static PayLoan _instance;
        private static Util.Mode _mode;
        payment _newPayment;

        private PayLoan()
        {
            InitializeComponent();
            _newPayment = new payment();
            PaymentReset(_newPayment);
            GridPayment.DataContext = _newPayment;
        }

        public void PaymentReset(payment _p)
        {
            _p.DATE_TIME = System.DateTime.Now;
            _p.AMOUNT = (decimal)0.00;
        }

        public static PayLoan Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PayLoan();
                }
                _instance.setLoan();
                Mode = Mode.NEW;
                return _instance;
            }

        }

        public List<payment> PaymentL { get; set; }
        public payment Selected { get; set; }

        public static Mode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                _instance.updateControlButtonPanelView();
            }
        }

        private void collapseAllControlButtonPanelItems()
        {
            foreach (Button child in ControlButtonPanel.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void updateControlButtonPanelView()
        {
            collapseAllControlButtonPanelItems();
            if (Mode == Mode.NEW)
            {
                LoanSaveButton.Visibility = System.Windows.Visibility.Visible;
                SaveButtonContentLable.Content = "Save";
                LoanCancelButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Mode == Mode.EDIT)
            {
                LoanDeleteButton.Visibility = System.Windows.Visibility.Visible;
                LoanSaveButton.Visibility = System.Windows.Visibility.Visible;
                SaveButtonContentLable.Content = "Update";
                LoanCancelButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void setLoan()
        {
            if (Session.SelectedLoan != null)
            {
                setLoanDetails(Session.SelectedLoan);
                refreshLoanPaymentList(1);
            }
        }

        private void setLoanDetails(loan _loan)
        {
            CustomerLabel.Content = LetterHandller.UppercaseFirst(_loan.customer.FIRST_NAME) + " " + LetterHandller.UppercaseFirst(_loan.customer.LAST_NAME);
            CustomerCodeLabel.Content = _loan.customer.FullCustomerCode;
            LoanIDTextBox.Text = _loan.LOAN_ID;
            EmployeeLabel.Content = LetterHandller.UppercaseFirst(_loan.employee.FULLNAME);
        }

        private payment setLoanPaymentDetails()
        {
            try
            {
                payment _payment = new payment();

                _payment.ID = IDHandller.generateID("payment");
                _payment.PAYMENT_ID = IDHandller.generateCode("payment");
                _payment.AMOUNT = Convert.ToDecimal(AmountTextBox.Text);
                _payment.DATE_TIME = Convert.ToDateTime(PayDatePicker.Text);
                _payment.PAIDBY = PayedByTextBox.Text;
                _payment.REMARK = RemarkTextBox.Text;

                _payment.FK_LOAN_ID = Session.SelectedLoan.ID;

                _payment.STATUS = true;
                _payment.INSERT_DATETIME = System.DateTime.Now;
                _payment.INSERT_USER_ID = Session.LoggedEmployee.ID;

                return _payment;
            }
            catch
            {
                return null;
            }
        }

        private async void LoanSaveButton_Click(object sender, RoutedEventArgs e)
        {
            payment _payment = setLoanPaymentDetails();
            if (Mode == Mode.NEW)
            {
                if (PaymentService.InsertPayment(_payment) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Payment Added Success!", MessageDialogStyle.Affirmative);
                    clearLoanIssuePage();
                    refreshLoanPaymentList(1);
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
            else if (Mode == Mode.EDIT)
            {
                _payment.ID = Selected.ID;
                int _result=PaymentService.UpdatePayment(_payment);

                if (_result == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Payment Update Success!", MessageDialogStyle.Affirmative);
                    clearLoanIssuePage();
                    refreshLoanPaymentList(1);
                }
                else if (_result == 0)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Payment Update Success!", MessageDialogStyle.Affirmative);
                    clearLoanIssuePage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
        }

        private void refreshLoanPaymentList(int page)
        {
            PagingCollection<payment> _PagingCollection = Session.SelectedLoan.PAYMENT_LIST(page);

            PaymentL = _PagingCollection.Collection;
            List<PageData> PagingList = _PagingCollection.PagesList;

            PaymentList.ItemsSource = PaymentL;
            decimal sumPaid = Session.SelectedLoan.sumPaidByLoanID();
            sumPaidLable.Content = sumPaid.ToString();

            decimal totalPaid = Session.SelectedLoan.totalToPayByLoanID();
            totalToPayLable.Content = totalPaid.ToString();

            PaymentList.Items.Refresh();

            PagingListView.ItemsSource = PagingList;
            PagingListView.Items.Refresh();
        }

        private void PaginationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int selectedpage = int.Parse(button.Content.ToString());

            refreshLoanPaymentList(selectedpage);
        }

        private void clearLoanIssuePage()
        {
            AmountTextBox.Clear();
            PayDatePicker.SelectedDate=System.DateTime.Now;
            PayedByTextBox.Clear();
            RemarkTextBox.Clear();

            PaymentReset(_newPayment);
            Selected = _newPayment;
            refreshLoanPaymentList(1);
            Mode = Mode.NEW;
        }

        ListViewItem _previousRow;

        private async void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_previousRow != null)
            {
                _previousRow.Background = Brushes.Transparent;
            }

            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Content;
            Label lbl = sp.Children.OfType<Label>().FirstOrDefault();

            if (lbl.Content.ToString() != "")
            {
                Selected = PaymentL.Single(c => c.ID == lbl.Content.ToString());
               
                int index=PaymentList.Items.IndexOf(Selected);
                //PaymentList.SelectedIndex = index;
                ListViewItem row = PaymentList.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;
                row.Background = Brushes.Red;
                _previousRow = row;
                GridPayment.DataContext = new payment();
                GridPayment.DataContext = Selected;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Select a Payment", MessageDialogStyle.Affirmative);
            }
            Mode=Mode.EDIT;
        }

        private async void LoanDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult result = await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Do you want to Delete Payment?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (PaymentService.DeletePayment(Selected) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Payment Deleted Successfully..", MessageDialogStyle.Affirmative);
                    clearLoanIssuePage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Payment Delete fail..", MessageDialogStyle.Affirmative);
                }
            }
        }

        private void LoanCancelButton_Click(object sender, RoutedEventArgs e)
        {
            clearLoanIssuePage();
        }

        }
    }

