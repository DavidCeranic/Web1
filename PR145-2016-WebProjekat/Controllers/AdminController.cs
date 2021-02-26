using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class AdminController : Controller
    {
        public string id_;
        public static List<Korisnik> admini = new List<Korisnik>();
        public static List<Amenitie> ameni = new List<Amenitie>();
        public static string putanjaAdmini, putanjaAmenities;
        
        // GET: Admin
        public ActionResult Index()
        {
            putanjaAdmini = HttpContext.Server.MapPath("~/Database/Apartmani.json");
            putanjaAmenities = HttpContext.Server.MapPath("~/Database/Amenities.json");
            SingInOrRegisterController.CitajAdmineJson();
            Session["filter"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult Start()
        {
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
            return View("StartAdmin");
        }

        [HttpPost]
        public ActionResult ChangeInfo(string id, int Id_)
        {
            ViewBag.info = "sakrij";



            id_ = id;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            foreach (var item in SingInOrRegisterController.admini)
            {
                if (item.KorisnickoIme == id)
                {
                    ViewBag.trenutni = item;
                }
            }

            foreach (var item in SingInOrRegisterController.admini)
            {
                if (item.Id == Id_)
                {
                    ViewBag.trenutni2 = item;
                }
            }




            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;

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
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("StartAdmin");
        }

        [HttpPost]
        public ActionResult SaveChanges(Korisnik novi)
        {
            if (novi.Ime == null || novi.Prezime == null || novi.Lozinka==null || novi.KorisnickoIme == null)
            {
                return View("Error");
            }
            int i = SingInOrRegisterController.admini.FindIndex(a => a.KorisnickoIme == novi.KorisnickoIme);
            SingInOrRegisterController.admini[i] = novi;

            foreach (var item in SingInOrRegisterController.admini)
            {
                if (item.KorisnickoIme == novi.KorisnickoIme)
                {
                    item.KorisnickoIme = novi.KorisnickoIme;
                    item.Ime = novi.Ime;
                    item.Prezime = novi.Prezime;
                    item.Lozinka = novi.Lozinka;
                    item.Pol = novi.Pol;
                    item.Uloga = novi.Uloga;
                    ViewBag.taj = item.KorisnickoIme;
                    item.Uloga = Uloge.Administrator;
                }
            }

            ViewBag.domacini = SingInOrRegisterController.admini;

            SingInOrRegisterController.UpisAdminaJson();

            ViewBag.info = "nesakrij";
            ViewBag.taj = ((Korisnik)Session["aktivanAdmin"]).KorisnickoIme;

            admini.Add(novi);
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            SingInOrRegisterController.UpisAdminaJson();

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
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.sakrij10 = "svi";
            return View("StartAdmin");
        }


        public ActionResult Obrisi(int IdBrisanja)
        {
            foreach (var item in DomacinController.apartmani)
            {
                if (item.Id == IdBrisanja)
                    item.Obirsan = true;
            }

            DomacinController.UpisApartmanJson();
            ViewBag.taj3 = ((Korisnik)Session["aktivanAdmin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Korisnik)Session["aktivanAdmin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni3 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Korisnik)Session["aktivanAdmin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni3 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;


            ViewBag.sakrij10 = "svi";
            return View("StartAdmin");
        }

        [HttpPost]
        public ActionResult Izmeni(string IdIzmene)
        {
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            foreach (var item in DomacinController.apartmani)
            {
                if (item.Id == int.Parse(IdIzmene))
                    ViewBag.a = item;
            }

            return View("Izmeni");
        }

        [HttpPost]
        public ActionResult Sacuvaj(Apartman novi, double GeografskaSirina, double GeografskaDuzina, string Ulica, int Broj, string NaseljenoMesto, string PostanskiBroj)
        {
            int i = DomacinController.apartmani.FindIndex(a => a.CenaPoNoci == novi.CenaPoNoci);
            DomacinController.apartmani[i] = novi;


            DomacinController.apartmani[i].Tip = novi.Tip;
            DomacinController.apartmani[i].BrojSoba = novi.BrojSoba;
            DomacinController.apartmani[i].BrojGostiju = novi.BrojGostiju;

            DomacinController.apartmani[i].Lokacija = new Lokacija();
            DomacinController.apartmani[i].Lokacija.Adresa = new Adresa();
            DomacinController.apartmani[i].Lokacija.GeografskaSirina = GeografskaSirina;
            DomacinController.apartmani[i].Lokacija.GeografskaDuzina = GeografskaDuzina;
            DomacinController.apartmani[i].Lokacija.Adresa.Ulica = Ulica;
            DomacinController.apartmani[i].Lokacija.Adresa.Broj = Broj;
            DomacinController.apartmani[i].Lokacija.Adresa.NaseljenoMesto = NaseljenoMesto;
            DomacinController.apartmani[i].Lokacija.Adresa.PostanskiBroj = PostanskiBroj;
            DomacinController.apartmani[i].Id = int.Parse(Request["idap"]);
            DomacinController.apartmani[i].Slike = new List<string>();
            DomacinController.apartmani[i].Slike.Add(Request["slap"]);

            DomacinController.apartmani[i].CenaPoNoci = novi.CenaPoNoci;
            DomacinController.apartmani[i].VremeOdjave = novi.VremeOdjave;
            DomacinController.apartmani[i].VremePrijave = novi.VremePrijave;
            DomacinController.apartmani[i].StatusApartmana = novi.StatusApartmana;
            DomacinController.apartmani[i].VremePrijave = TimeSpan.Parse(Request["VremePrijave"]);
            DomacinController.apartmani[i].VremeOdjave = TimeSpan.Parse(Request["VremeOdjave"]);

            DomacinController.apartmani[i].Amenities = new List<Amenitie>();
            foreach (var item in SingInOrRegisterController.basic)
            {
                if (Request[item.Naziv] != null)
                    DomacinController.apartmani[i].Amenities.Add(item);
            }
            foreach (var item in SingInOrRegisterController.dining)
            {
                if (Request[item.Naziv] != null)
                    DomacinController.apartmani[i].Amenities.Add(item);
            }
            foreach (var item in SingInOrRegisterController.family)
            {
                if (Request[item.Naziv] != null)
                    DomacinController.apartmani[i].Amenities.Add(item);
            }

            DomacinController.UpisApartmanJson();
            ViewBag.taj3 = ((Korisnik)Session["aktivanAdmin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Korisnik)Session["aktivanAdmin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni3 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Korisnik)Session["aktivanAdmin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni3 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;


            ViewBag.sakrij10 = "svi";
            return View("StartAdmin");
        }

        [HttpPost]
        public ActionResult Aktivan(string Id)
        {
            DomacinController.CitajApartmaneJson();

            foreach (var item in DomacinController.apartmani)
            {
                if (item.Id == int.Parse(Id))
                    item.StatusApartmana = StatusApartmana.Aktivno;

            }

            DomacinController.UpisApartmanJson();

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
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.sakrij10 = "svi";
            return View("StartAdmin");

        }

        [HttpPost]
        public ActionResult AddHouseholder(Domacin k)
        {
            if(k.KorisnickoIme==null || k.Lozinka==null || k.Prezime==null || k.Ime == null)
            {
                return View("Error");
            }
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.gosti;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            k.Id = SingInOrRegisterController.domacini.Count();
            k.Uloga = Uloge.Domacin;
            SingInOrRegisterController.domacini.Add(k);
            SingInOrRegisterController.svi.Add(k);

            SingInOrRegisterController.UpisDomacinJson();
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.korisnici = SingInOrRegisterController.svi;

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
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.sakrij10 = "svi";

            return View("StartAdmin");
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

            ViewBag.sakrij10 = "filter";

            ViewBag.taj3 = ((Korisnik)Session["aktivanAdmin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {

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




            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            return View("StartAdmin");

        }




        [HttpPost]
        public ActionResult Pretraga()
        {
            ViewBag.sakrij10 = "pretraga";
            

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


            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            return View("StartAdmin");
        }


        [HttpPost]
        public ActionResult PretragaKorisnika()
        {

            List<Korisnik> lista = SingInOrRegisterController.svi;
            List<Korisnik> nova = new List<Korisnik>();

            foreach (var item in lista)
            {
                if (Request["tip"] == "Admin")
                {
                    if(item.Uloga== Uloge.Administrator)
                    {
                        if (!nova.Contains(item))
                            nova.Add(item);
                    }
                }

                if (Request["tip"] == "User")
                {
                    if (item.Uloga == Uloge.Gost)
                    {
                        if (!nova.Contains(item))
                            nova.Add(item);
                    }
                }

                if (Request["tip"] == "Householder")
                {
                    if (item.Uloga == Uloge.Domacin)
                    {
                        if (!nova.Contains(item))
                            nova.Add(item);
                    }
                }

                if (Request["gener"] == "Zensko")
                {
                    if (item.Pol == Polovi.Zensko)
                    {
                        if (!nova.Contains(item))
                            nova.Add(item);
                    }
                }

                if (Request["gener"] == "Musko")
                {
                    if (item.Pol == Polovi.Musko)
                    {
                        if (!nova.Contains(item))
                            nova.Add(item);
                    }
                }

                if (Request["Username"] != "")
                {
                    if (item.KorisnickoIme == Request["Username"])
                    {
                        if (!nova.Contains(item))
                        {
                            nova.Add(item);

                        }
                    }
                }

            }




            List<Apartman> lista5 = new List<Apartman>();
            foreach (var itemm in DomacinController.apartmani)
            {
                if (itemm.StatusApartmana == StatusApartmana.Aktivno)
                    lista5.Add(itemm);
            }
            ViewBag.Aktivni3 = lista5;


            List<Apartman> lista6 = new List<Apartman>();
            foreach (var itemm in DomacinController.apartmani)
            {
                if (itemm.StatusApartmana == StatusApartmana.Neaktivno)
                    lista6.Add(itemm);
            }
            ViewBag.Neaktivni3 = lista6;




            ViewBag.Aktivni10 = lista;
            ViewBag.sakrij20 = "sakrij";

            ViewBag.sakrij10 = "svi";
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = nova;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            return View("StartAdmin");
        }



        public ActionResult Sortiranje()
        {
            ViewBag.sakrij10 = "sortiranje";
            

            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                
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

            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            return View("StartAdmin");
        }




        public ActionResult Stavke()
        {
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("Stavkee");
        }


        public ActionResult Nazad()
        {
            ViewBag.sakrij10 = "svi";


            ViewBag.domacini = SingInOrRegisterController.domacini;


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {

                lista.Add(item);
            }
            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.taj3 = ((Korisnik)Session["aktivanAdmin"]).KorisnickoIme;

            List<Apartman> lista5 = new List<Apartman>();
            foreach (var itemm in DomacinController.apartmani)
            {
                if (itemm.StatusApartmana == StatusApartmana.Aktivno)
                    lista5.Add(itemm);
            }
            ViewBag.Aktivni3 = lista5;


            List<Apartman> lista6 = new List<Apartman>();
            foreach (var itemm in DomacinController.apartmani)
            {
                if (itemm.StatusApartmana == StatusApartmana.Neaktivno)
                    lista6.Add(itemm);
            }
            ViewBag.Neaktivni3 = lista6;


            return View("StartAdmin");
        }


        public ActionResult Nazadd()
        {
            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            return View("../SingInOrRegister/Start");
        }





        public static void CitajAmenitiesJson()
        {
            using (StreamReader r = new StreamReader(putanjaAmenities))
            {
                string json = r.ReadToEnd();
                ameni = JsonConvert.DeserializeObject<List<Amenitie>>(json);
            }

        }

        public static void UpisAmenitiesJson()
        {
            System.IO.File.WriteAllText(putanjaAmenities, JsonConvert.SerializeObject(ameni, Formatting.Indented));
        }
    }
}