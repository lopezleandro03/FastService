using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models.MetodosDePago
{
    public class PagoConChequeViewModel
    {
        [Display(Name = "Numero Cuotas")]
        public int NroCuotas { get; set; }
        public List<ChequeModel> Cheques { get; set; }
    }
}