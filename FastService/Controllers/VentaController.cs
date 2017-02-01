
using Model.Model;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace FastService.Controllers
{
    public class VentaController : Controller
    {
        private FastServiceEntities _dbContext { get; set; }



        public VentaController()
        {
            _dbContext = new FastServiceEntities();
        } 

        // GET: Venta
        public ActionResult Index()
        {
            return View();
        }

        // GET: Venta/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.PuntoDeVentaList = (from y in _dbContext.PuntoDeVenta
                                        select new SelectListItem()
                                        {
                                            Text = y.Nombre,
                                            Value = y.PuntoDeVentaId.ToString()
                                        }).ToList();
            return View();
        }

        // POST: Venta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var clienteId = Convert.ToInt32(collection.Get("ClienteId"));
                var cliente = _dbContext.Cliente.Find(clienteId);

                if (cliente != null)
                {
                    cliente.Nombre = collection.Get("NombreCliente");
                    cliente.Apellido = collection.Get("ApellidoCliente");
                    cliente.Mail = collection.Get("MailCliente");
                    cliente.Direccion = collection.Get("Direccion") ?? null;
                    cliente.Telefono1 = collection.Get("Telefono") ?? null;
                    cliente.Telefono2 = collection.Get("Celular") ?? null;
                }
                else
                {
                    Cliente nuevoCliente = new Cliente()
                    {
                        ClienteId = clienteId,
                        Nombre = collection.Get("NombreCliente"),
                        Apellido = collection.Get("ApellidoCliente"),
                        Mail = collection.Get("MailCliente"),
                        Direccion = collection.Get("Direccion") ?? null,
                        Telefono1 = collection.Get("Telefono") ?? null,
                        Telefono2 = collection.Get("Celular") ?? null
                    };

                    _dbContext.Cliente.Add(nuevoCliente);
                }

                Venta model = new Venta()
                {
                    FacturaId = null,
                    ClienteId = Convert.ToInt32(collection.Get("ClienteId")),
                    Monto = Convert.ToDecimal(collection.Get("Monto")),
                    PuntoDeVentaId = Convert.ToInt16(collection.Get("Origen")),
                    Fecha = DateTime.Now,
                    Vendedor = "llopez7"
                };

                _dbContext.Venta.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venta/Edit/5
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

        // GET: Venta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venta/Delete/5
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
