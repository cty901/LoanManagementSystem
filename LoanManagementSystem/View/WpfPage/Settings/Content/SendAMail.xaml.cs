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

namespace LoanManagementSystem.View.WpfPage.Settings.Content
{
    /// <summary>
    /// Interaction logic for SendAMail.xaml
    /// </summary>
    public partial class SendAMail : Page
    {
        private static SendAMail _instance;
        private EmailModel _emailModel;

        private SendAMail()
        {
            InitializeComponent();
        }

        public static SendAMail Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendAMail();
                }
                return _instance;
            }
        }

        public EmailModel EMAIL
        {
            get
            {
                return _emailModel;
            }
            set
            {
                _emailModel = value;
                if (_emailModel== null)
                {
                    EmailAddressTextBox.Clear();
                    EmailSubjectTextBox.Clear();
                    EmailContentTextBox.Clear();
                    AttachmentPathLabel.Content = "";
                }
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            createEmail();
            if (EMAIL != null)
            {
                bool result = EmailHandler.SendMail(EMAIL);
                if (result)
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Email has been sent", MessageDialogStyle.Affirmative);
                    EMAIL = null;
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Email sending failed", MessageDialogStyle.Affirmative);
                }
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please Enter Valied Email!", MessageDialogStyle.Affirmative);
            }
        }

        private void createEmail()
        {
            EmailModel email = new EmailModel();

            try
            {
                if(IsValidEmail(EmailAddressTextBox.Text))
                {
                    email.ToEmail = EmailAddressTextBox.Text;
                    email.Subject = EmailSubjectTextBox.Text;
                    email.Body = EmailContentTextBox.Text;
                    email.AttachmentPath = AttachmentPathLabel.Content.ToString();

                    EMAIL = email;
                }
                else 
                {
                    EMAIL = email;
                }

            }
            catch
            {
                EMAIL = null;
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void AttachmentBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                AttachmentPathLabel.Content = dlg.FileName;
            }
        }
    }
}
