using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Util
{
    class EmailHandler
    {
        public static bool SendMail(EmailModel mailModel)
        {
            try
            {
                MailMessage mail = new MailMessage("loaninfo@doerit.com", mailModel.ToEmail);
                SmtpClient client = new SmtpClient();
                client.Port = 26;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "mail.doerit.com";
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential("loaninfo@doerit.com", "1qaz2wsx@");
                mail.Subject = mailModel.Subject;
                mail.Body = mailModel.Body;

                if (mailModel.AttachmentPath != null && mailModel.AttachmentPath != "")
                {
                    Attachment attachment = new System.Net.Mail.Attachment(mailModel.AttachmentPath);
                    mail.Attachments.Add(attachment);
                }

                client.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public static bool IsValidEmail(string email)
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
    }
}
