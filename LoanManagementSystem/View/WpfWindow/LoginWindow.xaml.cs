using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.Properties;

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
using System.Windows.Shapes;
using LoanManagementSystem.DBModel;
using System.Data;
using System.ComponentModel;

namespace LoanManagementSystem.View.WpfWindow
{
    public partial class LoginWindow : MetroWindow
    {
        List<employee> userList;

        Exception capturedException = null;

        private static LoginWindow instance;

        public LoginWindow()
        {
            InitializeComponent();
            this.UserNameTextBox.Text = Settings.Default["RecentLoginName"].ToString();
            if (Settings.Default["RecentLoginUserProfPic"].ToString() != "")
            {
                byte[] ProfPic = System.Convert.FromBase64String(Settings.Default["RecentLoginUserProfPic"].ToString());
                Util.ImageHandller.setProfImage(ProfPic, ProfPicBox);
            }
        }


        public static LoginWindow Instance
        {
            get
            {
                if (instance == null || !instance.IsActive)
                {
                    instance = new LoginWindow();
                }

                return instance;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private bool TestConnEF()
        {
            using (var db = new loandbEntities())
            {
                try
                {
                    db.Database.Connection.Open();
                    if (db.Database.Connection.State == ConnectionState.Open)
                    {
                       return true;
                    }
                    return false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string password = PasswordTextBox.Password;

            username = "sampath";
            password = "sampath";

            try
            {
                userList = EmployeeService.GetLoggedEmployeeByUserNamePassword(username, password);

                if (userList.Count() == 1)
                {
                    Session.LoggedEmployee = userList.SingleOrDefault();
                    Session.Account_Type = userList.SingleOrDefault().ACCOUNT_TYPE;
                    Session.EmployeeID = userList.SingleOrDefault().EMP_ID;
                                      
                    MainWindow.Instance.Show();
                    ShouldClose = true;
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("Login Error", "Please check your username and password", MessageDialogStyle.Affirmative);
                    UserNameTextBox.Clear();
                    PasswordTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                capturedException=ex;
            }
            if (capturedException != null)
            {
                await this.ShowMessageAsync("Login Error", capturedException.Message.ToString(), MessageDialogStyle.Affirmative);
            }
        }        

        private bool? ShouldClose = null;

        private async void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                    Application.Current.Shutdown();
                }
            }
            else if (!(bool)ShouldClose)
            {
                e.Cancel = true; //prevent the window from closing.
                
            }
            ShouldClose = null;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.Clear();
            PasswordTextBox.Clear();

        }
    }
}
