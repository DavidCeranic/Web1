using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class HomeController : Controller
    {
        //public static List<Korisnik> admini = new List<Korisnik>();
        //public static List<Gost> gosti = new List<Gost>();
        //public static List<Domacin> domacini = new List<Domacin>();
        //public static List<Korisnik> svi = new List<Korisnik>();
        //public static string putanjaAdmini, putanjaGosti, putanjaDomacini;

        public ActionResult Index()
        {
            //putanjaAdmini = HttpContext.Server.MapPath("~/Database/putanjaAdmini.json");
            //putanjaGosti = HttpContext.Server.MapPath("/Database/putanjaGosti.json");
            //putanjaDomacini = HttpContext.Server.MapPath("/Database/putanjaDomacini.json");

            //CitajAdmineJson();
            //CitajGosteJson();
            //CitajDomacineJson();

            //foreach (var item in admini)
            //{
            //    svi.Add(item);
            //}

            //foreach (var item in gosti)
            //{
            //    svi.Add(item);
            //}

            //foreach (var item in domacini)
            //{
            //    svi.Add(item);
            //}

           

            return View();
        }

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


        //public static void CitajAdmineJson()
        //{
        //    using (StringReader r = new StringReader(putanjaAdmini))
        //    {
        //        string json = r.ReadToEnd();
        //        admini = JsonConvert.DeserializeObject<List<Korisnik>>(json);
        //    }
                    
        //}

        //public static void UpisAdminaJson()
        //{
        //    System.IO.File.WriteAllText(putanjaAdmini, JsonConvert.SerializeObject(admini, Formatting.Indented));
        //}



        //public static void CitajGosteJson()
        //{
        //    using (StringReader r = new StringReader(putanjaGosti))
        //    {
        //        string json = r.ReadToEnd();
        //        gosti = JsonConvert.DeserializeObject<List<Gost>>(json);
        //    }

        //}

        //public static void UpisGostJson()
        //{
        //    System.IO.File.WriteAllText(putanjaGosti, JsonConvert.SerializeObject(gosti, Formatting.Indented));
        //}



        //public static void CitajDomacineJson()
        //{
        //    using (StringReader r = new StringReader(putanjaDomacini))
        //    {
        //        string json = r.ReadToEnd();
        //        domacini = JsonConvert.DeserializeObject<List<Domacin>>(json);
        //    }

        //}

        //public static void UpisDomacinJson()
        //{
        //    System.IO.File.WriteAllText(putanjaDomacini, JsonConvert.SerializeObject(domacini, Formatting.Indented));
        //}
    }
}