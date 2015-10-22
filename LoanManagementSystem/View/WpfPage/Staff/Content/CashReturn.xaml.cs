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

using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LoanManagementSystem.View.WpfPage.Staff.Content
{
    /// <summary>
    /// Interaction logic for CashReturn.xaml
    /// </summary>
    public partial class CashReturn : Page
    {
        private static CashReturn _instance;
        private static string type = "return";

        private CashReturn()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, AmountReturnTextBox);
        }

        public static CashReturn Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CashReturn();
                }
                return _instance;
            }

        }

        public decimal Amount
        {
            get
            {
                return Convert.ToDecimal(AmountReturnTextBox.Text);
            }
            set
            {
                if (value == 0)
                {
                    AmountReturnTextBox.Text = "";
                }
                else
                    AmountReturnTextBox.Text = Convert.ToString(value);
                FocusManager.SetFocusedElement(this, AmountReturnTextBox);
            }
        }

        public DateTime BorrowDateTime
        {
            get
            {
                return Convert.ToDateTime(CashReturnDayPicker.Text + " " + System.DateTime.Now.TimeOfDay);
            }
            set
            {
                CashReturnDayPicker.Text = Convert.ToString(value);
            }
        }

        public string Remark
        {
            get
            {
                return Convert.ToString(CommentCashReturnTextBox.Text);
            }
            set
            {
                CommentCashReturnTextBox.Text = Convert.ToString(value);
            }
        }

        private void clearReturnForm()
        {
            Amount = Convert.ToDecimal("0");
            Remark = "";
        }

        private employee_cash getEmployee_CashReturn()
        {
            employee_cash emp_cash = new employee_cash();

            emp_cash.ID = IDHandller.generateID("employee_cash");
            emp_cash.TYPE = type;
            emp_cash.AMOUNT = Amount;
            emp_cash.TRANSACTION_DATE_TIME = BorrowDateTime;
            emp_cash.REMARK = Remark;

            emp_cash.STATUS = true;
            emp_cash.INSERT_DATETIME = DateTime.Now;
            emp_cash.INSERT_USER_ID = Session.LoggedEmployee.ID;
            emp_cash.UPDATE_DATETIME = DateTime.Now;
            emp_cash.UPDATE_USER_ID = Session.LoggedEmployee.ID;

            emp_cash.FK_EMPLOYEE_ID = Session.SelectedEmployee.ID;

            return emp_cash;
        }

        private async void CashReturnSaveButton_Click(object sender, RoutedEventArgs e)
        {
            employee_cash emp_cash = getEmployee_CashReturn();
            if (Employee_CashService.InsertEmployee_cash(emp_cash) == 1)
            {
                await MainWindow.Instance.ShowMessageAsync("Employe_cash Return Success", "Transaction Added Success!");
                clearReturnForm();
                CashBorrow.Instance.setTodayTransactionList();
                //await controller.CloseAsync();
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Employe_cash Insert Error", "Please check Deatails", MessageDialogStyle.Affirmative);
            }
        }
    }
}
