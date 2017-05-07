using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ProveedorModel
    {
        [Required]
        [Display(Name = "CUIT")]
        public string CUIT { get; set; }
        [Required]
        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }
        public List<string> Rubro { get; set; } //Listado de Rubros que cubre el proveedor

        [Required]
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Direccion { get; set; }
        public List<CorredorModel> Corredores { get; set; }
        public string DisplayName
        {
            get
            {
                return string.Format("[{0}] {1}", CUIT, RazonSocial);
            }
            set
            {
            }
        }

        public List<CompraAPagarModel> ComprasAPagar{get;set;}
    }
}