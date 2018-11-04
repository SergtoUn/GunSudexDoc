using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Areas.Manager.Models;
using WeaponDoc.Models;
using System.Globalization;
using System.Collections;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Pdf;
using Cyriller.Model;
using Cyriller;
using System.IO;
using System.Diagnostics;
using System.Data.Entity.Validation;

namespace WeaponDoc.Areas.Manager.Controllers
{
    public class CallViewModelsController : Controller
    {
        private GunSudexDbContext db = new GunSudexDbContext();


        string callexp;
        ///GET: Manager/CallViewModels
        public ActionResult Index()
        {

            CallViewModel model = new CallViewModel();

            var callData = db.Calls.ToList();
            var itemsData = db.Items.ToList();
            var custData = db.Customers.ToList();
            var repData = db.Representatives.ToList();
            var callDetails = db.CallDetails.ToList();
            


            var result = from cd in callData
                         join cust in custData on cd.CustomerID equals cust.CustomerID
                         join rep in repData on cust.RepresentativeID equals rep.RepresentativeID
                         select new { callID = cd.CallID, callDate = cd.CallDate.ToString(), callNumber = cd.DocNumber.ToString(), contractDate = cd.ContractDate.ToString(), docType = cd.DocType.ToString(), cName = cust.Name.ToString(), repFName = rep.FamilyName.ToString() };

            var result2 = from cd in callDetails
                          join itd in itemsData on cd.ItemID equals itd.ItemID
                          select new { callID = cd.CallID, itemName = cd.Item.ItemName, itQty = cd.ItemQty };

            
            ViewBag.CallData = result.ToList();
            ViewBag.ItemData = result2.ToList();

            

            return View();
        }

