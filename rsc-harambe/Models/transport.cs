//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rsc_harambe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class transport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public transport()
        {
            this.plans = new HashSet<plans>();
        }
    
        public int id { get; set; }
        public string transport_type { get; set; }
        public Nullable<int> start_id { get; set; }
        public Nullable<int> end_id { get; set; }
        public Nullable<int> duration { get; set; }
    
        public virtual locations locations { get; set; }
        public virtual locations locations1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<plans> plans { get; set; }
    }
}
