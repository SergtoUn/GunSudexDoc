using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeaponDoc.Areas.Manager.Models
{
    public class OrderViewModel
    {   [Key]
        public Guid Id { get; set; }
        // public System.Guid CallID { get; set; }
        public Guid callID { get; set; }
        public string ContractData { get; set; }
        public string ItemName { get; set; }
        public string Program { get; set; }
        public int QtyLeft { get; set; }
        public string ExpertFamName { get; set; }
        [LessThanOrEqualTo("QtyLeft", ErrorMessage = "Количество не должно быть больше указанного в договоре!")]
        public long OrderQty { get; set; }
        public Nullable<System.DateTime> FinalDate { get; set; }
        //[GreaterThan(, ErrorMessage = "Количество не должно быть больше указанного в договоре!")]
        public string Status;



        //public List<Itm> ItemsList { get { return _items; } }

        //private List<Itm> _items = new List<Itm>();

        //public class Itm
        //{
        //    public string ExpertFamName { get; set; }
        //    public string ItemName { get; set; }
        //    public int QtyLeft { get; set; }
        //    [LessThanOrEqualTo("QtyLeft", ErrorMessage = "Количество не должно быть больше указанного в договоре!")]
        //    public long OrderQty { get; set; }
        //    public Nullable<System.DateTime> FinalDate { get; set; }
        //}

    }
}