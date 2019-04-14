using FastService.Models;
using FastService.Models.Orden;
using FastService.Models.Reports;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace FastService.Common
{
    public class ClienteHelper
    {
        private FastServiceEntities _db { get; set; }

        public ClienteHelper()
        {
            _db = new FastServiceEntities();
        }

        public ClienteModel GetClient(int id) {
            return (from x in _db.Cliente
                    where x.ClienteId == id
                    select new ClienteModel
                    {
                        ClienteId = x.ClienteId,
                        Dni = x.Dni,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        Mail = x.Mail,
                        Direccion = x.Direccion,
                        Telefono = x.Telefono1,
                        Altura = x.Direccion1.Altura,
                        Calle = x.Direccion1.Calle,
                        Calle2 = x.Direccion1.Calle2,
                        Calle3 = x.Direccion1.Calle3,
                        Celular = x.Telefono2,
                        CodigoPostal = x.Direccion1.CodigoPostal,
                        Provincia = x.Direccion1.Provincia,
                        Latitud = x.Direccion1.Latitud,
                        Longitud = x.Direccion1.Longitud,
                        Pais = x.Direccion1.Pais,
                        Dir = (from y in _db.Direccion where y.DireccionId == x.DireccionId select new DireccionModel()
                        {
                            DireccionId = y.DireccionId,
                            Altura = y.Altura,
                            Calle = y.Calle,
                            Calle2 = y.Calle2,
                            Calle3 = y.Calle3,
                            CodigoPostal = y.CodigoPostal,
                            Provincia = y.Provincia,
                            Latitud = y.Latitud,
                            Longitud = y.Longitud,
                            Pais = y.Pais,
                            Ciudad = y.Ciudad
                        }).FirstOrDefault()
                    }).FirstOrDefault();
        }
    }

}