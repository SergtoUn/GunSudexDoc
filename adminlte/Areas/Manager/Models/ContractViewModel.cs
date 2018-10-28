using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Models;
using System.Globalization;

namespace WeaponDoc.Areas.Manager.Models
{
    public class ContractViewModel
    {
        //+callDate = cd.CallDate.ToString("dd MMMM yyyy г.", ci),
        //+callNumber = cd.Number.ToString(),
        //+expAffNum = cd.AffidavitNum.ToString(),
        //+expAffDate = cd.AffidavitDate.ToString("dd MMMM yyyy г.", ci),
        //+contractDate = cd.ContractDate.ToString("dd MMMM yyyy г.", ci),
        //itemName = cd.Item.ItemName.ToString(),
        //itemQty = cd.ItemQty.ToString(),
        //repdoc = rep.RepDoc.ToString(),
        //+repFName = rep.FirstName.ToString(),
        //+repMName = rep.MidName.ToString(),
        //+repFamName = rep.FamilyName.ToString(),
        //+repPosition = rep.Position.ToString(),
        //+repPhone = rep.PhoneNumber.ToString(),
        //+custName = cust.Name.ToString(),
        //+custAddress = cust.Address.ToString(),
        //+custTaxID = cust.TaxID.ToString(),
        //+custBIC = cust.BIC.ToString(),
        //+custPhoneNumber = cust.PhoneNumber.ToString(),
        //+custOKPO = cust.OKPO.ToString(),
        //+custAccount = cust.Account.ToString(),
        //+custBankBranch = cust.BankBranch.ToString(),
        //+custBranchAddress = cust.BranchAddress.ToString(),
        //+custBankCode = cust.BankCode.ToString(),
        //+custMphoneNumber = cust.MPhoneNumber.ToString()

        public System.Guid CallID { get; set; }

        public string callDate { get; set; }

        public string sampleActDate { get; set; }

        public string callNumber { get; set; }

        public Nullable<System.DateTime> contractDate { get; set; }

        public string DocType { get; set; }

        public decimal CostEfficiency { get; set; }

        public string repFamName { get; set; }

        public string repFName { get; set; }

        public string repMName { get; set; }

        public string repPosition { get; set; }

        public string repPhone { get; set; }

        public string JudicialStatus { get; set; }

        public string custName { get; set; }

        public string custAddress { get; set; }

        public string custTaxID { get; set; }

        public string custBIC { get; set; }

        public string custPhoneNumber { get; set; }

        public string custOKPO { get; set; }

        public string custAccount { get; set; }

        public string custBankBranch { get; set; }

        public string custBranchAddress { get; set; }

        public string custBankCode { get; set; }

        public string custMPhoneNumber { get; set; }

        public string Exp { get; set; }

        public string expAffNum { get; set; }

        public string expAffDate { get; set; }

        //public string itemName { get; set; }

        public string itemQty { get; set; }

        public string repdoc { get; set; }

        //public string custMphoneNumber { get; set; }

        public List<Itm> itmList { get { return _items; } set { _items = value; } }

        private List<Itm> _items = new List<Itm>();

        public class Itm
        {
            public string itemname;
            public string Qty;
            public string itemsubtype;
            public string additionals;
            public string calcDate;
            public string calcNum;
        }
    }



}