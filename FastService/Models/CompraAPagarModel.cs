using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class CompraAPagarModel : Compra
    {
        public decimal? Saldo { get; set; }
    }
}