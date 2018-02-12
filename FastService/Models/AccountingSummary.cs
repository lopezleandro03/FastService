using FastService.Common;
using Model.Model;
using System;
using System.Linq;

namespace FastService.Models
{
    public class AccountingSummary
    {
        public decimal TotalVentasConFactura { get; set; }
        public decimal TotalVentasSinFactura { get; set; }
        public decimal TotalComprasConFactura { get; set; }
        public decimal TotalComprasSinFactura { get; set; }
        public Chart Chart { get; set; }

        internal void Load(char mode)
        {
            var db = new FastServiceEntities();

            var ventas = (from x in db.Venta
                          where x.Fecha.Year == DateTime.Now.Year
                          select x).ToList();

            var compras = (from x in db.Compra
                           where x.FechaCreacion.Year == DateTime.Now.Year
                           select x).ToList();

            Chart = new Chart();
            var VentasSinFacturaChartDataSet = new ChartDataSet("Sin Factura", "rgba(255, 0, 0, 0.8)", "rgba(255, 0, 0, 0.8)", "1");
            var VentasConFacturaChartDataSet = new ChartDataSet("Con Factura", "rgba(54, 162, 235, 0.2)", "rgba(54, 162, 235, 0.2)", "2");

            if (mode == 'd')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);

                for (int i = 1; i < (int)DateTime.Now.DayOfWeek + 1; i++)
                {
                    VentasSinFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));
                    VentasConFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));

                    VentasSinFacturaChartDataSet.DataValues.Add(ventas.Where(x =>
                           x.Factura == null
                        && x.Fecha > x.Fecha.AddDays(-(int)x.Fecha.DayOfWeek)
                        && (int)x.Fecha.DayOfWeek == i).Sum(x => x.Monto));

                    VentasConFacturaChartDataSet.DataPoints.Add(ventas.Where(x =>
                           x.Factura != null
                        && x.Fecha > x.Fecha.AddDays(-(int)x.Fecha.DayOfWeek)
                        && (int)x.Fecha.DayOfWeek == i).Sum(x => x.Monto).ToString());
                }
            }

            if (mode == 'm')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Month == DateTime.Now.Month).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Month == DateTime.Now.Month).Sum(x => x.Monto);
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Month == DateTime.Now.Month).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Month == DateTime.Now.Month).Sum(x => x.Monto);

                for (int i = 1; i < (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; i++)
                {
                    VentasSinFacturaChartDataSet.DataPoints.Add(i.ToString());
                    VentasConFacturaChartDataSet.DataPoints.Add(i.ToString());

                    VentasSinFacturaChartDataSet.DataValues.Add(ventas.Where(x => x.Factura == null
                        && x.Fecha.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).Date).Sum(x => x.Monto));

                    VentasConFacturaChartDataSet.DataValues.Add(ventas.Where(x => x.Factura != null
                        && x.Fecha.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).Date).Sum(x => x.Monto));
                }
            }

            if (mode == 'y')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Year == DateTime.Now.Year).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Year == DateTime.Now.Year).Sum(x => x.Monto);
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Year == DateTime.Now.Year).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Year == DateTime.Now.Year).Sum(x => x.Monto);

                for (int i = 1; i < 12 + 1; i++)
                {
                    VentasSinFacturaChartDataSet.DataPoints.Add(new DateHelper().GetMonthName(i));
                    VentasConFacturaChartDataSet.DataPoints.Add(new DateHelper().GetMonthName(i));

                    VentasSinFacturaChartDataSet.DataValues.Add(ventas.Where(x => x.Factura == null
                        && x.Fecha.Month == new DateTime(DateTime.Now.Year, i, 1).Month).Sum(x => x.Monto));

                    VentasConFacturaChartDataSet.DataValues.Add(ventas.Where(x => x.Factura != null
                        && x.Fecha.Month == new DateTime(DateTime.Now.Year, i, 1).Month).Sum(x => x.Monto));
                }
            }

            Chart.Datasets.Add(VentasConFacturaChartDataSet);
            Chart.Datasets.Add(VentasSinFacturaChartDataSet);
        }
    }
}