using FastService.Common;
using FastService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return PartialView(new OrdenHelper().GetOrden(id));
        }

        // POST: Ticket/Save
        [HttpPost]
        public ActionResult Save(OrdenModel model)
        {
            var helper = new OrdenHelper();
            helper.Save(model, CurrentUserId);
            InitializeViewBag();
            ViewBag.Seguimiento = "true"; //this is a fucking workaround
            return PartialView("Details", helper.GetOrden(model.NroOrden));
        }
    }
}
