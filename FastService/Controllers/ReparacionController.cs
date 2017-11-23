using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ReparacionController : BaseController
    {
        private OrdenesIndexViewModel OrdenesIndexViewModel
        {
            get
            {
                return (OrdenesIndexViewModel)System.Web.HttpContext.Current.Session["ORDENES"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["ORDENES"] = value;
            }
        }

        private OrdenModel OrdenActiva
        {
            get
            {
                return (OrdenModel)System.Web.HttpContext.Current.Session["ORDENACTIVA"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["ORDENACTIVA"] = value;
            }
        }

        // GET: Reparacion
        public ActionResult Index()
        {
            var db = new FastServiceEntities();

            var model = new OrdenesIndexViewModel();

            model.Ordenes = (from r in db.Reparacion
                             select new OrdenModel()
                             {
                                 NroOrden = r.ReparacionId,
                                 Garantia = r.Garantia,
                                 EstadoCodigo = r.EstadoReparacionId,
                                 EstadoDesc = r.EstadoReparacion.nombre,
                                 EstadoFecha = r.ModificadoEn,

                                 ResponsableId = r.EmpleadoAsignadoId,
                                 ResponsableNombre = r.Usuario.Nombre,
                                 TecnicoId = r.EmpleadoAsignadoId,
                                 TecnicoNombre = r.Usuario1.Nombre,

                                 MarcaId = r.MarcaId,
                                 MarcaDesc = r.Marca.nombre,

                                 TipoId = r.TipoDispositivoId,
                                 TipoDesc = r.TipoDispositivo.nombre,
                                 Modelo = "Not Mapped",
                                 NroSerie = "Not Mapped",

                                 Cliente = new ClienteModel()
                                 {
                                     Dni = r.Cliente.Dni,
                                     Nombre = r.Cliente.Nombre,
                                     Apellido = r.Cliente.Apellido,
                                     Mail = r.Cliente.Mail,
                                     Direccion = r.Cliente.Direccion,
                                     Telefono = r.Cliente.Telefono1,
                                     Celular = r.Cliente.Telefono2
                                 },

                                 Novedades = (from n in db.novedad
                                              where n.reparacionId == r.ReparacionId
                                              select new NovedadModel()
                                              {
                                                  Id = n.novedadId,
                                                  Fecha = n.modificadoEn,
                                                  Observacion = n.observacion,
                                                  Descripcion = "Not Mapped",
                                                  Monto = n.monto
                                              }).ToList()

                             }).OrderByDescending(x => x.NroOrden).Take(100).ToList();

            model.OrdenActiva = model.Ordenes.First();

            OrdenesIndexViewModel = model;
            OrdenActiva = model.OrdenActiva;

            return PartialView(model);
        }

        public ActionResult MyIndex()
        {
            var db = new FastServiceEntities();

            var model = new OrdenesIndexViewModel();

            model.Ordenes = (from r in db.Reparacion
                             where r.Usuario.UserId == CurrentUserId
                               || r.Usuario1.UserId == CurrentUserId
                             select new OrdenModel()
                             {
                                 NroOrden = r.ReparacionId,
                                 Garantia = r.Garantia,
                                 EstadoCodigo = r.EstadoReparacionId,
                                 EstadoDesc = r.EstadoReparacion.nombre,
                                 EstadoFecha = r.ModificadoEn,

                                 ResponsableId = r.EmpleadoAsignadoId,
                                 ResponsableNombre = r.Usuario.Nombre,
                                 TecnicoId = r.EmpleadoAsignadoId,
                                 TecnicoNombre = r.Usuario1.Nombre,

                                 MarcaId = r.MarcaId,
                                 MarcaDesc = r.Marca.nombre,

                                 TipoId = r.TipoDispositivoId,
                                 TipoDesc = r.TipoDispositivo.nombre,
                                 Modelo = "Not Mapped",
                                 NroSerie = "Not Mapped",

                                 Cliente = new ClienteModel()
                                 {
                                     Dni = r.Cliente.Dni,
                                     Nombre = r.Cliente.Nombre,
                                     Apellido = r.Cliente.Apellido,
                                     Mail = r.Cliente.Mail,
                                     Direccion = r.Cliente.Direccion,
                                     Telefono = r.Cliente.Telefono1,
                                     Celular = r.Cliente.Telefono2
                                 },

                                 Novedades = (from n in db.novedad
                                              where n.reparacionId == r.ReparacionId
                                              select new NovedadModel()
                                              {
                                                  Id = n.novedadId,
                                                  Fecha = n.modificadoEn,
                                                  Observacion = n.observacion,
                                                  Descripcion = "Not Mapped",
                                                  Monto = n.monto
                                              }).ToList()

                             }).OrderByDescending(x => x.NroOrden).Take(100).ToList();

            model.OrdenActiva = model.Ordenes.First();

            OrdenesIndexViewModel = model;
            OrdenActiva = model.OrdenActiva;

            return PartialView(model);
        }

        // GET: Reparacion/Details/5
        public ActionResult Details(int id)
        {
            var orden = OrdenesIndexViewModel.Ordenes.Where(x => x.NroOrden == id).FirstOrDefault();


            if (orden != null)
            {
                OrdenActiva = orden;
                OrdenesIndexViewModel.OrdenActiva = orden;
                return PartialView("OrdenResumen", OrdenesIndexViewModel);
            }

            throw new Exception("Numero de orden no encontrada");

        }

        // GET: Reparacion/Create
        public ActionResult NuevaOrden()
        {
            return View();
        }

        // POST: Reparacion/Create
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

        // GET: Reparacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reparacion/Edit/5
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

        // GET: Reparacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reparacion/Delete/5
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
