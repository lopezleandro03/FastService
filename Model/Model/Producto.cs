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
    
    public partial class Producto
    {
        public int id { get; set; }
        public Nullable<int> origenid { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
        public string categoria2 { get; set; }
        public string categoria3 { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> minimo { get; set; }
        public Nullable<decimal> costo { get; set; }
        public Nullable<decimal> utilidad { get; set; }
    }
}