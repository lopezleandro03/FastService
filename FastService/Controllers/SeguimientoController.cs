using FastService.Common;
using FastService.Models.Seguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class SeguimientoController : BaseController
    {
        // GET: Seguimiento
        public ActionResult Index()
        {
            InitializeViewBag();
            return PartialView();
        }

        public ActionResult KanbanColumn(int id, DateTime? desde, DateTime? hasta, int? tecnicoid, int? comercioid, int? responsableid)
        {

            var model = new ListaOrdenesModel();

            switch (id)
            {
                case 1:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.INGRESADO, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.INGRESADO.ToString();
                    break;
                case 2:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.PRESUPUESTADO, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.PRESUPUESTADO.ToString();
                    break;
                case 3:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.ESPREPUESTO, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.ESPREPUESTO.ToString();
                    break;
                case 4:
                    var helper = new OrdenHelper();
                    model.Ordenes = helper.GetOrdenesByEstado(ReparacionEstado.AREPARAR, desde, hasta, tecnicoid, comercioid, responsableid);
                    model.Ordenes.AddRange(helper.GetOrdenesByEstado(ReparacionEstado.REINGRESADO, desde, hasta, tecnicoid, comercioid, responsableid));
                    ViewBag.Title = ReparacionEstado.AREPARAR.ToString();
                    break;
                case 5:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.REPARADO, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.REPARADO.ToString();
                    break;
                case 6:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.RECHAZADO, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.RECHAZADO.ToString();
                    break;
                case 7:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.PARAENTREGAR, desde, hasta, tecnicoid, comercioid, responsableid);
                    ViewBag.Title = ReparacionEstado.PARAENTREGAR.ToString();
                    break;
                default:
                    break;
            }

            ViewBag.ColumnId = $"column-{id}";
            return PartialView(model);
        }
    }
}
