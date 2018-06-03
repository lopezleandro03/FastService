using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models.Reports
{
    public class HojadeRutaReportModel
    {
        public int Ticket { get; set; }
        public string Nombre { get; set; }
        public string Telefonos { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Marca { get; set; }
        public string Direccion { get; set; }
        public string Calle { get; set; }
        public string Altura { get; set; }
        public string Calle2 { get; set; }
        public string Calle3 { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Garantia { get; set; }
    }
}