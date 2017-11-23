using System.Collections.Generic;

namespace FastService.Models
{
    public class OrdenesIndexViewModel
    {
        public List<OrdenModel> Ordenes { get; set; }
        public OrdenModel OrdenActiva { get; set; }
    }
}