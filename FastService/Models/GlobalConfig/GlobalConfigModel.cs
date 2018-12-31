using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models.GlobalConfig
{
    public class GlobalConfigModel
    {

        [Display(Name = "Dias de inactividad")]
        public int NotificacionesMinimoDiasInactividad { get; set; }

        [Display(Name = "Notificaciones desde año")]
        public int NotificacionesFechaInicioAno{ get; set; }

        [Display(Name = "Notificaciones desde mes")]
        public int NotificacionesFechaInicioMes { get; set; }
    }
}