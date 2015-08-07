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
                if (Session.SelectedEmployee != null)
                {
                    SetEmployeeDetails(Session.SelectedEmployee);
                }
            }
            if (mode.Equals(Mode.NEW))
            {
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, false, false, false, false, false);

                if (Session.SelectedEmployee != null)
                {
                    SetEmployeeDetails(Session.SelectedEmployee);
                }
            }
        }
    }
}
