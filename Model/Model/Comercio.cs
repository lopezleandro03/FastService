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
    
    public partial class Comercio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comercio()
        {
            this.Reparacion = new HashSet<Reparacion>();
        }
    
        public int ComercioId { get; set; }
        public string Code { get; set; }
        public string Descripcion { get; set; }
        public string Contacto { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string cuit { get; set; }
        public bool activo { get; set; }
        public Nullable<System.DateTime> modificadoEn { get; set; }
        public Nullable<int> modificadoPor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reparacion> Reparacion { get; set; }
    }
}
