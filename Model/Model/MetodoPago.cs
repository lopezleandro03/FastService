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
    
    public partial class MetodoPago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetodoPago()
        {
            this.Pago = new HashSet<Pago>();
        }
    
        public int MetodoPagoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pago> Pago { get; set; }
    }
}
