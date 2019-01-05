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
        public int? ClienteId { get; set; }

        //[Required]
        [Display(Name = "Documento/CUIT")]
        public int? Dni { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        public string Celular { get; set; }

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

        public string DisplayNameAndAddress
        {
            get
            {
                return string.Format("{0}-{1} {2} ({3})", Dni, Apellido, Nombre, Direccion).Trim();
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

        public string PrettyAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(Calle) && !string.IsNullOrEmpty(Altura))
                {
                    return $"{Calle} {Altura}";
                }
                else if (!string.IsNullOrEmpty(Calle) && string.IsNullOrEmpty(Altura) && string.IsNullOrEmpty(Calle2) && string.IsNullOrEmpty(Calle3))
                {
                    return $"{Calle} {Altura} entre {Calle2} y {Calle3}";
                }

                return string.Empty;
            }
            set
            {
            }
        }

        public DireccionModel Dir { get; set; }
        public string Calle { get; internal set; }
        public string Altura { get; internal set; }
        public string Calle2 { get; internal set; }
        public string Calle3 { get; internal set; }
        public object Pais { get; internal set; }
        public string Provincia { get; internal set; }
        public string CodigoPostal { get; internal set; }
        public decimal? Latitud { get; internal set; }
        public decimal? Longitud { get; internal set; }
    }
}