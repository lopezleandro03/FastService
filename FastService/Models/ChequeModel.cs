using System;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models
{
    public class ChequeModel
    {
        [Display(Name = "Nro. Cheque")]
        public int NroCheque { get; set; }
        public DateTime FechaEmision { get; set; }
        [Display(Name = "Fecha debito")]
        public DateTime FechaDebito { get; set; }
        public Decimal Monto { get; set; }
        public Decimal Destinatario { get; set; }
        public Decimal Creador { get; set; }
    }
}