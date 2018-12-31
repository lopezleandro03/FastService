using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models.Notificacion
{
    public class NotificacionModel
    {
        public string Icon { get; set; }
        public string Desc { get; set; }
        public int TicketId { get; set; }
        public int Dias { get; set; }
        public string Link { get; set; }
        public string Tipo { get; set; }
    }
}