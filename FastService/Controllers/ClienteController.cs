﻿using FastService.Models;
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

            if (cliente.Direccion1 != null)
            {
                model.Calle = cliente.Direccion1.Calle;
                model.Altura = cliente.Direccion1.Altura;
                model.Calle2 = cliente.Direccion1.Calle2;
                model.Calle3 = cliente.Direccion1.Calle3;
                model.CodigoPostal = cliente.Direccion1.CodigoPostal;
                model.Provincia = cliente.Direccion1.Provincia;
                model.Pais = cliente.Direccion1.Pais;
                model.Latitud = cliente.Direccion1.Latitud;
                model.Longitud = cliente.Direccion1.Longitud;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            var result = new List<ClienteModel>();

            Prefix = Prefix.Contains("  ") ? Prefix.Replace("  ", " ") : Prefix;

            if (Prefix.Trim().Contains(" "))
            {

                var apellido = Prefix.Split(' ')[0];
                var nombre = Prefix.Split(' ')[1];

                result = (from x in _db.Cliente
                          where x.Nombre.Contains(nombre)
                             && x.Apellido.Contains(apellido)
                          select new ClienteModel() { Dni = x.Dni, Nombre = x.Nombre, Apellido = x.Apellido }).OrderBy(x => x.Nombre).Take(25).ToList();
            }
            else
            {
                result = (from x in _db.Cliente
                          where x.Dni.ToString().Contains(Prefix)
                             || x.Nombre.Contains(Prefix)
                             || x.Apellido.Contains(Prefix)
                          select new ClienteModel() { Dni = x.Dni, Nombre = x.Nombre, Apellido = x.Apellido }).OrderBy(x => x.Nombre).Take(25).ToList();
            }

            var clienteList = (from N in result
                               select new { N.DisplayName, N.Dni }
                                 ).ToList();

            return Json(clienteList, JsonRequestBehavior.AllowGet);
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
