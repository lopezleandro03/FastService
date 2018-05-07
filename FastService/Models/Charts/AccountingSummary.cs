using FastService.Common;
using FastService.Models.Charts;
using Model.Model;
using System;
using System.Linq;

namespace FastService.Models
{
    public class AccountingSummary
    {
        public decimal TotalVentasConFactura { get; set; }
        public decimal TotalVentasSinFactura { get; set; }
        public decimal TotalVentas
        {
            get { return TotalVentasConFactura + TotalVentasSinFactura; }
        }

        public decimal TotalComprasConFactura { get; set; }
        public decimal TotalComprasSinFactura { get; set; }
        public decimal TotalCompras
        {
            get { return TotalComprasConFactura + TotalComprasSinFactura; }
        }

        public decimal BalanceSinFactura
        {
            get { return TotalVentasSinFactura - TotalComprasSinFactura; }
        }
        public decimal BalanceConFactura
        {
            get { return TotalVentasConFactura - TotalComprasConFactura; }
        }
        public decimal BalanceTotal
        {
            get { return (TotalVentasSinFactura + TotalVentasConFactura) - (TotalComprasSinFactura + TotalVentasConFactura); }
        }

        public Chart Chart { get; set; }

        public AccountingSummary()
        {
            Chart = new Chart();
        }

        internal void LoadVentas(char mode)
        {
            var db = new FastServiceEntities();

            var ventas = (from x in db.Venta
                          where x.Fecha.Year == DateTime.Now.Year
                          select x).ToList();

            var VentasSinFacturaChartDataSet = new ChartDataSet("Ventas Sin Factura", Chart.Color1, Chart.Color1, "1");
            var VentasConFacturaChartDataSet = new ChartDataSet("Ventas Con Factura", Chart.Color2, Chart.Color3, "2");

            if (mode == 'd')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);

                for (int i = 6; i < 22 + 1; i++)
                {
                    VentasSinFacturaChartDataSet.DataPoints.Add(i.ToString());
                    VentasConFacturaChartDataSet.DataPoints.Add(i.ToString());

                    VentasSinFacturaChartDataSet.DataValues.Add(ventas.Where(x =>
                           x.Factura == null
                        && x.Fecha.Date == DateTime.Now.Date
                        && x.Fecha.Hour == i).Sum(x => x.Monto));

                    VentasConFacturaChartDataSet.DataValues.Add(ventas.Where(x =>
                           x.Factura != null
                        && x.Fecha.Date == DateTime.Now.Date
                        && x.Fecha.Hour == i).Sum(x => x.Monto));
                }
            }

