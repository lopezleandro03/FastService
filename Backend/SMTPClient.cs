using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Backend
{
    public class SMTPClient
    {
        private readonly string SERVICEEMAILACCOUNT = "pagosfastservice@gmail.com";
        private readonly string SMTPURL = "smtp.gmail.com";
        private readonly string SUBJECT = "FAST SERVICE - Pago orden de trabajo {0}";
        private MailAddress FromAddress { get; set; }
        private MailAddress ToAddress { get; set; }
        private string FromPassword { get; set; }
        private SmtpClient smtp { get; set; }

        public SMTPClient(string serviceAccountPassword, string receipientAddress, string receipientName)
        {
            FromAddress = new MailAddress(SERVICEEMAILACCOUNT, "Fast Service");
            ToAddress = new MailAddress(receipientAddress, receipientName);
            FromPassword = serviceAccountPassword;

            smtp = new SmtpClient
            {
                Host = SMTPURL,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(FromAddress.Address, FromPassword),
                Timeout = 20000
            };
        }

        public void SendPaymentInformation(string serviceId, string paymentLink)
        {
            var body = new StringBuilder();
            body.Append(String.Format("Estimado Cliente,{0}", Environment.NewLine));
            body.Append(String.Format("Lo contactamos a fin de comunicarle que la orden de pago {0} se encuentra lista para ser pagada. {1}", serviceId, Environment.NewLine));
            body.Append(String.Format("Haga click en el link que se encuentra a continuación para pagar con tarjeta de crédito por medio de Mercado Pago:{0}{1}", Environment.NewLine, Environment.NewLine));
            body.Append(String.Format("{0}{1}{2}", paymentLink, Environment.NewLine, Environment.NewLine));
            body.Append(String.Format("Ante cualquier duda, nos encontramos a su disposición para asistirlo. Gracias.{0}{1}", Environment.NewLine, Environment.NewLine));
            body.Append(String.Format("Atentamente,{0} Fast Service Gestión Online", Environment.NewLine));

            try
            {
                using (var message = new MailMessage(FromAddress, ToAddress)
                {
                    Subject = string.Format(SUBJECT, serviceId),
                    Body = body.ToString()
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}