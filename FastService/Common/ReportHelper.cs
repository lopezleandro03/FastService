//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Reporting.WebForms;

//namespace FastService.Common
//{
//    public class ReportHelper
//    {
//        public byte[] RenderReport(string reportFilePath, List<ReportDataSource> reportDataSources, List<ReportParameter> reportParameters, ReportType reportType)
//        {
//            LocalReport report = new LocalReport()
//            {
//                ReportPath = reportFilePath
//            };

//            if (reportDataSources != null)
//            {
//                foreach (var dataSource in reportDataSources)
//                {
//                    report.DataSources.Add(dataSource);
//                }
//            }

//            if (reportParameters != null)
//            {
//                report.SetParameters(reportParameters);
//            }

//            string mimeType;
//            string encoding;
//            string fileNameExtension;
//            string[] streams;
//            Warning[] warnings;
//            string deviceInfo = "<DeviceInfo><OutputFormat>" + reportType.ToString() + "</OutputFormat></DeviceInfo>";
//            report.EnableHyperlinks = true;

//            var result = report.Render(
//            reportType.ToString(),
//            deviceInfo,
//            out mimeType,
//            out encoding,
//            out fileNameExtension,
//            out streams,
//            out warnings);

//            return result;
//        }
//    }

//    public enum ReportType
//    {
//        PDF,
//        Excel
//    }
//}
