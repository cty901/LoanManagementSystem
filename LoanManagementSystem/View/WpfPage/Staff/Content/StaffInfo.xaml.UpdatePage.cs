using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using LoanManagementSystem.DBService;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;

namespace LoanManagementSystem.View.WpfPage.Staff
{
    /// <summary>
    /// Interaction logic for Employee EmployeesInfo Update
    /// </summary>
    public partial class StaffInfo : Page
    {
       // List<Contact> ContactsList { get; set; }

        public StaffInfo(Mode mode)
        {
            InitializeComponent();

            if (mode.Equals(Mode.EDIT))
            {
                enableContent(false);

                if (Session.SelectedEmployee != null)
                {
                    SetEmployeeDetails(Session.SelectedEmployee);
                }
            }
        }
    }
}
