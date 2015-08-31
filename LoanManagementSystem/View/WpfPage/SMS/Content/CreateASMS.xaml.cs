using LoanManagementSystem.DBModel;
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

using LoanManagementSystem.Model.SMSModel;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.View.WpfWindow;
using LoanManagementSystem.Util;
using LoanManagementSystem.DBService.Implementions;

namespace LoanManagementSystem.View.WpfPage.SMS.Content
{
    /// <summary>
    /// Interaction logic for MakeASMS.xaml
    /// </summary>
    public partial class CreateASMS : Page
    {
        private static CreateASMS _instance;
        private customer _selectedCustomer;
        private sm _sms;

        private CreateASMS()
        {
            InitializeComponent();
        }

        public static CreateASMS Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CreateASMS();
                }
                return _instance;
            }
        }

        public customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
               _selectedCustomer=value;
               if (_selectedCustomer != null)
               {
                   CustomerNameTextBox.Text = _selectedCustomer.FULLNAME;
                   CustomerCodeTextBox.Text = _selectedCustomer.CUSTOMER_ID;
                   PhoneNumberTextBox.Text = _selectedCustomer.PHONE_HP1;
               }
               else
               {
                   CustomerNameTextBox.Clear();
                   CustomerCodeTextBox.Clear();
                   PhoneNumberTextBox.Clear();
               }
            }

        }

        public sm SMS
        {
            get
            {
                return _sms;
            }
            set
            {
                _sms = value;
                if (_sms== null)
                {
                    PhoneNumberTextBox.Clear();
                    SMSContentTextBox.Clear();
                    SelectedCustomer = null;
                }
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            createSMS();
            if (SMS != null)
            {
                int result = SMSManager.Instance.SendASMS(SMS.PHONE_NUMBER, SMS.CONTENT);
                if (result == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Sms has been sent", MessageDialogStyle.Affirmative);
                    SMS.SENDING_STATUS = true;
                    SMS.SEND_DATE_TIME = System.DateTime.Now;
                    SMSService.InsertSMS(SMS);
                    SMS = null;
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Sms sending failed", MessageDialogStyle.Affirmative);
                    SMS.SENDING_STATUS = false;
                    SMS.SEND_DATE_TIME = System.DateTime.Now;
                    SMSService.InsertSMS(SMS);
                }
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please Select a Customer!", MessageDialogStyle.Affirmative);
            }
        }

        private void createSMS()
        {
            sm _nsms = new sm();

            try
            {
                _nsms.ID = IDHandller.generateID("sms");

                _nsms.PHONE_NUMBER = PhoneNumberTextBox.Text;
                _nsms.CONTENT = SMSContentTextBox.Text;
                _nsms.TYPE = "send";
                _nsms.SENDING_STATUS = true;

                _nsms.STATUS = true;
                _nsms.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _nsms.INSERT_DATETIME = System.DateTime.Now;

                _nsms.FK_EMPLOYEE_ID = Session.LoggedEmployee.ID;
                _nsms.FK_CUSTOMER_ID = SelectedCustomer.ID;
                SMS = _nsms;
            }
            catch
            {
                SMS = null;
            }
        }
    }
}
