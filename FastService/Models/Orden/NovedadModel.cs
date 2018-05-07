﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models
{
    public class NovedadModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int NroOrden { get; set; }

        [Display(Name = "Novedad")]
        public string Descripcion { get; set; }
        public string Observacion { get; set; }

        public decimal? Monto { get; set; }
        public double Material { get; set; }
        public bool Facturado
        {
            get
            {
                return !String.IsNullOrEmpty(NroReferencia) && !string.IsNullOrWhiteSpace(NroReferencia);
            }
        }

        [Display(Name = "Numero de Factura")]
        public string NroReferencia { get; set; }


        [Display(Name = "Tecnico")]
        public int TecnicoId { get; set; }
        [Display(Name = "Tecnico")]
        public string TecnicoNombre { get; set; }


        [Display(Name = "Responsable")]
        public int ResponsableId { get; set; }
        [Display(Name = "Responsable")]
        public string ResponsableNombre { get; set; }

        public string TipoReingreso { get; set; }
        public int TipoNovedadId { get; set; }

        [Display(Name = "Metodo de Pago")]
        public int MetodoDePagoId { get; set; }

        public string Accion { get; set; }

        [Display(Name = "Fecha Entrega")]
        public DateTime FechaEntrega { get; set; }


    }

}