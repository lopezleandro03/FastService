using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model;

namespace FastService.Controllers
{
    public class StockController : BaseController
    {
        public FastServiceEntities _db { get; set; }

        public StockController() {
            InitializeViewBag();
            _db = new FastServiceEntities();
        }


        // GET: Stock
        public ActionResult Index()
        {

            return PartialView();
        }

        public JsonResult List(string nombre, string descripcion, string categoria)
        {
            var result = (from x in _db.Producto
                          where (x.nombre.Contains(nombre) || nombre == null || nombre.Trim() == string.Empty)
                          && (x.descripcion.Contains(descripcion) || descripcion == null || descripcion.Trim() == string.Empty)
                          && (x.categoria.Contains(categoria) || categoria == null || categoria.Trim() == string.Empty)
                          select x).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalle(string id)
        {
            var model = (from x in _db.Producto
                          where x.id.ToString() == id
                          select x).FirstOrDefault();

            return PartialView(model);

        }
    }
}