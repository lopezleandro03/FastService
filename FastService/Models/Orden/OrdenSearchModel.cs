using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FastService.Models
{
    public class OrdenSearchModel
    {
        public int NroOrden { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}