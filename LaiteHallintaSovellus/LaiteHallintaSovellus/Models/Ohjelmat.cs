//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaiteHallintaSovellus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ohjelmat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ohjelmat()
        {
            this.Asennukset = new HashSet<Asennukset>();
        }
    
        public int Ohjelma_ID { get; set; }
        public string Ohjelma { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asennukset> Asennukset { get; set; }
    }
}
