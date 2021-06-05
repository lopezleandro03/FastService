//using Backend;
//using FastService.Models;
//using mercadopago;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;

//namespace FastService.Controllers
//{
//    public class PaymentController : BaseController
//    {
//        //
//        // GET: /Payment/
//        public ActionResult Index()
//        {
//            return View();
//        }

//        //
//        // GET: /Payment/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        //
//        // GET: /Payment/Create
//        public ActionResult Create()
//        {
//            var acc1 = new SelectListItem() { Text = "luislopezfv@gmail.com", Value = "1", Selected = false };
//            var acc2 = new SelectListItem() { Text = "pagosfastservice@gmail.com", Value = "2", Selected = true };
//            ViewBag.MercadoPagoAccList = new List<SelectListItem>() { acc2, acc1 };
//            return PartialView("PaymentCreate");
//        }

//        //
//        // POST: /Payment/Create
//        [HttpPost]
//        public PartialViewResult Create(FormCollection collection)
//        {
//            try
//            {
//                var AccId = collection.Get("MercadoPagoAcc");
//                _mpClient = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MPCLIENT_" + AccId).Decrypt() : ConfigurationManager.AppSettings["MPCLIENT_" + AccId].Decrypt();
//                _mpSecret = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MPSECRET_" + AccId).Decrypt() : ConfigurationManager.AppSettings["MPSECRET_" + AccId].Decrypt();
//                _serviceMailAddress = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("SERVICEMAILADDRESS") : ConfigurationManager.AppSettings["SERVICEMAILADDRESS"];
//                string NotificationFlag = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("NOTIFICATIONSENABLED") : ConfigurationManager.AppSettings["NOTIFICATIONSENABLED"];
//                NOTIFICATIONSENABLED = NotificationFlag == "YES" ? true : false;

//                string _ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? "";
//                if (_ip == "" || _ip.ToLower() == "unknown")
//                    _ip = Request.ServerVariables["REMOTE_ADDR"] ?? "";

//                PaymentModel model = new PaymentModel();
//                model.customerId = collection.Get("customerId");
//                model.customerName = collection.Get("customerName");
//                model.paymentNetAmount = collection.Get("paymentNetAmount");
//                model.serviceId = collection.Get("serviceId");
//                model.customerMail = collection.Get("customerMail");

//                String paymentTitle = string.Format(PAYMENTTITLE, model.serviceId, model.customerId, model.customerName);

//                MP mp = new MP(_mpClient, _mpSecret);
//                String preferenceData = "{\"items\":" +
//                                            "[{" +
//                                                "\"title\":\"" + paymentTitle + "\"," +
//                                                "\"quantity\":1," +
//                                                "\"currency_id\":\"" + PAYMENTCURRENCY + "\"," +
//                                                "\"unit_price\":" + model.paymentNetAmount + "" +
//                                            "}]" +
//                                        "}";

//                Hashtable preference = mp.createPreference(preferenceData);
//                int responseCode = (int)preference["status"];

//                if (responseCode == 201)
//                {
//                    Hashtable response = (Hashtable)preference["response"];
//                    String paymentLink = _testMode ? (String)response["sandbox_init_point"] : (String)response["init_point"];

//                    model.paymentLink = paymentLink;

//                    ViewBag.customerId = model.customerId;
//                    ViewBag.customerName = model.customerName;
//                    ViewBag.customerMail = model.customerMail;
//                    ViewBag.serviceId = model.serviceId;
//                    ViewBag.paymentNetAmount = model.paymentNetAmount;
//                    ViewBag.PaymentLink = model.paymentLink;
//                    ViewBag.PayLabel = PAYLABEL;
//                    ViewBag.ServiceMailAddress = _serviceMailAddress;

//                    if (NOTIFICATIONSENABLED) new SMTPClient().SendSuccessNotification(model.ToString(), _ip);

//                    return PartialView("PaymentConfirmation", model);
//                }
//                else
//                {
//                    SMTPClient smtp = new SMTPClient();
//                    Hashtable response = (Hashtable)preference["response"];

//                    StringBuilder message = new StringBuilder(string.Format("MercadoPago Exception message: {0}", (String)response["message"]));
//                    message.AppendLine();
//                    message.Append(string.Format("Originator IP Address: {0}", _ip));
//                    message.AppendLine();
//                    message.Append(model.ToString());

//                    smtp.SendFailureNotification(message.ToString(), "CreatePayment");

//                    ViewBag.ExceptionMessage = message;
//                    return PartialView("Error");
//                }
//            }
//            catch (Exception ex)
//            {
//                SMTPClient smtp = new SMTPClient();
//                smtp.SendFailureNotification(ex.Message, "CreatePayment");

//                ViewBag.ExceptionMessage = ex.Message.ToString() + Environment.NewLine + ex.StackTrace;
//                return PartialView("Error");
//            }
//        }

//        [HttpGet]
//        public ActionResult Mail(string serviceAccountPassword, string customerId, string customerName, string paymentNetAmount, string serviceId, string paymentLink, string customerMail)
//        {
//            SMTPClient smtp = new SMTPClient();
//            if (!smtp.SendPaymentInformation(serviceAccountPassword, customerMail, customerName, serviceId, paymentLink))
//            {
//                var result = new { Success = "False", Message = "Error Message" };
//                return Json(result, JsonRequestBehavior.AllowGet);
//            }
//            else
//            {
//                var resultOK = new { Success = "True" };
//                return Json(resultOK, JsonRequestBehavior.AllowGet);
//            }
//        }

//        //
//        // GET: /Payment/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        //
//        // POST: /Payment/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        //
//        // GET: /Payment/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        //
//        // POST: /Payment/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        private string _mpSecret;
//        private string _mpClient;
//        private string _serviceMailAddress;
//        private readonly bool _testMode = CommonUtility.IsDevelopmentServer() ? true : false;

//        private readonly string PAYLABEL = "PAGAR";
//        private readonly string PAYMENTTITLE = "Orden_{0}_{1}_{2}";
//        private readonly string PAYMENTCURRENCY = "ARS";
//        private bool NOTIFICATIONSENABLED;

//    }
//}
