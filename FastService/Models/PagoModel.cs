using FastService.Models.MetodosDePago;

namespace FastService.Models
{
    public class PagoModel
    {
        public int Proveedor { get; set; }
        public int Compra { get; set; }
        public int Facturado { get; set; }
        public MetodoPagoModel MetodoPago { get; set; }
    }
}