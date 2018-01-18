﻿using FastService.Models;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastService.Common
{
    public class OrdenHelper
    {
        private FastServiceEntities _db { get; set; }

        public OrdenHelper()
        {
            _db = new FastServiceEntities();
        }

        public List<OrdenModel> GetOrdenes(bool OnlyMyOrders, string searchCriteria, int currentUserId)
        {
            var Ordenes = new List<OrdenModel>();

            if (!string.IsNullOrEmpty(searchCriteria) && !string.IsNullOrWhiteSpace(searchCriteria))
            {
                Ordenes = (from r in _db.Reparacion
                           where (r.Cliente.Dni.ToString() == searchCriteria.Trim()
                           || r.Cliente.Mail == searchCriteria.Trim()
                           || r.Cliente.Nombre == searchCriteria.Trim()
                           || r.Cliente.Apellido == searchCriteria.Trim()
                           || r.ReparacionId.ToString() == searchCriteria.Trim()
                           || r.Marca.nombre == searchCriteria.Trim()
                           || r.TipoDispositivo.nombre == searchCriteria.Trim()
                           || r.Usuario.Nombre == searchCriteria.Trim()
                           || r.Usuario.Apellido == searchCriteria.Trim()
                           || r.Usuario.Email == searchCriteria.Trim()
                           || r.Usuario1.Nombre == searchCriteria.Trim()
                           || r.Usuario1.Apellido == searchCriteria.Trim()
                           || r.Usuario1.Email == searchCriteria.Trim())
                           select new OrdenModel()
                           {
                               NroOrden = r.ReparacionId,
                               Garantia = r.ReparacionDetalle.EsGarantia,
                               Domicilio = r.ReparacionDetalle.EsDomicilio,
                               EstadoCodigo = r.EstadoReparacionId,
                               EstadoDesc = r.EstadoReparacion.nombre,
                               EstadoFecha = r.ModificadoEn,

                               ResponsableId = r.EmpleadoAsignadoId,
                               ResponsableNombre = r.Usuario.Nombre,
                               TecnicoId = r.EmpleadoAsignadoId,
                               TecnicoNombre = r.Usuario1.Nombre,

                               MarcaId = r.MarcaId,
                               MarcaDesc = r.Marca.nombre,

                               TipoId = r.TipoDispositivoId,
                               TipoDesc = r.TipoDispositivo.nombre,
                               Modelo = r.ReparacionDetalle.Modelo,
                               NroSerie = r.ReparacionDetalle.Serie,

                               Comercio = r.Comercio,

                               Cliente = new ClienteModel()
                               {
                                   Dni = r.Cliente.Dni,
                                   Nombre = r.Cliente.Nombre,
                                   Apellido = r.Cliente.Apellido,
                                   Mail = r.Cliente.Mail,
                                   Direccion = r.Cliente.Direccion,
                                   Dir = (from d in _db.Direccion
                                          where d.DireccionId == r.Cliente.DireccionId
                                          select new DireccionModel()
                                          {
                                              Calle = d.Calle,
                                              Altura = d.Altura,
                                              Ciudad = d.Ciudad,
                                              CodigoPostal = d.CodigoPostal,
                                              Provincia = d.Provincia,
                                              Pais = d.Pais,
                                              Latitud = d.Latitud,
                                              Longitud = d.Longitud,
                                              ChangedOn = d.ChangedOn,
                                              ChangedBy = d.ChangedBy
                                          }).FirstOrDefault(),
                                   Telefono = r.Cliente.Telefono1,
                                   Celular = r.Cliente.Telefono2,

                               }

                           })?.OrderByDescending(x => x.NroOrden)?.Take(20)?.ToList();

            }
            else
            {
                if (OnlyMyOrders)
                {
                    Ordenes = (from r in _db.Reparacion
                               where r.Usuario.UserId == currentUserId
                                  || r.Usuario1.UserId == currentUserId
                               select new OrdenModel()
                               {
                                   NroOrden = r.ReparacionId,
                                   Garantia = r.ReparacionDetalle.EsGarantia,
                                   Domicilio = r.ReparacionDetalle.EsDomicilio,
                                   EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                                   EstadoFecha = r.ModificadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.TecnicoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       Dni = r.Cliente.Dni,
                                       Nombre = r.Cliente.Nombre,
                                       Apellido = r.Cliente.Apellido,
                                       Mail = r.Cliente.Mail,
                                       Direccion = r.Cliente.Direccion,
                                       Dir = (from d in _db.Direccion
                                              where d.DireccionId == r.Cliente.DireccionId
                                              select new DireccionModel()
                                              {
                                                  Calle = d.Calle,
                                                  Altura = d.Altura,
                                                  Ciudad = d.Ciudad,
                                                  CodigoPostal = d.CodigoPostal,
                                                  Provincia = d.Provincia,
                                                  Pais = d.Pais,
                                                  Latitud = d.Latitud,
                                                  Longitud = d.Longitud,
                                                  ChangedOn = d.ChangedOn,
                                                  ChangedBy = d.ChangedBy
                                              }).ToList().FirstOrDefault(),
                                       Telefono = r.Cliente.Telefono1,
                                       Celular = r.Cliente.Telefono2
                                   }

                               }).OrderByDescending(x => x.NroOrden).Take(30).ToList();
                }
                else
                {
                    Ordenes = (from r in _db.Reparacion
                               select new OrdenModel()
                               {
                                   NroOrden = r.ReparacionId,
                                   Garantia = r.ReparacionDetalle.EsGarantia,
                                   Domicilio = r.ReparacionDetalle.EsDomicilio,
                                   EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                                   EstadoFecha = r.ModificadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.TecnicoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       Dni = r.Cliente.Dni,
                                       Nombre = r.Cliente.Nombre,
                                       Apellido = r.Cliente.Apellido,
                                       Mail = r.Cliente.Mail,
                                       Direccion = r.Cliente.Direccion,
                                       Dir = (from d in _db.Direccion
                                              where d.DireccionId == r.Cliente.DireccionId
                                              select new DireccionModel()
                                              {
                                                  Calle = d.Calle,
                                                  Altura = d.Altura,
                                                  Ciudad = d.Ciudad,
                                                  CodigoPostal = d.CodigoPostal,
                                                  Provincia = d.Provincia,
                                                  Pais = d.Pais,
                                                  Latitud = d.Latitud,
                                                  Longitud = d.Longitud,
                                                  ChangedOn = d.ChangedOn,
                                                  ChangedBy = d.ChangedBy
                                              }).ToList().FirstOrDefault(),
                                       Telefono = r.Cliente.Telefono1,
                                       Celular = r.Cliente.Telefono2
                                   }

                               }).OrderByDescending(x => x.NroOrden).Take(20).ToList();
                }
            }

            foreach (var item in Ordenes)
                item.Novedades = GetNovedades(item.NroOrden);

            return Ordenes;
        }

        public List<OrdenModel> GetOrdenesADomicilio()
        {
            var Ordenes = new List<OrdenModel>();

            Ordenes = (from r in _db.Reparacion
                       where r.ReparacionDetalle.EsDomicilio == true
                       select new OrdenModel()
                       {
                           NroOrden = r.ReparacionId,
                           Garantia = r.ReparacionDetalle.EsGarantia,
                           Domicilio = r.ReparacionDetalle.EsDomicilio,
                           EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                           EstadoFecha = r.ModificadoEn,

                           ResponsableId = r.EmpleadoAsignadoId,
                           ResponsableNombre = r.Usuario.Nombre,
                           TecnicoId = r.TecnicoAsignadoId,
                           TecnicoNombre = r.Usuario1.Nombre,

                           MarcaId = r.MarcaId,
                           MarcaDesc = r.Marca.nombre,

                           TipoId = r.TipoDispositivoId,
                           TipoDesc = r.TipoDispositivo.nombre,
                           Modelo = r.ReparacionDetalle.Modelo,
                           NroSerie = r.ReparacionDetalle.Serie,

                           Comercio = r.Comercio,

                           Cliente = new ClienteModel()
                           {
                               Dni = r.Cliente.Dni,
                               Nombre = r.Cliente.Nombre,
                               Apellido = r.Cliente.Apellido,
                               Mail = r.Cliente.Mail,
                               Direccion = r.Cliente.Direccion,
                               Dir = (from d in _db.Direccion
                                      where d.DireccionId == r.Cliente.DireccionId
                                      select new DireccionModel()
                                      {
                                          Calle = d.Calle,
                                          Altura = d.Altura,
                                          Ciudad = d.Ciudad,
                                          CodigoPostal = d.CodigoPostal,
                                          Provincia = d.Provincia,
                                          Pais = d.Pais,
                                          Latitud = d.Latitud,
                                          Longitud = d.Longitud,
                                          ChangedOn = d.ChangedOn,
                                          ChangedBy = d.ChangedBy
                                      }).ToList().FirstOrDefault(),
                               Telefono = r.Cliente.Telefono1,
                               Celular = r.Cliente.Telefono2
                           }

                       }).OrderByDescending(x => x.NroOrden).ToList();

            return Ordenes;
        }

            private List<NovedadModel> GetNovedades(int reparacionId)
        {
            return (from n in _db.Novedad
                    where n.reparacionId == reparacionId
                    select new NovedadModel()
                    {
                        Id = n.novedadId,
                        Fecha = n.modificadoEn,
                        Observacion = n.observacion,
                        Descripcion = "Not Mapped",
                        Monto = n.monto
                    })?.OrderByDescending(x => x.Fecha)?.ToList();
        }
    }
}