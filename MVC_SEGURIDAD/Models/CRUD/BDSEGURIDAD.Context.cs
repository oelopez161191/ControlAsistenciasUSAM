﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_SEGURIDAD.Models.CRUD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SEGURIDADEntities : DbContext
    {
        public SEGURIDADEntities()
            : base("name=SEGURIDADEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MODULOS> MODULOS { get; set; }
        public virtual DbSet<OPERACIONES> OPERACIONES { get; set; }
        public virtual DbSet<PERSONAS> PERSONAS { get; set; }
        public virtual DbSet<ROL_OPERA> ROL_OPERA { get; set; }
        public virtual DbSet<ROLES> ROLES { get; set; }
        public virtual DbSet<UserState> UserState { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
    }
}
