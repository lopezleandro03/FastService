//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        public int VentaId { get; set; }
        public Nullable<int> ClienteId { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> FacturaId { get; set; }
        public string RefNumber { get; set; }
        public int PuntoDeVentaId { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Vendedor { get; set; }
        public Nullable<int> MetodoPagoId { get; set; }
        public int TipoTransaccionId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Factura Factura { get; set; }
        public virtual Factura Factura1 { get; set; }
        public virtual PuntoDeVenta PuntoDeVenta { get; set; }
        public virtual PuntoDeVenta PuntoDeVenta1 { get; set; }
        public virtual TipoTransaccion TipoTransaccion { get; set; }
    }
}
