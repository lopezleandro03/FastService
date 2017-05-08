using System.Collections.Generic;

namespace FastService.Models.MetodosDePago
{
    public class MetodoPagoModel
    {
        public int Id { get; set; }
        public int NroCuotas { get; set; }
        public List<CuotaModel> Cuotas{get;set;}
    }
}