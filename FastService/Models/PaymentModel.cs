using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace FastService.Models
{
    public class PaymentModel
    {
        [Required(ErrorMessage = "El DNI del cliente es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ingrese solo números")]
        [DisplayName("DNI del cliente")]
        public string customerId { get; set; }
        [Required(ErrorMessage = "Nombre y Apellido del cliente es obligatorio")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Ingrese solo letras o números")]
        [DisplayName("Nombre y Apellido")]
        public string customerName { get; set; }
        [Required(ErrorMessage = "El monto total a pagar es obligatorio")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ingrese solo números")]
        [DisplayName("Total a pagar")]
        public string paymentNetAmount { get; set; }
        [StringLength(1024)]
        [DisplayName("Numero de orden")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Ingrese solo números")]
        [Required(ErrorMessage = "El numero de orden es obligatorio")]
        public string serviceId { get; set; }
        public string paymentLink { get; set; }

        [DisplayName("Mail del cliente")]
        [EmailAddress(ErrorMessage = "Direccion de Mail invalida")]
        public string customerMail { get; set; }
        [DisplayName("Cuenta Mercado Pago")]
        public string MercadoPagoAcc { get; set; }

        public string ToString()
        {
            StringBuilder str = new StringBuilder("Payment Information");
            str.AppendLine();
            str.AppendLine();
            str.Append(string.Format("Customer ID:{0}", customerId ?? "NULL"));
            str.AppendLine();
            str.Append(string.Format("Customer Name:{0}", customerName ?? "NULL"));
            str.AppendLine();
            str.Append(string.Format("Customer Mail:{0}", customerMail ?? "NULL"));
            str.AppendLine();
            str.Append(string.Format("Payment Amount:{0}", paymentNetAmount ?? "NULL"));
            str.AppendLine();
            str.Append(string.Format("Service ID:{0}", serviceId ?? "NULL"));
            str.AppendLine();
            str.Append(string.Format("Payment Link:{0}", paymentLink ?? "Not generated"));

            return str.ToString();
        }
    }
}