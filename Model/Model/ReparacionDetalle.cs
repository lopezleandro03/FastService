//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReparacionDetalle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReparacionDetalle()
        {
            this.Reparacion = new HashSet<Reparacion>();
        }
    
        public int ReparacionDetalleId { get; set; }
        public bool EsGarantia { get; set; }
        public bool EsDomicilio { get; set; }
        public string NroReferencia { get; set; }
        public Nullable<System.DateTime> FechoCompra { get; set; }
        public string NroFactura { get; set; }
        public Nullable<decimal> Presupuesto { get; set; }
        public Nullable<System.DateTime> PresupuestoFecha { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Serbus { get; set; }
        public string ModificadoPor { get; set; }
        public System.DateTime ModificadoEn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reparacion> Reparacion { get; set; }
    }
}
