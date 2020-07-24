using Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastService.Models
{
    public class VentaModel
    {
        [Display(Name = "ID Venta")]
        public int VentaId { get; set; }
        [Required]
        [Display(Name = "Comercio")]
        public int Origen { get; set; }
        public string OrigenNombre { get; set; }
        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} debe ser un monto correcto, por ejemplo 5250.50")]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        public ClienteModel Cliente { get; set; }
        public List<PagoModel> Pago { get; set; }
        public DateTime Fecha { get; set; }
        public int Vendedor { get; set; }
        [Display(Name = "Nro. Factura")]
        public string NroFactura { get; set; }
        [Display(Name = "Venta Facturada?")]
        public bool Facturado { get; set; }

        [Display(Name = "Tipo de Factura")]
        public string TipoDeFactura { get; set; }
        [Display(Name = "Tipo de Factura")]
        public int TipoDeFacturaId { get; set; }
        [Display(Name = "Metodo de pago")]
        public string MetodoDePago { get; set; }
        [Display(Name = "Cantidad de cuotas")]
        public int NroCuotas { get; set; }
        public int Cuotas { get; set; }
        public int? MetodoDePagoId { get; set; }

        public ProductoModel Producto { get; set; }
    }
}
