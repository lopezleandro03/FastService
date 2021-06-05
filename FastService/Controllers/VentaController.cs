
using Model.Model;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using FastService.Models;
using FastService.Helpers;
using FastService.Models.MetodosDePago;

namespace FastService.Controllers
{
    public class VentaController : BaseController
    {
        private FastServiceEntities _dbContext { get; set; }

        public VentaController()
        {
            _dbContext = new FastServiceEntities();
        }

        // GET: Venta
        public ActionResult Index()
        {
            return PartialView((from x in _dbContext.Venta
                                select new VentaModel()
                                {
                                    Cliente = new ClienteModel()
                                    {
                                        Apellido = x.Cliente.Apellido,
                                        Nombre = x.Cliente.Nombre,
                                        Celular = x.Cliente.Telefono1,
                                        Mail = x.Cliente.Mail,
                                        Direccion = x.Cliente.Direccion,
                                        Dni = x.Cliente.ClienteId,
                                        Telefono = x.Cliente.Telefono2
                                    },
                                    VentaId = x.VentaId,
                                    Monto = x.Monto,
                                    Origen = x.PuntoDeVenta.PuntoDeVentaId,
                                    OrigenNombre = x.PuntoDeVenta.Nombre,
                                    Vendedor = x.Vendedor,
                                    Facturado = x.Facturado,
                                    Descripcion = x.Descripcion,
                                    MetodoDePago = (from y in _dbContext.MetodoPago where y.MetodoPagoId == x.MetodoPagoId select y.Nombre).FirstOrDefault(),
                                    NroFactura = x.Factura.NroFactura,
                                    TipoDeFactura = x.Factura.TipoFactura.Nombre,
                                    Fecha = x.Fecha
                                }).OrderByDescending(x => x.Fecha).Take(1000).OrderByDescending(x => x.Fecha));
        }

        // GET: Venta/Create
        public ActionResult Create(string id)
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

            ViewBag.ListaTipoFactura = (from y in new FastServiceEntities().TipoFactura
                                        select new SelectListItem()
                                        {
                                            Text = y.Nombre,
                                            Value = y.TipoFacturaId.ToString()
                                        }).ToList();

            ViewBag.NroCuotasList = (new List<SelectListItem>() {
                new SelectListItem() { Selected = true, Text = "1", Value = "1"},
                new SelectListItem() { Selected = false, Text = "2", Value = "2"},
                new SelectListItem() { Selected = false, Text = "3", Value = "3"},
                new SelectListItem() { Selected = false, Text = "4", Value = "4"},
                new SelectListItem() { Selected = false, Text = "5", Value = "5"},
                new SelectListItem() { Selected = false, Text = "6", Value = "6"}
                                    }).ToList();

            var model = new VentaModel();
            model.Producto = (from x in _dbContext.Producto
                              where x.id.ToString() == id
                              select new ProductoModel() {
                                  Nombre = x.nombre,
                                  Descripcion = x.descripcion,
                                  Costo = x.costo,
                                  Categoria = x.categoria,
                                  Categoria2 = x.categoria2,
                                  Categoria3 = x.categoria3,
                                  Utilidad = x.utilidad
                              }).FirstOrDefault();
            
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult GetMetodoDePago(int id, int cuotas)
        {
            var metodo = (from x in _dbContext.MetodoPago where x.MetodoPagoId == id select x.Nombre.ToUpper()).FirstOrDefault();
            var model = new Object();
            switch (metodo)
            {
                case "CHEQUE":
                    model = new PagoConChequeViewModel()
                    {
                        NroCuotas = cuotas,
                        Cheques = new List<ChequeModel>()
                    };

                    for (int i = 0; i < cuotas; i++)
                        ((PagoConChequeViewModel)model).Cheques.Add(new ChequeModel());
                    break;

                case "TRANSFERENCIA":
                    model = new PagoConTransferenciaViewModel();

                    break;

                case "EFECTIVO":
                    model = new Models.MetodosDePago.PagoConEfectivoViewModel();

                    break;

                default:
                    break;
            }



            return PartialView(string.Format("MetodoPago{0}", metodo), model);
        }

