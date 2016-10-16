using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Salvis.Framework.Net
{
    public class Mailer : IDisposable
    {

        private readonly MailMessage _mailMessage;

        private String _mailTemplateContent;
        private String _mailTemplatePath;

        public Mailer()
        {
            _mailMessage = new MailMessage();
            var resources = this.GetType().Assembly.GetManifestResourceNames().Where(s => !s.Contains(".html"));   
            LoadTemplate(MailTemplateStyle.Simple);
        }

        ~Mailer()
        {
            _mailMessage.Dispose();
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Loads a Mail template, if not indicated, the Simple style will be used.
        /// </summary>
        public void LoadTemplate(MailTemplateStyle templateStyle)
        {

            switch (templateStyle)
            {
                case MailTemplateStyle.Welcome:
                    _mailTemplatePath = @"/Resources/MailTemplate_Welcome.html";
                    break;
                case MailTemplateStyle.PasswordReset:
                    _mailTemplatePath = @"/Resources/MailTemplate_PasswordReset.html";
                    break;
                case MailTemplateStyle.Simple:
                default:
                    _mailTemplatePath = @"/Resources/MailTemplate_Simple.html";
                    break;
            }

            //using (var stream = new StreamReader(Path.Combine(Environment.NewLine, _mailTemplatePath)))
            //{
            //    _mailTemplateContent = stream.ReadToEnd();
            //    stream.Close();
            //}

        }

        public void PrepareMail(string to, string subject, string body, string userName = "")
        {
            //_mailMessage.From = new MailAddress("salvis@protonmail.com", SalvisConstant.APP_NAME);

            //_mailMessage.To.Add(to);
            //_mailMessage.Subject = subject;

            //SetTemplateValue(MailTemplateCustomTags.TITLE, subject);
            //SetTemplateValue(MailTemplateCustomTags.BODY, body);
            //if (!String.IsNullOrEmpty(userName))
            //    SetTemplateValue(MailTemplateCustomTags.NAME, userName);

            //_mailMessage.IsBodyHtml = true;
            //_mailMessage.Body = _mailTemplateContent;
        }

        public void PrepareMail(string to, string subject, IDictionary<String, object> messageContent, string userName = "")
        {
            //_mailMessage.From = new MailAddress("salvis@protonmail.com", SalvisConstant.APP_NAME);

            //_mailMessage.To.Add(to);
            //_mailMessage.Subject = subject;

            //SetTemplateValue(MailTemplateCustomTags.TITLE, subject);
            //if (!String.IsNullOrEmpty(userName))
            //    SetTemplateValue(MailTemplateCustomTags.NAME, userName);

            //foreach (var item in messageContent)
            //{
            //    SetTemplateValue(String.Format("@[{0}]", item.Key), item.Value);
            //}

            //_mailMessage.IsBodyHtml = true;
            //_mailMessage.Body = _mailTemplateContent;
        }

        public Task SendMail()
        {
            var task = Task.Factory.StartNew(() =>
                {
                    var sender = new SmtpClient
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("salvis@protonmail.com", "Contraseña@123")
                        };
                    //sender.Send(_mailMessage);
                });
            return task;
        }

        private void SetTemplateValue(String customTag, object value)
        {
            //_mailTemplateContent = _mailTemplateContent.Replace(customTag, value + "");
        }

        public class MailTemplateCustomTags
        {
            public const String TITLE = "@[title]";
            public const String NAME = "@[Name]";
            public const String BODY = "@[body]";
        }

    }

    public enum MailTemplateStyle
    {
        Welcome,
        PasswordReset,
        Simple
    }

}
