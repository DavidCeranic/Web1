using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class StavkeCController : Controller
    {
        // GET: Stavke
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dodaj()
        {
            if(Request["Tip"] == "basic")
            {
                Amenitie a = new Amenitie();
                a.Naziv = Request["Naziv"];
                a.Id = SingInOrRegisterController.basic.Count+1;
                SingInOrRegisterController.basic.Add(a);
            }

            if (Request["Tip"] == "family")
            {
                Amenitie a = new Amenitie();
                a.Naziv = Request["Naziv"];
                a.Id = SingInOrRegisterController.family.Count + 1;
                SingInOrRegisterController.family.Add(a);
            }

            if (Request["Tip"] == "dining")
            {
                Amenitie a = new Amenitie();
                a.Naziv = Request["Naziv"];
                a.Id = SingInOrRegisterController.dining.Count + 1;
                SingInOrRegisterController.dining.Add(a);
            }

            


            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("../Admin/Stavkee");
        }
    }
}