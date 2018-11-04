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
    [Serializable]
    public class CallViewModel
    {
        //Call
        [Key]
        [Column(Order = 0)]
        [HiddenInput(DisplayValue = false)]
        public System.Guid CallID { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //[HiddenInput(DisplayValue = false)]
        //public System.Guid ItemID { get; set; }

        [Required, Display(Name = "Дата заявки (заявления)")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MM yyyy} г.")]
        public System.DateTime CallDate { get; set; }

        [Required, Display(Name = "Номер договора")]
        public string Number { get; set; }

        [Display(Name = "Дата акта отбора образцов"), DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MM yyyy} г.")]
        public System.DateTime SampleActDate { get; set; }


        [Display(Name = "Дата договора"), DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MM yyyy} г.")]
        public System.DateTime ContractDate { get; set; }

        

        [Required, Display(Name = "Рентабельность, %")]
        public decimal CostEfficiency { get; set; }

        [Required, Display(Name = "Тип документа")]
        public string CallName { get; set; }

        //Representative
        [Required, Display(Name = "Фамилия")]
        public string RepFamilyName { get; set; }

        [Required, Display(Name = "Имя")]
        public string RepFirstName { get; set; }

        [Required, Display(Name = "Отчество")]
        public string RepMidName { get; set; }

        [Required, Display(Name = "Тип документа")]
        public string DocType { get; set; }

        [Required, Display(Name = "Должность")]
        public string RepPosition { get; set; }

        [Required, Display(Name = "Телефон (###) ###-##-##")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(###) ###-##-##}")]
        public string RepPhoneNumber { get; set; }

        //Customer
        [Required, Display(Name = "Юридическое лицо / физическое лицо")]
        public string JudicialStatus { get; set; }

        [Required, Display(Name = "Название")]
        public string CustomerName { get; set; }

        [Required, Display(Name = "Адрес")]
        public string CustomerAddress { get; set; }

        [Required, Display(Name = "УНП")]
        public string TaxID { get; set; }

        [Display(Name = "БИК")]
        public string BIC { get; set; }

        [Required, Display(Name = "Телефон (###) ###-##-##")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(###) ###-##-##}")]
        public string CustomerPhoneNumber { get; set; }

        [Required, Display(Name = "ОКПО")]
        public int OKPO { get; set; }

        [Required, Display(Name = "Банковский счёт")]
        public string CustomerBankAccount { get; set; }

        [Required, Display(Name = "Отделение банка")]
        public string BankBranch { get; set; }

        [Required, Display(Name = "Адрес банка")]
        public string BranchAddress { get; set; }

        [Display(Name = "Код")]
        public Nullable<int> BankCode { get; set; }

        [Display(Name = "Мобильный телефон")]
        public string MPhoneNumber { get; set; }

        //Person
        [Required, Display(Name = "Эксперт")]
        //public string Exp { get; set; }
        public Guid ExpID { get; set; }
        //[Required, Display(Name = "Имя")]
        //public string FirstName { get; set; }

        //[Required, Display(Name = "Отчество")]
        //public string MidName { get; set; }
        //Expert
        //[Required, Display(Name = "Должность")]
        //public string PositionDescr { get; set; }


        [Required, Display(Name = "Номер доверенности")]
        public int AffidavitNum { get; set; }

        [Required, Display(Name = "Дата доверенности"), 
            DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd MM yyyy} г.", ApplyFormatInEditMode = true)]
        public System.DateTime AffidavitDate { get; set; }

        public List<ItemClass> ItemsList { get { return _items; } }

        private List<ItemClass> _items = new List<ItemClass>();
                
        public class ItemClass
        {  //Item

            public Guid ItemID { get; set; }

            [Required, Display(Name = "Подтип оружия")]
            public Guid ItemSubtype { get; set; }
            
            [Required, Display(Name = "Наименование")]
            public string ItemName { get; set; }

            [Required, Display(Name = "Производитель")]
            public string ItemProducer { get; set; }

            [Required, Display(Name = "Количество")]
            public int ItemQty { get; set; }

            //[Display(Name = "Комментарии")]
            //public string Comments { get; set; }

            //[Required, Display(Name = "Изображение")]
            //public HttpPostedFileBase Image { get; set; }

            [Display(Name = "Дополнительно предоставляемые объекты")]
            public string Additional { get; set; }
            //Program
            //[Required, Display(Name = "Полное название")]
            //public string ProgramNameFull { get; set; }

            [Required, Display(Name = "Краткое название")]
            public string ProgramNameShort { get; set; }

            [Required, Display(Name = "Дата калькуляции")]
            public string calcDate { get; set; }

            [Required, Display(Name = "Номер калькуляции")]
            public string calcNum { get; set; }

            //List<Program> Programs { get; set; }
            //SelectList slPrograms;

            //public IEnumerable<SelectListItem> PhoneTypeSelectListItems
            //{
            //    get;

            //    //{
            //    //    //foreach (Program prog in new { GunSudexDbContext db})
            //    //    //{
            //    //    //    SelectListItem selectListItem = new SelectListItem();
            //    //    //    selectListItem.Text = type.ToString();
            //    //    //    selectListItem.Value = type.ToString();
            //    //    //    selectListItem.Selected = Type == type;

            //    //    yield return selectListItem;
            //    //    //}
            //    //}
            //}
        }
    }

        
    
}