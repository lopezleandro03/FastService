using FastService.Common;
using FastService.Models;
using FastService.Models.Orden;
using Microsoft.Reporting.WebForms;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReparacionController : BaseController
    {
        private IIndexModel OrdenesModel
        {
            get
            {
                return (OrdenesIndexViewModel)System.Web.HttpContext.Current.Session["ORDENES"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["ORDENES"] = value;
            }
        }
        private bool IsMyOrdersMode
        {
            get
            {
                return (bool)System.Web.HttpContext.Current.Session["MODE"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["MODE"] = value;
            }
        }
        private FastServiceEntities _db { get; set; }
        private OrdenHelper _OrdenHelper { get; set; }

        public ReparacionController()
        {
            _db = new FastServiceEntities();
            _OrdenHelper = new OrdenHelper();
        }

        // GET: Reparacion
        public ActionResult Index()
        {
            var auxOrdenActiva = 0;
            if (OrdenesModel != null && OrdenesModel.OrdenActiva != null)
                auxOrdenActiva = OrdenesModel.OrdenActiva.NroOrden;

            InitializeViewBag();
            IsMyOrdersMode = false;

            OrdenesModel = new OrdenesIndexViewModel();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId);

            if (auxOrdenActiva != 0)
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.Where(x => x.NroOrden == auxOrdenActiva).FirstOrDefault();
            else
            { 
                OrdenesModel.OrdenActiva = new OrdenModel();
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.First();
            }

            OrdenesModel.IsTecnico = CurrentUserRoles.Exists(x => x.ToUpper() == AplicationRole.TECNICO);

            return PartialView(OrdenesModel);
        }

        public ActionResult MyIndex()
        {
            var auxOrdenActiva = 0;
            if (OrdenesModel != null && OrdenesModel.OrdenActiva != null)
                auxOrdenActiva = OrdenesModel.OrdenActiva.NroOrden;

            InitializeViewBag();
            IsMyOrdersMode = true;

            OrdenesModel = new OrdenesIndexViewModel();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId);

            if (auxOrdenActiva != 0)
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.Where(x => x.NroOrden == auxOrdenActiva).FirstOrDefault();
            else
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes?.First();

            OrdenesModel.IsTecnico = CurrentUserRoles.Exists(x => x.ToUpper() == AplicationRole.TECNICO);
            OrdenesModel.IsMyVIew = true;

            return PartialView("Index", OrdenesModel);
        }
        // GET: Reparacion
        public ActionResult Filter(string searchCriteria)
        {
            InitializeViewBag();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, searchCriteria, CurrentUserId);

            if (OrdenesModel.Ordenes.Any())
            {
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes?.First();
            }

            return PartialView("Index", OrdenesModel);
        }

        // GET: Reparacion/Details/5
        public ActionResult Details(int id)
        {
            var orden = OrdenesModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();
            InitializeViewBag();

            if (orden != null)
            {
                OrdenesModel.NuevaOrden = false;
                OrdenesModel.OrdenActiva = orden;
                return PartialView("OrdenResumen", OrdenesModel);
            }

            throw new Exception("Numero de orden no encontrada");

        }

        [HttpGet]
        public ActionResult Novedad(int tipo)
        {
            var model = new NovedadModel();
            model.ResponsableNombre = OrdenesModel.OrdenActiva.ResponsableNombre;
            model.TecnicoNombre = OrdenesModel.OrdenActiva.TecnicoNombre;
            model.Monto = 0;
            model.Material = 0;
            model.TipoNovedadId = tipo;

            InitializeViewBag();

            if (tipo == (int)NovedadTipo.PRESUPINFOR 
                || tipo == (int)NovedadTipo.ENTREGA 
                || tipo == (int)NovedadTipo.REPDOMICILIO 
                || tipo == (int)NovedadTipo.REPARADO)
            {
                return PartialView("NovedadConPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.ACEPTA 
                || tipo == (int)NovedadTipo.RECHAZA 
                || tipo == (int)NovedadTipo.NOTA 
                || tipo == (int)NovedadTipo.LLAMADO
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO)
            {
                return PartialView("NovedadSimple", model);
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return PartialView("NovedadReingreso", model);
            }
            else
            {
                return PartialView("NovedadConPresupuesto", model);
            }
        }

        [HttpPost]
        public ActionResult Novedad(NovedadModel model)
        {
            //cache active order
            var nroOrdenActiva = OrdenesModel.OrdenActiva.NroOrden;

            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadCommitted;
            opt.Timeout = TimeSpan.MaxValue;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                try
                {
                    _db.Novedad.Add(new Novedad()
                    {
                        reparacionId = OrdenesModel.OrdenActiva.NroOrden,
                        observacion = model.Observacion,
                        monto = model.Monto,
                        tipoNovedadId = model.TipoNovedadId,
                        UserId = CurrentUserId,
                        modificadoPor = CurrentUserId.ToString(),
                        modificadoEn = DateTime.Now
                    });

                    var orden = _db.Reparacion.Find(nroOrdenActiva);
                    orden.TecnicoAsignadoId = model.TecnicoId;
                    orden.ReparacionDetalle.Presupuesto = model.Monto == 0 ? orden.ReparacionDetalle.Presupuesto : model.Monto;

                    var estados = _db.EstadoReparacion.Select(x => x).ToList();

                    if (orden != null)
                    {
                        if (model.TipoNovedadId == (int)NovedadTipo.ACEPTA)
                        {
                            orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.AREPARAR).First().EstadoReparacionId;
                        }
                        if (model.TipoNovedadId == (int)NovedadTipo.RECHAZA)
                        {
                            orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RECHAZADO).First().EstadoReparacionId;
                        }
                        if (model.TipoNovedadId == (int)NovedadTipo.REINGRESO)
                        {
                            orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.REINGRESADO).First().EstadoReparacionId;
                        }
                        if (model.TipoNovedadId == (int)NovedadTipo.PRESUPINFOR)
                        {
                            orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.PRESUPUESTADO).First().EstadoReparacionId;
                        }
                        if (model.TipoNovedadId == (int)NovedadTipo.RETIRA || model.TipoNovedadId == (int)NovedadTipo.ENTREGA)
                        {
                            orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RETIRADO).First().EstadoReparacionId;
                            orden.ReparacionDetalle.Precio = model.Monto;

                            var venta = new Venta()
                            {
                                ClienteId = orden.ClienteId,
                                Descripcion = $"Pago por servicio de FastService orden {orden.ReparacionId}",
                                Fecha = DateTime.Now,
                                Monto = (decimal)orden.ReparacionDetalle.Precio,
                                RefNumber = model.NroReferencia,
                                PuntoDeVentaId = 1,
                                FacturaId = null,
                                MetodoPagoId = 1,
                                TipoTransaccionId = model.Facturado ? 1 : 2,
                                Vendedor = orden.EmpleadoAsignadoId
                            };

                            _db.Venta.Add(venta);
                        }
                    }

                    _db.SaveChanges();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }

            InitializeViewBag();

            OrdenesModel = new OrdenesIndexViewModel()
            {
                Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId),
                OrdenActiva = OrdenesModel.Ordenes.Where(x => x.NroOrden == nroOrdenActiva).FirstOrDefault()
            };

            return RedirectToAction("Index");
        }

        // POST: Reparacion/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                OrdenesModel.OrdenActiva = new OrdenModel();
                OrdenesModel.NuevaOrden = true;
                OrdenesModel.OrdenActiva.EstadoFecha = DateTime.Now;
                OrdenesModel.OrdenActiva.NroOrden = OrdenesModel.Ordenes.Select(x => x.NroOrden).Max() + 1;
                OrdenesModel.OrdenActiva.EstadoDesc = ReparacionEstado.NUEVA;

                InitializeViewBag();

                return PartialView("OrdenResumen", OrdenesModel);
            }
            catch
            {
                return View();
            }
        }

        // POST: Reparacion/Create
        [HttpPost]
        public ActionResult Save(OrdenesIndexViewModel model)
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadCommitted;
            opt.Timeout = TimeSpan.MaxValue;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                try
                {
                    var cli = _db.Cliente.Where(x => x.Dni == model.OrdenActiva.Cliente.Dni).FirstOrDefault();
                    if (cli == null)
                    {
                        cli = new Cliente()
                        {
                            Dni = model.OrdenActiva.Cliente.Dni,
                            Nombre = model.OrdenActiva.Cliente.Nombre,
                            Apellido = model.OrdenActiva.Cliente.Apellido,
                            Telefono1 = model.OrdenActiva.Cliente.Telefono,
                            Telefono2 = model.OrdenActiva.Cliente.Celular,
                            Mail = model.OrdenActiva.Cliente.Mail,
                            Direccion = model.OrdenActiva.Cliente.Direccion,
                            Direccion1 = new Direccion()
                            {
                                Calle = model.OrdenActiva.Cliente.Dir.Calle,
                                Altura = model.OrdenActiva.Cliente.Dir.Altura,
                                Ciudad = model.OrdenActiva.Cliente.Dir.Ciudad,
                                CodigoPostal = model.OrdenActiva.Cliente.Dir.CodigoPostal,
                                Provincia = model.OrdenActiva.Cliente.Dir.Provincia,
                                Pais = model.OrdenActiva.Cliente.Dir.Pais,
                                Latitud = model.OrdenActiva.Cliente.Dir.Latitud,
                                Longitud = model.OrdenActiva.Cliente.Dir.Longitud,
                                ChangedBy = CurrentUserId,
                                ChangedOn = DateTime.Now
                            }
                        };

                        _db.Cliente.Add(cli);
                    }
                    else
                    {
                        cli.Dni = model.OrdenActiva.Cliente.Dni;
                        cli.Nombre = model.OrdenActiva.Cliente.Nombre;
                        cli.Apellido = model.OrdenActiva.Cliente.Apellido;
                        cli.Telefono1 = model.OrdenActiva.Cliente.Telefono;
                        cli.Telefono2 = model.OrdenActiva.Cliente.Celular;
                        cli.Mail = model.OrdenActiva.Cliente.Mail;
                        cli.Direccion = model.OrdenActiva.Cliente.Direccion;

                        if (cli.Direccion1 == null)
                            cli.Direccion1 = new Direccion();

                        cli.Direccion1.Calle = model.OrdenActiva.Cliente.Dir.Calle;
                        cli.Direccion1.Altura = model.OrdenActiva.Cliente.Dir.Altura;
                        cli.Direccion1.Ciudad = model.OrdenActiva.Cliente.Dir.Ciudad;
                        cli.Direccion1.CodigoPostal = model.OrdenActiva.Cliente.Dir.CodigoPostal;
                        cli.Direccion1.Provincia = model.OrdenActiva.Cliente.Dir.Provincia;
                        cli.Direccion1.Pais = model.OrdenActiva.Cliente.Dir.Pais;
                        cli.Direccion1.Latitud = model.OrdenActiva.Cliente.Dir.Latitud;
                        cli.Direccion1.Longitud = model.OrdenActiva.Cliente.Dir.Longitud;
                        cli.Direccion1.ChangedBy = CurrentUserId;
                        cli.Direccion1.ChangedOn = DateTime.Now;
                    }

                    var rep = _db.Reparacion.Find(model.OrdenActiva.NroOrden);
                    if (rep == null)
                    {
                        var repDet = new ReparacionDetalle()
                        {
                            EsGarantia = model.OrdenActiva.Garantia,
                            EsDomicilio = model.OrdenActiva.Domicilio,
                            NroReferencia = string.Empty,

                            Presupuesto = Convert.ToDecimal(model.OrdenActiva.Presupuesto),
                            PresupuestoFecha = DateTime.Now,
                            Precio = model.OrdenActiva.Monto,

                            Modelo = model.OrdenActiva.Modelo,
                            Serie = model.OrdenActiva.NroSerie,
                            Serbus = model.OrdenActiva.SerBus, //To be corrected

                            ModificadoEn = DateTime.Now,
                            ModificadoPor = CurrentUserId.ToString()
                        };

                        _db.ReparacionDetalle.Add(repDet);

                        _db.SaveChanges();

                        var estados = _db.EstadoReparacion.Select(x => x);

                        _db.Reparacion.Add(new Reparacion()
                        {
                            ReparacionId = model.OrdenActiva.NroOrden,
                            ReparacionDetalleId = repDet.ReparacionDetalleId,
                            ClienteId = cli.ClienteId,
                            EmpleadoAsignadoId = model.OrdenActiva.ResponsableId,
                            TecnicoAsignadoId = model.OrdenActiva.TecnicoId,
                            EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.INGRESADO).First().EstadoReparacionId,
                            ComercioId = model.OrdenActiva.Garantia ? (int?)model.OrdenActiva.Comercio.ComercioId : 1,
                            MarcaId = model.OrdenActiva.MarcaId,
                            TipoDispositivoId = model.OrdenActiva.TipoId,
                            ModificadoEn = DateTime.Now,
                            ModificadoPor = CurrentUserId.ToString(),
                        });
                    }
                    else
                    {
                        rep.ClienteId = cli.ClienteId;
                        rep.EmpleadoAsignadoId = model.OrdenActiva.ResponsableId == 0 ? rep.EmpleadoAsignadoId : model.OrdenActiva.ResponsableId;
                        rep.TecnicoAsignadoId = model.OrdenActiva.TecnicoId == 0 ? rep.TecnicoAsignadoId : model.OrdenActiva.TecnicoId;
                        rep.EstadoReparacionId = model.OrdenActiva.EstadoCodigo == 0 ? rep.EstadoReparacionId : model.OrdenActiva.EstadoCodigo;
                        rep.ComercioId = model.OrdenActiva.Garantia ? (model.OrdenActiva.Comercio.ComercioId == 0 ? rep.ComercioId : model.OrdenActiva.Comercio.ComercioId) : null;
                        rep.MarcaId = model.OrdenActiva.MarcaId == 0 ? rep.MarcaId : model.OrdenActiva.MarcaId; ;
                        rep.TipoDispositivoId = model.OrdenActiva.TipoId == 0 ? rep.TipoDispositivoId : model.OrdenActiva.TipoId; ;

                        rep.ModificadoEn = DateTime.Now;
                        rep.ModificadoPor = CurrentUserId.ToString();

                        var repDet = _db.ReparacionDetalle.Find(rep.ReparacionDetalleId);

                        if (repDet != null)
                        {
                            repDet.EsGarantia = model.OrdenActiva.Garantia;
                            repDet.EsDomicilio = model.OrdenActiva.Domicilio;
                            repDet.NroReferencia = string.Empty;
                            repDet.Presupuesto = Convert.ToDecimal(model.OrdenActiva.Presupuesto);
                            repDet.PresupuestoFecha = DateTime.Now;
                            repDet.Precio = model.OrdenActiva.Monto;
                            repDet.Modelo = model.OrdenActiva.Modelo;
                            repDet.Serie = model.OrdenActiva.NroSerie;
                            repDet.Serbus = model.OrdenActiva.SerBus; //To be corrected
                            repDet.ModificadoEn = DateTime.Now;
                            repDet.ModificadoPor = CurrentUserId.ToString();
                        }
                    }

                    _db.SaveChanges();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult ImprimirRecibo()
        {
            string reportName = "Recibo.pdf";

            //string reportFilePath = "C:/Users/Leandro/source/repos/FastService/FastService.Reports/bin/DebugRecibo.rdl";
            string reportFilePath = "~/Reports/Recibo.rdl";
            var reportType = ReportType.PDF;
            var contentType = string.Format("application/{0}", reportType.ToString().ToLower());

            List<ReportDataSource> dataSources = new List<ReportDataSource>();

            dataSources.Add(new ReportDataSource("Reparacion", _OrdenHelper.GetReparacionReciboData(OrdenesModel.OrdenActiva.NroOrden)));
            var report = new ReportHelper();
            var result = report.RenderReport(Server.MapPath(reportFilePath), dataSources, null, reportType);
            Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", reportName));

            return File(result, contentType);
        }



        private void InitializeViewBag()
        {
            ViewBag.ListaMarcas = (from y in _db.Marca
                                   select new SelectListItem()
                                   {
                                       Text = y.nombre,
                                       Value = y.MarcaId.ToString()
                                   }).ToList();

            ViewBag.ListaComercio = (from y in _db.Comercio
                                     select new SelectListItem()
                                     {
                                         Text = y.Code,
                                         Value = y.ComercioId.ToString()
                                     }).ToList();

            ViewBag.ListaTipoDispositivo = (from y in _db.TipoDispositivo
                                            select new SelectListItem()
                                            {
                                                Text = y.nombre,
                                                Value = y.TipoDispositivoId.ToString()
                                            }).ToList();


            ViewBag.ListaTecnicos = (from y in _db.Usuario
                                     select new SelectListItem()
                                     {
                                         Text = y.Nombre,
                                         Value = y.UserId.ToString()
                                     }).ToList();

            ViewBag.ListaResponsables = (from y in _db.Usuario
                                         select new SelectListItem()
                                         {
                                             Text = y.Nombre,
                                             Value = y.UserId.ToString()
                                         }).ToList();

        }
    }
}
