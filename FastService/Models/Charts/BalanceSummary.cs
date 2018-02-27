using FastService.Models.Charts;

namespace FastService.Models
{
    public class BalanceSummary : IDrawable
    {
        public AccountingSummary ResumenDiario { get; set; }
        public AccountingSummary ResumenSemanal { get; set; }
        public AccountingSummary ResumenMensual { get; set; }
        public AccountingSummary ResumenAnual { get; set; }
        public AccountingSummary ResumenActivo { get; set; }

        public BalanceSummary(char defaultPeriod = 'm')
        {
            ResumenDiario = new AccountingSummary();
            ResumenSemanal = new AccountingSummary();
            ResumenMensual = new AccountingSummary();
            ResumenAnual = new AccountingSummary();
            ResumenActivo = new AccountingSummary();

            ResumenDiario.LoadVentas('d');
            ResumenSemanal.LoadVentas('w');
            ResumenMensual.LoadVentas('m');
            ResumenAnual.LoadVentas('y');

            ResumenDiario.LoadCompras('d');
            ResumenSemanal.LoadCompras('w');
            ResumenMensual.LoadCompras('m');
            ResumenAnual.LoadCompras('y');

            switch (defaultPeriod)
            {
                case 'd':
                    ResumenActivo = ResumenDiario;
                    break;
                case 'w':
                    ResumenActivo = ResumenSemanal;
                    break;
                case 'm':
                    ResumenActivo = ResumenMensual;
                    break;
                case 'y':
                    ResumenActivo = ResumenAnual;
                    break;
                default:
                    break;
            }
        }
    }
}