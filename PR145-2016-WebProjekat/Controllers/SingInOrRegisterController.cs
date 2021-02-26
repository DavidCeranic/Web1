using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class SingInOrRegisterController : Controller
    {
        public static List<Korisnik> admini = new List<Korisnik>();
        public static List<Gost> gosti = new List<Gost>();
        public static List<Domacin> domacini = new List<Domacin>();
        public static List<Korisnik> svi = new List<Korisnik>();
        public static List<Amenitie> basic = new List<Amenitie>() {  };
        public static List<Amenitie> family = new List<Amenitie>();
        public static List<Amenitie> dining = new List<Amenitie>();
        public static string putanjaAdmini, putanjaGosti, putanjaDomacini;
        int i = 0;

        // GET: SingInOrRegister
        public ActionResult Index()
        {
            if (basic.Count < 4)
            {
                basic.Add(new Amenitie(1, "TV"));
                basic.Add(new Amenitie(2, "WiFi"));
                basic.Add(new Amenitie(3, "Air"));
                basic.Add(new Amenitie(4, "Hot Water"));
            }

            if (family.Count < 1)
            {
                family.Add(new Amenitie(5, "Crib"));
            }

            if (dining.Count < 1)
            {
                dining.Add(new Amenitie(6, "Kitchen"));
            }
            Session["filter"] = null;

            ViewBag.basic = basic;
            ViewBag.family = family;
            ViewBag.dining = dining;

            putanjaAdmini = HttpContext.Server.MapPath("~/Database/Admini.json");
            putanjaGosti = HttpContext.Server.MapPath("/Database/Gosti.json");
            putanjaDomacini = HttpContext.Server.MapPath("/Database/Domacini.json");
            DomacinController.putanjaApartmani = HttpContext.Server.MapPath("~/Database/Apartmani.json");


            CitajAdmineJson();

            
                foreach (var item in admini)
                {
                bool potvrda = false;
                    foreach (var item2 in svi)
                    {
                    if (item.KorisnickoIme == item2.KorisnickoIme)
                    {
                        potvrda = true;
                    }

                    }
                if (potvrda == false)
                {
                    svi.Add(item);
                }
                }
            







            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if(item.StatusApartmana==StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            return View("Start");
        }

        [HttpGet]
        public ActionResult Start()
        {
            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            ViewBag.basic = basic;
            ViewBag.family = family;
            ViewBag.dining = dining;
            return View("Start");
        }

        [HttpPost]
        public ActionResult Register()
        {
            ViewBag.dodaj = "register";
            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            ViewBag.basic = basic;
            ViewBag.family = family;
            ViewBag.dining = dining;
            return View("Start");
        }

        [HttpPost]
        public ActionResult Register2(Gost k)
        {
            //Dodati proveru dal vec postoji sa countom ako je 0
            if (k.Ime == null || k.Prezime == null || k.Lozinka == null)
            {
                return View("Error");
            }

            k.Id = gosti.Count();
            k.Uloga = Uloge.Gost;
            gosti.Add(k);
            UpisGostJson();
            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            ViewBag.basic = basic;
            ViewBag.family = family;
            ViewBag.dining = dining;
            return View("Start");

        }

        [HttpPost]
        public ActionResult SingIn()
        {
            ViewBag.dodaj = "singin";
            DomacinController.CitajApartmaneJson();
            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            ViewBag.basic = basic;
            ViewBag.family = family;
            ViewBag.dining = dining;
            return View("Start");
        }

        [HttpPost]
        public ActionResult SingIn2(string KorisnickoIme, string Lozinka)
        {
            if(Request["KorisnickoIme"] == null || Request["Lozinka"] == null)
            {
                return View("Error");
            }
            Session["filter"] = null;
            CitajGosteJson();
            CitajDomacineJson();

            foreach (var item in gosti)
            {
                bool potvrda = false;
                foreach (var item2 in svi)
                {
                    if (item.KorisnickoIme == item2.KorisnickoIme)
                    {
                        potvrda = true;
                    }

                }
                if (potvrda == false)
                {
                    svi.Add(item);
                }
            }


            foreach (var item in domacini)
            {
                bool potvrda = false;
                foreach (var item2 in svi)
                {
                    if (item.KorisnickoIme == item2.KorisnickoIme)
                    {
                        potvrda = true;
                    }

                }
                if (potvrda == false)
                {
                    svi.Add(item);
                }
            }


            
            

            foreach (var item in admini)
            {
                if (item.KorisnickoIme == KorisnickoIme)
                {
                    if (item.Lozinka == Lozinka)
                    {
                        ViewBag.admini = admini;
                        ViewBag.gosti = gosti;
                        ViewBag.domacini = domacini;
                        Session["aktivanAdmin"] = item;
                        ViewBag.korisnici = svi;


                        DomacinController.CitajApartmaneJson();



                        List<Apartman> lista = new List<Apartman>();
                        foreach (var itemm in DomacinController.apartmani)
                        {
                            if (itemm.StatusApartmana == StatusApartmana.Aktivno)
                                lista.Add(itemm);
                        }
                        ViewBag.Aktivni3 = lista;


                        List<Apartman> lista2 = new List<Apartman>();
                        foreach (var itemm in DomacinController.apartmani)
                        {
                            if (itemm.StatusApartmana == StatusApartmana.Neaktivno)
                                lista2.Add(itemm);
                        }
                        ViewBag.Neaktivni3 = lista2;

                        ViewBag.basic = basic;
                        ViewBag.family = family;
                        ViewBag.dining = dining;
                        ViewBag.sakrij10 = "svi";
                        return View("../Admin/StartAdmin");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                
            }

            foreach (var item2 in gosti)
            {
                if (item2.KorisnickoIme == KorisnickoIme)
                {
                    if (item2.Lozinka == Lozinka)
                    {
                        ViewBag.admini = admini;
                        ViewBag.gosti = gosti;
                        ViewBag.domacini = domacini;
                        Session["aktivanGost"] = item2;
                        ViewBag.korisnici = svi;
                        ViewBag.taj3 = item2.KorisnickoIme;
                        DomacinController.CitajApartmaneJson();
                        List<Apartman> lista = new List<Apartman>();
                        foreach (var item in DomacinController.apartmani)
                        {
                            if (item.StatusApartmana == StatusApartmana.Aktivno)
                                lista.Add(item);
                        }
                        ViewBag.Aktivni10 = lista;
                        ViewBag.sakrij10 = "svi";
                        ViewBag.basic = basic;
                        ViewBag.family = family;
                        ViewBag.dining = dining;
                        return View("../User/StartUser");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                
            }

            foreach (var item3 in domacini)
            {
                if (item3.KorisnickoIme == KorisnickoIme)
                {
                    if (item3.Lozinka == Lozinka)
                    {
                        ViewBag.admini = admini;
                        ViewBag.gosti = gosti;
                        ViewBag.domacini = domacini;
                        ViewBag.korisnici = svi;
                        DomacinController.CitajApartmaneJson();

                        Session["aktivanDomacin"] = item3;

                        List<Apartman> lista = new List<Apartman>();
                        foreach (var item in DomacinController.apartmani)
                        {
                            if (item.StatusApartmana == StatusApartmana.Aktivno)
                                if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                                    lista.Add(item);
                        }
                        ViewBag.Aktivni2 = lista;


                        List<Apartman> lista2 = new List<Apartman>();
                        foreach (var item in DomacinController.apartmani)
                        {
                            if (item.StatusApartmana == StatusApartmana.Neaktivno)
                                if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                                    lista2.Add(item);
                        }
                        ViewBag.Neaktivni2 = lista2;


                        Session["aktivanDomacin"] = item3;
                        ViewBag.taj3 = item3.KorisnickoIme;
                        ViewBag.basic = basic;
                        ViewBag.basic = basic;
                        ViewBag.family = family;
                        ViewBag.dining = dining;
                        ViewBag.sakrij10 = "svi";
                        return View("../Domacin/StartDomacin");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Error");
                }
            }


            return View("Error");
        }

        [HttpPost]
        public ActionResult AddKorisnik()
        {
            return View();
        }



















        [HttpPost]
        public ActionResult Pretraga()
        {
            //ViewBag.sakrij10 = "pretraga";
            //ViewBag.taj3 = ((Korisnik)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();

            foreach (var item in DomacinController.apartmani)
            {
                if (Request["Lokacija"] != "")
                {
                    if (item.Lokacija.Adresa.NaseljenoMesto == Request["Lokacija"])
                    {
                        if (!lista.Contains(item))
                        {
                            lista.Add(item);

                        }
                    }
                }

                if (Request["OdCena"] != "" && Request["DoCena"] != "")
                {
                    if (item.CenaPoNoci >= double.Parse(Request["OdCena"]) && item.CenaPoNoci <= double.Parse(Request["DoCena"]))
                    {
                        if (!lista.Contains(item))
                        {
                            lista.Add(item);

                        }
                    }
                }

                if (Request["OdSoba"] != "" && Request["DoSoba"] != "")
                {
                    if (item.BrojSoba >= int.Parse(Request["OdSoba"]) && item.BrojSoba <= int.Parse(Request["DoSoba"]))
                    {
                        if (!lista.Contains(item))
                        {
                            lista.Add(item);

                        }
                    }
                }

                if (Request["BrojOsoba"] != "")
                {
                    if (item.BrojGostiju == int.Parse(Request["BrojOsoba"]))
                    {
                        if (!lista.Contains(item))
                        {
                            lista.Add(item);

                        }
                    }
                }
            }


            ViewBag.Aktivni = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("Start");
        }

        public ActionResult Filter()
        {
            List<Amenitie> k = (List<Amenitie>)Session["filter"];
            if (k == null)
            {
                k = new List<Amenitie>();
                Session["filter"] = k;
            }
            foreach (var item in SingInOrRegisterController.basic)
            {
                if (item.Naziv == Request["naziv"])
                {
                    k.Add(item);
                }
            }

            foreach (var item in SingInOrRegisterController.family)
            {
                if (item.Naziv == Request["naziv"])
                {
                    k.Add(item);
                }
            }

            foreach (var item in SingInOrRegisterController.dining)
            {
                if (item.Naziv == Request["naziv"])
                {
                    k.Add(item);
                }
            }


            //ViewBag.sakrij10 = "filtrirani";
           // ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    //if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                   // {
                        foreach (var item2 in item.Amenities)
                        {
                            foreach (var item3 in k)
                            {
                                if (item2.Naziv == item3.Naziv)
                                {
                                    if (lista.Contains(item))
                                    {
                                    }
                                    else
                                        lista.Add(item);
                                }
                            }
                        }

                        if (item.StatusApartmana.ToString() == (string)Request["naziv3"])
                        {
                            if (lista.Contains(item))
                            {
                            }
                            else
                                lista.Add(item);
                        }

                        if (item.Tip.ToString() == (string)Request["naziv2"])
                        {
                            if (lista.Contains(item))
                            {
                            }
                            else
                                lista.Add(item);
                        }
                    }

           // }


            ViewBag.Aktivni = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            return View("Start");

        }


        public ActionResult Sortiranje()
        {
            //ViewBag.sakrij10 = "sortiranje";
            //ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    //if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }

            if (Request["sort"] != null)
            {
                if (Request["sort"] == "dole")
                {
                    for (int i = 0; i < lista.Count - 1; i++)
                    {
                        for (int j = i + 1; j < lista.Count; j++)
                        {
                            if (lista[i].CenaPoNoci < lista[j].CenaPoNoci)
                            {
                                Apartman temp = lista[i];
                                lista[i] = lista[j];
                                lista[j] = temp;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lista.Count - 1; i++)
                    {
                        for (int j = i + 1; j < lista.Count; j++)
                        {
                            if (lista[i].CenaPoNoci > lista[j].CenaPoNoci)
                            {
                                Apartman temp = lista[i];
                                lista[i] = lista[j];
                                lista[j] = temp;
                            }
                        }
                    }
                }
            }

            ViewBag.Aktivni = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("Start");
        }























        public static void CitajAdmineJson()
        {
            using (StreamReader r = new StreamReader(putanjaAdmini))
            {
                string json = r.ReadToEnd();
                admini = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            }

        }

        public static void UpisAdminaJson()
        {
            System.IO.File.WriteAllText(putanjaAdmini, JsonConvert.SerializeObject(admini, Formatting.Indented));
        }



        public static void CitajGosteJson()
        {
            using (StreamReader r = new StreamReader(putanjaGosti))
            {
                string json = r.ReadToEnd();
                gosti = JsonConvert.DeserializeObject<List<Gost>>(json);
            }

        }

        public static void UpisGostJson()
        {
            System.IO.File.WriteAllText(putanjaGosti, JsonConvert.SerializeObject(gosti, Formatting.Indented));
        }



        public static void CitajDomacineJson()
        {
            using (StreamReader r = new StreamReader(putanjaDomacini))
            {
                string json = r.ReadToEnd();
                domacini = JsonConvert.DeserializeObject<List<Domacin>>(json);
            }

        }

        public static void UpisDomacinJson()
        {
            System.IO.File.WriteAllText(putanjaDomacini, JsonConvert.SerializeObject(domacini, Formatting.Indented));
        }
    }
}