        // POST: Venta/Create
        [HttpPost]
        public ActionResult Create(VentaModel venta)
        {
            var cliente = _dbContext.Cliente.FirstOrDefault(x => x.Dni == venta.Cliente.Dni);

            if (cliente != null)
            {
                cliente.Dni = venta.Cliente.Dni;
                cliente.Nombre = venta.Cliente.Nombre;
                cliente.Apellido = venta.Cliente.Apellido;
                cliente.Mail = venta.Cliente.Mail;
                cliente.Direccion = venta.Cliente.Direccion;
                cliente.Telefono1 = venta.Cliente.Telefono;
                cliente.Telefono2 = venta.Cliente.Celular;
            }
            else
            {
                cliente = new Cliente()
                {
                    Dni = venta.Cliente.Dni,
                    Nombre = venta.Cliente.Nombre,
                    Apellido = venta.Cliente.Apellido,
                    Mail = venta.Cliente.Mail,
                    Direccion = venta.Cliente.Direccion,
                    Telefono1 = venta.Cliente.Telefono,
                    Telefono2 = venta.Cliente.Celular
                };

                _dbContext.Cliente.Add(cliente);
                _dbContext.SaveChanges();
            }

            int? fac = null;

            if (venta.Facturado)
            {
                var factura = new Factura()
                {
                    NroFactura = venta.NroFactura,
                    TipoFacturaId = venta.TipoDeFacturaId,
                    ModificadoEn = DateTime.Now,
                    ModificadoPor = CurrentUserId
                };

                _dbContext.Factura.Add(factura);
                _dbContext.SaveChanges();

                fac = factura.FacturaId;
            }

            Venta model = new Venta()
            {
                FacturaId = fac,
                RefNumber = venta.NroFactura, //cual seria la referencia?
                Descripcion = venta.Descripcion,
                PuntoDeVentaId = venta.Origen,
                Fecha = venta.Fecha,
                Vendedor = CurrentUserId,
                Facturado = venta.Facturado,
                TipoTransaccionId = venta.Facturado ? 1 : 2,
                MetodoPagoId = venta.MetodoDePagoId,
                Monto = venta.Monto,
                ClienteId = cliente.ClienteId
            };

            _dbContext.Venta.Add(model);
            _dbContext.SaveChanges();

            //var result = new { Success = "true", Message = "Completado!" };
            //return Json(result, JsonRequestBehavior.AllowGet);

            return RedirectToAction("Create");
        }

        public ActionResult Edit(VentaModel venta)
        {
            var cliente = _dbContext.Cliente.FirstOrDefault(x => x.Dni == venta.Cliente.Dni);

            if (cliente != null)
            {
                cliente.Dni = venta.Cliente.Dni;
                cliente.Nombre = venta.Cliente.Nombre;
                cliente.Apellido = venta.Cliente.Apellido;
                cliente.Mail = venta.Cliente.Mail;
                cliente.Direccion = venta.Cliente.Direccion;
                cliente.Telefono1 = venta.Cliente.Telefono;
                cliente.Telefono2 = venta.Cliente.Celular;
            }
            else
            {
                cliente = new Cliente()
                {
                    Dni = venta.Cliente.Dni,
                    Nombre = venta.Cliente.Nombre,
                    Apellido = venta.Cliente.Apellido,
                    Mail = venta.Cliente.Mail,
                    Direccion = venta.Cliente.Direccion,
                    Telefono1 = venta.Cliente.Telefono,
                    Telefono2 = venta.Cliente.Celular
                };

                _dbContext.Cliente.Add(cliente);
                _dbContext.SaveChanges();
            }

            int? fac = null;

            if (venta.Facturado)
            {
                var factura = new Factura()
                {
                    NroFactura = venta.NroFactura,
                    TipoFacturaId = venta.TipoDeFacturaId,
                    ModificadoEn = DateTime.Now,
                    ModificadoPor = CurrentUserId
                };

                _dbContext.Factura.Add(factura);
                _dbContext.SaveChanges();

                fac = factura.FacturaId;
            }

            Venta model = new Venta()
            {
                FacturaId = fac,
                RefNumber = venta.NroFactura, //cual seria la referencia?
                Descripcion = venta.Descripcion,
                PuntoDeVentaId = venta.Origen,
                Fecha = venta.Fecha,
                Vendedor = CurrentUserId,
                Facturado = venta.Facturado,
                TipoTransaccionId = venta.Facturado ? 1 : 2,
                MetodoPagoId = venta.MetodoDePagoId,
                Monto = venta.Monto,
                ClienteId = cliente.ClienteId
            };

            _dbContext.Venta.Add(model);
            _dbContext.SaveChanges();

            //var result = new { Success = "true", Message = "Completado!" };
            //return Json(result, JsonRequestBehavior.AllowGet);

            return RedirectToAction("Create");
        }

        // GET: Venta
        public JsonResult Get()
        {
            return Json((from x in _dbContext.Venta
                         select new VentaModel()
                         {
                             Cliente = new ClienteModel()
                             {
                                 Apellido = x.Cliente.Apellido,
                                 Nombre = x.Cliente.Nombre,
                                 Celular = x.Cliente.Telefono1,
                                 Mail = x.Cliente.Mail,
                                 Direccion = x.Cliente.Direccion,
                                 Dni = x.Cliente.ClienteId,
                                 Telefono = x.Cliente.Telefono2
                             },
                             VentaId = x.VentaId,
                             Monto = x.Monto,
                             Origen = x.PuntoDeVenta.PuntoDeVentaId,
                             OrigenNombre = x.PuntoDeVenta.Nombre,
                             Vendedor = x.Vendedor,
                             Facturado = x.Facturado,
                             Descripcion = x.Descripcion,
                             MetodoDePago = (from y in _dbContext.MetodoPago where y.MetodoPagoId == x.MetodoPagoId select y.Nombre).FirstOrDefault(),
                             NroFactura = x.Factura.NroFactura,
                             TipoDeFactura = x.Factura.TipoFactura.Nombre,
                             Fecha = x.Fecha
                         }).OrderByDescending(x => x.VentaId).Take(500).OrderByDescending(x=>x.VentaId), JsonRequestBehavior.AllowGet);
        }

        // GET: Venta
        [HttpPost]
        public void Delete(int VentaId)
        {
            var _db = new FastServiceEntities();
            var item = _db.Venta.Where( x=> x.VentaId == VentaId).FirstOrDefault();
            _db.Venta.Remove(item);
            _db.SaveChanges();
        }
    }
}
