using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ModelLayer.Model
{
    public class Send
    {
        public string SendMail(string emailTo,string token)
        {
            string emailfrom = "shanmukhasridharreddy@gmail.com";
            MailMessage message = new MailMessage(emailfrom, emailTo);
            string mailbody = "Token Generated: " + token;
            message.Subject="Token Will Expire after 1 Hour";
            message.Body = mailbody.ToString();
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587);
            NetworkCredential credential = new NetworkCredential("shanmukhasridharreddy@gmail.com", "hmpf wscs fbcv tigv");

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            
            smtpClient.Send(message);
            return emailTo;
        }
    }
}
