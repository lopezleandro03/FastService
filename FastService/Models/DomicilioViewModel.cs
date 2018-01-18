using System.Collections.Generic;
using System.Web.Mvc;

namespace FastService.Models
{
    public class DomicilioViewModel
    {
        public List<OrdenModel> OrdenesPendientes { get; set; }
        public List<OrdenModel> OrdenesDelDia { get; set; }
        public string MapaRutaUrl { get; set; }

        public string GetWayPoints()
        {
            var waypoints = string.Empty;

            foreach (var Orden in OrdenesDelDia)
            {
                if (string.IsNullOrEmpty(waypoints))
                    waypoints = $"{Orden.Cliente.Dir.Latitud},{Orden.Cliente.Dir.Longitud}";
                else
                    waypoints = $"{waypoints}|{Orden.Cliente.Dir.Latitud},{Orden.Cliente.Dir.Longitud}";
            }

            return waypoints;
        }
    }
}