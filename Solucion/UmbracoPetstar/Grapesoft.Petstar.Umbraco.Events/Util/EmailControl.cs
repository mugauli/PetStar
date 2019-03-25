using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;

namespace Grapesoft.Petstar.Events.Util
{
    public class EmailControl
    {

        /// <summary>
        /// Envio de correo electronico
        /// </summary>
        /// <param name="From">correo remitente</param>
        /// <param name="To">correo destinatario</param>
        /// <param name="Subject">Asunto</param>
        /// <param name="IsBodyHtml">Define si el contenido es en html</param>
        /// <param name="Body">html de correo </param>
        /// <param name="host">direccion de host para envio</param>
        /// <param name="port">puerto</param>
        /// <param name="user">usuario</param>
        /// <param name="pass">contraeña</param>
        /// <param name="UserCredentials">Define si se va a autenticar con credenciales estaticas</param>
        /// <param name="ssl">Define si el envio es con SSL</param>
        /// <returns>Metodo de retorno con la respuesta exitosa con code = 0</returns>
        public MethodResponse<int> SendMail(String From, String To, String Subject, bool IsBodyHtml, String Body, string host, int port, string user, string pass, bool UserCredentials, bool ssl)
        {
            var response = new MethodResponse<int> { code = 0 };
            try
            {
                MailMessage message = new MailMessage(From, To, Subject, Body);
                message.BodyEncoding = System.Text.Encoding.UTF8;

                message.IsBodyHtml = IsBodyHtml;

                SmtpClient emailClient = new SmtpClient(host, port);

                //Autenticación con credenciales estaticas
                emailClient.UseDefaultCredentials = UserCredentials;
                if (UserCredentials)
                {
                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(user, pass);
                    emailClient.Credentials = SMTPUserInfo;
                }

                //Envío con seguridad SSL
                emailClient.EnableSsl = ssl;

                //Envio del correo 
                emailClient.Send(message);
                

                response.Result = 1;
            }
            catch (Exception ex)
            {
                response.Result = -1;
                response.message = ex.Message;
            }
            return response;
        }


        //public string SendMail(String From, String To, String Subject, String Body, bool withError)
        //{
        //    string result = "-1";
        //    try
        //    {
        //        MailMessage message = new MailMessage(From, To, Subject, Body);
        //        message.IsBodyHtml = true;
        //        message.BodyEncoding = System.Text.Encoding.UTF8;
        //        SmtpClient emailClient = new SmtpClient("smtp.gmail.com", 587);
        //        System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("jerry.ds@hotmail.com", "Jerry1992");
        //        emailClient.EnableSsl = true;
        //        emailClient.UseDefaultCredentials = false;
        //        emailClient.Credentials = SMTPUserInfo;
        //        emailClient.Send(message);
        //        result = "1";
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //    }

        //    return result;
        //}

        //public int SendMailattach(String From, String To, String Subject, String Body, String Attach)
        //{
        //    int result = -1;
        //    try
        //    {
        //        MailMessage message = new MailMessage(From, To, Subject, Body);
        //        message.BodyEncoding = System.Text.Encoding.UTF8;
        //        message.IsBodyHtml = true;
        //        if (Attach != "")
        //        {
        //            Attachment File = new Attachment(HttpContext.Current.Server.MapPath(Attach));
        //            message.Attachments.Add(File);
        //        }
        //        SmtpClient emailClient = new SmtpClient("smtp.gmail.com", 587);
        //        System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("panadis.analitycs@gmail.com", "panadis2011");
        //        emailClient.EnableSsl = true;
        //        emailClient.UseDefaultCredentials = false;
        //        emailClient.Credentials = SMTPUserInfo;
        //        emailClient.Send(message);
        //        result = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -1;
        //    }
        //    return result;
        //}
    }
}
