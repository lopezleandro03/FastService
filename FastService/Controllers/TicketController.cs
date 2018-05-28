using FastService.Common;
using FastService.Models;
using Microsoft.Reporting.WebForms;
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
            var model = new OrdenHelper().GetOrden(id);

            return PartialView(model);
        }

        public ActionResult Create()
        {
            var model = new OrdenModel();
            model.EstadoFecha = DateTime.Now;
            model.NroOrden = new OrdenHelper().GetNextOrderNro();
            model.EstadoDesc = ReparacionEstado.NUEVA;
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
            return PartialView("Details", helper.GetOrden(model.NroOrden));
        }

        public ActionResult ImprimirRecibo(int id)
        {
            var ticket = new OrdenHelper().GetReparacionReciboData(id);

            if (ticket.Any())
            {
                string reportName = "Recibo.pdf";
                string reportFilePath = "~/Reports/Recibo.rdl";
                var reportType = ReportType.PDF;
                var contentType = string.Format("application/{0}", reportType.ToString().ToLower());

                List<ReportDataSource> dataSources = new List<ReportDataSource>();

                dataSources.Add(new ReportDataSource("Reparacion", ticket));
                var report = new ReportHelper();
                var reportParameters = new List<ReportParameter>();

                var param = new ReportParameter("ticket", id.ToString());
                reportParameters.Add(param);

                var param2 = new ReportParameter("cliente", ticket.First().Nombre + ticket.First().Apellido);
                reportParameters.Add(param2);

                var param3 = new ReportParameter("fecha", ticket.First().ModificadoEn.ToShortDateString());
                reportParameters.Add(param3);

                var result = report.RenderReport(Server.MapPath(reportFilePath), dataSources, reportParameters, reportType);
                Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", reportName));

                return File(result, contentType);
            }
            else
            {
                return View("Error");
            }
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
