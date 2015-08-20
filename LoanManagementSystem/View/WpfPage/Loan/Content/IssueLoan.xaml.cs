﻿using LoanManagementSystem.DBModel;
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
    /// Interaction logic for IssueLoan.xaml
    /// </summary>
    public partial class IssueLoan : Page
    {
        private static IssueLoan _instance;
        private employee _selectedEmployee;
        private customer _selectedCustomer;
        private loan_type _selectedLoanType;

        public employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                EmployeeTextBox.Text = value.FULLNAME;
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
                CustomerTextBox.Text = value.FULLNAME;
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
                LoanTypeTextBox.Text = value.LOAN_TYPE_ID;
                InstalmentTextBox.Text = value.INSTALLMENT.ToString();
                AmountTextBox.Text = value.AMOUNT.ToString();
                EndDateDatePicker.SelectedDate = System.DateTime.Now.Date.AddDays(Convert.ToInt32(value.DAYS));
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

        private void clearData()
        {
            _selectedCustomer = null;
            _selectedEmployee = null;
            _selectedLoanType = null;
        }

        private loan getLoanData()
        {
            try
            {
                loan _loan = new loan();

                _loan.ID = IDHandller.generateID("loan");

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

                _loan.STATUS = true;
                _loan.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _loan.INSERT_DATETIME = System.DateTime.Now;

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
            if (LoanService.InsertLoan(_loan) == 1)
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Added Success!", MessageDialogStyle.Affirmative);
                clearLoanIssuePage();
                MultiSearch.Instance.ClearSearchResult();
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
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
                
    }
}