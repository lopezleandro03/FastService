using System;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models.MetodosDePago
{
    public class PagoConTransferenciaViewModel
    {
        [Required]
        public string RefNumber { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}