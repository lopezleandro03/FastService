using System.Collections.Generic;
using System.Web.Mvc;

namespace FastService.Models
{
    public class OrdenesIndexViewModel
    {
        public List<OrdenModel> Ordenes { get; set; }
        public OrdenModel OrdenActiva { get; set; }
        public bool NuevaOrden { get; internal set; }
    }
}