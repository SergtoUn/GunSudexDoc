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
    
    public partial class EquipmentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentType()
        {
            this.LabEquipments = new HashSet<LabEquipment>();
            this.TestMethods = new HashSet<TestMethod>();
        }
    
        public System.Guid EquipmentTypeID { get; set; }
        public System.Guid MethodID { get; set; }
        public string EquipmentTypeName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LabEquipment> LabEquipments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestMethod> TestMethods { get; set; }
    }
}
