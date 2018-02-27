using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class NovedadModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        [Display(Name = "Novedad")]
        public string Descripcion { get; set; }
        public string Observacion { get; set; }

        public decimal? Monto { get; set; }
        public double Material { get; set; }
        public bool Facturado
        {
            get
            {
                return !String.IsNullOrEmpty(NroReferencia) && !string.IsNullOrWhiteSpace(NroReferencia);
            }
        }

        [Display(Name = "Numero de Factura")]
        public string NroReferencia { get; set; }

        public int TecnicoId { get; set; }
        public string TecnicoNombre { get; set; }

        public int ResponsableId { get; set; }
        public string ResponsableNombre { get; set; }

        public string TipoReingreso { get; set; }
        public int TipoNovedadId { get; set; }
    }
}