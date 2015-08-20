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

        private PayLoan()
        {
            InitializeComponent();
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
                return _instance;
            }

        }

        private void setLoan()
        {
            if (Session.SelectedLoan != null)
            {
                setLoanDetails(Session.SelectedLoan);
                refreshLoanPaymentList();
            }
        }

        private void setLoanDetails(loan _loan)
        {
            CustomerLabel.Content = LetterHandller.UppercaseFirst(_loan.customer.FULLNAME);
            CustomerCodeLabel.Content = _loan.customer.CUSTOMER_ID;
            LoanIDTextBox.Text = _loan.LOAN_ID;
            EmployeeLabel.Content = "Loan is Made by " + LetterHandller.UppercaseFirst(_loan.employee.FULLNAME);
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
            if (PaymentService.InsertPayment(_payment) == 1)
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Payment Added Success!", MessageDialogStyle.Affirmative);
                clearLoanIssuePage();
                refreshLoanPaymentList();
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
            }
        }

        private void refreshLoanPaymentList()
        {
            PaymentList.ItemsSource = Session.SelectedLoan.PAYMENT_LIST;
        }

        private void clearLoanIssuePage()
        {
            AmountTextBox.Clear();
            PayDatePicker.SelectedDate=System.DateTime.Now;
            PayedByTextBox.Clear();
            RemarkTextBox.Clear();
        }























        }
    }

