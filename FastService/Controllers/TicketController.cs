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
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            InitializeViewBag();
            return PartialView(new OrdenHelper().GetOrden(id));
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Save(OrdenModel model)
        {
            var helper = new OrdenHelper();
            helper.Save(model, CurrentUserId);
            InitializeViewBag();
            return PartialView("Details", helper.GetOrden(model.NroOrden));
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
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

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket/Delete/5
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
