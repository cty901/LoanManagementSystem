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
using System.Drawing;

namespace LoanManagementSystem.View.WpfPage.Reports
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        private static ReportsPage instance;
        public IList<string> ErrorList { get; set; }

        public ReportsPage()
        {
            InitializeComponent();
            ContentFrame.Content = LoanReportPage.Instance;
        }

        public static ReportsPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportsPage();
                }
                return instance;
            }
        }

        private void LoanButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = LoanReportPage.Instance;
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = PaymentReportPage.Instance;
        }

        private void EmployeePaymentButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = EmployeeReportPage.Instance;
        }      
    }
}
