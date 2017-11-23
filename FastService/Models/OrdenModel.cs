using System;
using System.Collections.Generic;

namespace FastService.Models
{
    public class OrdenModel
    {
        public int NroOrden { get; set; }
        public bool Garantia { get; set; }

        public int EstadoCodigo { get; set; }
        public string EstadoDesc { get; set; }
        public DateTime EstadoFecha { get; set; }

        public int ResponsableId { get; set; }
        public string ResponsableNombre { get; set; }

        public int TecnicoId { get; set; }
        public string TecnicoNombre { get; set; }

        public int TipoId { get; set; }
        public string TipoDesc { get; set; }

        public int MarcaId { get; set; }
        public string MarcaDesc { get; set; }
        public string Modelo { get; set; }
        public string NroSerie { get; set; }

        public ClienteModel Cliente { get; set; }
        public List<NovedadModel> Novedades {get;set;}
    }
}