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

namespace LoanManagementSystem.View.WpfPage.LoanTypes.Content
{
    /// <summary>
    /// Interaction logic for LoanTypeDetails.xaml
    /// </summary>
    public partial class LoanTypeDetails : Page
    {
        private static LoanTypeDetails _instance;

        private LoanTypeDetails()
        {
            InitializeComponent();
        }

        public static LoanTypeDetails Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoanTypeDetails();
                }
                return _instance;
            }
        }

        private async void LoanSaveButton_Click(object sender, RoutedEventArgs e)
        {
            loan_type _loanType = getLoanTypeData();
                if (LoanTypeService.InsertLoanType(_loanType) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Type Added Success!", MessageDialogStyle.Affirmative);
                    clearLoanTypePage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                }
        }

        private void clearLoanTypePage()
        {
            HandleControllers.clearContent(HandleControllers.GetLogicalChildCollection<Control>(this));
        }

        private loan_type getLoanTypeData()
        {
            
            try
            {
                loan_type _loanType = new loan_type();
                _loanType.ID = IDHandller.generateID("loan_type");
                _loanType.LOAN_TYPE_ID=LoanTypeCodeTextBox.Text;

                _loanType.STATUS = true;
                _loanType.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _loanType.INSERT_DATETIME = System.DateTime.Now;

                _loanType.AMOUNT=Convert.ToDecimal(LoanAmountTextBox.Text);
                _loanType.INSTALLMENT=Convert.ToDecimal(InstallmentTextBox.Text);
                _loanType.DAYS=Convert.ToInt32(DaysTextBox.Text);

                _loanType.FULLNAME=ShortNameTextBox.Text;
                _loanType.REMARK=RemarkTextBox.Text;
                

                return _loanType;
            }
            catch
            {
                return null;
            }
        }

        private void LoanTypeCodeGenButton_Click(object sender, RoutedEventArgs e)
        {
            LoanTypeCodeTextBox.Text = IDHandller.generateCode("loan_type");
        }
    }
}

