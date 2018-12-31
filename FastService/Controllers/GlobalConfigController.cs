using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model;
using FastService.Models.GlobalConfig;
using FastService.Common;

namespace FastService.Controllers
{
    public class GlobalConfigController : Controller
    {
        public FastServiceEntities _db { get; set; }
        public GlobalConfigHelper helper { get; set; }

        // GET: GlobalConfig
        public ActionResult Index()
        {
            helper = new GlobalConfigHelper();

            var model = new GlobalConfigModel()
            {
                NotificacionesFechaInicioAno = Convert.ToInt32(helper.GetVal("NotificacionesFechaInicioAno")),
                NotificacionesFechaInicioMes = Convert.ToInt32(helper.GetVal("NotificacionesFechaInicioMes")),
                NotificacionesMinimoDiasInactividad = Convert.ToInt32(helper.GetVal("NotificacionesMinimoDiasInactividad")),
            };                

            return PartialView(model);
        }

        // GET: GlobalConfig/Details/5
        [HttpPost]
        public ActionResult Save(GlobalConfigModel model)
        {
            helper = new GlobalConfigHelper();

            helper.SetVal("NotificacionesFechaInicioAno", model.NotificacionesFechaInicioAno.ToString());
            helper.SetVal("NotificacionesFechaInicioMes", model.NotificacionesFechaInicioMes.ToString());
            helper.SetVal("NotificacionesMinimoDiasInactividad", model.NotificacionesMinimoDiasInactividad.ToString());

            return null;
        }

        // GET: GlobalConfig/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GlobalConfig/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GlobalConfig/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GlobalConfig/Edit/5
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

        // GET: GlobalConfig/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GlobalConfig/Delete/5
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