        public ActionResult CreateContract( Guid callID)
        {
            var callData = db.Calls.ToList();
            var itemsData = db.Items.ToList();
            var custData = db.Customers.ToList();
            var repData = db.Representatives.ToList();
            var callDetails = db.CallDetails.ToList();
            //var progdata = db.Programs.ToList();

            var callData2 = (from c in db.CallDetails
                             join item in itemsData on c.ItemID equals item.ItemID
                             join calcdet in db.CalcDetails.ToList() on item.CalcDetailsID equals calcdet.CalcID
                             where (c.CallID == callID)
                             select new ContractViewModel.Itm { itemname = item.ItemName, Qty = c.ItemQty.ToString(), itemsubtype = item.ItemSubtype.ItemSubtype1, additionals = item.Additional, calcDate = calcdet.CalcDate.ToString(), calcNum = calcdet.CalcNum }).ToList();

            
            ContractViewModel contract = new ContractViewModel();

            CultureInfo ci = new CultureInfo("RU-ru");

            contract = (from cd in callData
                        join cust in custData on cd.CustomerID equals cust.CustomerID
                        join rep in repData on cust.RepresentativeID equals rep.RepresentativeID
                        join calldet in callDetails on cd.CallID equals calldet.CallID
                        join item in itemsData on calldet.ItemID equals item.ItemID
                        
                        where (cd.CallID == (Guid)callID)
                        select new ContractViewModel {
                                                        CallID = cd.CallID,
                                                        callDate = cd.CallDate.ToString("dd MMMM yyyy г.", ci),
                                                        sampleActDate = cd.SampleActDate.ToString("dd MMMM yyyy г.", ci),
                                                        callNumber = cd.DocNumber.ToString(),
                                                        expAffNum = cd.AffidavitNum.ToString(),
                                                        expAffDate = cd.AffidavitDate.ToString("dd MMMM yyyy г.", ci),
                                                        contractDate = cd.ContractDate,
                                                        repdoc = rep.RepDoc.ToString(),
                                                        repFName = rep.FirstName.ToString(),
                                                        repMName = rep.MidName.ToString(),
                                                        repFamName = rep.FamilyName.ToString(),
                                                        repPosition = rep.Position.ToString(),
                                                        repPhone = rep.PhoneNumber.ToString(),
                                                        custName = cust.Name.ToString(),
                                                        custAddress = cust.Address.ToString(),
                                                        custTaxID = cust.TaxID.ToString(),
                                                        custBIC = cust.BIC.ToString(),
                                                        custPhoneNumber = cust.PhoneNumber.ToString(),
                                                        custOKPO = cust.OKPO.ToString(),
                                                        custAccount = cust.Account.ToString(),
                                                        custBankBranch = cust.BankBranch.ToString(),
                                                        custBranchAddress = cust.BranchAddress.ToString(),
                                                        custBankCode = cust.BankCode.ToString(),
                                                        custMPhoneNumber = cust.MPhoneNumber.ToString()

                                                        
                        }
                                                        
                        ).FirstOrDefault();
           
            contract.itmList = callData2;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////

            //                              ВВОД ДАННЫХ В ШАБЛОН ДОГОВОРА                                        //

            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            List<string> expertData = callexp.Split(' ').Reverse().ToList();

            string expName = expertData[0] + expertData[1] + expertData[2];

            int nameIndex = callexp.IndexOf(expName);

            string expertPosition = callexp.Remove(nameIndex, expName.Length);

            string firstWordInPosition = callexp.Split(' ').First();

            int firstWordIndex = expertPosition.IndexOf(firstWordInPosition);

            string expPositionTail = expertPosition.Remove(firstWordIndex, firstWordInPosition.Length);

            CyrNounCollection nouns = new CyrNounCollection();
            CyrAdjectiveCollection adj = new CyrAdjectiveCollection();
            CyrPhrase phrase = new CyrPhrase(nouns, adj);
            CyrResult expname = phrase.Decline(expName, GetConditionsEnum.Strict);
            expName = expname.Genitive;

            CyrNoun strict = nouns.Get(firstWordInPosition, GetConditionsEnum.Strict);
            CyrResult expPosition = strict.Decline();
            string firstWordDecl = expPosition.Genitive;
            expertPosition = string.Concat(firstWordDecl, expPositionTail);

            firstWordInPosition = contract.repPosition.Split(' ').First();

            firstWordIndex = contract.repPosition.IndexOf(firstWordInPosition);

            string repPositionTail = contract.repPosition.Remove(firstWordIndex, firstWordInPosition.Length);

            CyrResult repname = phrase.Decline((contract.repFamName + contract.repFName + contract.repMName), GetConditionsEnum.Strict);
            string repName = repname.Genitive;

            strict = nouns.Get(firstWordInPosition, GetConditionsEnum.Strict);
            CyrResult repPosition = strict.Decline();
            firstWordDecl = repPosition.Genitive;
            string representativePosition = string.Concat(firstWordDecl, expPositionTail);
            

            try
            {
                Document document = new Document();
                document.LoadFromFile("~/Content/FileTemplates/ContractTemplateCorporate.docx");
                BookmarksNavigator bookmarkNavigator = new BookmarksNavigator(document);
                bookmarkNavigator.MoveToBookmark("ContractNum");
                bookmarkNavigator.ReplaceBookmarkContent(contract.callNumber, true);

                bookmarkNavigator.MoveToBookmark("ContractDay");
                if (contract.contractDate.HasValue)
                {
                    bookmarkNavigator.ReplaceBookmarkContent((contract.contractDate?.ToString("dd", new CultureInfo("ru-RU"))), true);

                    bookmarkNavigator.MoveToBookmark("ContractMonth");
                    bookmarkNavigator.ReplaceBookmarkContent(contract.contractDate?.ToString(" MMMM ", new CultureInfo("ru-RU")), true);

                    bookmarkNavigator.MoveToBookmark("ContractYear");
                    bookmarkNavigator.ReplaceBookmarkContent(contract.contractDate?.ToString(" yyyy г.", new CultureInfo("ru-RU")), true);
                }

                else
                {
                    bookmarkNavigator.ReplaceBookmarkContent((DateTime.Today.ToString("dd", new CultureInfo("ru-RU"))), true);

                    bookmarkNavigator.MoveToBookmark("ContractMonth");
                    bookmarkNavigator.ReplaceBookmarkContent(DateTime.Today.ToString(" MMMM ", new CultureInfo("ru-RU")), true);

                    bookmarkNavigator.MoveToBookmark("ContractYear");
                    bookmarkNavigator.ReplaceBookmarkContent(DateTime.Today.ToString(" yyyy г.", new CultureInfo("ru-RU")), true);
                }

                bookmarkNavigator.MoveToBookmark("ContractExpertPosition");
                bookmarkNavigator.ReplaceBookmarkContent(expertPosition, true);

                bookmarkNavigator.MoveToBookmark("ContractExpertFullName");
                bookmarkNavigator.ReplaceBookmarkContent(expName, true);

                bookmarkNavigator.MoveToBookmark("AffidavitNum");
                bookmarkNavigator.ReplaceBookmarkContent(contract.expAffNum, true);

                bookmarkNavigator.MoveToBookmark("AffidavitDate");
                bookmarkNavigator.ReplaceBookmarkContent(contract.expAffDate, true);

                bookmarkNavigator.MoveToBookmark("CustomerName");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custName, true);

                bookmarkNavigator.MoveToBookmark("CustomerDoc");
                bookmarkNavigator.ReplaceBookmarkContent(" ", true);

                bookmarkNavigator.MoveToBookmark("RepresentativePosition");
                bookmarkNavigator.ReplaceBookmarkContent(representativePosition, true);

                bookmarkNavigator.MoveToBookmark("RepresentativeName");
                bookmarkNavigator.ReplaceBookmarkContent(repName, true);

                bookmarkNavigator.MoveToBookmark("CustomerCompanyDoc");
                bookmarkNavigator.ReplaceBookmarkContent(contract.repdoc, true);

                foreach (ContractViewModel.Itm i in contract.itmList)
                {
                    CyrResult it_name = phrase.Decline(i.itemsubtype, GetConditionsEnum.Strict);
                    i.itemsubtype = it_name.Genitive;

                    bookmarkNavigator.MoveToBookmark("ItemName");
                    bookmarkNavigator.ReplaceBookmarkContent(i.itemsubtype + i.itemname, true);

                    string progname = (from prog in db.Programs.ToList()
                                join t in db.ItemTypes.ToList() on prog.ItemTypeID equals t.ItemTypeID
                                join st in db.ItemSubtypes.ToList() on t.ItemTypeID equals st.ItemTypeID
                                join item in db.Items.ToList() on st.ItemSubtypeID equals item.ItemSubtypeID
                                where item.ItemName == i.itemname
                                select prog.ProgramNameShort).FirstOrDefault();


                    bookmarkNavigator.MoveToBookmark("ProgramShortName");
                    bookmarkNavigator.ReplaceBookmarkContent(progname, true);

                    bookmarkNavigator.MoveToBookmark("ItemsNum");
                    bookmarkNavigator.ReplaceBookmarkContent(i.Qty, true);


                    string [] stringarr = i.additionals.Split(' ');
                    CyrResult addCyr = phrase.Decline(stringarr[0], GetConditionsEnum.Strict);
                    string additionals = addCyr.Genitive;
                    int index = i.additionals.IndexOf(additionals);
                    string additionalsTail = i.additionals.Remove(index, stringarr[0].Length);

                    bookmarkNavigator.MoveToBookmark("Additionals");
                    bookmarkNavigator.ReplaceBookmarkContent(additionals + additionalsTail, true);

                    bookmarkNavigator.MoveToBookmark("CalculationOrderDate");
                    bookmarkNavigator.ReplaceBookmarkContent(i.calcDate, true);

                    bookmarkNavigator.MoveToBookmark("CalculationOrderNum");
                    bookmarkNavigator.ReplaceBookmarkContent(i.calcNum, true);

                    

                }

                decimal TotalSum = 0;
                var costs = from callitem in callDetails
                            where (callitem.CallID == callID)
                            select callitem.ItemTestCost;
                TotalSum = costs.Sum(c => Convert.ToDecimal(c));

                bookmarkNavigator.MoveToBookmark("TotalSum");
                bookmarkNavigator.ReplaceBookmarkContent(TotalSum.ToString(), true);

                bookmarkNavigator.MoveToBookmark("TestingDurationCalDays");
                bookmarkNavigator.ReplaceBookmarkContent("30", true);

                bookmarkNavigator.MoveToBookmark("ExpertFIO");
                bookmarkNavigator.ReplaceBookmarkContent(expName, true);

                bookmarkNavigator.MoveToBookmark("RepresentativeFIO");
                bookmarkNavigator.ReplaceBookmarkContent(repName, true);

                bookmarkNavigator.MoveToBookmark("CustomerName2");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custName, true);

