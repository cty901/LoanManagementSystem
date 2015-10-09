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
using System.ComponentModel;
using LoanManagementSystem.View.WpfPage.Loan;
using LoanManagementSystem.View.WpfPage.Loan.Content;

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
                 DeleteButton.Visibility = System.Windows.Visibility.Visible;
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
            SaveChanges(DetailsPage.Instance, e);
           // ContentFrame.Content = navigation;
           //ViewMode = Mode.LIST;
        }

        private void SelectedCusLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges(DetailsPage.Instance, e);  
        }
        private async void SaveChanges(object sender, RoutedEventArgs e)
        {
            DetailsPage page = (DetailsPage)sender;

            if (page.Customer.NeedToSave == true)
            {
                MessageDialogResult result = await MainWindow.Instance.ShowMessageAsync(this.Title, "Do You Want to Save Changes?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    DetailsPage.Instance.CustomerDetailsSaveButton_Click(sender, e);
                }
                else
                {
                    DetailsPage.Instance.clearDetailsPage();
                }
                Session.LogOutSelectedCustomer();
            }
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

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           MessageDialogResult result=await MainWindow.Instance.ShowMessageAsync(Messages.MSG_DELETE_CUSTOMER, "Do you want to Delete Customer?", MessageDialogStyle.AffirmativeAndNegative);

           if (result == MessageDialogResult.Affirmative)
           {
               if (Session.deleteSelectedCustomer() == 1)
               {
                   await MainWindow.Instance.ShowMessageAsync(Messages.MSG_DELETE_CUSTOMER, "Customer Deleted Successfully..", MessageDialogStyle.Affirmative);
                   Session.LogOutSelectedCustomer();
               }
               else
               {
                   await MainWindow.Instance.ShowMessageAsync(Messages.MSG_DELETE_CUSTOMER, "Customer Delete fail..", MessageDialogStyle.Affirmative);
               }
           }

        }

        private void IssueLoanButton_Click(object sender, RoutedEventArgs e)
        {
            //logout
            string _customerID = Session.SelectedCustomer.ID_NUM;
            CustomerPage.Instance.SelectedCusLogOutButton_Click(sender, e);
            DashBoardPage.Instance.LoanBtn_Click(DashBoardPage.Instance.LoanBtn, e);
            LoanPage.Instance.IssueLoanButton_Click(LoanPage.Instance.IssueLoanButton, e);
            MultiSearch.Instance.SearchTextBox.Text = _customerID;
            MultiSearch.Instance.SearchButton_Click(MultiSearch.Instance.SearchButton, e);
            //ContentPageLoan.Instance.SearchContentFrame.Content = MultiSearch.Instance;
            //ContentPageLoan.Instance.LoanContentFrame.Content = IssueLoan.Instance;
            //IssueLoan.Instance.ViewMode = Mode.NEW;
            //LoanPage.ViewMode = Mode.LOANISSUE;
        }

        private void LoanPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            //var _customer = Session.SelectedCustomer;
            //var _loan = _customer.loans;
            //SelectedCusLogOutButton_Click(sender, e);//logout
            //DashBoardPage.Instance.LoanBtn_Click(sender, e);
        }       
    }
}
