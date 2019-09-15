using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml.Serialization;

namespace BrowserHistoryServer.Data
{
    class DataMailSender : IDisposable
    {
        MailAddress from;
        MailAddress to;
        MailMessage msg;
        string login;
        string pass;
        MailObject obj;

        public DataMailSender()
        {
        }
        public DataMailSender(string emailFrom, string emailTo, string reportPath)
        {
            this.from = new MailAddress(emailFrom);
            this.to = new MailAddress(emailTo);
            this.msg = new MailMessage(from, to);

            this.msg.Subject = "Site report from " + DateTime.Now.ToLongDateString();
            this.msg.Attachments.Add(new Attachment(reportPath));

            this.login = "browserhistory@dietcenter.com.ua";
            this.pass = "bcfVG7A2";
            obj = new MailObject();
            obj.From_email = emailFrom;
            obj.To_email = emailTo;
        }
        #region construct (for all life situations)
        public DataMailSender(string emailFrom, string emailTo, string reportPath, string login, string pass) : this(emailFrom, emailTo, reportPath)
        {
            this.login = login;
            this.pass = pass;
        }
        public DataMailSender(string emailFrom, string emailTo, string reportPath, string login, string pass, string subject) : this(emailFrom, emailTo, reportPath, login, pass)
        {
            this.msg.Subject = subject;
        }
        public DataMailSender(string emailFrom, string emailTo, string reportPath, string login, string pass, string subject, string fromName) : this(emailFrom, emailTo, reportPath, login, pass, subject)
        {
            this.from = new MailAddress(emailFrom, fromName);
        }
        public DataMailSender(string emailFrom, string emailTo, string reportPath, string login, string pass, string subject, string fromName, string body) : this(emailFrom, emailTo, reportPath, login, pass, subject, fromName)
        {
            this.msg.Body = body;
        }

        public void Dispose()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MailObject));
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\settings.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }
        #endregion

        public void Send()
        {
            using (SmtpClient smtp = new SmtpClient("mail2.dietcenter.com.ua", 587))
            {
                smtp.EnableSsl = false;
                // логин и пароль
                smtp.Credentials = new NetworkCredential(login, pass);
                smtp.Send(msg);

            }
        }
        public void Send(string server,int port)
        {
            using (SmtpClient smtp = new SmtpClient(server, port))
            {
                smtp.EnableSsl = false;
                // логин и пароль
                smtp.Credentials = new NetworkCredential(login, pass);
                smtp.Send(msg);

            }
        }
        public void Send(int port)
        {
            using (SmtpClient smtp = new SmtpClient("mail2.dietcenter.com.ua", port))
            {
                smtp.EnableSsl = false;
                // логин и пароль
                smtp.Credentials = new NetworkCredential(login, pass);
                smtp.Send(msg);

            }
        }
    }
}
