using LoanManagementSystem.DBModel;
using LoanManagementSystem.View.WpfPage;
using LoanManagementSystem.View.WpfPage.Staff;

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
        public static Object Navigation = null;

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
                    StaffPage.Instance.SelectedEmployeeName.Content = value.FIRST_NAME;
                    StaffPage.Instance.SelectedEmpLogOutButton.Visibility = Visibility.Visible;

                    if (Navigation != null)
                    {
                        StaffPage.Instance.ContentFrame.Content = Navigation;
                    }
                    else
                    {
                        StaffPage.Instance.ContentFrame.Content = EditProfilePage.Instance;
                        EditProfilePage.Instance.EmployeeContentFrame.Content = new StaffInfo(Mode.VIEW);
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
            StaffPage.Instance.setMenuButtonView(0);

            SelectedEmployee = null; ;
        }
    }
}
