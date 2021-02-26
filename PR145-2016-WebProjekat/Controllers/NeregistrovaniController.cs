using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class NeregistrovaniController : Controller
    {
        // GET: Neregistrovani
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StartNeReg()
        {
            return View("StartNeReg");
        }
    }
}