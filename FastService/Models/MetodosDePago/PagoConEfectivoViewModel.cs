using System;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models.MetodosDePago
{
    public class PagoConEfectivoViewModel
    {
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public bool Facturado { get; set; }
    }
}