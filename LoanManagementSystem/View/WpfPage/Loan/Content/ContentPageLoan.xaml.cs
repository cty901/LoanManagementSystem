﻿using LoanManagementSystem.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoanManagementSystem.View.WpfPage.Loan.Content
{
    /// <summary>
    /// Interaction logic for EditProfilePage.xaml
    /// </summary>
    public partial class ContentPageLoan : Page
    {
        private static ContentPageLoan instance;
        public IList<string> ErrorList { get; set; }

        private ContentPageLoan()
        {
            InitializeComponent();
        }

        public static ContentPageLoan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentPageLoan();
                }

                //instance.EmployeeContentFrame.Content = new StaffInfo(Mode.NEW);
                //instance.ContactsContentFrame.Content = new ContactsInfo(Mode.EDIT);
                return instance;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var h = ((System.Windows.Controls.Panel)Application.Current.MainWindow.Content).ActualHeight;
            ScrollBar.Height = h - 150;
        }

    }
}
