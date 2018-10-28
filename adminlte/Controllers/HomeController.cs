using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Models;

namespace WeaponDoc.Controllers
{
    

    public class HomeController : BaseController
    {
        private GunSudexDbContext db = new GunSudexDbContext();

        //public ActionResult Index(string incomingIP)
        //{

        //    string checkitem = (from exp in db.Experts.ToList()
        //                        join r in db.Roles.ToList() on exp.RoleID equals r.RoleID
        //                        where exp.IP.Equals(incomingIP)
        //                        select (r.RoleDescr)).FirstOrDefault();

        //    //List <Role> roles = db.Roles.ToList();

        //    if (checkitem.Equals("Manager"))
        //    {
        //        return RedirectPermanent("/Areas/Manager/ManagerHome/Index");
        //    }

        //    else
        //    {
        //        if (checkitem.Equals("Practitioner"))
        //        {
        //            return RedirectPermanent("/Areas/Practitoner/PractitionerHome/Index");
        //        }

        //        else
        //        {
        //            RedirectPermanent("/Home/Index/Page404");
        //        }
        //    }
            

            


        //    return RedirectPermanent("/Home/Index/Page404");
        //}


        public ActionResult Index()
        {
            //UserViewModel user = (UserViewModel)(System.Web.HttpContext.Current.Session["User"]);

            //string checkitem = (from exp in db.Experts.ToList()
            //                    join r in db.Roles.ToList() on exp.RoleID equals r.RoleID
            //                    where exp.IP.Equals(user.ip)
            //                    select (r.RoleDescr)).FirstOrDefault();

            ////List <Role> roles = db.Roles.ToList();

            //if (checkitem.Equals("Manager"))
            //{
            return RedirectPermanent("/Manager/ManagerHome/Index");
            //}

            //else
            //{
            //    if (checkitem.Equals("Practitioner"))
            //    {
            //        return RedirectPermanent("/Areas/Practitoner/PractitionerHome/Index");
            //    }

            //    else
            //    {
            //        RedirectPermanent("/Home/Index/Page404");
            //    }
        }





            //return RedirectPermanent("/Home/Index/Page404");

            //return View();
       // }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Page404()
        {
            

            return View();
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