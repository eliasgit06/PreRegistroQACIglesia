using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace QACIglesia.Infrastructure
{
    public class AvanzaMailKit
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public AvanzaMailKit(List<string> to, string subject)
        {
            To = to;
            Subject = subject;
        }

        public void ParseBody(string filePath, Dictionary<string, string> filter)
        {
            StreamReader reader = File.OpenText(HttpContext.Current.Server.MapPath("~/Notificaciones/" + filePath));
            Body = reader.ReadToEnd();
            foreach (var word in filter.Keys)
                Body = Body.Replace(word, filter[word]);


        }

        public void Send()
        {
            var message = new MimeMessage();
            foreach (string a in To)
            {
                message.To.Add(new MailboxAddress(a));
            }

            message.From.Add(new MailboxAddress("Avanza Administrador", ConfigurationManager.AppSettings["emailClientUser"].ToString()));
            message.Subject = Subject;

            message.Body = new TextPart("html")
            {
                Text = Body

            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(
                    ConfigurationManager.AppSettings["emailClientUser"].ToString(),
                    ConfigurationManager.AppSettings["emailClientPassword"].ToString())
                ;

                client.Send(message);
                client.Disconnect(true);
            }
        }


    }
}
