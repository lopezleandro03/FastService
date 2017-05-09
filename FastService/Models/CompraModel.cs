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
        [Required]
        [Display(Name = "Comercio")]
        public int Origen { get; set; }
        public string OrigenNombre { get; set; }
        public int CompraId { get; set; }
        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} Debe ser un monto correcto")]
        public decimal Monto { get; set; }
        public ProveedorModel Proveedor { get; set; }

        [Display(Name = "Factura Nro.")]
        public int? FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprador { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
    }
}
