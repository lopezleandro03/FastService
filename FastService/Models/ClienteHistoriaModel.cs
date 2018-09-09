using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ClienteHistoriaModel
    {
        public int NroOrden { get; set; }
        public string EstadoDesc { get; set; }
        public DateTime EstadoFecha { get; set; }
        public decimal? Monto { get; set; }
        public string TecnicoNombre { get; set; }
        public string TipoDesc { get; set; }
        public string MarcaDesc { get; set; }
        public string Modelo { get; set; }
        public string NroSerie { get; set; }

    }
}