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
    
    public partial class novedad
    {
        public int novedadId { get; set; }
        public int reparacionId { get; set; }
        public int UserId { get; set; }
        public int tipoNovedadId { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string observacion { get; set; }
        public string modificadoPor { get; set; }
        public System.DateTime modificadoEn { get; set; }
    }
}