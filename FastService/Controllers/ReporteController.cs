using FastService.Common;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Recibo(int id)
        {
            var ticket = new OrdenHelper().GetReparacionReciboData(id);

            if (ticket != null)
            {
                return View(ticket);
            }
            else
            {
                return View("Error");
            }
        }

        //public ActionResult Dorso(int id)
        //{
        //    var ticket = new OrdenHelper().GetReparacionReciboData(id);

        //    if (ticket != null)
        //    {
        //        return View(ticket);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}


        //public ActionResult HojaDeRuta(int id)
        //{
        //    var ticket = new OrdenHelper().GetReparacionReciboData(id);

        //    if (ticket != null)
        //    {
        //        return View(ticket);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
    }
}