//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeaponDoc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Calls = new HashSet<Call>();
        }
    
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxID { get; set; }
        public string BIC { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> OKPO { get; set; }
        public string Account { get; set; }
        public string BankBranch { get; set; }
        public string BranchAddress { get; set; }
        public Nullable<int> BankCode { get; set; }
        public System.Guid CustomerID { get; set; }
        public System.Guid RepresentativeID { get; set; }
        public string MPhoneNumber { get; set; }
        public Nullable<System.Guid> JudicialStatusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Call> Calls { get; set; }
        public virtual JudicialStatus JudicialStatus { get; set; }
        public virtual Representative Representative { get; set; }
    }
}
