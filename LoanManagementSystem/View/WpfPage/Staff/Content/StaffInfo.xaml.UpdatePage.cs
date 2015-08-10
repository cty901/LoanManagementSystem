using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.DBModel;
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
        Mode mode;

        public StaffInfo(Mode mode)
        {
            InitializeComponent();

            if (mode.Equals(Mode.EDIT))
            {
                this.mode = mode;

                if (Session.SelectedEmployee != null)
                {
                    SetEmployeeDetails(Session.SelectedEmployee);
                }
            }
            if (mode.Equals(Mode.VIEW))
            {
                this.mode = mode;

                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, false, false, false, false, false);

                if (Session.SelectedEmployee != null)
                {
                    SetEmployeeDetails(Session.SelectedEmployee);
                }
            }
            if (mode.Equals(Mode.NEW))
            {
                this.mode = mode;
            }
        }
    }
}
