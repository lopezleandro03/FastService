//using System;
//using System.Configuration;
//using System.Net;
//using System.Net.Mail;
//using System.Text;

//namespace Backend
//{
//    public class SMTPClient
//    {
//        private string _smtpUrl;
//        private string _supportMailAddress;
//        private string _serviceMailAddress;
//        private string _serviceMailPassword;

//        public SMTPClient()
//        {
//            _smtpUrl = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("SMTPURL") : ConfigurationManager.AppSettings["SMTPURL"];
//            _supportMailAddress = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("SUPPORTMAILADDRESS") : ConfigurationManager.AppSettings["SUPPORTMAILADDRESS"];
//            _serviceMailAddress = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("SERVICEMAILADDRESS") : ConfigurationManager.AppSettings["SERVICEMAILADDRESS"];
//            _serviceMailPassword = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("SERVICEMAILPASSWORD").Decrypt() : ConfigurationManager.AppSettings["SERVICEMAILPASSWORD"].Decrypt();
//        }

//        public bool SendPaymentInformation(string serviceAccountPassword, string receipientAddress, string receipientName, string serviceId, string paymentLink)
//        {
//            string MailSubject = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MAILSUBJECT") : ConfigurationManager.AppSettings["MAILSUBJECT"];
//            string MailSender = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MAILSENDER") : ConfigurationManager.AppSettings["MAILSENDER"];
//            string FromPassword = serviceAccountPassword;
//            MailAddress FromAddress = new MailAddress(_serviceMailAddress, MailSender);
//            MailAddress ToAddress = new MailAddress(receipientAddress, receipientName);

//            SmtpClient Smtp = new SmtpClient
//            {
//                Host = _smtpUrl,
//                Port = 587,
//                EnableSsl = true,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                Credentials = new NetworkCredential(FromAddress.Address, FromPassword),
//                Timeout = 20000
//            };

//            var body = new StringBuilder();
//            body.Append(String.Format("Estimado Cliente,{0}", Environment.NewLine));
//            body.Append(String.Format("Lo contactamos a fin de comunicarle que la orden de pago {0} se encuentra lista para ser pagada. {1}", serviceId, Environment.NewLine));
//            body.Append(String.Format("Haga click en el link que se encuentra a continuación para pagar con tarjeta de crédito por medio de Mercado Pago:{0}{1}", Environment.NewLine, Environment.NewLine));
//            body.Append(String.Format("{0}{1}{2}", paymentLink, Environment.NewLine, Environment.NewLine));
//            body.Append(String.Format("Ante cualquier duda, nos encontramos a su disposición para asistirlo. Gracias.{0}{1}", Environment.NewLine, Environment.NewLine));
//            body.Append(String.Format("Atentamente,{0}Fast Service Gestión Online", Environment.NewLine));

//            try
//            {
//                using (var message = new MailMessage(FromAddress, ToAddress)
//                {
//                    Subject = string.Format(MailSubject, serviceId),
//                    Body = body.ToString()
//                })
//                {
//                    Smtp.Send(message);
//                }
//                return true;
//            }
//            catch (Exception ex)
//            {
//                this.SendFailureNotification(ex.Message, "SendPaymentInformation");
//                return false;
//            }
//        }

//        public void SendFailureNotification(string message, string method)
//        {
//            MailAddress FromAddress = new MailAddress(_serviceMailAddress, "MercadoPago Integration Service");
//            MailAddress ToAddress = new MailAddress(_supportMailAddress, "MercadoPago Integration Support");
//            SmtpClient Smtp = new SmtpClient
//            {
//                Host = _smtpUrl,
//                Port = 587,
//                EnableSsl = true,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                Credentials = new NetworkCredential(FromAddress.Address, _serviceMailPassword),
//                Timeout = 20000
//            };

//            var body = new StringBuilder();
//            body.Append(String.Format("Mercado Pago Integration Support,{0}", Environment.NewLine));
//            body.Append(String.Format("Se ha producido una falla en el sistema en el metodo {0}.{1}{2}", method, Environment.NewLine, Environment.NewLine));
//            body.Append(String.Format("Detalle de excepcion:{0}", message));

//            try
//            {
//                using (var notification = new MailMessage(FromAddress, ToAddress)
//                {
//                    Subject = "Mercado Pago Integration Failure",
//                    Body = body.ToString()
//                })
//                {
//                    Smtp.Send(notification);
//                }

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public void SendSuccessNotification(string p, string ip)
//        {
//            MailAddress FromAddress = new MailAddress(_serviceMailAddress, "MercadoPago Integration Service");
//            MailAddress ToAddress = new MailAddress(_supportMailAddress, "MercadoPago Integration Support");
//            SmtpClient Smtp = new SmtpClient
//            {
//                Host = _smtpUrl,
//                Port = 587,
//                EnableSsl = true,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                Credentials = new NetworkCredential(FromAddress.Address, _serviceMailPassword),
//                Timeout = 20000
//            };

//            var body = new StringBuilder();
//            body.Append(String.Format("Mercado Pago Integration Support,{0}", Environment.NewLine));
//            body.AppendLine();
//            body.Append(String.Format("Se ha generado un nuevo pago."));
//            body.AppendLine();
//            body.Append(p);
//            body.AppendLine();
//            body.Append(string.Format("Originator IP Address: {0}", ip));

//            try
//            {
//                using (var notification = new MailMessage(FromAddress, ToAddress)
//                {
//                    Subject = "Mercado Pago Integration New Payment",
//                    Body = body.ToString()
//                })
//                {
//                    Smtp.Send(notification);
//                }

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}