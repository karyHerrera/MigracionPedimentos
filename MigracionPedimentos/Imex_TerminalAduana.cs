//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Imex_TerminalAduana
    {
        public int TerminalAduanaId { get; set; }
        public int TerminalId { get; set; }
        public int TipoServicioId { get; set; }
        public Nullable<bool> Aduana { get; set; }
        public Nullable<bool> AduanaEDI417 { get; set; }
        public Nullable<bool> Robados { get; set; }
        public Nullable<bool> OutgateRez1Pickup { get; set; }
        public Nullable<int> IdCat_PatioDefault { get; set; }
        public Nullable<int> IdCat_PatioDefaultTren { get; set; }
        public Nullable<bool> AduanaManual { get; set; }
        public Nullable<bool> HabilitaFacturatario { get; set; }
        public string ClaveRecinto { get; set; }
        public Nullable<int> IdCatPuertoAduana { get; set; }
    }
}
