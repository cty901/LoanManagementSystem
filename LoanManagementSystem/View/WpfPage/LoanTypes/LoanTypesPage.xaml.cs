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
using LoanManagementSystem.View.WpfPage.LoanTypes.Content;


namespace LoanManagementSystem.View.WpfPage.LoanTypes
{
    /// <summary>
    /// Interaction logic for LoanPage.xaml
    /// </summary>
    public partial class LoanTypesPage : Page
    {
        private static LoanTypesPage instance;


        public LoanTypesPage()
        {
            InitializeComponent();
            hideMenu();
            ContentFrame.Content = LoanTypeDetails.Instance;
        }

        public static LoanTypesPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoanTypesPage();
                }
                return instance;
            }
        }

      

        private void collapseAllMenuItems()
        {
            foreach (Button child in MenuBar.Children)
            {
                child.Visibility = System.Windows.Visibility.Collapsed;

            }
        }

        private void hideMenu()
        {
            collapseAllMenuItems();
            SelectedLoanPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
  
    }
}
