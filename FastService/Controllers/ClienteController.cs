using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ClienteController : BaseController
    {
        public FastServiceEntities _db { get; set; }
        // GET: Cliente

        public ClienteController()
        {
            this._db = new FastServiceEntities();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Get(string ClienteId)
        {
            var cliente = _db.Cliente.Where(x => x.Dni.ToString() == ClienteId).FirstOrDefault();

            var model = new ClienteModel()
            {
                Dni = cliente.Dni,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono1,
                Celular = cliente.Telefono2,
                Direccion = cliente.Direccion,
                Mail = cliente.Mail
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            //Note : you can bind same list from database  
            var ObjList = (from x in _db.Cliente
                           where x.Dni.ToString().Contains(Prefix)
                              || x.Nombre.Contains(Prefix)
                              || x.Apellido.Contains(Prefix)
                              || x.Mail.Contains(Prefix)
                           select new ClienteModel() { Dni = x.Dni, Nombre = x.Nombre, Apellido = x.Apellido }).Take(20).ToList();

            //Searching records from list using LINQ query  
            var NombreCliente = (from N in ObjList
                                 select new { N.DisplayName, N.Dni }
                                 ).ToList();

            return Json(NombreCliente, JsonRequestBehavior.AllowGet);
        }

        // POST: Cliente/Create
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

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
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

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cliente/Delete/5
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
