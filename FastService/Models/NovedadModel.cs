using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class NovedadModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public decimal? Monto { get; set; }
    }
}