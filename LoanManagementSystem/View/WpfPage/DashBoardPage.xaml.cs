using LoanManagementSystem.Util;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.View.WpfWindow;

using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Data;
using LoanManagementSystem.View.WpfPage.Customer;
using LoanManagementSystem.View.WpfPage.Loan;
using LoanManagementSystem.View.WpfPage.LoanTypes;
using LoanManagementSystem.View.WpfPage.SMS;
using LoanManagementSystem.View.WpfPage.Settings;


namespace LoanManagementSystem.View.WpfPage
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class DashBoardPage : Page
    {
        private static DashBoardPage instance;
        public IList<string> ErrorList { get; set; }

        private DashBoardPage()
        {
            InitializeComponent();
        }

        public static DashBoardPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DashBoardPage();
                }
                return instance;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
            //UsernameText.Text = "";
            //PasswordText.Password = "";
        }

        private void EnterClicked(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            { //Keys.Enter
               
                e.Handled = true;
            }
        }

        private void LoadIrisPatientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        
        private void CompanyBtn_Click(object sender, RoutedEventArgs e)
        {
           // MainWindow.Instance.ContentFrame.Content = CompanyPage.Instance;
           // CompanyPage.Instance.ContentFrame.Content = ViewPage.Instance;
        }

        private async void EmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
                MainWindow.Instance.ContentFrame.Content = CustomerPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages", "Error: Insufficient Previlages", MessageDialogStyle.Affirmative);

            }
        }

        private void ReportsBtn_Click(object sender, RoutedEventArgs e)
        {
           // MainWindow.Instance.ContentFrame.Content = ReportPage.Instance;
        }
        
        private async void HolidaysBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
                MainWindow.Instance.ContentFrame.Content = StaffPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages","Error: Insufficient Previlages", MessageDialogStyle.Affirmative);

            }
        }

        private async void LoanBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
                MainWindow.Instance.ContentFrame.Content = LoanPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages", "Error: Insufficient Previlages", MessageDialogStyle.Affirmative);

            }
        }

        private async void LoanTemplateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
                MainWindow.Instance.ContentFrame.Content = LoanTypesPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages", "Error: Insufficient Previlages", MessageDialogStyle.Affirmative);

            }
        }

        private async void SMSBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
               MainWindow.Instance.ContentFrame.Content = SMSPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages", "Error: Insufficient Previlages", MessageDialogStyle.Affirmative);

            }
        }

        private async void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Account_Type == "admin")
            {
                MainWindow.Instance.ContentFrame.Content = SettingsPage.Instance;
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync("Previlages", "Error: Insufficient Previlages", MessageDialogStyle.Affirmative);
            }
        }
    }
}
