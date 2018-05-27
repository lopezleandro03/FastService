﻿using FastService.Common;
using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class NovedadController : BaseController
    {
        // GET: Novedad
        public ActionResult Index()
        {
            return View();
        }

        // GET: Novedad/Details/5
        [HttpGet]
        public ActionResult Create(int tipo, int nroOrden)
        {
            var orden = new OrdenHelper().GetOrden(nroOrden);
            var model = new NovedadModel();

            model.NroOrden = orden.NroOrden;
            model.ResponsableId = orden.ResponsableId;
            model.ResponsableNombre = orden.ResponsableNombre;
            model.TecnicoId = orden.TecnicoId;
            model.TecnicoNombre = orden.TecnicoNombre;
            model.Monto = orden.Presupuesto;
            model.TipoNovedadId = tipo;

            InitializeViewBag();

            if (tipo == (int)NovedadTipo.REPDOMICILIO
                || tipo == (int)NovedadTipo.PRESUPUESTADO
                || tipo == (int)NovedadTipo.REPARADO)
            {
                return PartialView("NovedadPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.PRESUPINFOR
                || tipo == (int)NovedadTipo.LLAMADO)
            {
                return PartialView("NovedadInformarPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.NOTA
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO)
            {
                return PartialView("NovedadSimple", model);
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return PartialView("NovedadReingreso", model);
            }
            else if (tipo == (int)NovedadTipo.ENTREGA)
            {
                return PartialView("NovedadCoordinarEntrega", model);
            }
            else if (tipo == (int)NovedadTipo.RETIRA)
            {
                ViewBag.ListaMetodoDePago = (from y in new FastServiceEntities().MetodoPago
                                             select new SelectListItem()
                                             {
                                                 Text = y.Nombre,
                                                 Value = y.MetodoPagoId.ToString()
                                             }).ToList();

                return PartialView("NovedadRetiro", model);
            }
            else
            {
                return PartialView("NovedadSimple", model);
            }
        }

        // POST: Novedad/Create
        [HttpPost]
        public ActionResult Create(NovedadModel model)
        {
            var helper = new OrdenHelper();
            helper.Save(model, CurrentUserId);
            InitializeViewBag();
            return PartialView("~/Views/Ticket/Details.cshtml", helper.GetOrden(model.NroOrden));
        }

        // GET: Novedad/Edit/5
        public ActionResult Edit(int ticketid, int id)
        {
            var ticket = new OrdenHelper().GetOrden(ticketid);
            var model = ticket.Novedades.Where(x => x.Id == id).FirstOrDefault();
            model.NroOrden = ticketid;
            var tipo = model.TipoNovedadId;

            InitializeViewBag();

            if (tipo == (int)NovedadTipo.PRESUPINFOR
            || tipo == (int)NovedadTipo.ENTREGA
            || tipo == (int)NovedadTipo.REPDOMICILIO
            || tipo == (int)NovedadTipo.REPARADO)
            {
                return PartialView("NovedadPresupuesto", model);
            }
            else if (tipo == (int)NovedadTipo.ACEPTA
                || tipo == (int)NovedadTipo.RECHAZA
                || tipo == (int)NovedadTipo.NOTA
                || tipo == (int)NovedadTipo.LLAMADO
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO)
            {
                return PartialView("NovedadSimple", model);
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return PartialView("NovedadReingreso", model);
            }
            else
            {
                return PartialView("NovedadPresupuesto", model);
            }

        }

        [HttpPost]
        public ActionResult Delete(int ticketid, int id)
        {
            var _db = new FastServiceEntities();
            var novedad = _db.Novedad.Find(id);
            _db.Novedad.Remove(novedad);
            _db.SaveChanges();
            InitializeViewBag();

            return PartialView("~/Views/Ticket/Details.cshtml", new OrdenHelper().GetOrden(ticketid));
        }
    }
}