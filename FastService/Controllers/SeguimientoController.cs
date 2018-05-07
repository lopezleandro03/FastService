using FastService.Common;
using FastService.Models.Seguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class SeguimientoController : Controller
    {
        // GET: Seguimiento
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult KanbanColumn(int id)
        {

            var model = new ListaOrdenesModel();

            switch (id)
            {
                case 1:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.INGRESADO);
                    ViewBag.Title = ReparacionEstado.INGRESADO.ToString();
                    break;
                case 2:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.PRESUPUESTADO);
                    ViewBag.Title = ReparacionEstado.PRESUPUESTADO.ToString();
                    break;
                case 3:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.ESPREPUESTO);
                    ViewBag.Title = ReparacionEstado.ESPREPUESTO.ToString();
                    break;
                case 4:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.AREPARAR);
                    ViewBag.Title = ReparacionEstado.AREPARAR.ToString();
                    break;
                case 5:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.REPARADO);
                    ViewBag.Title = ReparacionEstado.REPARADO.ToString();
                    break;
                case 6:
                    model.Ordenes = new OrdenHelper().GetOrdenesByEstado(ReparacionEstado.PARAENTREGAR);
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
