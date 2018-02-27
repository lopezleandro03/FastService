using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastService.Models.Charts
{
    public interface IDrawable
    {
        AccountingSummary ResumenDiario { get; set; }
        AccountingSummary ResumenSemanal { get; set; }
        AccountingSummary ResumenMensual { get; set; }
        AccountingSummary ResumenAnual { get; set; }
        AccountingSummary ResumenActivo { get; set; }
    }
}
