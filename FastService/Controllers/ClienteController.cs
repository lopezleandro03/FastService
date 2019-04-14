using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Controllers
{
    public class ClienteController : BaseController
    {
        public FastServiceEntities _db { get; set; }

        // GET: Cliente
        public ClienteController()
        {
            this._db = new FastServiceEntities();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Historia()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult GetForSearch(string criteria)
        {
            var result = new List<ClienteModel>();

            if (String.IsNullOrEmpty(criteria))
            {
                result = (from x in _db.Cliente
                          select new ClienteModel()
                          {
                              ClienteId = x.ClienteId,
                              Dni = x.Dni,
                              Nombre = x.Nombre,
                              Apellido = x.Apellido,
                              Mail = x.Mail,
                              Direccion = x.Direccion,
                              Telefono = x.Telefono1,
                              Celular = x.Telefono2,
                              Dir = new DireccionModel()
                              {
                                  Calle = x.Direccion1.Calle,
                                  Altura = x.Direccion1.Altura,
                                  Calle2 = x.Direccion1.Calle2,
                                  Calle3 = x.Direccion1.Calle3,
                                  Ciudad = x.Direccion1.Ciudad,
                                  CodigoPostal = x.Direccion1.CodigoPostal,
                                  Provincia = x.Direccion1.Provincia,
                                  Pais = x.Direccion1.Pais,
                                  Longitud = x.Direccion1.Longitud,
                                  Latitud = x.Direccion1.Latitud
                              }
                          }).OrderByDescending(x => x.ClienteId).Take(200).ToList();
            }
            else
            {
                criteria = criteria.Contains("  ") ? criteria.Replace("  ", " ").ToLower() : criteria.ToLower();

                //if contains spaces, then search by name AND surname
                if (criteria.Trim().Contains(" "))
                {
                    var apellido = criteria.Split(' ')[0];
                    var nombre = criteria.Split(' ')[1];

                    result = (from x in _db.Cliente
                              where (x.Nombre.ToLower().Contains(nombre)
                                 && x.Apellido.ToLower().Contains(apellido))
                                 || x.Direccion.ToLower().Contains(criteria.Trim())
                              select new ClienteModel()
                              {
                                  ClienteId = x.ClienteId,
                                  Dni = x.Dni,
                                  Nombre = x.Nombre,
                                  Apellido = x.Apellido,
                                  Mail = x.Mail,
                                  Direccion = x.Direccion,
                                  Telefono = x.Telefono1,
                                  Celular = x.Telefono2,
                                  Dir = new DireccionModel()
                                  {
                                      Calle = x.Direccion1.Calle,
                                      Altura = x.Direccion1.Altura,
                                      Calle2 = x.Direccion1.Calle2,
                                      Calle3 = x.Direccion1.Calle3,
                                      Ciudad = x.Direccion1.Ciudad,
                                      CodigoPostal = x.Direccion1.CodigoPostal,
                                      Provincia = x.Direccion1.Provincia,
                                      Pais = x.Direccion1.Pais,
                                      Longitud = x.Direccion1.Longitud,
                                      Latitud = x.Direccion1.Latitud
                                  }
                              }).OrderBy(x => x.Nombre).Take(150).ToList();
                }
                //else try match name OR surname
                else
                {
                    result = (from x in _db.Cliente
                              where x.Dni.ToString().Contains(criteria)
                                 || x.Nombre.ToLower().Contains(criteria)
                                 || x.Apellido.ToLower().Contains(criteria)
                                 || x.Direccion.ToLower().Contains(criteria.Trim())
                                 || x.Telefono1.ToLower().Contains(criteria.Trim())
                              select new ClienteModel()
                              {
                                  ClienteId = x.ClienteId,
                                  Dni = x.Dni,
                                  Nombre = x.Nombre,
                                  Apellido = x.Apellido,
                                  Mail = x.Mail,
                                  Direccion = x.Direccion,
                                  Telefono = x.Telefono1,
                                  Celular = x.Telefono2,
                                  Dir = new DireccionModel()
                                  {
                                      Calle = x.Direccion1.Calle,
                                      Altura = x.Direccion1.Altura,
                                      Calle2 = x.Direccion1.Calle2,
                                      Calle3 = x.Direccion1.Calle3,
                                      Ciudad = x.Direccion1.Ciudad,
                                      CodigoPostal = x.Direccion1.CodigoPostal,
                                      Provincia = x.Direccion1.Provincia,
                                      Pais = x.Direccion1.Pais,
                                      Longitud = x.Direccion1.Longitud,
                                      Latitud = x.Direccion1.Latitud
                                  }
                              }).OrderBy(x => x.Nombre).Take(150).ToList();
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHistory(int id)
        {
            var result = new List<ClienteHistoriaModel>();

            result = (from x in _db.Reparacion
                      where x.ClienteId == id
                      select new ClienteHistoriaModel
                      {
                          NroOrden = x.ReparacionId,
                          EstadoDesc = x.EstadoReparacion.nombre,
                          EstadoFecha = x.ModificadoEn,
                          MarcaDesc = x.Marca.nombre,
                          Modelo = x.ReparacionDetalle.Modelo,
                          Monto = x.ReparacionDetalle.Precio,
                          NroSerie = x.ReparacionDetalle.Serie,
                          TecnicoNombre = x.Usuario1.Nombre,
                          TipoDesc = x.TipoDispositivo.nombre
                      }).OrderByDescending(x=>x.NroOrden).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Get(string ClienteId)
        {
            var cliente = _db.Cliente.Where(x => x.Dni.ToString() == ClienteId).FirstOrDefault();

            var model = new ClienteModel()
            {
                Dni = cliente.Dni,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono1,
                Celular = cliente.Telefono2,
                Direccion = cliente.Direccion,
                Mail = cliente.Mail
            };

            if (cliente.Direccion1 != null)
            {
                model.Calle = cliente.Direccion1.Calle;
                model.Altura = cliente.Direccion1.Altura;
                model.Calle2 = cliente.Direccion1.Calle2;
                model.Calle3 = cliente.Direccion1.Calle3;
                model.CodigoPostal = cliente.Direccion1.CodigoPostal;
                model.Provincia = cliente.Direccion1.Provincia;
                model.Pais = cliente.Direccion1.Pais;
                model.Latitud = cliente.Direccion1.Latitud;
                model.Longitud = cliente.Direccion1.Longitud;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            var result = new List<ClienteModel>();

            Prefix = Prefix.Contains("  ") ? Prefix.Replace("  ", " ").ToLower() : Prefix.ToLower();

            //if contains spaces, then search by name AND surname
            if (Prefix.Trim().Contains(" "))
            {

                var apellido = Prefix.Split(' ')[0];
                var nombre = Prefix.Split(' ')[1];

                result = (from x in _db.Cliente
                          where (x.Nombre.ToLower().Contains(nombre)
                             && x.Apellido.ToLower().Contains(apellido))
                             || x.Direccion.ToLower().Contains(Prefix.Trim())
                          select new ClienteModel() { Dni = x.Dni, Nombre = x.Nombre, Apellido = x.Apellido, Direccion = x.Direccion }).OrderBy(x => x.Nombre).Take(18).ToList();
            }
            //else try match name OR surname
            else
            {
                result = (from x in _db.Cliente
                          where x.Dni.ToString().Contains(Prefix)
                             || x.Nombre.ToLower().Contains(Prefix)
                             || x.Apellido.ToLower().Contains(Prefix)
                             || x.Direccion.ToLower().Contains(Prefix.Trim())
                          select new ClienteModel() { Dni = x.Dni, Nombre = x.Nombre, Apellido = x.Apellido, Direccion = x.Direccion }).OrderBy(x => x.Nombre).Take(18).ToList();
            }

            var clienteList = (from N in result
                               select new { N.DisplayNameAndAddress, N.Dni }
                                 ).ToList();

            return Json(clienteList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Buscar()
        {
            return PartialView();
        }
    }
}
