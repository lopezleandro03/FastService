using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models.Reports
{
    public class ReciboReportModel
    {
        public int ReparacionId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoAsignadoId { get; set; }
        public int TecnicoAsignadoId { get; set; }
        public int EstadoReparacionId { get; set; }
        public int? ComercioId { get; set; }
        public int MarcaId { get; set; }
        public int TipoDispositivoId { get; set; }
        public int? ReparacionDetalleId { get; set; }
        public int? ModificadoPor { get; set; }
        public DateTime ModificadoEn { get; set; }
        public bool EsGarantia { get; set; }
        public bool EsDomicilio { get; set; }
        public string NroReferencia { get; set; }
        public DateTime? FechoCompra { get; set; }
        public string NroFactura { get; set; }
        public decimal? Presupuesto { get; set; }
        public DateTime? PresupuestoFecha { get; set; }
        public decimal? Precio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Serbus { get; set; }
        public int? Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Direccion { get; set; }
        public int? DireccionId { get; set; }
        public string Localidad { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string ReparacionDesc { get; internal set; }
        public string Accesorios { get; internal set; }
        public string Comercio { get;  set; }
        public DateTime CreadoEn { get; set; }
    }
}