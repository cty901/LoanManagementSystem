using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.View.WpfPage;
using LoanManagementSystem.View.WpfPage.Customer;
using LoanManagementSystem.View.WpfPage.Customer.Content;
using LoanManagementSystem.View.WpfPage.Loan;
using LoanManagementSystem.View.WpfPage.Loan.Content;
using LoanManagementSystem.View.WpfPage.Staff;
using LoanManagementSystem.View.WpfPage.Staff.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoanManagementSystem.Util
{
    public class Session
    {
        //("admin")("staff")("other")
        public static string Account_Type = "other";
        public static string EmployeeID = "ID_NOT_ASSIGN";       

        public static employee LoggedEmployee = null;
        private static employee _selectedEmployee = null;
        private static customer _selectedCustomer = null;
        private static loan _selectedLoan = null;
        public static Object Navigation = null;
        public static Object CopySelected { get; set; }

        public static customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set{
                if (value != null)
                {
                    _selectedCustomer = value;
                    CustomerPage.Instance.SelectedCustomerName.Content = value.FULLNAME;
                    CustomerPage.Instance.SelectedCusLogOutButton.Visibility = Visibility.Visible;

                    if (Navigation != null)
                    {
                        CustomerPage.Instance.ContentFrame.Content = Navigation;
                    }
                    else
                    {
                        CustomerPage.Instance.ContentFrame.Content = ContentPage.Instance;
                        ContentPage.Instance.ContentFrame.Content = new DetailsPage(Mode.VIEW);
                    }
                    
                }
                _selectedCustomer = value;

            }
        }

        public static employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                if (value != null)
                {
                    _selectedEmployee = value;
                    StaffPage.Instance.SelectedEmployeeName.Content = value.FULLNAME;
                    StaffPage.Instance.SelectedEmpLogOutButton.Visibility = Visibility.Visible;

                    if (Navigation != null)
                    {
                        StaffPage.Instance.ContentFrame.Content = Navigation;
                    }
                    else
                    {
                        StaffPage.Instance.ContentFrame.Content = EditProfilePage.Instance;
                        EditProfilePage.Instance.EmployeeContentFrame.Content =CashBorrow.Instance;
                        CashBorrow.Instance.setTodayTransactionList();
                    }
                    StaffPage.Instance.setMenuButtonView(1);
                }
                _selectedEmployee = value;
            }
        }

        public static loan SelectedLoan
        {
            get
            {
                return _selectedLoan;
            }
            set
            {
                if (value != null)
                {
                    _selectedLoan = value;
                    LoanPage.Instance.SelectedLoan.Content = value.LOAN_ID;
                    LoanPage.Instance.SelectedLoanLogOutButton.Visibility = Visibility.Visible;

                    if (Navigation != null)
                    {
                        LoanPage.Instance.ContentFrame.Content = Navigation;
                    }
                    else
                    {
                        LoanPage.Instance.ContentFrame.Content = ContentPageLoan.Instance;
                        ContentPageLoan.Instance.LoanContentFrame.Content = PayLoan.Instance;
                        ContentPageLoan.Instance.SearchContentFrame.Content = null;
                        LoanPage.ViewMode = Mode.LOANPAY;
                    }
                }
                _selectedLoan = value;
            }
        }

        public static Boolean LogOut()
        {
            if (SelectedEmployee != null || SelectedCustomer != null || SelectedLoan != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void LogOutSelectedEmployee()
        {
            if (StaffPage.Instance != null)
            {
                StaffPage.Instance.SelectedEmployeeName.Content = "No Employee Selected";
                StaffPage.Instance.SelectedEmpLogOutButton.Visibility = Visibility.Hidden;
                StaffPage.Instance.ContentFrame.Content = QuickSearchPageStaff.Instance;
                QuickSearchPageStaff.Instance.RefreshPage();
                StaffPage.Instance.setMenuButtonView(0);
            }
                SelectedEmployee = null;
           
        }

        public static void LogOutSelectedCustomer()
        {
            if (CustomerPage.Instance != null)
            {
                CustomerPage.Instance.SelectedCustomerName.Content = "No Customer Selected";
                CustomerPage.Instance.SelectedCusLogOutButton.Visibility = Visibility.Hidden;
                CustomerPage.Instance.ContentFrame.Content = QuickSearchPage.Instance;
                QuickSearchPage.Instance.RefreshPage();
                CustomerPage.ViewMode = Mode.LIST;
            }

            SelectedCustomer = null;
        }

        public static void LogOutSelectedLoan()
        {
            if (LoanPage.Instance != null)
            {
                LoanPage.Instance.SelectedLoan.Content = "No Loan Selected";
                LoanPage.Instance.SelectedLoanLogOutButton.Visibility = Visibility.Hidden;
                LoanPage.Instance.ContentFrame.Content = QuickSearchPageLoan.Instance;
                QuickSearchPageLoan.Instance.RefreshPage();
                LoanPage.ViewMode = Mode.LIST;
            }
            SelectedLoan = null;
        }
        public static int deleteSelectedEmployee()
        {
            if (SelectedEmployee != null)
            {
                EmployeeService.DeleteEmployee(SelectedEmployee);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static int deleteSelectedCustomer()
        {
            if (SelectedCustomer != null)
            {
                CustomerService.DeleteCustomer(SelectedCustomer);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static int deleteSelectedLoan()
        {
            if (SelectedLoan != null)
            {
                LoanService.DeleteLoan(SelectedLoan);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
