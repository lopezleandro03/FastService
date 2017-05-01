
using Model.Model;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using FastService.Models;

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
            return PartialView(from x in _dbContext.Venta
                               select new VentaModel()
                               {
                                   Cliente = new ClienteModel()
                                   {
                                       Apellido = x.Cliente.Apellido,
                                       Nombre = x.Cliente.Nombre,
                                       Celular = x.Cliente.Telefono1,
                                       Mail = x.Cliente.Mail,
                                       Direccion = x.Cliente.Direccion,
                                       Id = x.Cliente.ClienteId,
                                       Telefono = x.Cliente.Telefono2
                                   },
                                   VentaId = x.VentaId,
                                   Monto = x.Monto,
                                   Origen = x.PuntoDeVenta.PuntoDeVentaId,
                                   Vendedor = x.Vendedor
                               });
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

        [HttpGet]
        public ActionResult GetMetodoDePago(int id, int cuotas)
        {
            var metodo = (from x in _dbContext.MetodoPago where x.MetodoPagoId == id select x.Nombre).FirstOrDefault();
            var model = new PagoConChequeModel()
            {
                NroCuotas = cuotas,
                Cheques = new List<ChequeModel>()
            };

            for (int i = 0; i < cuotas; i++)
                model.Cheques.Add(new ChequeModel());



            return PartialView(string.Format("MetodoPago{0}", metodo), model);
        }

        // POST: Venta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (!String.IsNullOrEmpty(collection.Get("Cliente.Id")) && (!String.IsNullOrWhiteSpace(collection.Get("Cliente.Id"))))
                {
                    var clienteId = Convert.ToInt32(collection.Get("Cliente.Id"));
                    var cliente = _dbContext.Cliente.Find(clienteId);

                    if (cliente != null)
                    {
                        cliente.Nombre = collection.Get("Cliente.Nombre");
                        cliente.Apellido = collection.Get("Cliente.Apellido");
                        cliente.Mail = collection.Get("Cliente.MailCliente");
                        cliente.Direccion = collection.Get("Cliente.Direccion") ?? null;
                        cliente.Telefono1 = collection.Get("Cliente.Telefono") ?? null;
                        cliente.Telefono2 = collection.Get("Cliente.Celular") ?? null;
                    }
                    else
                    {
                        Cliente nuevoCliente = new Cliente()
                        {
                            ClienteId = clienteId,
                            Nombre = collection.Get("Cliente.Nombre"),
                            Apellido = collection.Get("Cliente.Apellido"),
                            Mail = collection.Get("Cliente.Mail"),
                            Direccion = collection.Get("Cliente.Direccion") ?? null,
                            Telefono1 = collection.Get("Cliente.Telefono") ?? null,
                            Telefono2 = collection.Get("Cliente.Celular") ?? null
                        };

                        _dbContext.Cliente.Add(nuevoCliente);
                    }

                    Venta model = new Venta()
                    {
                        FacturaId = null,
                        ClienteId = Convert.ToInt32(collection.Get("Cliente.Id")),
                        Monto = Convert.ToDecimal(collection.Get("Monto")),
                        PuntoDeVentaId = Convert.ToInt16(collection.Get("Origen")),
                        Fecha = DateTime.Now,
                        Vendedor = System.Web.HttpContext.Current.Session["USER"].ToString(),
                        MetodoPagoId = 1
                    };

                    _dbContext.Venta.Add(model);
                    _dbContext.SaveChanges();
                }
                else
                {
                    Venta model = new Venta()
                    {
                        FacturaId = null,
                        Monto = Convert.ToDecimal(collection.Get("Monto")),
                        PuntoDeVentaId = Convert.ToInt16(collection.Get("Origen")),
                        Fecha = DateTime.Now,
                        Vendedor = System.Web.HttpContext.Current.Session["USER"].ToString(),
                        MetodoPagoId = 1,
                        ClienteId = Convert.ToInt32(collection.Get("Cliente.Id"))
                    };

                    _dbContext.Venta.Add(model);
                    _dbContext.SaveChanges();
                }

                var result = new { Success = "true", Message = "Completado!" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Success = "false", Message = "Error!" };
                return Json(result, JsonRequestBehavior.AllowGet);
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
