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
                if (OnlyMyOrders)
                {
                    Ordenes = (from r in _db.Reparacion
                               where r.TecnicoAsignadoId == currentUserId
                               && (r.Cliente.Dni.ToString() == searchCriteria.Trim()
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
                               || r.Usuario1.Email == searchCriteria.Trim()
                               || r.ReparacionDetalle.Serie == searchCriteria.Trim()
                               || r.ReparacionDetalle.Serbus == searchCriteria.Trim()
                               || r.ReparacionDetalle.NroReferencia == searchCriteria.Trim()
                               || r.Comercio.Code == searchCriteria.Trim()
                               || r.Comercio.Descripcion == searchCriteria.Trim())
                               select new OrdenModel()
                               {
                                   NroOrden = r.ReparacionId,
                                   Garantia = r.ReparacionDetalle.EsGarantia,
                                   Domicilio = r.ReparacionDetalle.EsDomicilio,
                                   EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre,
                                   EstadoFecha = r.ModificadoEn,
                                   InformadoEn = r.InformadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.EmpleadoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                                   Monto = r.ReparacionDetalle.Precio ?? 0,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,
                                   Ubicacion = r.ReparacionDetalle.Unicacion,
                                   FechaCompra = r.ReparacionDetalle.FechoCompra,
                                   ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                                   Accesorios = r.ReparacionDetalle.Accesorios,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       ClienteId = r.Cliente.ClienteId,
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
                                                  Calle2 = d.Calle2,
                                                  Calle3 = d.Calle3,
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

                               })?.OrderByDescending(x => x.EstadoFecha).Take(50)?.ToList();
                }
                else
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
                               || r.ReparacionDetalle.Serie == searchCriteria.Trim()
                               || r.ReparacionDetalle.Serbus == searchCriteria.Trim()
                               || r.ReparacionDetalle.NroReferencia == searchCriteria.Trim()
                               || r.Comercio.Code == searchCriteria.Trim()
                               || r.Comercio.Descripcion == searchCriteria.Trim()
                               select new OrdenModel()
                               {
                                   NroOrden = r.ReparacionId,
                                   Garantia = r.ReparacionDetalle.EsGarantia,
                                   Domicilio = r.ReparacionDetalle.EsDomicilio,
                                   EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre,
                                   EstadoFecha = r.ModificadoEn,
                                   InformadoEn = r.InformadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.EmpleadoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                                   Monto = r.ReparacionDetalle.Precio ?? 0,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,
                                   Ubicacion = r.ReparacionDetalle.Unicacion,
                                   FechaCompra = r.ReparacionDetalle.FechoCompra,
                                   ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                                   Accesorios = r.ReparacionDetalle.Accesorios,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       ClienteId = r.Cliente.ClienteId,
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
                                                  Calle2 = d.Calle2,
                                                  Calle3 = d.Calle3,
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

                               })?.OrderByDescending(x => x.EstadoFecha).Take(50)?.ToList();
                }
            }
            else
            {
                if (OnlyMyOrders)
                {
                    Ordenes = (from r in _db.Reparacion
                               where r.Usuario1.UserId == currentUserId
                                  && r.EstadoReparacion.nombre != ReparacionEstado.CANCELADO.ToString()
                                  && r.EstadoReparacion.nombre != ReparacionEstado.RECHAZADO.ToString()
                                  && r.EstadoReparacion.nombre != ReparacionEstado.RETIRADO.ToString()
                                  && r.EstadoReparacion.nombre != ReparacionEstado.RECHAZOPRESUP.ToString()
                               select new OrdenModel()
                               {
                                   NroOrden = r.ReparacionId,
                                   Garantia = r.ReparacionDetalle.EsGarantia,
                                   Domicilio = r.ReparacionDetalle.EsDomicilio,
                                   EstadoCodigo = r.EstadoReparacionId,
                                   EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                                   EstadoFecha = r.ModificadoEn,
                                   InformadoEn = r.InformadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.TecnicoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                                   Monto = r.ReparacionDetalle.Precio ?? 0,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,
                                   Ubicacion = r.ReparacionDetalle.Unicacion,
                                   FechaCompra = r.ReparacionDetalle.FechoCompra,
                                   ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                                   Accesorios = r.ReparacionDetalle.Accesorios,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       ClienteId = r.Cliente.ClienteId,
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
                                                  Calle2 = d.Calle2,
                                                  Calle3 = d.Calle3,
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

                               })?.OrderByDescending(x => x.EstadoFecha).Take(50)?.ToList();
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
                                   InformadoEn = r.InformadoEn,

                                   ResponsableId = r.EmpleadoAsignadoId,
                                   ResponsableNombre = r.Usuario.Nombre,
                                   TecnicoId = r.TecnicoAsignadoId,
                                   TecnicoNombre = r.Usuario1.Nombre,

                                   Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                                   Monto = r.ReparacionDetalle.Precio ?? 0,

                                   MarcaId = r.MarcaId,
                                   MarcaDesc = r.Marca.nombre,

                                   TipoId = r.TipoDispositivoId,
                                   TipoDesc = r.TipoDispositivo.nombre,
                                   Modelo = r.ReparacionDetalle.Modelo,
                                   NroSerie = r.ReparacionDetalle.Serie,
                                   Ubicacion = r.ReparacionDetalle.Unicacion,
                                   FechaCompra = r.ReparacionDetalle.FechoCompra,
                                   ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                                   Accesorios = r.ReparacionDetalle.Accesorios,

                                   Comercio = r.Comercio,

                                   Cliente = new ClienteModel()
                                   {
                                       ClienteId = r.Cliente.ClienteId,
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
                                                  Calle2 = d.Calle2,
                                                  Calle3 = d.Calle3,
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

                               })?.OrderByDescending(x => x.EstadoFecha).Take(50)?.ToList();
                }
            }

            ////var cacheNov = (from o in Ordenes join n in _db.Novedad on o.NroOrden equals n.reparacionId select n).ToList();
            //Dictionary<int, string> dicTipoNov = (from x in _db.TipoNovedad select new { x.TipoNovedadId, x.nombre }).ToDictionary(k => k.TipoNovedadId, v => v.nombre);

            //foreach (var orden in Ordenes)
            //{
            //    orden.Novedades = (from n in _db.Novedad
            //                       where n.reparacionId == orden.NroOrden
            //                       select new NovedadModel()
            //                       {
            //                           Id = n.novedadId,
            //                           Fecha = n.modificadoEn,
            //                           Observacion = n.observacion,
            //                           TipoNovedadId = n.tipoNovedadId,
            //                           Monto = n.monto
            //                       })?.ToList();

            //    orden.Novedades.Select(x => x.Descripcion = dicTipoNov[x.TipoNovedadId]);
            //}

            return Ordenes;
        }

        public List<OrdenSearchModel> GetOrdersNro(string prefix)
        {
            return (from x in _db.Reparacion
                    where (x.ReparacionId.ToString().Contains(prefix)
                    || x.Cliente.Nombre.Contains(prefix)
                    || x.Cliente.Apellido.Contains(prefix))
                    select new OrdenSearchModel()
                    {
                        NroOrden = x.ReparacionId,
                        Nombre = x.Cliente.Nombre,
                        Apellido = x.Cliente.Apellido
                    }).OrderByDescending(x => x.NroOrden).Take(30).ToList();

        }

        public int GetNextOrderNro()
        {
            return _db.Reparacion.Max(x => x.ReparacionId) + 1;

        }

        public void Save(NovedadModel model, int CurrentUserId)
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadCommitted;
            opt.Timeout = TimeSpan.MaxValue;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                try
                {
                    if (model.Id == 0)
                    {
                        var tipoNovedad = model.TipoNovedadId;

                        if (tipoNovedad == (int)NovedadTipo.PRESUPINFOR)
                        {
                            if (model.Accion.ToUpper() == "ACEPTA")
                            {
                                tipoNovedad = (int)NovedadTipo.ACEPTA;
                            }
                            else if (model.Accion.ToUpper() == "RECHAZA")
                            {
                                tipoNovedad = (int)NovedadTipo.RECHAZA;
                            }
                            else
                            {
                                tipoNovedad = (int)NovedadTipo.PRESUPINFOR;
                            }
                        }

                        //add novedad
                        _db.Novedad.Add(new Novedad()
                        {
                            reparacionId = model.NroOrden,
                            observacion = model.Observacion,
                            monto = model.Monto,
                            tipoNovedadId = tipoNovedad,
                            UserId = CurrentUserId,
                            modificadoPor = CurrentUserId,
                            modificadoEn = DateTime.Now
                        });

                        //edit orden
                        var orden = _db.Reparacion.Find(model.NroOrden);
                        orden.TecnicoAsignadoId = model.TecnicoId;
                        orden.EmpleadoAsignadoId = model.ResponsableId;
                        orden.ReparacionDetalle.Presupuesto = model.Monto == 0 ? orden.ReparacionDetalle.Presupuesto : model.Monto;

                        ActualizarEstado(model, orden, CurrentUserId);
                    }
                    else
                    {
                        //edit novedad
                        var novedad = _db.Novedad.Find(model.Id);
                        novedad.monto = model.Monto;
                        novedad.observacion = model.Observacion;

                        //edit orden
                        var orden = _db.Reparacion.Find(model.NroOrden);
                        orden.TecnicoAsignadoId = model.TecnicoId;
                        orden.EmpleadoAsignadoId = model.ResponsableId;
                        orden.ReparacionDetalle.Presupuesto = model.Monto == 0 ? orden.ReparacionDetalle.Presupuesto : model.Monto;

                        //audit
                        novedad.modificadoPor = CurrentUserId;
                        novedad.modificadoEn = DateTime.Now;
                    }

                    _db.SaveChanges();
                    ts.Complete();

                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }

        public void Save(OrdenModel model, int CurrentUserId)
        {
            TransactionOptions opt = new TransactionOptions();
            opt.IsolationLevel = IsolationLevel.ReadCommitted;
            opt.Timeout = TimeSpan.MaxValue;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, opt))
            {
                try
                {
                    var cli = _db.Cliente.Where(x => x.ClienteId == model.Cliente.ClienteId).FirstOrDefault();
                    if (cli == null)
                    {
                        cli = new Cliente()
                        {
                            Dni = model.Cliente.Dni,
                            Nombre = model.Cliente.Nombre,
                            Apellido = model.Cliente.Apellido,
                            Telefono1 = model.Cliente.Telefono,
                            Telefono2 = model.Cliente.Celular,
                            Mail = model.Cliente.Mail,
                            Direccion = model.Cliente.Direccion,
                            Direccion1 = new Direccion()
                            {
                                Calle = model.Cliente.Dir.Calle,
                                Calle2 = model.Cliente.Dir.Calle2,
                                Calle3 = model.Cliente.Dir.Calle3,
                                Altura = model.Cliente.Dir.Altura,
                                Ciudad = model.Cliente.Dir.Ciudad,
                                CodigoPostal = model.Cliente.Dir.CodigoPostal,
                                Provincia = model.Cliente.Dir.Provincia,
                                Pais = model.Cliente.Dir.Pais,
                                Latitud = model.Cliente.Dir.Latitud,
                                Longitud = model.Cliente.Dir.Longitud,
                                ChangedBy = CurrentUserId,
                                ChangedOn = DateTime.Now
                            }
                        };

                        _db.Cliente.Add(cli);
                    }
                    else
                    {
                        cli.Dni = model.Cliente.Dni;
                        cli.Nombre = model.Cliente.Nombre;
                        cli.Apellido = model.Cliente.Apellido;
                        cli.Telefono1 = model.Cliente.Telefono;
                        cli.Telefono2 = model.Cliente.Celular;
                        cli.Mail = model.Cliente.Mail;
                        cli.Direccion = model.Cliente.Direccion;

                        if (cli.Direccion1 == null)
                            cli.Direccion1 = new Direccion();

                        cli.Direccion1.Calle = model.Cliente.Dir.Calle;
                        cli.Direccion1.Altura = model.Cliente.Dir.Altura;
                        cli.Direccion1.Calle2 = model.Cliente.Dir.Calle2;
                        cli.Direccion1.Calle3 = model.Cliente.Dir.Calle3;
                        cli.Direccion1.Ciudad = model.Cliente.Dir.Ciudad;
                        cli.Direccion1.CodigoPostal = model.Cliente.Dir.CodigoPostal;
                        cli.Direccion1.Provincia = model.Cliente.Dir.Provincia;
                        cli.Direccion1.Pais = model.Cliente.Dir.Pais;
                        cli.Direccion1.Latitud = model.Cliente.Dir.Latitud;
                        cli.Direccion1.Longitud = model.Cliente.Dir.Longitud;
                        cli.Direccion1.ChangedBy = CurrentUserId;
                        cli.Direccion1.ChangedOn = DateTime.Now;
                    }

                    var rep = _db.Reparacion.Find(model.NroOrden);
                    if (rep == null)
                    {
                        var repDet = new ReparacionDetalle()
                        {
                            EsGarantia = model.Garantia,
                            EsDomicilio = model.Domicilio,
                            NroReferencia = string.Empty,

                            Presupuesto = Convert.ToDecimal(model.Presupuesto),
                            PresupuestoFecha = DateTime.Now,
                            Precio = model.Monto,

                            Modelo = model.Modelo,
                            Serie = model.NroSerie,
                            Serbus = model.SerBus,
                            Unicacion = model.Ubicacion,
                            Accesorios = model.Accesorios,
                            FechoCompra = model.FechaCompra,

                            ModificadoEn = DateTime.Now,
                            ModificadoPor = CurrentUserId
                        };

                        _db.ReparacionDetalle.Add(repDet);

                        _db.SaveChanges();

                        var estados = _db.EstadoReparacion.Select(x => x);

                        var nuevaRep = new Reparacion()
                        {
                            ReparacionId = GetNextOrderNro(),
                            ReparacionDetalleId = repDet.ReparacionDetalleId,
                            ClienteId = cli.ClienteId,
                            EmpleadoAsignadoId = model.ResponsableId,
                            TecnicoAsignadoId = model.TecnicoId,
                            EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.INGRESADO).First().EstadoReparacionId,
                            ComercioId = model.Garantia ? (int?)model.Comercio.ComercioId : null,
                            MarcaId = model.MarcaId,
                            TipoDispositivoId = model.TipoId,
                            ModificadoEn = DateTime.Now,
                            ModificadoPor = CurrentUserId,
                            CreadoEn = DateTime.Now,
                            CreadoPor = CurrentUserId,
                        };

                        _db.Reparacion.Add(nuevaRep);
                        _db.SaveChanges();

                        //syc model id
                        model.NroOrden = nuevaRep.ReparacionId;

                        _db.Novedad.Add(new Novedad()
                        {
                            reparacionId = nuevaRep.ReparacionId,
                            monto = repDet.Presupuesto,
                            UserId = nuevaRep.TecnicoAsignadoId,
                            tipoNovedadId = (int)NovedadTipo.INGRESO,
                            observacion = "Ingresado a sistema",
                            modificadoEn = DateTime.Now,
                            modificadoPor = CurrentUserId,
                        });
                    }
                    else
                    {
                        rep.ClienteId = cli.ClienteId;
                        rep.EmpleadoAsignadoId = model.ResponsableId == 0 ? rep.EmpleadoAsignadoId : model.ResponsableId;
                        rep.TecnicoAsignadoId = model.TecnicoId == 0 ? rep.TecnicoAsignadoId : model.TecnicoId;
                        rep.EstadoReparacionId = model.EstadoCodigo == 0 ? rep.EstadoReparacionId : model.EstadoCodigo;
                        rep.ComercioId = model.Garantia ? (model.Comercio.ComercioId == 0 ? rep.ComercioId : model.Comercio.ComercioId) : null;
                        rep.MarcaId = model.MarcaId == 0 ? rep.MarcaId : model.MarcaId; ;
                        rep.TipoDispositivoId = model.TipoId == 0 ? rep.TipoDispositivoId : model.TipoId; ;

                        rep.ModificadoEn = DateTime.Now;
                        rep.ModificadoPor = CurrentUserId;

                        var repDet = _db.ReparacionDetalle.Find(rep.ReparacionDetalleId);

                        if (repDet != null)
                        {
                            repDet.EsGarantia = model.Garantia;
                            repDet.EsDomicilio = model.Domicilio;
                            repDet.NroReferencia = string.Empty;
                            repDet.Presupuesto = Convert.ToDecimal(model.Presupuesto);
                            repDet.PresupuestoFecha = DateTime.Now;
                            repDet.Precio = model.Monto;

                            repDet.Modelo = model.Modelo;
                            repDet.Serie = model.NroSerie;
                            repDet.Serbus = model.SerBus;
                            repDet.Unicacion = model.Ubicacion;
                            repDet.Accesorios = model.Accesorios;
                            repDet.FechoCompra = model.FechaCompra;

                            repDet.ModificadoEn = DateTime.Now;
                            repDet.ModificadoPor = CurrentUserId;
                        }
                    }

                    if (model.Garantia)
                    {
                        var com = _db.Comercio.Find(model.Comercio.ComercioId);

                        if (com != null)
                        {
                            com.Telefono = model.Comercio.Telefono;
                        }
                    }

                    _db.SaveChanges();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }

        public List<OrdenModel> GetOrdenesByEstado(string id, DateTime? desde, DateTime? hasta, int? tecnicoid, int? comercioid, int? responsableid)
        {
            desde = desde == null ? DateTime.Now.AddDays(-100) : (DateTime)desde;
            hasta = hasta == null ? DateTime.Now : (DateTime)hasta;
            var filterByTecnico = tecnicoid == null ? false : true;
            var filterByComercio = comercioid == null ? false : true;
            var filterByResponsable = responsableid == null ? false : true;

            var ordenes = (from r in _db.Reparacion
                           where r.EstadoReparacion.nombre.ToUpper() == id
                           && r.CreadoEn > desde
                           && r.CreadoEn < hasta
                           && (filterByTecnico == false || r.TecnicoAsignadoId == tecnicoid)
                           && (filterByComercio == false || r.ComercioId == comercioid)
                           && (filterByResponsable == false || r.EmpleadoAsignadoId == responsableid)
                           select new OrdenModel()
                           {
                               NroOrden = r.ReparacionId,
                               Garantia = r.ReparacionDetalle.EsGarantia,
                               Domicilio = r.ReparacionDetalle.EsDomicilio,
                               EstadoCodigo = r.EstadoReparacionId,
                               EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                               EstadoFecha = r.ModificadoEn,
                               InformadoEn = r.InformadoEn,

                               ResponsableId = r.EmpleadoAsignadoId,
                               ResponsableNombre = r.Usuario.Nombre,
                               TecnicoId = r.TecnicoAsignadoId,
                               TecnicoNombre = r.Usuario1.Nombre,

                               Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                               Monto = r.ReparacionDetalle.Precio ?? 0,

                               MarcaId = r.MarcaId,
                               MarcaDesc = r.Marca.nombre,

                               TipoId = r.TipoDispositivoId,
                               TipoDesc = r.TipoDispositivo.nombre,
                               Modelo = r.ReparacionDetalle.Modelo,
                               NroSerie = r.ReparacionDetalle.Serie,
                               Ubicacion = r.ReparacionDetalle.Unicacion,
                               FechaCompra = r.ReparacionDetalle.FechoCompra,
                               ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                               Accesorios = r.ReparacionDetalle.Accesorios,

                               Comercio = r.Comercio,

                               Cliente = new ClienteModel()
                               {
                                   ClienteId = r.Cliente.ClienteId,
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
                                              Calle3 = d.Calle3,
                                              Calle2 = d.Calle2,
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
                               },
                           }).OrderByDescending(x => x.NroOrden).Take(50).ToList();

            return ordenes;
        }

        public void ActualizarEstado(NovedadModel model, Reparacion orden, int currentUserId)
        {
            if (orden != null)
            {
                var estados = _db.EstadoReparacion.Select(x => x).ToList();
                var estadoAnterior = orden.EstadoReparacion.nombre;

                if (model.TipoNovedadId == (int)NovedadTipo.PRESUPINFOR)
                {
                    if (model.Accion.ToUpper() == "ACEPTA")
                    {
                        orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.AREPARAR).First().EstadoReparacionId;
                        orden.EmpleadoAsignadoId = orden.TecnicoAsignadoId;
                    }
                    else if (model.Accion.ToUpper() == "RECHAZA")
                    {
                        orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RECHAZOPRESUP).First().EstadoReparacionId;
                    }
                    else
                    {
                        orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.PRESUPUESTADO).First().EstadoReparacionId;
                    }

                    orden.ReparacionDetalle.PresupuestoFecha = DateTime.Now;
                    orden.InformadoEn = DateTime.Now;
                    orden.InformadoPor = currentUserId;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.REINGRESO)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.REINGRESADO).First().EstadoReparacionId;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.RECHAZA)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RECHAZADO).First().EstadoReparacionId;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.ESPERAREPUESTO)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.ESPREPUESTO).First().EstadoReparacionId;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.REPARADO)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.REPARADO).First().EstadoReparacionId;
                    orden.ReparacionDetalle.ReparacionDesc = model.Observacion;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.RETIRA)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RETIRADO).First().EstadoReparacionId;
                    orden.ReparacionDetalle.Precio = model.Monto;
                    GenerarContabilidad(model, orden, currentUserId);
                }

                if (model.TipoNovedadId == (int)NovedadTipo.SENA)
                {
                    GenerarContabilidad(model, orden, currentUserId);
                }

                if (model.TipoNovedadId == (int)NovedadTipo.REPDOMICILIO)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.RETIRADO).First().EstadoReparacionId;
                    orden.ReparacionDetalle.Precio = model.Monto;
                    GenerarContabilidad(model, orden, currentUserId);
                }

                if (model.TipoNovedadId == (int)NovedadTipo.ENTREGA)
                {
                    orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.PARAENTREGAR).First().EstadoReparacionId;
                    orden.FechaEntrega = model.FechaEntrega;
                    orden.ReparacionDetalle.Precio = model.Monto;
                }

                if (model.TipoNovedadId == (int)NovedadTipo.PRESUPUESTADO)
                {
                    if (model.Domicilio)
                        orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.PRESUPENDOMICILIO).First().EstadoReparacionId;
                    else
                        orden.EstadoReparacionId = estados.Where(x => x.nombre.ToUpper() == ReparacionEstado.PRESUPUESTADO).First().EstadoReparacionId;

                    orden.ReparacionDetalle.Presupuesto = model.Monto;

                }

                orden.ModificadoPor = currentUserId;
                orden.ModificadoEn = DateTime.Now;
            }
        }

        private void GenerarContabilidad(NovedadModel model, Reparacion orden, int currentUserId)
        {
            if (model.Monto > 0)
            {
                int? fac = null;

                if (model.Facturado)
                {
                    var factura = new Factura()
                    {
                        NroFactura = model.Factura,
                        TipoFacturaId = model.TipoFactura,
                        ModificadoEn = DateTime.Now,
                        ModificadoPor = currentUserId
                    };

                    _db.Factura.Add(factura);
                    _db.SaveChanges();

                    fac = factura.FacturaId;
                }

                var desc = model.TipoNovedadId == (int)NovedadTipo.SENA ? $"Seña por servicio de FastService orden {orden.ReparacionId}" : $"Pago por servicio de FastService orden {orden.ReparacionId}";

                var venta = new Venta()
                {
                    Descripcion = desc,
                    ClienteId = orden.ClienteId,
                    Monto = (decimal)orden.ReparacionDetalle.Precio,
                    RefNumber = orden.ReparacionId.ToString(), //guardo como referencia la orden de trabajo
                    PuntoDeVentaId = 1,
                    FacturaId = fac,
                    MetodoPagoId = model.MetodoDePagoId,
                    TipoTransaccionId = model.Facturado ? 1 : 2,

                    Fecha = DateTime.Now,
                    Vendedor = orden.EmpleadoAsignadoId
                };

                _db.Venta.Add(venta);
            }
        }

        public List<OrdenModel> GetOrdenesADomicilio()
        {
            var Ordenes = new List<OrdenModel>();

            Ordenes = (from r in _db.Reparacion
                       where (r.ReparacionDetalle.EsDomicilio == true || r.FechaEntrega != null)
                          && (r.EstadoReparacion.nombre == ReparacionEstado.INGRESADO
                          || r.EstadoReparacion.nombre == ReparacionEstado.REINGRESADO
                          || r.EstadoReparacion.nombre == ReparacionEstado.PARAENTREGAR)
                          && r.ReparacionId > 109800
                       select new OrdenModel()
                       {
                           NroOrden = r.ReparacionId,
                           Garantia = r.ReparacionDetalle.EsGarantia,
                           Domicilio = r.ReparacionDetalle.EsDomicilio,
                           EstadoCodigo = r.EstadoReparacionId,
                           EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                           EstadoFecha = r.ModificadoEn,
                           InformadoEn = r.InformadoEn,

                           ResponsableId = r.EmpleadoAsignadoId,
                           ResponsableNombre = r.Usuario.Nombre,
                           TecnicoId = r.TecnicoAsignadoId,
                           TecnicoNombre = r.Usuario1.Nombre,

                           Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                           Monto = r.ReparacionDetalle.Precio ?? 0,

                           MarcaId = r.MarcaId,
                           MarcaDesc = r.Marca.nombre,

                           TipoId = r.TipoDispositivoId,
                           TipoDesc = r.TipoDispositivo.nombre,
                           Modelo = r.ReparacionDetalle.Modelo,
                           NroSerie = r.ReparacionDetalle.Serie,
                           Ubicacion = r.ReparacionDetalle.Unicacion,
                           FechaCompra = r.ReparacionDetalle.FechoCompra,
                           ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                           Accesorios = r.ReparacionDetalle.Accesorios,

                           Comercio = r.Comercio,

                           Cliente = new ClienteModel()
                           {
                               ClienteId = r.Cliente.ClienteId,
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
                                          Calle2 = d.Calle2,
                                          Calle3 = d.Calle3,
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

                       }).OrderByDescending(y => y.EstadoFecha).Take(100).ToList();

            return Ordenes;
        }

        internal ReciboReportModel GetReparacionReciboData(int id)
        {
            IList<ReciboReportModel> data = (from r in _db.Reparacion
                                             join rd in _db.ReparacionDetalle on r.ReparacionDetalleId equals rd.ReparacionDetalleId
                                             join c in _db.Cliente on r.ClienteId equals c.ClienteId
                                             where r.ReparacionId == id
                                             select new ReciboReportModel()
                                             {
                                                 ReparacionId = r.ReparacionId,
                                                 ClienteId = r.ClienteId,
                                                 EmpleadoAsignadoId = r.EmpleadoAsignadoId,
                                                 TecnicoAsignadoId = r.TecnicoAsignadoId,
                                                 EstadoReparacionId = r.EstadoReparacionId,
                                                 ComercioId = r.ComercioId,
                                                 MarcaId = r.MarcaId,
                                                 TipoDispositivoId = r.TipoDispositivoId,
                                                 ReparacionDetalleId = r.ReparacionDetalleId,
                                                 ModificadoPor = r.ModificadoPor,
                                                 ModificadoEn = r.ModificadoEn,
                                                 CreadoEn = r.CreadoEn,
                                                 EsGarantia = rd.EsGarantia,
                                                 EsDomicilio = rd.EsDomicilio,
                                                 NroReferencia = rd.NroReferencia,
                                                 FechoCompra = rd.FechoCompra,
                                                 NroFactura = rd.NroFactura,
                                                 Presupuesto = rd.Presupuesto,
                                                 PresupuestoFecha = rd.PresupuestoFecha,
                                                 Precio = rd.Precio,
                                                 Marca = r.Marca.nombre,
                                                 Modelo = rd.Modelo,
                                                 Serie = rd.Serie,
                                                 Serbus = rd.Serbus,
                                                 Dni = c.Dni,
                                                 Nombre = c.Nombre.Trim().ToUpper() + " " + c.Apellido.Trim().ToUpper(),
                                                 Apellido = c.Apellido,
                                                 Mail = c.Mail,
                                                 Telefono1 = c.Telefono1,
                                                 Telefono2 = c.Telefono2,
                                                 Direccion = c.Direccion1.Calle
                                                     + " " + c.Direccion1.Altura
                                                     + " " + c.Direccion1.Calle2
                                                     + " " + c.Direccion1.Calle3
                                                     + ", " + c.Direccion1.Ciudad,
                                                 DireccionId = c.DireccionId,
                                                 Localidad = c.Localidad,
                                                 Latitud = c.Latitud,
                                                 Longitud = c.Longitud,
                                                 ReparacionDesc = rd.ReparacionDesc,
                                                 Accesorios = rd.Accesorios,
                                                 Comercio = r.Comercio.Descripcion
                                             }).ToList();

            return data.FirstOrDefault();

        }

        public OrdenModel GetOrden(int nroOrden)
        {
            return (from r in _db.Reparacion
                    where r.ReparacionId == nroOrden
                    select new OrdenModel()
                    {
                        NroOrden = r.ReparacionId,
                        Garantia = r.ReparacionDetalle.EsGarantia,
                        Domicilio = r.ReparacionDetalle.EsDomicilio,
                        EstadoCodigo = r.EstadoReparacionId,
                        EstadoDesc = r.EstadoReparacion.nombre.ToUpper(),
                        EstadoFecha = r.ModificadoEn,
                        InformadoEn = r.InformadoEn,

                        ResponsableId = r.EmpleadoAsignadoId,
                        ResponsableNombre = r.Usuario.Nombre,
                        TecnicoId = r.TecnicoAsignadoId,
                        TecnicoNombre = r.Usuario1.Nombre,

                        Presupuesto = r.ReparacionDetalle.Presupuesto ?? 0,
                        Monto = r.ReparacionDetalle.Precio ?? 0,

                        MarcaId = r.MarcaId,
                        MarcaDesc = r.Marca.nombre,

                        TipoId = r.TipoDispositivoId,
                        TipoDesc = r.TipoDispositivo.nombre,
                        Modelo = r.ReparacionDetalle.Modelo,
                        NroSerie = r.ReparacionDetalle.Serie,
                        Ubicacion = r.ReparacionDetalle.Unicacion,
                        FechaCompra = r.ReparacionDetalle.FechoCompra,
                        ReparacionDesc = r.ReparacionDetalle.ReparacionDesc,
                        Accesorios = r.ReparacionDetalle.Accesorios,

                        Comercio = r.Comercio,

                        Cliente = new ClienteModel()
                        {
                            ClienteId = r.Cliente.ClienteId,
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
                                       Calle3 = d.Calle3,
                                       Calle2 = d.Calle2,
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
                        },
                        Novedades = (from n in _db.Novedad
                                     where n.reparacionId == nroOrden
                                     select new NovedadModel()
                                     {
                                         Id = n.novedadId,
                                         Fecha = n.modificadoEn,
                                         Observacion = n.observacion,
                                         Descripcion = (from x in _db.TipoNovedad where x.TipoNovedadId == n.tipoNovedadId select x.nombre.ToUpper()).FirstOrDefault(),
                                         Monto = n.monto
                                     }).ToList()

                    }).FirstOrDefault();

        }

        public List<ReparacionReportListModel> GetOrders(OrdenListFilterBar filter)
        {
            var fy = filter.FromYear == 0 ? DateTime.Now.AddMonths(-12).Year : filter.FromYear;
            var fm = filter.FromMonth == 0 ? DateTime.Now.AddMonths(-12).Month : filter.FromMonth;
            var ty = filter.ToYear == 0 ? DateTime.Now.Year : filter.ToYear;
            var tm = filter.ToMonth == 0 ? DateTime.Now.Month : filter.ToMonth;
            var df = new DateTime(fy, fm, 1);
            var dt = new DateTime(ty, tm, 1);
            var id = filter.MinInactiveDays ?? Convert.ToInt32(new GlobalConfigHelper().GetVal("NotificacionesMinimoDiasInactividad"));
            var eList = filter.SelectedEstados ?? (from x in _db.EstadoReparacion where x.activo == true select x.EstadoReparacionId).ToList();

            var list = (from x in _db.Reparacion
                        where x.CreadoEn >= df
                           && x.CreadoEn <= dt
                           && (eList.Contains(x.EstadoReparacionId))
                        select new ReparacionReportListModel
                        {
                            Ticketid = x.ReparacionId,
                            Estado = x.EstadoReparacion.nombre.ToUpper(),
                            FechaIngreso = x.CreadoEn,
                            FechaEstado = x.ModificadoEn,
                            Cliente = x.Cliente.Nombre.ToUpper() + " " + x.Cliente.Apellido.ToUpper(),
                            UltimaNovedad = (from y in _db.Novedad where y.reparacionId == x.ReparacionId select y)
                                            .OrderByDescending(z=>z.modificadoEn).FirstOrDefault().observacion.ToUpper(),
                            Monto = "$" + x.ReparacionDetalle.Precio,
                            Marca = x.Marca.nombre.ToUpper()

                        }).ToList();

            list.Select(x => x.FechaEstadoString = x.FechaEstado.ToShortDateString()).ToList();
            list.Select(x => x.FechaIngresoString = x.FechaIngreso.ToShortDateString()).ToList();

            return list.Where(x => x.FechaEstado < DateTime.Now.AddDays(-id)).Select(y => y).ToList();
        }
    }

    public class ReparacionReportListModel
    {
        public int Ticketid { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEstado { get; set; }
        public string FechaEstadoString { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string FechaIngresoString { get; set; }
        public string Cliente { get; set; }
        public string UltimaNovedad { get; set; }
        public string Monto { get; set; }
        public string Marca { get; set; }
    }
}