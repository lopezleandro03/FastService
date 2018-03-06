using System;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models
{
    public class DireccionModel
    {
        public int DireccionId { get; set; }
        public string Altura { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Comentarios { get; set; }
        public Nullable<decimal> Latitud { get; set; }
        public Nullable<decimal> Longitud { get; set; }
        public System.DateTime ChangedOn { get; set; }
        public int ChangedBy { get; set; }
        [Display(Name = "Entre calle 1")]
        public string Calle2 { get; set; }
        [Display(Name = "Entre calle 2")]
        public string Calle3 { get; set; }
    }
}