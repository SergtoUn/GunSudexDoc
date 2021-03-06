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
    
    public partial class Expert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expert()
        {
            this.Orders = new HashSet<Order>();
            this.LabEquipments = new HashSet<LabEquipment>();
            this.Orders1 = new HashSet<Order>();
        }
    
        public System.Guid ExpertID { get; set; }
        public System.Guid PositionID { get; set; }
        public System.Guid RoleID { get; set; }
        public Nullable<System.Guid> LabRoomID { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string Email { get; set; }
        public string MPhone { get; set; }
        public byte[] Portrait { get; set; }
        public string IP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LabEquipment> LabEquipments { get; set; }
        public virtual LabRoom LabRoom { get; set; }
        public virtual Position Position { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders1 { get; set; }
    }
}
