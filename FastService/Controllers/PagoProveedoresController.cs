using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class PagoProveedoresController : Controller
    {
        private FastServiceEntities _dbContext;

        public PagoProveedoresController()
        {
            _dbContext = new FastServiceEntities();
        }
        // GET: PagoProveedores
        public ActionResult Index()
        {
            ViewBag.MetodoDePagoList = (from y in _dbContext.MetodoPago
                                        select new SelectListItem()
                                        {
                                            Text = y.Nombre,
                                            Value = y.MetodoPagoId.ToString()
                                        }).ToList();

            ViewBag.NroCuotasList = (new List<SelectListItem>() {
                new SelectListItem() { Selected = true, Text = "1", Value = "1"},
                new SelectListItem() { Selected = false, Text = "2", Value = "2"},
                new SelectListItem() { Selected = false, Text = "3", Value = "3"},
                new SelectListItem() { Selected = false, Text = "4", Value = "4"},
                new SelectListItem() { Selected = false, Text = "5", Value = "5"},
                new SelectListItem() { Selected = false, Text = "6", Value = "6"}
                                    }).ToList();

            return View("CreatePagoWizard");
        }

        // GET: PagoProveedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PagoProveedores/Create
        public ActionResult Create()
        {
            try
            {

                ViewBag.MetodoDePagoList = (from y in _dbContext.MetodoPago
                                            select new SelectListItem()
                                            {
                                                Text = y.Nombre,
                                                Value = y.MetodoPagoId.ToString()
                                            }).ToList();

                ViewBag.NroCuotasList = (new List<SelectListItem>() {
                new SelectListItem() { Selected = true, Text = "1", Value = "1"},
                new SelectListItem() { Selected = false, Text = "2", Value = "2"},
                new SelectListItem() { Selected = false, Text = "3", Value = "3"},
                new SelectListItem() { Selected = false, Text = "4", Value = "4"},
                new SelectListItem() { Selected = false, Text = "5", Value = "5"},
                new SelectListItem() { Selected = false, Text = "6", Value = "6"}
                                    }).ToList();

                var model = new PagoWizardModel()
                {
                    MetodoDePago = "TEST",
                    Proveedores = (from x in _dbContext.Proveedor select x).ToList()
                };

                return PartialView("CreatePagoWizard", model);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult Post(PagoModel pago)
        {
            try
            {
                foreach (var cuota in pago.MetodoPago.Cuotas)
                {
                    var month = Convert.ToInt16(cuota.FechaDebito.Split('/')[0].TrimStart('0'));
                    var day = Convert.ToInt16(cuota.FechaDebito.Split('/')[1].TrimStart('0'));
                    var year = Convert.ToInt16(cuota.FechaDebito.Split('/')[2].TrimStart('0'));

                    var newPago = new Pago()
                    {
                        CompraId = Convert.ToInt32(pago.Compra),
                        FechaDebito = new DateTime(year, month, day),
                        FechaEmision = DateTime.Now,
                        Monto = cuota.Monto,
                        NroReferencia = cuota.ChequeNro.ToString()
                    };

                    _dbContext.Pago.Add(newPago);
                }

                _dbContext.SaveChanges();

                var resultOK = new { Success = "true" };
                return Json(resultOK, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var resultOK = new { Success = "false" };
                return Json(resultOK, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: PagoProveedores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PagoProveedores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PagoProveedores/Edit/5
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

        // GET: PagoProveedores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PagoProveedores/Delete/5
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
    }
}
