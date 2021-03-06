﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class FastServiceEntities : DbContext
    {
        public FastServiceEntities()
            : base("name=FastServiceEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Comercio> Comercio { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<EstadoReparacion> EstadoReparacion { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<GlobalConfig> GlobalConfig { get; set; }
        public virtual DbSet<ItemMenu> ItemMenu { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<MetodoPago> MetodoPago { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Novedad> Novedad { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<PuntoDeVenta> PuntoDeVenta { get; set; }
        public virtual DbSet<Reparacion> Reparacion { get; set; }
        public virtual DbSet<ReparacionDetalle> ReparacionDetalle { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<TipoDispositivo> TipoDispositivo { get; set; }
        public virtual DbSet<TipoFactura> TipoFactura { get; set; }
        public virtual DbSet<TipoNovedad> TipoNovedad { get; set; }
        public virtual DbSet<TipoTransaccion> TipoTransaccion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<vw_ComprasAPagar> vw_ComprasAPagar { get; set; }
        public virtual DbSet<vw_ProveedoresAcreedores> vw_ProveedoresAcreedores { get; set; }
        public virtual DbSet<vw_ReparacionTiempos> vw_ReparacionTiempos { get; set; }
        public virtual DbSet<vw_VentasAnuales> vw_VentasAnuales { get; set; }
        public virtual DbSet<vw_VentasDiarias> vw_VentasDiarias { get; set; }
        public virtual DbSet<vw_VentasMensuales> vw_VentasMensuales { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
    }
}
