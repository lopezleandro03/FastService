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
        public int Origen { get; set; }
        public int CompraId { get; set; }
        [Required]
        public decimal Monto { get; set; }
        public ProveedorModel Proveedor { get; set; }
        public int? FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprador { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }



        [Display(Name = "Metodo de pago")]
        public string MetodoDePago { get; set; }
        [Display(Name = "Cantidad de cuotas")]
        public int NroCuotas { get; set; }
        public int Cuotas { get; set; }
    }
}
