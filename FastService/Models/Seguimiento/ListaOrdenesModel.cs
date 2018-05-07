using FastService.Models.Orden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models.Seguimiento
{
    public class ListaOrdenesModel : IIndexModel
    {
        public List<OrdenModel> Ordenes { get; set; }
        public OrdenModel OrdenActiva { get => null; set => throw new NotImplementedException(); }
        public bool NuevaOrden { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsTecnico { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsMyVIew { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<OrdenModel> OrdenesAReparar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<OrdenModel> OrdenesEnEspera { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Order()
        {
            throw new NotImplementedException();
        }

        public void Sync(int ordenNro)
        {
            throw new NotImplementedException();
        }

        public void Sync()
        {
            throw new NotImplementedException();
        }
    }
}