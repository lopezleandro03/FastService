using FastService.Common;
using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReparacionController : BaseController
    {
        private OrdenesIndexViewModel OrdenesModel
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
            InitializeViewBag();
            IsMyOrdersMode = false;

            OrdenesModel = new OrdenesIndexViewModel();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId);
            OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.First();

            return PartialView(OrdenesModel);
        }

        public ActionResult MyIndex()
        {
            InitializeViewBag();
            IsMyOrdersMode = true;

            OrdenesModel = new OrdenesIndexViewModel();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId);
            OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.First();


            return PartialView("Index", OrdenesModel);
        }
        // GET: Reparacion
        public ActionResult Filter(string searchCriteria)
        {
            var model = new OrdenesIndexViewModel();
            InitializeViewBag();

            OrdenesModel = new OrdenesIndexViewModel();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId);
            OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.First();


            return PartialView("index", OrdenesModel);
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

            if (tipo == 1)
            {
                return PartialView("NovedadConPresupuesto", model);
            }
            else if (tipo == 2)
            {
                return PartialView("NovedadSimple", model);
            }
            else if (tipo == 3)
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
            var nroOrdenActiva = OrdenesModel.OrdenActiva.NroOrden;

            _db.Novedad.Add(new Novedad()
            {
                reparacionId = OrdenesModel.OrdenActiva.NroOrden,
                observacion = model.Observacion,
                monto = model.Monto,
                tipoNovedadId = 1,
                modificadoPor = CurrentUserId.ToString(),
                modificadoEn = DateTime.Now
            });

            _db.SaveChanges();

            InitializeViewBag();

            OrdenesModel = new OrdenesIndexViewModel()
            {
                Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, CurrentUserId),
                OrdenActiva = OrdenesModel.Ordenes.Where(x => x.NroOrden == nroOrdenActiva).FirstOrDefault()
            };

            return PartialView("OrdenResumen", OrdenesModel);
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
                OrdenesModel.OrdenActiva.EstadoDesc = "NUEVA";

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

                    _db.Cliente.Add(cli);

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
                            Precio = null,

                            Modelo = model.OrdenActiva.Modelo,
                            Serie = model.OrdenActiva.NroSerie,
                            Serbus = model.OrdenActiva.SerBus, //To be corrected

                            ModificadoEn = DateTime.Now,
                            ModificadoPor = CurrentUserId.ToString()
                        };

                        _db.ReparacionDetalle.Add(repDet);

                        _db.SaveChanges();

                        _db.Reparacion.Add(new Reparacion()
                        {
                            ReparacionId = model.OrdenActiva.NroOrden,
                            ReparacionDetalleId = repDet.ReparacionDetalleId,
                            ClienteId = cli.ClienteId,
                            EmpleadoAsignadoId = model.OrdenActiva.ResponsableId,
                            TecnicoAsignadoId = model.OrdenActiva.TecnicoId,
                            EstadoReparacionId = 1,
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
                            repDet.Precio = null;
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
