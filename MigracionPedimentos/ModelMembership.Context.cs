﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigracionPedimentos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MembershipEntities : DbContext
    {
        public MembershipEntities()
            : base("name=MembershipEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Imex_Info_EntregaAduana> Imex_Info_EntregaAduana { get; set; }
        public virtual DbSet<Imex_Info_EntregaAduana_Pedimentos> Imex_Info_EntregaAduana_Pedimentos { get; set; }
        public virtual DbSet<Imex_TerminalAduana> Imex_TerminalAduana { get; set; }
    
        public virtual ObjectResult<GetEquiposExportacionSinCT_Result> GetEquiposExportacionSinCT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEquiposExportacionSinCT_Result>("GetEquiposExportacionSinCT");
        }
    }
}
