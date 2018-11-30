using FastService.Common;
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
            model.FechaEntrega = DateTime.Now;

            InitializeViewBag();

            var view = SetNovedadModal(tipo);
            return PartialView(view, model);
        }

        // POST: Novedad/Create
        [HttpPost]
        public ActionResult Create(NovedadModel model)
        {
            var helper = new OrdenHelper();
            helper.Save(model, CurrentUserId);
            InitializeViewBag();

            return RedirectToAction("Details", "Ticket", new { id = model.NroOrden });
        }

        // GET: Novedad/Edit/5
        public ActionResult Edit(int ticketid, int id)
        {
            var ticket = new OrdenHelper().GetOrden(ticketid);
            var model = ticket.Novedades.Where(x => x.Id == id).FirstOrDefault();
            model.NroOrden = ticketid;
            var tipo = model.TipoNovedadId;

            InitializeViewBag();

            var view = SetNovedadModal(tipo);
            return PartialView(view, model);
        }

        [HttpPost]
        public ActionResult Delete(int ticketid, int id)
        {
            var _db = new FastServiceEntities();
            var novedad = _db.Novedad.Find(id);
            _db.Novedad.Remove(novedad);
            _db.SaveChanges();
            InitializeViewBag();

            return RedirectToAction("Details", "Ticket", new { id = ticketid });
        }


        private string SetNovedadModal(int tipo)
        {
            if (tipo == (int)NovedadTipo.PRESUPUESTADO
                || tipo == (int)NovedadTipo.REPARADO)
            {
                return "NovedadPresupuesto";
            }
            else if (tipo == (int)NovedadTipo.REPDOMICILIO)
            {
                return "NovedadReparadoDomicilio";
            }
            else if (tipo == (int)NovedadTipo.PRESUPINFOR)
            {
                return "NovedadInformarPresupuesto";
            }
            else if (tipo == (int)NovedadTipo.NOTA
                || tipo == (int)NovedadTipo.VERIFICAR
                || tipo == (int)NovedadTipo.ACONTROLAR
                || tipo == (int)NovedadTipo.ESPERAREPUESTO
                || tipo == (int)NovedadTipo.RECHAZA)
            {
                return "NovedadSimple";
            }
            else if (tipo == (int)NovedadTipo.REINGRESO)
            {
                return "NovedadReingreso";
            }
            else if (tipo == (int)NovedadTipo.ENTREGA)
            {
                return "NovedadCoordinarEntrega";
            }
            else if (tipo == (int)NovedadTipo.RETIRA || tipo == (int)NovedadTipo.SENA)
            {
                ViewBag.ListaMetodoDePago = (from y in new FastServiceEntities().MetodoPago
                                             select new SelectListItem()
                                             {
                                                 Text = y.Nombre,
                                                 Value = y.MetodoPagoId.ToString()
                                             }).ToList();

                ViewBag.ListaTipoFactura = (from y in new FastServiceEntities().TipoFactura
                                            select new SelectListItem()
                                            {
                                                Text = y.Nombre,
                                                Value = y.TipoFacturaId.ToString()
                                            }).ToList();

                return "NovedadRetiro";
            }
            else
            {
                return "NovedadSimple";
            }
        }

    }
}
