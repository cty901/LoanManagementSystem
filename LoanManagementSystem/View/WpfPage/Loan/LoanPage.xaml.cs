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
using LoanManagementSystem.View.WpfPage.Loan.Content;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls.Dialogs;


namespace LoanManagementSystem.View.WpfPage.Loan
{
    /// <summary>
    /// Interaction logic for LoanPage.xaml
    /// </summary>
    public partial class LoanPage : Page
    {
        private static LoanPage instance;
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


        public LoanPage()
        {
            InitializeComponent();
            ContentFrame.Content = QuickSearchPageLoan.Instance;
            viewMode = Mode.LIST;
        }

        public static LoanPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoanPage();
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
                IssueLoanButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.LOANPAY)
            {
                LoanViewButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.LOANISSUE)
            {
                BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.VIEW)
            {
                LoanPaymentButton.Visibility = System.Windows.Visibility.Visible;
                EditLoanButton.Visibility = System.Windows.Visibility.Visible;
            }
            else if (ViewMode == Mode.EDIT)
            {
                BackButtonTemp.Visibility = System.Windows.Visibility.Visible;
                DeleteLoanButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void collapseAllMenuItems()
        {
            foreach (Button child in MenuBar.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;

            }
        }

        private void BackButtonTemp_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = navigation;
            ViewMode = Mode.LIST;
            Session.LogOutSelectedLoan();
        }

        private void SelectedCusLogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Session.LogOutSelectedLoan(); 
        }

        public void IssueLoanButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = ContentPageLoan.Instance;
            ContentPageLoan.Instance.SearchContentFrame.Content = MultiSearch.Instance;
            ContentPageLoan.Instance.LoanContentFrame.Content = IssueLoan.Instance;
            IssueLoan.Instance.ViewMode = Mode.NEW;
            LoanPage.ViewMode = Mode.LOANISSUE;
        }

        private void LoanViewButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = ContentPageLoan.Instance;
            ContentPageLoan.Instance.LoanContentFrame.Content = IssueLoan.Instance;
            ContentPageLoan.Instance.SearchContentFrame.Content = null;
            IssueLoan.Instance.ViewMode = Mode.VIEW;
            ViewMode = Mode.VIEW;
        }

        private void LoanPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = ContentPageLoan.Instance;
            ContentPageLoan.Instance.LoanContentFrame.Content = PayLoan.Instance;
            ContentPageLoan.Instance.SearchContentFrame.Content = null;
            ViewMode = Mode.LOANPAY;
        }

        private void EditLoanButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = ContentPageLoan.Instance;
            ContentPageLoan.Instance.LoanContentFrame.Content = IssueLoan.Instance;
            ContentPageLoan.Instance.SearchContentFrame.Content = null;
            IssueLoan.Instance.ViewMode = Mode.EDIT;
            ViewMode = Mode.EDIT;
        }

        private async void DeleteLoanButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult result = await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Do you want to Delete Loan?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (Session.deleteSelectedLoan() == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Deleted Successfully..", MessageDialogStyle.Affirmative);
                    Session.LogOutSelectedLoan();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Loan Delete fail..", MessageDialogStyle.Affirmative);
                }
            }
        }
        
    }
}
