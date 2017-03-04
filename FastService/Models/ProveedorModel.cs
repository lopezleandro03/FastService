﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Models
{
    public class ProveedorModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string DisplayName
        {
            get
            {
                return string.Format("[{0}] {1}", Id, Nombre);
            }
            set
            {
            }
        }
    }
}