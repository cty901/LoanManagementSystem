using LoanManagementSystem.View.WpfPage.Staff;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Data;


namespace LoanManagementSystem.View.WpfPage
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private static StaffPage instance;
        public IList<string> ErrorList { get; set; }

        public StaffPage()
        {
            InitializeComponent();

            ContentFrame.Content = QuickSearchPageStaff.Instance;

            //var test = ManagerService.GetManagerListByUserNamePassword("Test", "Test");

            //string str = test.ToString();
        }

        public static StaffPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StaffPage();
                }

                return instance;
            }
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = EditProfilePage.Instance;
            EditProfilePage.Instance.EmployeeContentFrame.Content = new StaffInfo(Mode.NEW);

            MenuDefaultEmployeeDetails.Height = 0;
            MenuAddEmployeeDetails.Height = Double.NaN;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = QuickSearchPageStaff.Instance;

            MenuAddEmployeeDetails.Height = 0;
            MenuDefaultEmployeeDetails.Height = Double.NaN;
        }

        private void SearchEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
           // ContentFrame.Content = SearchPage.Instance;
        }

        private void ChangeTitleButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Session.SelectedEmployee == null)
            //{
            //    MessageWindow msg = new MessageWindow(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE);
            //    msg.ShowDialog();
            //    ContentFrame.Content = QuickSearchPage.Instance;

            //    Session.Navigation = TitleChangePage.Instance;
            //}
            //else
            //{
            //    ContentFrame.Content = TitleChangePage.Instance;
            //}
        }

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (Session.SelectedEmployee == null)
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE,MessageDialogStyle.Affirmative);
                ContentFrame.Content = QuickSearchPageStaff.Instance;

                Session.Navigation = EditProfilePage.Instance;
            }
            else
            {
                ContentFrame.Content = EditProfilePage.Instance;
                EditProfilePage.Instance.EmployeeContentFrame.Content = new StaffInfo(Mode.EDIT);
            }
        }

        private void LeaveRequestButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Session.SelectedEmployee == null)
            //{
            //    MessageWindow msg = new MessageWindow(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE);
            //    msg.ShowDialog();
            //    ContentFrame.Content = QuickSearchPage.Instance;

            //    Session.Navigation = LeaveRequestPage.Instance;
            //}
            //else
            //{
            //    ContentFrame.Content = LeaveRequestPage.Instance;
            //}
        }

        private void EmployeeDetailsSaveButton_Click(object sender, RoutedEventArgs e)
        {
           // DetailsPage.Instance.SaveNewEmployeeDetails();
        }

        private void SelectedEmpLogOutButton_Click(object sender, RoutedEventArgs e)
        {           
           Session.LogOutSelectedEmployee();            
        }

        private void Border_Loaded_1(object sender, RoutedEventArgs e)
        {
            instance.setMenuButtonView(0);
        }

        public void setMenuButtonView(int type)
        {
            if (type == 0)
            {
                this.AddEmployeeButton.Visibility = System.Windows.Visibility.Visible;
                this.SearchEmployeeButton.Visibility = System.Windows.Visibility.Visible;
              //  this.LeaveRequestButton.Visibility = System.Windows.Visibility.Collapsed;
              //  this.ChangeTitleButton.Visibility = System.Windows.Visibility.Collapsed;
                this.EditProfileButton.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (type == 1)
            {
                this.AddEmployeeButton.Visibility = System.Windows.Visibility.Collapsed;
                this.SearchEmployeeButton.Visibility = System.Windows.Visibility.Collapsed;
               // this.LeaveRequestButton.Visibility = System.Windows.Visibility.Visible;
               // this.ChangeTitleButton.Visibility = System.Windows.Visibility.Visible;
                this.EditProfileButton.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
