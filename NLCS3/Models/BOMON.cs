//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NLCS3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOMON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOMON()
        {
            this.VIENCHUCs = new HashSet<VIENCHUC>();
        }
    
        public string BM_MA { get; set; }
        public int K_MA { get; set; }
        public string BM_TEN { get; set; }
    
        public virtual KHOA KHOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIENCHUC> VIENCHUCs { get; set; }
    }
}
