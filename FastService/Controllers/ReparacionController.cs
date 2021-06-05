using FastService.Common;
using FastService.Models;
using FastService.Models.Orden;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Caching;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReparacionController : BaseController
    {
        private bool IsMyOrdersMode
        {
            get
            {
                return (bool)System.Web.HttpContext.Current.Session["MODE"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["MODE"] = value;
            }
        }

        private OrdenListFilterBar ReportFilter
        {
            get
            {
                return (OrdenListFilterBar)System.Web.HttpContext.Current.Session["ReportFilter"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["ReportFilter"] = value;
            }
        }

        private FastServiceEntities _db { get; set; }
        private OrdenHelper _OrdenHelper { get; set; }

        public ReparacionController()
        {
            _db = new FastServiceEntities();
            _OrdenHelper = new OrdenHelper();
        }

        // GET: Reparacion
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index(int id = 0)
        {
            InitializeViewBag();
            ViewBag.Seguimiento = "false";
            IsMyOrdersMode = false;

            var model = new OrdenesIndexViewModel()
            {
                IsMyVIew = false,
                Ordenes = _OrdenHelper.GetOrdenes(false, null, CurrentUserId)
            };

            return PartialView(model);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult MyIndex(int id = 0)
        {
            InitializeViewBag();
            ViewBag.Seguimiento = "false";
            IsMyOrdersMode = true;

            var model = new OrdenesIndexViewModel()
            {
                IsMyVIew = true,
                Ordenes = _OrdenHelper.GetOrdenes(true, null, CurrentUserId)
            };

            return PartialView(model);
        }

        // GET: Reparacion
        public ActionResult Filter(string searchCriteria)
        {
            var model = new OrdenesIndexViewModel()
            {
                IsMyVIew = IsMyOrdersMode,
                Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, searchCriteria, CurrentUserId)
            };

            if (IsMyOrdersMode)
            {
                return PartialView("MyIndex", model);
            }
            else
            {
                return PartialView("Index", model);
            }
        }

        public ActionResult FilterByTecnico(int id)
        {
            var model = new OrdenesIndexViewModel()
            {
                IsMyVIew = IsMyOrdersMode,
                Ordenes = _OrdenHelper.GetOrdenes(IsMyOrdersMode, null, id)
            };

            if (IsMyOrdersMode)
            {
                return PartialView("MyIndex", model);
            }
            else
            {
                return PartialView("Index", model);
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            var mdict = new Dictionary<int, string>();
            var ydict = new Dictionary<int, int>();
            var helper = new DateHelper();

            for (int i = 1; i < 12 + 1; i++)
            {
                mdict.Add(i, helper.GetMonthName(i));
            }

            var mList = (from x in mdict select new { id = x.Key, value = x.Value }).ToList();

            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 20; i--)
            {
                ydict.Add(i, i);
            }

            var yList = (from x in ydict select new { id = x.Key, value = x.Value }).ToList();

            var eList = (from x in _db.EstadoReparacion where x.activo == true select new { id = x.EstadoReparacionId, value = x.nombre.ToUpper() }).ToList().OrderBy(y => y.value);

            var filterModel = new OrdenListFilterBar()
            {
                MonthList = new SelectList(mList, "id", "value"),
                YearList = new SelectList(yList, "id", "value"),
                EstadosList = new SelectList(eList, "id", "value"),
                MinInactiveDays = null
            };

            return PartialView(filterModel);
        }

        [HttpPost]
        public ActionResult FilterList(OrdenListFilterBar filter)
        {
            this.ReportFilter = filter;
            return Json(new OrdenHelper().GetOrders(filter));
        }

    }
}