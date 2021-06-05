using FastService.Common;
using FastService.Helpers;
using FastService.Models;
using FastService.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class DomicilioController : BaseController
    {
        private OrdenHelper _ordenHelper { get; set; }
        private DomicilioViewModel Model
        {
            get
            {
                return (DomicilioViewModel)System.Web.HttpContext.Current.Session["DOMICILIOVIEWMODEL"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["DOMICILIOVIEWMODEL"] = value;
            }
        }
        private string Origen { get; set; }
        private string Destino { get; set; }
        // GET: Domicilio

        public DomicilioController()
        {
            _ordenHelper = new OrdenHelper();
            Origen = "-34.784654,-58.315066"; //FastService
            Destino = "-34.784654,-58.315066"; //FastService
        }

        public ActionResult Index()
        {
            Model = new DomicilioViewModel()
            {
                OrdenesPendientes = _ordenHelper.GetOrdenesADomicilio(),
                OrdenesDelDia = new List<OrdenModel>()
            };

            Model.MapaRutaUrl = new GoogleMapsHelper().GetDirectionApiUrl(Origen, Destino, Model.GetWayPoints());

            return PartialView(Model);
        }

        // GET: Domicilio/Create
        public ActionResult Add(int id)
        {
            if (Model.OrdenesPendientes.Where(x => x.NroOrden == id).FirstOrDefault() != null)
            {
                Model.OrdenesDelDia.Add(Model.OrdenesPendientes.Where(x => x.NroOrden == id).FirstOrDefault());
                Model.OrdenesPendientes.Remove(Model.OrdenesPendientes.Where(x => x.NroOrden == id).FirstOrDefault());
                Model.MapaRutaUrl = new GoogleMapsHelper().GetDirectionApiUrl(Origen, Destino, Model.GetWayPoints());
            }

            Model.OrdenesPendientes = Model.OrdenesPendientes.OrderByDescending(x => x.NroOrden).ToList();
            Model.OrdenesDelDia = Model.OrdenesDelDia.OrderByDescending(x => x.NroOrden).ToList();

            return PartialView("Index", Model);
        }

        public ActionResult Remove(int id)
        {
            if (Model.OrdenesDelDia.Where(x => x.NroOrden == id).FirstOrDefault() != null)
            {
                Model.OrdenesPendientes.Add(Model.OrdenesDelDia.Where(x => x.NroOrden == id).FirstOrDefault());
                Model.OrdenesDelDia.Remove(Model.OrdenesDelDia.Where(x => x.NroOrden == id).FirstOrDefault());
                Model.MapaRutaUrl = new GoogleMapsHelper().GetDirectionApiUrl(Origen, Destino, Model.GetWayPoints());
            }

            Model.OrdenesPendientes = Model.OrdenesPendientes.OrderByDescending(x => x.NroOrden).ToList();
            Model.OrdenesDelDia = Model.OrdenesDelDia.OrderByDescending(x => x.NroOrden).ToList();

            return PartialView("Index", Model);
        }

        //public ActionResult ImprimirRuta()
        //{
        //    var tickets = GetHojaDeRuta();

        //    if (tickets.Any())
        //    {
        //        string reportName = "HojaDeRuta.pdf";
        //        string reportFilePath = "~/Reports/HojaDeRuta.rdl";
        //        var reportType = ReportType.PDF;
        //        var contentType = string.Format("application/{0}", reportType.ToString().ToLower());

        //        List<ReportDataSource> dataSources = new List<ReportDataSource>();

        //        dataSources.Add(new ReportDataSource("tickets", tickets));
        //        var report = new ReportHelper();
        //        var reportParameters = new List<ReportParameter>();

        //        var fecha = new ReportParameter("fecha", DateTime.Now.ToShortDateString());
        //        reportParameters.Add(fecha);

        //        var result = report.RenderReport(Server.MapPath(reportFilePath), dataSources, reportParameters, reportType);
        //        Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", reportName));

        //        return File(result, contentType);

        //    }
        //    else
        //        return null;
        //}

        private IList<HojadeRutaReportModel> GetHojaDeRuta()
        {
            return (from x in Model.OrdenesDelDia
                    select new HojadeRutaReportModel()
                    {
                        Ticket = x.NroOrden,
                        Nombre = x.Cliente.Apellido?.ToUpper() + " " + x.Cliente.Nombre.ToUpper(),
                        Telefonos = x.Cliente?.Telefono + " " + x.Cliente.Celular,
                        Modelo = x.Modelo?.ToUpper(),
                        Serie = x.NroSerie?.ToUpper(),
                        Marca = x.MarcaDesc?.ToUpper(),
                        Direccion = x.Cliente.Direccion?.ToUpper(),
                        Calle = x.Cliente.Dir.Calle?.ToUpper(),
                        Altura = x.Cliente.Dir.Altura?.ToUpper(),
                        Calle2 = x.Cliente.Dir.Calle2?.ToUpper(),
                        Calle3 = x.Cliente.Dir.Calle3?.ToUpper(),
                        Ciudad = x.Cliente.Dir.Ciudad?.ToUpper(),
                        CodigoPostal = x.Cliente.Dir.CodigoPostal?.ToUpper(),
                        Garantia = x.Garantia ? "E" : "C"
                    }).ToList();
        }
    }
}