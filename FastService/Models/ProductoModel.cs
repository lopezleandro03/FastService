using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ProductoModel
    {
        [Required]
        [Display(Name = "CUIT")]
        public string Nombre {get;set; }
        public string Descripcion { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Utilidad { get; set; }
        public string Categoria { get; set; }
        public string Categoria2 { get; set; }
        public string Categoria3 { get; set; }
        public decimal? Precio
        {
            get
            {
                return this.Costo + (this.Costo * this.Utilidad / 100);
            }
        }

    }
}