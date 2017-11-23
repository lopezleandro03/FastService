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
    
    public partial class Reparacion
    {
        public int ReparacionId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoAsignadoId { get; set; }
        public int TecnicoAsignadoId { get; set; }
        public int EstadoReparacionId { get; set; }
        public int MarcaId { get; set; }
        public int TipoDispositivoId { get; set; }
        public int ReparacionDetalleId { get; set; }
        public bool Garantia { get; set; }
        public string ModificadoPor { get; set; }
        public System.DateTime ModificadoEn { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual EstadoReparacion EstadoReparacion { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual TipoDispositivo TipoDispositivo { get; set; }
    }
}