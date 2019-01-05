using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FastService.Models
{
    public class OrdenModel
    {
        [Display(Name = "Orden")]
        [Required]
        public int NroOrden { get; set; }
        public bool Garantia { get; set; }
        public bool Domicilio { get; set; }

        [Required]
        public int EstadoCodigo { get; set; }
        [Display(Name = "Estado")]
        public string EstadoDesc { get; set; }

        [Required]
        [Display(Name = "Fecha Estado")]
        public DateTime EstadoFecha { get; set; }

        public DateTime? InformadoEn { get; set; }

        [Display(Name = "Fecha Compra")]
        public DateTime? FechaCompra { get; set; }

        [Required]
        [Display(Name = "Presupuesto")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} debe ser un monto correcto, por ejemplo 5250.50")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Presupuesto { get; set; }

        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} debe ser un monto correcto, por ejemplo 5250.50")]
        [Display(Name = "Monto Final")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Monto { get; set; }

        [Required]
        public int ResponsableId { get; set; }

        [Display(Name = "Responsable")]
        public string ResponsableNombre { get; set; }

        [Required]
        public int TecnicoId { get; set; }
        [Display(Name = "Tecnico asignado")]
        public string TecnicoNombre { get; set; }

        [Required]
        public int TipoId { get; set; }

        [Required]
        [Display(Name = "Tipo Aparato")]
        public string TipoDesc { get; set; }

        [Required]
        public int MarcaId { get; set; }
        [Display(Name = "Marca Aparato")]
        public string MarcaDesc { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Numero de serie")]
        public string NroSerie { get; set; }

        [Required]
        [Display(Name = "SerBus")]
        public string SerBus { get; set; }

        [Display(Name = "Ubicacion")]
        public string Ubicacion { get; set; }


        public ClienteModel Cliente { get; set; }
        public Comercio Comercio { get; set; }
        public List<NovedadModel> Novedades { get; set; }

        public string ResumenAparato
        {
            get
            {
                if (string.IsNullOrEmpty(Modelo))
                {
                    return $"{TipoDesc}-{MarcaDesc}";
                }

                return $"{TipoDesc}-{MarcaDesc}-{Modelo}";
            }
            set { }
        }

        public DateTime FechaUltimaNovedad
        {
            get
            {
                if (Novedades != null)
                {
                    if (Novedades.Any())
                    {
                        return Novedades.Max(x => x.Fecha);
                    }
                }
                else
                {
                    return DateTime.Now.AddDays(-99);
                }

                return DateTime.Now.AddDays(-99);
            }
            set { }
        }

        public int DiasDesdeUltimaNotification
        {
            get
            {
                if (InformadoEn != null)
                {
                    return (int)(DateTime.Now - (DateTime)InformadoEn).TotalDays;
                }

                return 0;
            }
            set { }
        }

        public string ReparacionDesc { get; set; }
        public string Accesorios { get; set; }
    }
}