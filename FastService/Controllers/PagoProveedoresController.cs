using FastService.Helpers;
using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class PagoProveedoresController : BaseController
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
                MetodoDePago = "Def"//TODO:What the heck is that? 
            };

            var acreedores = (from p in _dbContext.vw_ProveedoresAcreedores
                              select new ProveedorModel
                              {
                                  RazonSocial = p.Nombre,
                                  Mail = p.Mail,
                                  CUIT = p.ProveedorId.ToString(),
                                  ComprasAPagar = (from c in _dbContext.vw_ComprasAPagar
                                                   where c.ProveedorId == p.ProveedorId
                                                   select new CompraAPagarModel
                                                   {
                                                       CompraId = c.CompraId,
                                                       Monto = c.Monto,
                                                       ProveedorId = c.ProveedorId,
                                                       Saldo = c.saldo,
                                                       FechaCreacion = c.FechaCreacion,
                                                       Descripcion = c.Descripcion
                                                   }).ToList()
                              }).ToList();

            model.Proveedores = acreedores;

            return PartialView("CreatePagoWizard", model);

        }

        [HttpPost]
        public JsonResult Post(PagoModel pago)
        {
            var metodo = (from x in _dbContext.MetodoPago where x.MetodoPagoId == pago.MetodoPago.Id select x.Nombre.ToUpper()).FirstOrDefault();

            switch (metodo)
            {
                case "CHEQUE":

                    foreach (var cuota in pago.MetodoPago.Cuotas)
                    {
                        var nuevoPagoCheque = new Pago()
                        {
                            CompraId = Convert.ToInt32(pago.Compra),
                            FechaDebito = DateHelper.ParseJSDate(cuota.FechaDebito),
                            FechaEmision = DateTime.Now,
                            Monto = Math.Round(cuota.Monto),
                            NroReferencia = cuota.RefNumber.ToString(),
                            CreadoPor = CurrentUserId,
                            FechaCreacion = DateTime.Now,
                            MetodoDePagoId = pago.MetodoPago.Id,
                            TipoTransaccionId = 1
                        };

                        _dbContext.Pago.Add(nuevoPagoCheque);
                    }

                    break;
                case "TRANSFERENCIA":
                    var nuevoPagoTransf = new Pago()
                    {
                        CompraId = Convert.ToInt32(pago.Compra),
                        FechaDebito = DateHelper.ParseJSDate(pago.MetodoPago.Cuotas.First().FechaDebito),
                        FechaEmision = DateTime.Now,
                        Monto = Math.Round(pago.MetodoPago.Cuotas.First().Monto),
                        NroReferencia = pago.MetodoPago.Cuotas.First().RefNumber.ToString(),
                        CreadoPor = CurrentUserId,
                        FechaCreacion = DateTime.Now,
                        MetodoDePagoId = pago.MetodoPago.Id,
                        TipoTransaccionId = 1
                    };
                    _dbContext.Pago.Add(nuevoPagoTransf);
                    break;
                case "EFECTIVO":
                    var nuevoPagoEfect = new Pago()
                    {
                        CompraId = Convert.ToInt32(pago.Compra),
                        FechaDebito = DateHelper.ParseJSDate(pago.MetodoPago.Cuotas.First().FechaDebito),
                        FechaEmision = DateTime.Now,
                        Monto = Math.Round(pago.MetodoPago.Cuotas.First().Monto),
                        NroReferencia = pago.MetodoPago.Cuotas.First().RefNumber.ToString(),
                        CreadoPor = CurrentUserId,
                        FechaCreacion = DateTime.Now,
                        MetodoDePagoId = pago.MetodoPago.Id,
                        TipoTransaccionId = pago.Facturado
                    };
                    _dbContext.Pago.Add(nuevoPagoEfect);

                    break;
                default:
                    break;
            }

            _dbContext.SaveChanges();

            var resultOK = new { Success = "true" };
            return Json(resultOK, JsonRequestBehavior.AllowGet);
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
