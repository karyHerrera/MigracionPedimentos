﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsertarPedimentos
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
    
        public virtual DbSet<Imex_Container> Imex_Container { get; set; }
        public virtual DbSet<Imex_Info_EntregaAduana> Imex_Info_EntregaAduana { get; set; }
        public virtual DbSet<Imex_Info_EntregaAduana_Pedimentos> Imex_Info_EntregaAduana_Pedimentos { get; set; }
        public virtual DbSet<Imex_Terminal> Imex_Terminal { get; set; }
    
        public virtual ObjectResult<GridInventarioV20_Result> GridInventarioV20(string userName, Nullable<int> tipoServicioId, Nullable<int> tipoServicioIdFlag, Nullable<int> terminalId, Nullable<int> terminalFlag, Nullable<int> idDeposito, Nullable<int> depositoFlag, Nullable<int> idCat_Patios, Nullable<int> idCat_PatiosFlag, Nullable<long> shipperId, Nullable<int> shipperIdFlag, string contenedor, Nullable<int> contenedorFlag, string chassis, Nullable<int> chassisFlag, Nullable<int> page, Nullable<int> rows, Nullable<int> condContenedor, Nullable<int> condContenedorFlag, Nullable<int> condChassis, Nullable<int> condChassisFlag)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var tipoServicioIdParameter = tipoServicioId.HasValue ?
                new ObjectParameter("TipoServicioId", tipoServicioId) :
                new ObjectParameter("TipoServicioId", typeof(int));
    
            var tipoServicioIdFlagParameter = tipoServicioIdFlag.HasValue ?
                new ObjectParameter("TipoServicioIdFlag", tipoServicioIdFlag) :
                new ObjectParameter("TipoServicioIdFlag", typeof(int));
    
            var terminalIdParameter = terminalId.HasValue ?
                new ObjectParameter("TerminalId", terminalId) :
                new ObjectParameter("TerminalId", typeof(int));
    
            var terminalFlagParameter = terminalFlag.HasValue ?
                new ObjectParameter("TerminalFlag", terminalFlag) :
                new ObjectParameter("TerminalFlag", typeof(int));
    
            var idDepositoParameter = idDeposito.HasValue ?
                new ObjectParameter("IdDeposito", idDeposito) :
                new ObjectParameter("IdDeposito", typeof(int));
    
            var depositoFlagParameter = depositoFlag.HasValue ?
                new ObjectParameter("DepositoFlag", depositoFlag) :
                new ObjectParameter("DepositoFlag", typeof(int));
    
            var idCat_PatiosParameter = idCat_Patios.HasValue ?
                new ObjectParameter("IdCat_Patios", idCat_Patios) :
                new ObjectParameter("IdCat_Patios", typeof(int));
    
            var idCat_PatiosFlagParameter = idCat_PatiosFlag.HasValue ?
                new ObjectParameter("IdCat_PatiosFlag", idCat_PatiosFlag) :
                new ObjectParameter("IdCat_PatiosFlag", typeof(int));
    
            var shipperIdParameter = shipperId.HasValue ?
                new ObjectParameter("ShipperId", shipperId) :
                new ObjectParameter("ShipperId", typeof(long));
    
            var shipperIdFlagParameter = shipperIdFlag.HasValue ?
                new ObjectParameter("ShipperIdFlag", shipperIdFlag) :
                new ObjectParameter("ShipperIdFlag", typeof(int));
    
            var contenedorParameter = contenedor != null ?
                new ObjectParameter("Contenedor", contenedor) :
                new ObjectParameter("Contenedor", typeof(string));
    
            var contenedorFlagParameter = contenedorFlag.HasValue ?
                new ObjectParameter("ContenedorFlag", contenedorFlag) :
                new ObjectParameter("ContenedorFlag", typeof(int));
    
            var chassisParameter = chassis != null ?
                new ObjectParameter("Chassis", chassis) :
                new ObjectParameter("Chassis", typeof(string));
    
            var chassisFlagParameter = chassisFlag.HasValue ?
                new ObjectParameter("ChassisFlag", chassisFlag) :
                new ObjectParameter("ChassisFlag", typeof(int));
    
            var pageParameter = page.HasValue ?
                new ObjectParameter("Page", page) :
                new ObjectParameter("Page", typeof(int));
    
            var rowsParameter = rows.HasValue ?
                new ObjectParameter("Rows", rows) :
                new ObjectParameter("Rows", typeof(int));
    
            var condContenedorParameter = condContenedor.HasValue ?
                new ObjectParameter("CondContenedor", condContenedor) :
                new ObjectParameter("CondContenedor", typeof(int));
    
            var condContenedorFlagParameter = condContenedorFlag.HasValue ?
                new ObjectParameter("CondContenedorFlag", condContenedorFlag) :
                new ObjectParameter("CondContenedorFlag", typeof(int));
    
            var condChassisParameter = condChassis.HasValue ?
                new ObjectParameter("CondChassis", condChassis) :
                new ObjectParameter("CondChassis", typeof(int));
    
            var condChassisFlagParameter = condChassisFlag.HasValue ?
                new ObjectParameter("CondChassisFlag", condChassisFlag) :
                new ObjectParameter("CondChassisFlag", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GridInventarioV20_Result>("GridInventarioV20", userNameParameter, tipoServicioIdParameter, tipoServicioIdFlagParameter, terminalIdParameter, terminalFlagParameter, idDepositoParameter, depositoFlagParameter, idCat_PatiosParameter, idCat_PatiosFlagParameter, shipperIdParameter, shipperIdFlagParameter, contenedorParameter, contenedorFlagParameter, chassisParameter, chassisFlagParameter, pageParameter, rowsParameter, condContenedorParameter, condContenedorFlagParameter, condChassisParameter, condChassisFlagParameter);
        }
    
        public virtual int UpdatePedimentoExportacion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdatePedimentoExportacion");
        }
    }
}