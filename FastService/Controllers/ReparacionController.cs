using FastService.Common;
using FastService.Models;
using FastService.Models.Orden;
using Microsoft.Reporting.WebForms;
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
    }
}