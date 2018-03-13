using System.Collections.Generic;

namespace FastService.Models.Orden
{
    public interface IIndexModel
    {
        List<OrdenModel> Ordenes { get; set; }
        OrdenModel OrdenActiva { get; set; }
        bool NuevaOrden { get; set; }
        bool IsTecnico { get; set; }
        bool IsMyVIew { get; set; }
        List<OrdenModel> OrdenesAReparar { get; set; }
        List<OrdenModel> OrdenesEnEspera { get; set; }

        void Sync(int ordenNro);
        void Sync();
        void Order();
    }
}
