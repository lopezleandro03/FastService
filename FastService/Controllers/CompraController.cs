using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class CompraController : Controller
    {

        private FastServiceEntities _dbContext { get; set; }

        public CompraController()
        {
            _dbContext = new FastServiceEntities();
        }
        // GET: Compra
        public ActionResult Index()
        {
            return PartialView();
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
            return PartialView();
        }

        // POST: Compra/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var proveedorId = Convert.ToInt32(collection.Get("ProveedorId"));
                var proveedor = _dbContext.Proveedor.Find(proveedorId);

                if (proveedor != null)
                {
                    proveedor.Nombre = collection.Get("ProveedorNombre");
                    proveedor.Mail = collection.Get("Mail");
                    proveedor.Direccion = collection.Get("Direccion") ?? null;
                    proveedor.Telefono1 = collection.Get("Telefono") ?? null;
                    proveedor.Telefono2 = collection.Get("Celular") ?? null;
                }
                else
                {
                    Proveedor nuevoCliente = new Proveedor()
                    {
                        ProveedorId = proveedorId,
                        Nombre = collection.Get("ProveedorNombre"),
                        Mail = collection.Get("Mail"),
                        Direccion = collection.Get("Direccion") ?? null,
                        Telefono1 = collection.Get("Telefono") ?? null,
                        Telefono2 = collection.Get("Celular") ?? null
                    };

                    _dbContext.Proveedor.Add(nuevoCliente);
                }

                Compra model = new Compra()
                {
                    FacturaId = null,
                    ProveedorId = Convert.ToInt32(collection.Get("ProveedorId")),
                    Monto = Convert.ToDecimal(collection.Get("Monto")),
                    PuntoDeVentaId = Convert.ToInt16(collection.Get("Origen")),
                    Fecha = DateTime.Now,
                    Comprador = "llopez7"
                };

                _dbContext.Compra.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return PartialView();
            }
        }

        // GET: Compra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
