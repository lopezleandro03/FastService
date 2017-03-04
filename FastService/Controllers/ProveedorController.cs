using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ProveedorController : Controller
    {
        public FastServiceEntities _db { get; set; }
        // GET: Cliente

        public ProveedorController()
        {
            this._db = new FastServiceEntities();
        }
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Get(string ClienteId)
        {
            var Proveedor = _db.Proveedor.Find(Convert.ToInt32(ClienteId));
            var model = new ProveedorModel()
            {
                Id = Proveedor.ToString(),
                Nombre = Proveedor.Nombre,
                Telefono = Proveedor.Telefono1,
                Celular = Proveedor.Telefono2,
                Direccion = Proveedor.Direccion,
                Mail = Proveedor.Mail
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            //Note : you can bind same list from database  
            var ObjList = (from x in _db.Proveedor select new ProveedorModel() { Id = x.ProveedorId.ToString(), Nombre = x.Nombre }).ToList();

            //Searching records from list using LINQ query  
            var Nombre = (from N in ObjList
                                 where N.Nombre.Contains(Prefix) || N.Nombre.StartsWith(Prefix)
                                 select new { N.DisplayName, N.Id }
                                 ).ToList();

            return Json(Nombre, JsonRequestBehavior.AllowGet);
        }


        // POST: Proveedor/Create
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

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proveedor/Edit/5
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

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedor/Delete/5
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
