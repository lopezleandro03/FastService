using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using AspNetCore.Reporting;
using System.Text;

namespace ReportService
{
    public class Imprimir
    {
        public string ReporteDorsoPath = Environment.GetEnvironmentVariable("ReporteDorsoPath");

        [FunctionName("ImprimirDorso")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {

                ReporteDorsoPath = ReporteDorsoPath ?? "ReciboDorso.rdlc";

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                string mimtype = "";
                int extension = 1;

                Dictionary<string, string> parameters = new Dictionary<string, string>();
                //parameters.Add("rp1", "ASP.NET CORE RDLC Report");
                LocalReport localReport = new LocalReport(ReporteDorsoPath);
                var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);

                return new FileContentResult(result.MainStream, "application/pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
