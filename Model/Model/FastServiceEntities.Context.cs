﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using Backend;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class FastServiceEntities : DbContext
    {
        public FastServiceEntities()
            : base(!CommonUtility.IsDevelopmentServer() ? "name=LocalFastServiceEntities" : "name=FastServiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<PuntoDeVenta> PuntoDeVenta { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<MetodoPago> MetodoPago { get; set; }
        public virtual DbSet<ItemMenu> ItemMenu { get; set; }
        public virtual DbSet<TipoTransaccion> TipoTransaccion { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<ProveedoresAcreedores> ProveedoresAcreedores { get; set; }
        public virtual DbSet<ComprasAPagar> ComprasAPagar { get; set; }
    }
}
