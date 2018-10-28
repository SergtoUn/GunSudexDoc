using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeaponDoc.Areas.Manager.Models;
using WeaponDoc.Models;

namespace WeaponDoc.Areas.Manager.Controllers
{
    //public abstract class BaseController : Controller
    //{

    //    private GunSudexDbContext db = new GunSudexDbContext();

    //    public string ip;

    //    public UserViewModel user;
    //    // GET: Manager/Base
    //    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        ip = Request.UserHostAddress;

    //        user = (from ex in db.Experts
    //                where (ex.IP == ip)
    //                select new UserViewModel { ip=ex.IP, portrait=ex.Portrait, fName=ex.FirstName, sName=ex.FamilyName }).FirstOrDefault();

    //        ViewBag.VBuser = user;

    //        //ViewBag.UserPortrait = user.portrait;

    //        //ViewBag.UserName = String.Concat(user.fName, user.sName);

    //        base.OnActionExecuting(filterContext);

    //    }
    //}
}