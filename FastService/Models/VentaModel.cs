using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class VentaModel
    {
        public int VentaId { get; set; }
        [DisplayName("DNI del cliente")]
        public string ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string MailCliente { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public decimal Monto { get; set; }
        public int? FacturaId { get; set; }
        public int Origen { get; set; }
        public DateTime Fecha { get; set; } 
        public string Vendedor { get; set; }
        public string FacturaID { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set;}
    }
}
