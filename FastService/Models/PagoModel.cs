using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class PagoModel
    {
        public int Proveedor { get; set; }
        public int Compra { get; set; }
        public MetodoPagoModel MetodoPago { get; set; }
    }
}