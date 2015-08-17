using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Data;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfPage.Customer.Content;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls.Dialogs;


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
        private object navigation;

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
            ContentFrame.Content = QuickSearchPage.Instance;
            viewMode = Mode.LIST;
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
               // BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
                EditProfileButton.Visibility = System.Windows.Visibility.Visible;
                IssueLoanButton.Visibility = System.Windows.Visibility.Visible;
                LoanPaymentButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.EDIT)
            {
                 BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
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
            navigation = QuickSearchPage.Instance;
            //EditProfilePage.Instance.EmployeeContentFrame.Content = new StaffInfo(Mode.NEW);

        }

        private void BackButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigation;
            ViewMode = Mode.LIST;
            Session.LogOutSelectedCustomer();
        }

        private void SelectedCusLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Session.LogOutSelectedCustomer(); 
        }

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (Session.SelectedCustomer == null)
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, Messages.MSG_SELECT_CUSTOMER, MessageDialogStyle.Affirmative);
                ContentFrame.Content = QuickSearchPage.Instance;
            }
            else
            {
                viewMode = Mode.EDIT;
                CustomerPage.Instance.ContentFrame.Content = new DetailsPage(Mode.EDIT);
                
            }
        }

    }
}
