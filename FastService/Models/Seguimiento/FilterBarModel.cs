using System;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models.Seguimiento
{
    public class FilterBarModel
    {
        [Display(Name = "Desde")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "Hasta")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Tecnico")]
        public int TecnicoId { get; set; }
        [Display(Name = "Empresa")]
        public int ComercioId { get; set; }
    }
}