                bookmarkNavigator.MoveToBookmark("TaxID");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custTaxID, true);

                bookmarkNavigator.MoveToBookmark("OKPO");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custOKPO, true);

                bookmarkNavigator.MoveToBookmark("CustomerAddress");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custAddress, true);

                bookmarkNavigator.MoveToBookmark("CustomerPhoneNum");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custPhoneNumber, true);

                bookmarkNavigator.MoveToBookmark("CustomerMphoneNum");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custMPhoneNumber, true);

                bookmarkNavigator.MoveToBookmark("BankAccount");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custAccount, true);

                bookmarkNavigator.MoveToBookmark("BankBranch");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custBankBranch, true);

                bookmarkNavigator.MoveToBookmark("BankAddress");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custBranchAddress, true);

                bookmarkNavigator.MoveToBookmark("BankCode");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custBankCode, true);

                bookmarkNavigator.MoveToBookmark("BIC");
                bookmarkNavigator.ReplaceBookmarkContent(contract.custBIC, true);

                //Save doc file.

                if (document != null)
                {
                    string path = Server.MapPath(@"~\Area\Manager\Files\Contracts\");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    document.SaveToFile(path + @"docx\" + contract.CallID.ToString() + ".docx", Spire.Doc.FileFormat.Docx);
                    document.SaveToFile(path + @"pdf\" + contract.CallID.ToString() + ".pdf", Spire.Doc.FileFormat.PDF);
                    ViewBag.pdf = (path + @"pdf\" + contract.CallID.ToString() + ".pdf");
                }

                
            }

            catch (Exception e)
            {
                return new HttpNotFoundResult(e.ToString());
            }




            return View();
        }


        //CREATE CONTRACT
        [HttpPost]
        public ActionResult CreateContract(ContractViewModel cvm)

        {

            Call call = (from c in db.Calls.ToList()
                              where c.CallID == cvm.CallID
                              select c).FirstOrDefault();

            call.CallStatus = 1;

            db.Entry(call).State = EntityState.Modified;
            db.SaveChanges();

            return View();
        }


        //CREATE CALLVIEWMODEL
        public ActionResult Create()
        {
            CallViewModel model = new CallViewModel();
            SelectList jstatuses = new SelectList(db.JudicialStatuses,"JudicialStatus1", "JudicialStatusName");
            ViewBag.VBjstatuses = jstatuses;

            //cvm.itemclass 
            SelectList programs = new SelectList(db.Programs, "ProgramID", "ProgramNameShort");
            ViewBag.VBprograms = programs;

            SelectList subtypes = new SelectList(db.ItemSubtypes, "ItemSubtypeID", "ItemSubtype1");
            ViewBag.Stypes = subtypes;


            ViewBag.VBexpID = new SelectList(db.Experts, "ExpertID", "FamilyName");
            //Debugger.Break();
                               
            return View(model);
        }

        // POST: Manager/CallViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.







        /// <summary>
        /// 
        ///             CallViewModel CREATE HTTPPOST           //////////////////////////////////
        /// 
        /// </summary>
        /// <param name="callViewModel"></param>
        /// <returns></returns>
        /// 









        [HttpPost]
        public ActionResult Create(CallViewModel callViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Call call = new Call();
                    Customer customer = new Customer();
            


                    Representative rep = new Representative
                    {
                        RepresentativeID = new Guid(),
                        FamilyName = callViewModel.RepFamilyName,
                        FirstName = callViewModel.RepFirstName,
                        MidName = callViewModel.RepMidName,
                        Position = callViewModel.RepPosition,
                        PhoneNumber = callViewModel.RepPhoneNumber
                    };

                    //TempData["msg"] = "<script>alert(@model.JudicialStatus);</script>";
                    //ViewBag.Jstat = callViewModel.JudicialStatus;
                    callexp = (from e in db.Experts
                               where (e.ExpertID == callViewModel.ExpID)
                               select (e.Position.PositionDescr + e.FamilyName + e.FirstName + e.MidName)).FirstOrDefault();

                    customer.CustomerID = new Guid();
                    call.CustomerID = customer.CustomerID;
                    customer.Name = callViewModel.CustomerName;
                    //customer.RepresentativeID = rep.RepresentativeID;


                    var js = Int32.Parse(callViewModel.JudicialStatus);


                    customer.JudicialStatusID = (from j in db.JudicialStatuses
                                                 where (j.JudicialStatus1 == js)
                                                 select (j.JudicialStatusID)).FirstOrDefault();

                    customer.Address = callViewModel.CustomerAddress;
                    customer.TaxID = callViewModel.TaxID;
                    customer.OKPO = callViewModel.OKPO;
                    customer.Account = callViewModel.CustomerBankAccount;
                    customer.BankBranch = callViewModel.BankBranch;
                    customer.BranchAddress = callViewModel.BranchAddress;
                    customer.BankCode = callViewModel.BankCode;
                    customer.BIC = callViewModel.BIC;
                    customer.PhoneNumber = callViewModel.CustomerPhoneNumber;
                    customer.MPhoneNumber = callViewModel.MPhoneNumber;

                    customer.RepresentativeID = rep.RepresentativeID;

                    callViewModel.CallID = Guid.NewGuid();
                    //callViewModel.ItemID = Guid.NewGuid();
                    call.CallID = callViewModel.CallID;
                    call.CallName = callViewModel.CallName;
                    call.CallDate = callViewModel.CallDate;
                    call.SampleActDate = callViewModel.SampleActDate;
                    call.DocNumber = callViewModel.Number;
                    call.ContractDate = DateTime.Today; //DateTime.Today.ToString("d MMMM yyyy г.", CultureInfo.GetCultureInfo("ru-ru"));
                    call.DocType = callViewModel.DocType;
                    call.AffidavitDate = callViewModel.AffidavitDate;
                    call.AffidavitNum = callViewModel.AffidavitNum;



                    List<decimal> testCosts = new List<decimal>();

                    //item.ItemID = callViewModel.ItemID;
                    //item.ItemName = callViewModel.ItemName;
                    //item.ItemProducer = callViewModel.ItemProducer;


                    foreach (var i in callViewModel.ItemsList)
                    {
                        CallDetail callDetail = new CallDetail();
                        CalcDetail cd = new CalcDetail();
                        Item item = new Item();
                        //Debugger.Break();
                        var itID = (from prog in db.Programs
                                    where (prog.ProgramNameShort.Equals(i.ProgramNameShort))
                                    select (prog.ItemTypeID)).FirstOrDefault();

                        callDetail.CallID = call.CallID;
                        callDetail.ProgramID = (from prog in db.Programs
                                                where (prog.ProgramNameShort.Equals(i.ProgramNameShort))
                                                select prog.ProgramID).FirstOrDefault();

                        i.ItemID = Guid.NewGuid();
                        item.ItemID = callDetail.ItemID = i.ItemID;
                        item.ItemName = i.ItemName;
                        item.ItemProducer = i.ItemProducer;
                        callDetail.ItemQty = i.ItemQty;
                        item.Additional = i.Additional;
                        cd.CalcID = item.CalcDetailsID = new Guid();


                        //////////////////////////////////////// IMAGE PATH //////////////////////////////////////////


                        item.ImagePath = "";
                
                        item.ItemSubtypeID = (from ist in db.ItemSubtypes
                                              where (ist.ItemSubtypeID == i.ItemSubtype)
                                              select (ist.ItemSubtypeID)).FirstOrDefault();
                        //Debugger.Break();
                        var progDuration = (from prog in db.Programs
                                            where (prog.ProgramNameShort.Equals(i.ProgramNameShort))
                                            select (prog.DurationPerUnit)).FirstOrDefault();

                        decimal itemsDuration;
                
                        if (!progDuration.HasValue)
                        {
                            progDuration = 1000;
                        }
                
                        itemsDuration = (decimal)(progDuration * i.ItemQty);

                        //var lastDate = db.Calculations.Select(a => new { a.CurentDate }).ToList().Max(p => p);
                        var calcdata = db.Calculations.Select(a => new { a.CurentDate, a.ConsumablesCost, a.HourFee, a.EquipmentMaintanance }).OrderByDescending(t => t.CurentDate).First();

                        callDetail.ItemTestCost = calcdata.HourFee * (decimal)itemsDuration;

                        //Ввод каждой себестоимости проведения исследования вида объекта
                
                        testCosts.Add(callDetail.ItemTestCost);
                        cd.CalcDate = Convert.ToDateTime(i.calcDate, new CultureInfo("RU-ru"));
                        cd.CalcNum = i.calcNum;

                        db.Items.Add(item);
                        db.CalcDetails.Add(cd);
                        db.CallDetails.Add(callDetail);

                    }

                    Debugger.Break();
            

                            db.Representatives.Add(rep);
                            db.Customers.Add(customer);
                    
                            db.Calls.Add(call);
                            //db.CallViewModels.Add(callViewModel);
                            db.SaveChanges();


                }

                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }

                catch (NullReferenceException ne)
                {
                    Debug.WriteLine(ne);
                }

                
                return RedirectToAction("Index");
                
            }
            return View();
        }

        // GET: Manager/CallViewModels/Edit/5

       
        public ActionResult Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CallViewModel callViewModel = db.CallViewModels(id);
            //if (callViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View(
                //callViewModel
                );
        }

        // POST: Manager/CallViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CallID,ItemID,CallDate,Number,ContractDate,DocType,RepFamilyName,RepFirstName,RepMidName,RepPosition,RepPhoneNumber,JudicialStatus,CustomerName,CustomerAddress,TaxID,BIC,CustomerPhoneNumber,OKPO,CustomerBankAccount,BankBranch,BranchAddress,BankCode,MPhoneNumber,FamilyName,FirstName,MidName,PositionDescr,AffidavitNum,AffidavitDate,ItemName,ItemProducer,ItemQty,Comments,Image,Additional,ProgramNameFull,ProgramNameShort")] CallViewModel callViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(callViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(callViewModel);
        }

        

        // GET: Manager/CallViewModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CallViewModel callViewModel = db.CallViewModels(id);
            //if (callViewModel == null)
            //{
            //    return HttpNotFound();
            //}
            return View(
            //    callViewModel
            );

        }

        // POST: Manager/CallViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            //CallViewModel callViewModel = db.CallViewModels.FindAsync(id);
            //db.CallViewModels.Remove(callViewModel);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
