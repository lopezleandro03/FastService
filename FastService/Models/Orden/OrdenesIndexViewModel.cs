using FastService.Common;
using FastService.Models.Orden;
using System.Collections.Generic;
using System.Linq;

namespace FastService.Models
{
    public class OrdenesIndexViewModel : IIndexModel
    {
        public List<OrdenModel> Ordenes { get; set; }
        public OrdenModel OrdenActiva { get; set; }
        public bool NuevaOrden { get; set; }
        public bool IsTecnico { get; set; }
        public bool IsMyVIew { get; set; }

        public List<OrdenModel> OrdenesAReparar
        {
            get { return (from x in Ordenes where x.EstadoDesc == ReparacionEstado.AREPARAR select x).OrderByDescending(x => x.FechaUltimaNovedad).ToList(); }
            set { }
        }

        public List<OrdenModel> OrdenesEnEspera
        {
            get { return (from x in Ordenes where x.EstadoDesc != ReparacionEstado.AREPARAR select x).OrderByDescending(x => x.FechaUltimaNovedad).ToList(); }
            set { }
        }

        public OrdenesIndexViewModel Sync(NovedadModel model)
        {
            var ordenes = Ordenes.Where(x => x.NroOrden == this.OrdenActiva.NroOrden).FirstOrDefault();
            var novedades = ordenes.Novedades;
            novedades.Add(model);
            this.Ordenes.Where(x => x.NroOrden == this.OrdenActiva.NroOrden).Select(y => y.Novedades = novedades);

            return this;
        }
    }
}