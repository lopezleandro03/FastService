using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class MetodoPagoModel
    {
        public int Id { get; set; }
        public int NroCuotas { get; set; }
        public List<CuotaModel> Cuotas{get;set;}
    }
}