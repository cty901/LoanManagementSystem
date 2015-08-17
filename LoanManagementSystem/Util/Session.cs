using LoanManagementSystem.DBModel;
using LoanManagementSystem.View.WpfPage;
using LoanManagementSystem.View.WpfPage.Customer;
using LoanManagementSystem.View.WpfPage.Customer.Content;
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
        public static Object Navigation = null;

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
                        EditProfilePage.Instance.EmployeeContentFrame.Content = CashBorrow.Instance;
                    }
                    StaffPage.Instance.setMenuButtonView(1);
                }
                _selectedEmployee = value;
            }
        }

        public static void LogOutSelectedEmployee()
        {
            StaffPage.Instance.SelectedEmployeeName.Content = "No Employee Selected";
            StaffPage.Instance.SelectedEmpLogOutButton.Visibility = Visibility.Hidden;
            StaffPage.Instance.ContentFrame.Content = QuickSearchPageStaff.Instance;
            QuickSearchPageStaff.Instance.RefreshPage();
            StaffPage.Instance.setMenuButtonView(0);

            SelectedEmployee = null; ;
        }

        public static void LogOutSelectedCustomer()
        {
            CustomerPage.Instance.SelectedCustomerName.Content = "No Customer Selected";
            CustomerPage.Instance.SelectedCusLogOutButton.Visibility = Visibility.Hidden;
            CustomerPage.Instance.ContentFrame.Content = QuickSearchPage.Instance;
            CustomerPage.ViewMode=Mode.LIST;

            SelectedCustomer = null; ;
        }

    }
}
