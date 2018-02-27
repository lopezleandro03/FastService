using FastService.Models;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ContabilidadController : BaseController
    {
        public ActionResult ResumenVentas()
        {
            var model = new VentaSummary();

            return PartialView(model);
        }

        public ActionResult VentaChart(char period)
        {
            var model = new VentaSummary(period);

            return PartialView("Chart", model);
        }

        public ActionResult Balance()
        {
            var model = new BalanceSummary();

            return PartialView(model);
        }

        public ActionResult BalanceChart(char period)
        {
            var model = new BalanceSummary(period);

            return PartialView("Chart", model);
        }

        // GET: Contabilidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contabilidad/Create
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

        // GET: Contabilidad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contabilidad/Edit/5
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

        // GET: Contabilidad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contabilidad/Delete/5
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
