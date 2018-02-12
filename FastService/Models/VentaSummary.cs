namespace FastService.Models
{
    public class VentaSummary
    {
        public AccountingSummary ResumenDiario { get; set; }
        public AccountingSummary ResumenSemanal { get; set; }
        public AccountingSummary ResumenMensual { get; set; }
        public AccountingSummary ResumenAnual { get; set; }
        public AccountingSummary ResumenActivo { get; set; }

        public VentaSummary(char defaultPeriod = 'm')
        {
            ResumenDiario = new AccountingSummary();
            ResumenSemanal = new AccountingSummary();
            ResumenMensual = new AccountingSummary();
            ResumenAnual = new AccountingSummary();
            ResumenActivo = new AccountingSummary();

            ResumenDiario.Load('d');
            ResumenSemanal.Load('d');
            ResumenMensual.Load('m');
            ResumenAnual.Load('y');

            switch (defaultPeriod)
            {
                case 'd':
                    ResumenActivo = ResumenDiario;
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