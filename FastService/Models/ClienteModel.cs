using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ClienteModel
    {
        [Required]
        [Display(Name = "Documento")]
        public int? Dni { get; set; }
        
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string Celular { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Direccion { get; set; }
        public string DisplayName
        {
            get
            {
                return string.Format("{0}-{1} {2}", Dni, Apellido, Nombre).Trim(); 
            }
            set
            {
            }
        }

        public string PrettyName
        {
            get
            {
                if (!string.IsNullOrEmpty(Apellido) && !string.IsNullOrEmpty(Nombre))
                {
                    return string.Format("{0}, {1}", Apellido.Trim().ToUpper(), Nombre.Trim().ToUpper());
                }
                else if (!string.IsNullOrEmpty(Apellido) && string.IsNullOrEmpty(Nombre))
                {
                    return Nombre.Trim().ToUpper();
                }
                else if (string.IsNullOrEmpty(Apellido) && !string.IsNullOrEmpty(Nombre))
                {
                    return Apellido.Trim().ToUpper();
                }
                else return "NO HAY INFORMACION DE CLIENTE";
            }
            set
            {
            }
        }

        public DireccionModel Dir { get; set; }
    }
}