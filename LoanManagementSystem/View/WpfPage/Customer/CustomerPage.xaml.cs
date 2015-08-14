using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Data;
using LoanManagementSystem.View.WpfPage.Customer.CustomerPages;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfPage.Customer.Content;


namespace LoanManagementSystem.View.WpfPage.Customer
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private static CustomerPage instance;
        public IList<string> ErrorList { get; set; }
        private static Mode viewMode;

        public static Mode ViewMode
        {
            get { return viewMode; }
            set
            {
                viewMode = value;
                instance.updateMenuButtonView();
            }
        }


        public CustomerPage()
        {
            InitializeComponent();
            viewMode = Mode.LIST;
            ContentFrame.Content = QuickSearchPage.Instance;

        }

        public static CustomerPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerPage();
                }
                instance.updateMenuButtonView();
                return instance;
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //if (Session.SelectedEmployee == null)
            //{
            //    MessageWindow msg = new MessageWindow(Messages.TTL_MSG, Messages.MSG_SELECT_EMPLOYEE);
            //    msg.ShowDialog();
            //    ContentFrame.Content = QuickSearchPage.Instance;

            //    Session.Navigation = EditProfilePage.Instance;
            //}
            //else
            //{
            //    ContentFrame.Content = EditProfilePage.Instance;
            //}
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
            // Session.LogOutSelectedEmployee();
        }

        private void Border_Loaded_1(object sender, RoutedEventArgs e)
        {
            instance.setMenuButtonView(0);
        }

        public void setMenuButtonView(int type)
        {
            if (type == 0)
            {
                //  this.AddEmployeeButton.Visibility = System.Windows.Visibility.Visible;
                //  this.LeaveRequestButton.Visibility = System.Windows.Visibility.Collapsed;
                //  this.ChangeTitleButton.Visibility = System.Windows.Visibility.Collapsed;
                this.EditProfileButton.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (type == 1)
            {
                //  this.AddEmployeeButton.Visibility = System.Windows.Visibility.Collapsed;
                // this.LeaveRequestButton.Visibility = System.Windows.Visibility.Visible;
                // this.ChangeTitleButton.Visibility = System.Windows.Visibility.Visible;
                this.EditProfileButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void updateMenuButtonView()
        {
            collapseAllMenuItems();
            if (ViewMode == Mode.LIST)
            {
                AddCustomerButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.NEW)
            {
                BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.VIEW)
            {
                BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
                EditProfileButton.Visibility = System.Windows.Visibility.Visible;
                IssueLoanButton.Visibility = System.Windows.Visibility.Visible;
                LoanPaymentButton.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void collapseAllMenuItems()
        {
            foreach (Button child in MenuBar.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;

            }
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            ViewMode = Mode.NEW;
            ContentFrame.Content = ContentPage.Instance;
            ContentPage.Instance.ContentFrame.Content = new DetailsPage(Mode.NEW);
            //EditProfilePage.Instance.EmployeeContentFrame.Content = new StaffInfo(Mode.NEW);

        }

        private void BackButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = QuickSearchPage.Instance;
            ViewMode = Mode.LIST;
        }

    }
}
