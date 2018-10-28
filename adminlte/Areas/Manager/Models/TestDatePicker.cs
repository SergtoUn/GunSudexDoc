using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeaponDoc.Areas.Manager.Models
{
    public class TestDatePicker
    {
        [Key]
        [Required]
        public DateTime Date { get; set; }
    }
}