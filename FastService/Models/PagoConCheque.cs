using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class PagoConChequeModel
    {
        [Display(Name = "Numero Cuotas")]
        public int NroCuotas { get; set; }
        public List<ChequeModel> Cheques { get; set; }
    }
}