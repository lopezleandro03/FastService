using FastService.Common;
using FastService.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            InitializeViewBag();
            ViewBag.Seguimiento = "true"; //this is a fucking workaround
            var model = new OrdenHelper().GetOrden(id);

            return PartialView(model);
        }

        public ActionResult Create()
        {
            var model = new OrdenModel();
            model.EstadoFecha = DateTime.Now;
            model.EstadoDesc = ReparacionEstado.NUEVA;
            InitializeViewBag();

            return PartialView("Details", model);
        }

        public ActionResult CreateForClient(int id)
        {
            var model = new OrdenModel();
            model.EstadoFecha = DateTime.Now;
            model.EstadoDesc = ReparacionEstado.NUEVA;
            model.Cliente = new ClienteHelper().GetClient(id);
            InitializeViewBag();

            return PartialView("Details", model);
        }

        // POST: Ticket/Save
        [HttpPost]
        public ActionResult Save(OrdenModel model)
        {
            var helper = new OrdenHelper();
            helper.Save(model, CurrentUserId);
            InitializeViewBag();
            ViewBag.Seguimiento = "true"; //this is a fucking workaround

            return RedirectToAction("Details", "Ticket", new { id = model.NroOrden });
            //return PartialView("Details", helper.GetOrden(model.NroOrden));
        }

        [HttpPost]
        public JsonResult Get(string Prefix)
        {
            var objList = new OrdenHelper().GetOrdersNro(Prefix);

            var matches = (from o in objList
                           select new { id = o.NroOrden, display = o.NroOrden + "-" + o.Nombre + " " + o.Apellido }
                                 ).OrderByDescending(x => x.id).ToList();

            return Json(matches, JsonRequestBehavior.AllowGet);
        }
    }
}
