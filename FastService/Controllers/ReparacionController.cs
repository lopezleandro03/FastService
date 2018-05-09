using FastService.Common;
using FastService.Models;
using FastService.Models.Orden;
using Microsoft.Reporting.WebForms;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Caching;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReparacionController : BaseController
    {
        private IIndexModel GetOrdenes()
        {
            return IsMyOrdersMode ? MyOrdenesModel : OrdenesModel;
        }

        private IIndexModel OrdenesModel
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ORDENES"] != null)
                {
                    return (OrdenesIndexViewModel)System.Web.HttpContext.Current.Session["ORDENES"];
                }
                else
                {
                    var temp = new OrdenesIndexViewModel();
                    temp.Ordenes = _OrdenHelper.GetOrdenes(false, null, CurrentUserId);
                    OrdenesModel = temp;
                    return temp;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["ORDENES"] = value;
            }
        }

        private IIndexModel MyOrdenesModel
        {
            get
            {

                if (System.Web.HttpContext.Current.Session["MISORDENES"] != null)
                {
                    return (OrdenesIndexViewModel)System.Web.HttpContext.Current.Session["MISORDENES"];
                }
                else
                {
                    var temp = new OrdenesIndexViewModel();
                    temp.Ordenes = _OrdenHelper.GetOrdenes(true, null, CurrentUserId);
                    MyOrdenesModel = temp;
                    return temp;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["MISORDENES"] = value;
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
            IsMyOrdersMode = false; //by default load as full mode
        }

        // GET: Reparacion
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index(int id = 0)
        {
            IsMyOrdersMode = false;
            InitializeViewBag();

            //if (id != 0)
            //    OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();
            //else
            //{
            //    OrdenesModel.OrdenActiva = new OrdenModel();
            //    OrdenesModel.OrdenActiva = OrdenesModel.Ordenes.OrderByDescending(x => x.FechaUltimaNovedad).First();
            //}

            OrdenesModel.IsTecnico = CurrentUserRoles.Exists(x => x.ToUpper() == AplicationRole.TECNICO);
            OrdenesModel.IsMyVIew = false;
            ViewBag.Seguimiento = "false";

            return PartialView(OrdenesModel);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult MyIndex(int id = 0)
        {
            IsMyOrdersMode = true;
            InitializeViewBag();

            //if (id != 0)
            //    MyOrdenesModel.OrdenActiva = MyOrdenesModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();
            //else
            //    MyOrdenesModel.OrdenActiva = MyOrdenesModel.Ordenes.OrderByDescending(x => x.FechaUltimaNovedad).First();

            MyOrdenesModel.IsTecnico = CurrentUserRoles.Exists(x => x.ToUpper() == AplicationRole.TECNICO);
            MyOrdenesModel.IsMyVIew = true;
            ViewBag.Seguimiento = "false";

            return PartialView("MyIndex", MyOrdenesModel);
        }
        // GET: Reparacion
        public ActionResult Filter(string searchCriteria)
        {
            InitializeViewBag();
            OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, searchCriteria, CurrentUserId);
            ViewBag.Seguimiento = "false";

            if (OrdenesModel.Ordenes.Any())
            {
                OrdenesModel.OrdenActiva = OrdenesModel.Ordenes?.OrderByDescending(x => x.FechaUltimaNovedad).First();
            }

            if (IsMyOrdersMode)
            {
                return PartialView("MyIndex", OrdenesModel);
            }

            return PartialView("Index", OrdenesModel);
        }

        public ActionResult FilterByTecnico(int id)
        {
            InitializeViewBag();
            MyOrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, id);
            ViewBag.Seguimiento = "false";

            if (MyOrdenesModel.Ordenes.Any())
            {
                MyOrdenesModel.OrdenActiva = MyOrdenesModel.Ordenes?.First();
            }

            return PartialView("MyIndex", MyOrdenesModel);
        }

        //public ActionResult FilterByComercio(int id)
        //{
        //    InitializeViewBag();
        //    OrdenesModel.Ordenes = _OrdenHelper.GetOrdenes(false, id.ToString(), CurrentUserId);

        //    if (OrdenesModel.Ordenes.Any())
        //    {
        //        OrdenesModel.OrdenActiva = OrdenesModel.Ordenes?.First();
        //    }

        //    return PartialView("Index", OrdenesModel);
        //}
        // GET: Reparacion/Details/5

        public ActionResult Details(int id)
        {
            if (IsMyOrdersMode)
            {
                var orden = MyOrdenesModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();

                InitializeViewBag();

                if (orden != null)
                {
                    MyOrdenesModel.NuevaOrden = false;
                    MyOrdenesModel.OrdenActiva = orden;
                    return PartialView("OrdenResumen", MyOrdenesModel);
                }

            }
            else
            {
                var orden = OrdenesModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();

                InitializeViewBag();

                if (orden != null)
                {
                    OrdenesModel.NuevaOrden = false;
                    OrdenesModel.OrdenActiva = orden;
                    return PartialView("OrdenResumen", OrdenesModel);
                }
            }

            throw new Exception("Numero de orden no encontrada");
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
            _OrdenHelper.Save(model.OrdenActiva, CurrentUserId);

            if (IsMyOrdersMode)
            {
                MyOrdenesModel.NuevaOrden = false;
                MyOrdenesModel.Sync();
                return RedirectToAction("MyIndex", new { id = model.OrdenActiva.NroOrden });
            }
            else
            {
                OrdenesModel.NuevaOrden = false;
                OrdenesModel.Sync();
                return RedirectToAction("Index", new { id = model.OrdenActiva.NroOrden });
            }
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

            dataSources.Add(new ReportDataSource("Reparacion", _OrdenHelper.GetReparacionReciboData(GetOrdenes().OrdenActiva.NroOrden)));
            var report = new ReportHelper();
            var reportParameters = new List<ReportParameter>();

            var param = new ReportParameter("ticket", GetOrdenes().OrdenActiva.NroOrden.ToString());
            reportParameters.Add(param);

            var param2 = new ReportParameter("cliente", GetOrdenes().OrdenActiva.Cliente.PrettyName);
            reportParameters.Add(param2);

            var param3 = new ReportParameter("fecha", GetOrdenes().OrdenActiva.EstadoFecha.ToShortDateString());
            reportParameters.Add(param3);

            var result = report.RenderReport(Server.MapPath(reportFilePath), dataSources, reportParameters, reportType);
            Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", reportName));

            return File(result, contentType);
        }

        [HttpGet]
        public ActionResult Novedad(int tipo, int nroOrden)
        {
            var model = new NovedadModel();
            //var ordenActiva = GetOrdenes().OrdenActiva;
            var ordenActiva = _OrdenHelper.GetOrden(nroOrden);

            model.ResponsableId = ordenActiva.ResponsableId;
            model.ResponsableNombre = ordenActiva.ResponsableNombre;
            model.TecnicoId = ordenActiva.TecnicoId;
            model.TecnicoNombre = ordenActiva.TecnicoNombre;
            model.Monto = ordenActiva.Presupuesto;
            model.TipoNovedadId = tipo;
            //model.Material = 0;

            InitializeViewBag();

            if (tipo == (int)NovedadTipo.REPDOMICILIO
                || tipo == (int)NovedadTipo.PRESUPUESTADO
                || tipo == (int)NovedadTipo.REPARADO)
            {
                return PartialView("~/Views/Novedad/NovedadPresupuesto.cshtml", model);
            }
            else if (tipo == (int)NovedadTipo.PRESUPINFOR
                || tipo == (int)NovedadTipo.LLAMADO)
            {
                return PartialView("~/Views/Novedad/NovedadInformarPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.NOTA
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO)
            {
                return PartialView("~/Views/Novedad/NovedadSimple", model);
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return PartialView("~/Views/Novedad/NovedadReingreso", model);
            }
            else if (tipo == (int)NovedadTipo.ENTREGA)
            {
                return PartialView("~/Views/Novedad/NovedadCoordinarEntrega", model);
            }
            else if (tipo == (int)NovedadTipo.RETIRA)
            {
                ViewBag.ListaMetodoDePago = (from y in _db.MetodoPago
                                             select new SelectListItem()
                                             {
                                                 Text = y.Nombre,
                                                 Value = y.MetodoPagoId.ToString()
                                             }).ToList();

                return PartialView("NovedadRetiro", model);
            }
            else
            {
                return PartialView("~/Views/Novedad/NovedadSimple", model);
            }
        }

        [HttpPost]
        public ActionResult Novedad(NovedadModel model)
        {
            var ordenActiva = GetOrdenes().OrdenActiva.NroOrden;

            _OrdenHelper.Save(model, CurrentUserId);

            if (IsMyOrdersMode)
            {
                MyOrdenesModel.Sync(ordenActiva);
                MyOrdenesModel.NuevaOrden = false;
                return RedirectToAction("MyIndex", new { id = ordenActiva });
            }
            else
            {
                OrdenesModel.Sync(ordenActiva);
                OrdenesModel.NuevaOrden = false;
                return RedirectToAction("Index", new { id = ordenActiva });
            }
        }

        public ActionResult NovedadDelete(int id)
        {
            var novedad = _db.Novedad.Find(id);
            _db.Novedad.Remove(novedad);
            _db.SaveChanges();

            if (IsMyOrdersMode)
            {
                MyOrdenesModel.Sync();
                return PartialView("NovedadIndex", MyOrdenesModel);
            }
            else
            {
                OrdenesModel.Sync();
                return PartialView("NovedadIndex", OrdenesModel);
            }

        }

        public ActionResult NovedadEdit(int id)
        {
            var model = GetOrdenes().OrdenActiva.Novedades.Where(x => x.Id == id).FirstOrDefault();
            var tipo = model.TipoNovedadId;
            InitializeViewBag();

            if (tipo == (int)NovedadTipo.PRESUPINFOR
            || tipo == (int)NovedadTipo.ENTREGA
            || tipo == (int)NovedadTipo.REPDOMICILIO
            || tipo == (int)NovedadTipo.REPARADO)
            {
                return PartialView("~/Views/Novedad/NovedadPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.ACEPTA
                || tipo == (int)NovedadTipo.RECHAZA
                || tipo == (int)NovedadTipo.NOTA
                || tipo == (int)NovedadTipo.LLAMADO
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO)
            {
                return PartialView("~/Views/Novedad/NovedadSimple", model);
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return PartialView("~/Views/Novedad/NovedadReingreso", model);
            }
            else
            {
                return PartialView("~/Views/Novedad/NovedadPresupuesto", model);
            }

        }
    }
}