using FastService.Models;
using FastService.Models.Notificacion;
using FastService.Models.Reports;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace FastService.Common
{
    public class NotificacionHelper
    {
        private FastServiceEntities _db { get; set; }
        private GlobalConfigHelper _configHelper { get; set; }
        private int year { get; set; }
        private int month { get; set; }
        private int days { get; set; }

        public NotificacionHelper()
        {
            _db = new FastServiceEntities();
            _configHelper = new GlobalConfigHelper();

            year = Convert.ToInt32(_configHelper.GetVal("NotificacionesFechaInicioAno"));
            month = Convert.ToInt32(_configHelper.GetVal("NotificacionesFechaInicioMes"));
            days = Convert.ToInt32(_configHelper.GetVal("NotificacionesMinimoDiasInactividad"));
        }

        public List<NotificacionModel> GetNotificaciones()
        {
            var Notificaciones = (from x in _db.Reparacion
                                  where x.CreadoEn.Year >= year
                                     && x.CreadoEn.Month >= month
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.CANCELADO
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.RECHAZADO
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.RECHAZOPRESUP
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.RETIRADO
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.REPDOMICILIO
                                     && x.EstadoReparacion.nombre.ToUpper() != ReparacionEstado.ESPREPUESTO
                                  select x).ToList();

            var noti = Notificaciones.Where(x => x.ModificadoEn < DateTime.Now.AddDays(-days))
                .Select(x =>
                    new NotificacionModel()
                    {
                        TicketId = x.ReparacionId,
                        Desc = "La orden " + x.ReparacionId + " lleva mas de " + (int)(DateTime.Now - (DateTime)x.ModificadoEn).TotalDays + " dias sin actividad.",
                        Tipo = "info",
                        Icon = "fa fa-bullhorn",
                        Dias = (int)(DateTime.Now - (DateTime)x.ModificadoEn).TotalDays,
                        Link = string.Empty
                    }).ToList();

            return noti;
        }

    }
}