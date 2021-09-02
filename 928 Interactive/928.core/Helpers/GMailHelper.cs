using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.IO;

namespace _928.Core.Helpers {
    
    public static class GMailHelper {

        public static string SendGmail(string sender, string subject, string body) {
            return SendGmail("support@studio969.net", sender, subject, body, null);
        }

        public static string SendGmail(string recipient, string sender, string subject, string body, IEnumerable<HttpPostedFileBase> attachments) {

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential("support@studio969.net", "Ru6n3R80");
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            var mail = new MailMessage();
            
            try {
                mail.From = new MailAddress(sender, "Site Generated Email", System.Text.Encoding.UTF8);

                mail.To.Add(recipient);
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = body;

                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.ReplyTo = new MailAddress(sender);
                if (attachments != null) {
                    foreach (var file in attachments) {
                        if (file != null) {
                            mail.Attachments.Add(new Attachment(file.InputStream, Path.GetFileName(file.FileName)));
                        }
                    }
                }
                SmtpServer.Send(mail);
            } catch (Exception ex) {
                return string.Format("Error creating account. Please email support@studio969.net for assistance. {0}", ex.Message);
            }
            return "";
        }

    }
}
