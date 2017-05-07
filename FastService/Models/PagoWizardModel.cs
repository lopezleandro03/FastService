using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class PagoWizardModel
    {
        public List<ProveedorModel> Proveedores { get; set; }
        public string MetodoPago { get; set; }
        [Display(Name = "Metodo de pago")]
        public string MetodoDePago { get; set; }
        [Display(Name = "Cantidad de cuotas")]
        public int NroCuotas { get; set; }
    }
}