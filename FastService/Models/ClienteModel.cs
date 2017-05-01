using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ClienteModel
    {
        [Display(Name = "DNI")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string DisplayName
        {
            get
            {
                return string.Format("[{0}]{1} {2}", Id, Apellido, Nombre); 
            }
            set
            {
            }
        }
    }
}