using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System;
using System.Windows.Media;

using LoanManagementSystem.View.WpfPage;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.Util;
using LoanManagementSystem.Properties;
using LoanManagementSystem.Model.SMSModel;
using System.ComponentModel;
using System.Threading;


namespace LoanManagementSystem.View.WpfWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static MainWindow instance;
        private SMSManager sm = SMSManager.Instance;
        Doerit.SMSLib.DoerSMSDeviceManager DeviceManager = Doerit.SMSLib.DoerSMSDeviceManager.Instance;

        private MainWindow()
        {
            InitializeComponent();
            ContentFrame.Content = DashBoardPage.Instance;
            setLoginDeatails();
            WorkingModemGrid.DataContext = sm.WorkingModem;
            instance = this;
            sm.PropertyChanged += setWorkingModem;
        }
        public void setWorkingModem(object source, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                //no direct interaction with the UI is allowed from this method
                Dispatcher.Invoke((Action)(() => WorkingModemGrid.DataContext = sm.WorkingModem));
            };
            //set the IsBusy before you start the thread
            worker.RunWorkerAsync();
        }

        public static MainWindow Instance
        {
            get
            {
                if (instance == null || !instance.IsActive)
                {
                    instance = new MainWindow();
                }

                return instance; 
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OnMinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
        }

        private void OnCloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DoctorHomePageButton_Click(object sender, RoutedEventArgs e)
        {
            TransferToDoctorHomePage();
        }

        public void TransferToDoctorHomePage()
        {
            //ContentFrame.Content = null;
            //ContentFrame.Content = DoctorHomePage.Instance;
        }

        private void LogoutPageButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Instance.FileMenu.Visibility = Visibility.Hidden;
            //ContentFrame.Content = DashBoardPage.Instance;
        }

        private void PatientsPageButton_Click(object sender, RoutedEventArgs e)
        {
           // ContentFrame.Content = PatientsPage.Instance;
        }

        private void DirectoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = EmployeePage.Instance;
        }

        private void DashBoardButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = DashBoardPage.Instance;
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
                MessageDialogResult result = await this.ShowMessageAsync("Log off", "Do You really want to Log off?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    LoginWindow.Instance.Show();
                    this.Hide();
                }

        }

        private void CompanyButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = CompanyPage.Instance;
            //CompanyPage.Instance.ContentFrame.Content = ViewPage.Instance;
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Content = ReportPage.Instance;
        }

        public async void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.LogOut())
            {
                await this.ShowMessageAsync(this.Title, "Please Log Out from Selected Element", MessageDialogStyle.Affirmative);
            }
            else
            {
                ContentFrame.Content = DashBoardPage.Instance;
            }
        }
        public void setLoginDeatails()
        {
            this.EmpUserNameLable.Content = Util.LetterHandller.UppercaseFirst(Session.LoggedEmployee.USERNAME);
            Util.ImageHandller.setProfImage(Session.LoggedEmployee.PROFPIC, this.ProfPicBox);

            string most_resent_logged_in_username = Session.LoggedEmployee.USERNAME;
            Settings.Default["RecentLoginName"] = most_resent_logged_in_username;

            if (Session.LoggedEmployee.PROFPIC != null)
            {
                string most_resent_logged_in_profilepic = System.Convert.ToBase64String(Session.LoggedEmployee.PROFPIC);
                Settings.Default["RecentLoginUserProfPic"] = most_resent_logged_in_profilepic;
            }

            Settings.Default.Save();
        }

        private bool? ShouldClose = null;

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ShouldClose == null)
            {
                e.Cancel = true; //stop the window from closing.
                MessageDialogResult result = await this.ShowMessageAsync(this.Title, "Do You really want to exit?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative)
                {
                    ShouldClose = false;
                }
                else
                {
                    ShouldClose = true;

                    //stop Device watcher before SMSManger
                    SMSManager.Instance.stopWacher();
                    Application.Current.Shutdown();
                }
            }
            else if (!(bool)ShouldClose)
            {
                e.Cancel = true; //prevent the window from closing.

            }
            ShouldClose = null;
        }

        private void UserSettingsButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
