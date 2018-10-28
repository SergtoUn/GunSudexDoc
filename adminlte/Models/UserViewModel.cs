using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeaponDoc.Models
{
    [Serializable]
    public class UserViewModel
    {
        public string ip { get; set; }
        public byte[] portrait { get; set; }
        public string fName { get; set; }
        public string sName { get; set; }
    }
}