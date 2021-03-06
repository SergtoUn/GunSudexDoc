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
    
    public partial class TestMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestMethod()
        {
            this.EquipmentTypes = new HashSet<EquipmentType>();
        }
    
        public System.Guid MethodID { get; set; }
        public string MethodName { get; set; }
        public string MethodDescr { get; set; }
        public string Standard { get; set; }
        public string Paragraph { get; set; }
        public System.Guid ParameterID { get; set; }
        public System.Guid EquipmentTypeID { get; set; }
    
        public virtual Parameter Parameter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipmentType> EquipmentTypes { get; set; }
    }
}
