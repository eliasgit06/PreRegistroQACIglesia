using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;
namespace QACIglesia.Infrastructure
{
    public class Email
    {

        private const string From = "SIMAG <simag@digepep.gob.do>";
        private const string Emaill_Message_Constant = "Saludos,\n\nUsted has sido postulado como animador dentro del programa de alfabetización para el muncipio <muncipio>. Esta postulación estará sujeta a aprobación. Por manténgase chequeando su email por actualizaciones de esta postulación.\n\nMuchas gracias,\n\nDirección General de Programas Especiales de la Presidencia\n\n--DIGEPEP--";
        private string Message_Body;
        private List<string> To;
        private List<string> Cc;
        private List<string> Bbc;
        private MailAddress Remitente;


        public Email(List<string> To = null, string correRemitente = null, string msg = null)
        {

            if (correRemitente != null)
                AgregarRemitente(correRemitente);
            else
                AgregarRemitente(From);

            if (To != null)
                AgregarDestinatario(To);

            if (msg != null)
                AgregarMensaje(msg);
            else
                AgregarMensaje(Emaill_Message_Constant);

            Message_Body = Emaill_Message_Constant;

        }
        public void AgregarDestinatario(List<string> To)
        {
            this.To = To;
        }

        public void AgregarDestinatarioBcc(List<string> Bcc)
        {
            this.Bbc = Bcc;
        }
        public void AgregarDestinatarioCc(List<string> Cc)
        {
            this.Cc = Cc;
        }


        public void AgregarRemitente(string From)
        {
            Remitente = new MailAddress(From);
        }

        public void AgregarMensaje(string msg)
        {
            Message_Body = msg;
        }

        public void SendEmail(Dictionary<string, string> filtro, string plantillaMensaje = null, string mensajeEnTexto = null)
        {
            using (MailMessage mensajero = new MailMessage())
            {

                mensajero.From = Remitente;
                if (this.To != null)
                {
                    foreach (var email in this.To)
                    {
                        mensajero.To.Add(email);
                    }
                }

                if (this.Cc != null)
                {
                    foreach (var email in this.Cc)
                    {
                        mensajero.CC.Add(email);
                    }
                }

                if (this.Bbc != null)
                {
                    foreach (var email in this.Bbc)
                    {
                        mensajero.Bcc.Add(email);
                    }
                }

                if (plantillaMensaje != null)
                {
                    mensajero.Body = createEmailHtmlBody(filtro, plantillaMensaje);
                    mensajero.IsBodyHtml = true;

                }
                else
                {
                    mensajero.Body = mensajeEnTexto;
                    mensajero.IsBodyHtml = false;
                }

                SmtpClient client = new SmtpClient();

                client.Port = 587;
                client.Host = "smtp.outlook.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("simag@digepep.gob.do", "Digepep01");

                try
                {
                    client.Send(mensajero);
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }


        private string createEmailHtmlBody(Dictionary<string, string> filter, string plantillaMensaje)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Notificaciones/" + plantillaMensaje)))

            {

                body = reader.ReadToEnd();

            }


            foreach (var word in filter.Keys)
                body = body.Replace(word, filter[word]);

            return body;
        }
    }
}
