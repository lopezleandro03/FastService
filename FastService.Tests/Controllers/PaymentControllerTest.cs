using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend;

namespace FastService.Tests.Controllers
{
    [TestClass]
    public class PaymentControllerTest
    {
        [TestMethod]
        public void SendEmailWithPaymentLinkUsingGmail()
        {
            SMTPClient client = new SMTPClient("fastservice10","lopezleandro03@gmail.com","Leandro Lopez");
            client.SendPaymentInformation("2020","https://google.com");
        }
    }
}