            if (mode == 'w')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.Monto);

                for (int i = 0; i < 7; i++)
                {
                    VentasSinFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));
                    VentasConFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));

                    VentasSinFacturaChartDataSet.DataValues.Add(ventas.Where(x =>
                           x.Factura == null
                        && x.Fecha > x.Fecha.AddDays(-(int)x.Fecha.DayOfWeek)
                        && (int)x.Fecha.DayOfWeek == i).Sum(x => x.Monto));

                    VentasConFacturaChartDataSet.DataValues.Add(ventas.Where(x =>
                           x.Factura != null
                        && x.Fecha > x.Fecha.AddDays(-(int)x.Fecha.DayOfWeek)
                        && (int)x.Fecha.DayOfWeek == i).Sum(x => x.Monto));
                }
            }

            if (mode == 'm')
            {
                TotalVentasConFactura = ventas.Where(x => x.Factura != null && x.Fecha.Month == DateTime.Now.Month).Sum(x => x.Monto);
                TotalVentasSinFactura = ventas.Where(x => x.Factura == null && x.Fecha.Month == DateTime.Now.Month).Sum(x => x.Monto);

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

        internal void LoadCompras(char mode)
        {
            var db = new FastServiceEntities();

            var compras = (from x in db.Compra
                           where x.FechaCreacion.Year == DateTime.Now.Year
                           select x).ToList();

            var ComprasSinFacturaChartDataSet = new ChartDataSet("Compras Sin Factura", Chart.Color3, Chart.Color3, "1");
            var ComprasConFacturaChartDataSet = new ChartDataSet("Comrpas Con Factura", Chart.Color4, Chart.Color4, "2");

            if (mode == 'd')
            {
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);

                for (int i = 6; i < 22 + 1; i++)
                {
                    ComprasSinFacturaChartDataSet.DataPoints.Add(i.ToString());
                    ComprasConFacturaChartDataSet.DataPoints.Add(i.ToString());

                    ComprasSinFacturaChartDataSet.DataValues.Add(compras.Where(x =>
                           !x.Facturado
                        && x.FechaCreacion.Date == DateTime.Now.Date
                        && x.FechaCreacion.Hour == i).Sum(x => x.Monto));

                    ComprasConFacturaChartDataSet.DataValues.Add(compras.Where(x =>
                           x.Facturado
                        && x.FechaCreacion.Date == DateTime.Now.Date
                        && x.FechaCreacion.Hour == i).Sum(x => x.Monto));
                }
            }

            if (mode == 'w')
            {
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Date == DateTime.Now.Date).Sum(x => x.Monto);

                for (int i = 0; i < 6; i++)
                {
                    ComprasSinFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));
                    ComprasConFacturaChartDataSet.DataPoints.Add(new DateHelper().GetDayName(i));

                    ComprasSinFacturaChartDataSet.DataValues.Add(compras.Where(x =>
                           !x.Facturado
                        && x.FechaCreacion > x.FechaCreacion.AddDays(-(int)x.FechaCreacion.DayOfWeek)
                        && (int)x.FechaCreacion.DayOfWeek == i).Sum(x => x.Monto));

                    ComprasConFacturaChartDataSet.DataValues.Add(compras.Where(x =>
                           x.Facturado
                        && x.FechaCreacion > x.FechaCreacion.AddDays(-(int)x.FechaCreacion.DayOfWeek)
                        && (int)x.FechaCreacion.DayOfWeek == i).Sum(x => x.Monto));
                }
            }

            if (mode == 'm')
            {
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Month == DateTime.Now.Month).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Month == DateTime.Now.Month).Sum(x => x.Monto);

                for (int i = 1; i < (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; i++)
                {
                    ComprasSinFacturaChartDataSet.DataPoints.Add(i.ToString());
                    ComprasConFacturaChartDataSet.DataPoints.Add(i.ToString());

                    ComprasSinFacturaChartDataSet.DataValues.Add(compras.Where(x => !x.Facturado
                        && x.FechaCreacion.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).Date).Sum(x => x.Monto));

                    ComprasConFacturaChartDataSet.DataValues.Add(compras.Where(x => x.Facturado
                        && x.FechaCreacion.Date == new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).Date).Sum(x => x.Monto));
                }
            }

            if (mode == 'y')
            {
                TotalComprasConFactura = compras.Where(x => x.FechaCreacion.Year == DateTime.Now.Year).Sum(x => x.Monto);
                TotalComprasSinFactura = compras.Where(x => x.FechaCreacion.Year == DateTime.Now.Year).Sum(x => x.Monto);

                for (int i = 1; i < 12 + 1; i++)
                {
                    ComprasSinFacturaChartDataSet.DataPoints.Add(new DateHelper().GetMonthName(i));
                    ComprasConFacturaChartDataSet.DataPoints.Add(new DateHelper().GetMonthName(i));

                    ComprasSinFacturaChartDataSet.DataValues.Add(compras.Where(x => !x.Facturado
                        && x.FechaCreacion.Month == new DateTime(DateTime.Now.Year, i, 1).Month).Sum(x => x.Monto));

                    ComprasConFacturaChartDataSet.DataValues.Add(compras.Where(x => x.Facturado
                        && x.FechaCreacion.Month == new DateTime(DateTime.Now.Year, i, 1).Month).Sum(x => x.Monto));
                }
            }

            Chart.Datasets.Add(ComprasConFacturaChartDataSet);
            Chart.Datasets.Add(ComprasSinFacturaChartDataSet);
        }
    }
}