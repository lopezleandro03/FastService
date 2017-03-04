using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class CompraModel
    {
        public int CompraId { get; set; }
        public string ProveedorId { get; set; }
        public string Monto { get; set; }
        public string ProveedorNombre { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public int? FacturaId { get; set; }
        public int Origen { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprador { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
    }
}
