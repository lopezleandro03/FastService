using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class CompraController : BaseController
    {

        private FastServiceEntities _dbContext { get; set; }

        public CompraController()
        {
            _dbContext = new FastServiceEntities();
        }
        // GET: Compra
        public ActionResult Index()
        {
            return PartialView((from x in _dbContext.Compra
                                select new CompraModel
                                {
                                    CompraId = x.CompraId,
                                    Descripcion = x.Descripcion,
                                    Fecha = x.FechaCreacion,
                                    Monto = x.Monto,
                                    Origen = x.PuntoDeVentaId,
                                    OrigenNombre = x.PuntoDeVenta.Nombre,
                                    Proveedor = new ProveedorModel
                                    {
                                        RazonSocial = x.Proveedor.Nombre
                                    }
                                }).ToList());
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            return PartialView();
        }

        // GET: Compra/Create
        public ActionResult Create()
        {
            ViewBag.PuntoDeVentaList = (from y in _dbContext.PuntoDeVenta
                                        select new SelectListItem()
                                        {
                                            Text = y.Nombre,
                                            Value = y.PuntoDeVentaId.ToString()
                                        }).ToList();

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

            return PartialView();
        }

        // POST: Compra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var proveedorId = collection.Get("Proveedor.CUIT");
            var proveedor = _dbContext.Proveedor.Find(proveedorId);

            if (proveedor != null)
            {
                proveedor.Nombre = collection.Get("Proveedor.RazonSocial");
                proveedor.Mail = collection.Get("Proveedor.Mail");
                proveedor.Direccion = collection.Get("Proveedor.Direccion") ?? null;
                proveedor.Telefono1 = collection.Get("Proveedor.Telefono1") ?? null;
                proveedor.Telefono2 = collection.Get("Proveedor.Telefono2") ?? null;
            }
            else
            {
                Proveedor nuevoCliente = new Proveedor()
                {
                    ProveedorId = proveedorId,
                    Nombre = collection.Get("Proveedor.RazonSocial"),
                    Mail = collection.Get("Proveedor.Mail"),
                    Direccion = collection.Get("Proveedor.Direccion") ?? null,
                    Telefono1 = collection.Get("Proveedor.Telefono1") ?? null,
                    Telefono2 = collection.Get("Proveedor.Telefono2") ?? null
                };

                _dbContext.Proveedor.Add(nuevoCliente);
            }

            Compra model = new Compra()
            {
                ProveedorId = collection.Get("Proveedor.CUIT"),
                Monto = Convert.ToDecimal(collection.Get("Monto")),
                Descripcion = collection.Get("Descripcion"),
                PuntoDeVentaId = Convert.ToInt16(collection.Get("Origen")),
                FechaCreacion = DateTime.Now,
                CreadoPor = CurrentUserId
            };

            _dbContext.Compra.Add(model);
            _dbContext.SaveChanges();

            var result = new { Success = "true", Message = "Completado!" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // POST: Compra/Edit/5
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

        // GET: Compra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Compra/Delete/5
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
