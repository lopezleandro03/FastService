﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [Display(Name = "Presupuesto")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} debe ser un monto correcto, por ejemplo 5250.50")]
        public double Presupuesto { get; set; }

        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} debe ser un monto correcto, por ejemplo 5250.50")]
        [Display(Name = "Monto Final")]
        public double Monto { get; set; }

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
        [Display(Name = "Marca")]
        public string MarcaDesc { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Numero de serie")]
        public string NroSerie { get; set; }

        [Required]
        [Display(Name = "SerBus")]
        public string SerBus { get; set; }

        public ClienteModel Cliente { get; set; }
        public Model.Model.Comercio Comercio { get; set; }
        public List<NovedadModel> Novedades {get;set;}
    }
}