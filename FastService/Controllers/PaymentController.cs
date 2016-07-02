using Backend;
using FastService.Models;
using mercadopago;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Payment/Create
        public ActionResult Create()
        {
            return View("PaymentCreate");
        }

        //
        // POST: /Payment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            _mpClient = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MPCLIENT").Decrypt() : ConfigurationManager.AppSettings["MPCLIENT"].Decrypt();
            _mpSecret = CommonUtility.IsDevelopmentServer() ? CommonUtility.GetConfigVal("MPSECRET").Decrypt() : ConfigurationManager.AppSettings["MPSECRET"].Decrypt();

            try
            {
                PaymentModel model = new PaymentModel();
                model.customerId = collection.Get("customerId");
                model.customerName = collection.Get("customerName");
                model.paymentNetAmount = collection.Get("paymentNetAmount");
                model.serviceId = collection.Get("serviceId");
                model.customerMail = collection.Get("customerMail");

                String paymentTitle = string.Format(PAYMENTTITLE, model.serviceId, model.customerId, model.customerName);

                MP mp = new MP(_mpClient, _mpSecret);
                String preferenceData = "{\"items\":" +
                                            "[{" +
                                                "\"title\":\"" + paymentTitle + "\"," +
                                                "\"quantity\":1," +
                                                "\"currency_id\":\"" + PAYMENTCURRENCY + "\"," +
                                                "\"unit_price\":" + model.paymentNetAmount + "" +
                                            "}]" +
                                        "}";
                Hashtable preference = mp.createPreference(preferenceData);
                int responseCode = (int)preference["status"];

                if (responseCode == 201)
                {
                    Hashtable response = (Hashtable)preference["response"];
                    String paymentLink = _testMode ? (String)response["sandbox_init_point"] : (String)response["init_point"];

                    model.paymentLink = paymentLink;

                    ViewBag.customerId = model.customerId;
                    ViewBag.customerName = model.customerName;
                    ViewBag.customerMail = model.customerMail;
                    ViewBag.serviceId = model.serviceId;
                    ViewBag.paymentNetAmount = model.paymentNetAmount;
                    ViewBag.PaymentLink = model.paymentLink;
                    ViewBag.PayLabel = PAYLABEL;

                    return View("PaymentConfirmation", model);
                }
                else
                {
                    throw new Exception("MercadoPago Exception");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Mail(string serviceAccountPassword, string customerId, string customerName, string paymentNetAmount, string serviceId, string paymentLink, string customerMail)
        {
            try
            {
                SMTPClient smtp = new SMTPClient(serviceAccountPassword, customerMail, customerName);
                smtp.SendPaymentInformation(serviceId, paymentLink);
            }
            catch (Exception)
            {
                var result = new { Success = "False", Message = "Error Message" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var resultOK = new { Success = "True" };
            return Json(resultOK, JsonRequestBehavior.AllowGet); ;
        }

        //
        // GET: /Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private string _mpSecret;
        private string _mpClient;
        private readonly bool _testMode = CommonUtility.IsDevelopmentServer() ? true : false;

        private readonly string PAYLABEL = "PAGAR";
        private readonly string PAYMENTTITLE = "Orden_{0}_{1}_{2}";
        private readonly string PAYMENTCURRENCY = "ARS";

    }
}
