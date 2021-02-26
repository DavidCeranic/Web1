using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class DomacinController : Controller
    {
        public string id_;
        public static List<Apartman> apartmani = new List<Apartman>();
        public static string putanjaApartmani;

        // GET: Domacin
        public ActionResult Index()
        {
            putanjaApartmani = HttpContext.Server.MapPath("~/Database/Apartmani.json");
            CitajApartmaneJson();
            Session["filter"] = null;

            return View();
        }

        public ActionResult StartDomacin()
        {
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("StartDomacin");
        }

        public ActionResult ChangeInfo(string id, int Id_)
        {
            ViewBag.info3 = "sakrij";
            id_ = id;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            foreach (var item in SingInOrRegisterController.domacini)
            {
                if (item.KorisnickoIme == id)
                {
                    ViewBag.trenutni = item;
                }
            }

            foreach (var item in SingInOrRegisterController.domacini)
            {
                if (item.Id == Id_)
                {
                    ViewBag.trenutni2 = item;
                }
            }

            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if(item.IdKorisnika==((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;


            return View("StartDomacin");
        }

        public ActionResult SaveChanges(Domacin novi)
        {
            if(novi.Ime==null || novi.KorisnickoIme==null || novi.Lozinka==null || novi.Prezime == null)
            {
                return View("Error");
            }
            int i = SingInOrRegisterController.domacini.FindIndex(a => a.KorisnickoIme == novi.KorisnickoIme);
            SingInOrRegisterController.domacini[i] = novi;

            foreach (var item in SingInOrRegisterController.domacini)
            {
                if (item.KorisnickoIme == novi.KorisnickoIme)
                {
                    item.KorisnickoIme = novi.KorisnickoIme;
                    item.Ime = novi.Ime;
                    item.Prezime = novi.Prezime;
                    item.Lozinka = novi.Lozinka;
                    item.Pol = novi.Pol;
                    //item.Uloga = novi.Uloga;
                    ViewBag.taj = item.KorisnickoIme;
                    item.Uloga = Uloge.Domacin;
                }
            }

            ViewBag.info3 = "nesakrij";
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;
            ViewBag.domacini = SingInOrRegisterController.domacini;

            SingInOrRegisterController.UpisDomacinJson();


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.sakrij10 = "svi";


            return View("StartDomacin");
        }

        public ActionResult PregledApartmana()
        {
            ViewBag.Sad = Session["aktivanDomacin"];
            CitajApartmaneJson();
            ViewBag.Ap = DomacinController.apartmani;
            return View("PregledApartmana");
        }

        public ActionResult DodajApartman()
        {
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            return View("DodajApartman");
        }

        [HttpPost]
        public ActionResult Dodaj(HttpPostedFileBase file)
        {
            CitajApartmaneJson();
            SingInOrRegisterController.CitajDomacineJson();

            Apartman a = new Apartman();

            a.Tip=(Tip)Enum.Parse(typeof(Tip),Request["Tip"]);
            a.BrojSoba = int.Parse(Request["BrojSoba"]);
            a.BrojGostiju = int.Parse(Request["BrojGostiju"]);

            a.Lokacija = new Lokacija()
            {
                GeografskaSirina = double.Parse(Request["GeografskaSirina"]),
                GeografskaDuzina = double.Parse(Request["GeografskaDuzina"]),

                Adresa = new Adresa()
                {
                    Ulica = Request["Ulica"],
                    Broj = int.Parse(Request["Broj"]),
                    NaseljenoMesto = Request["NaseljenoMesto"]
                }
            };

            a.Domacin = (Domacin)Session["aktivanDomacin"];
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            a.Lokacija.Adresa.PostanskiBroj = Request["PostanskiBroj"];

            a.DatumiDostupnost = new List<DateTime>();
            a.DatumiDostupnost.Add(DateTime.Parse(Request["DatumiDostupnost"]));

            a.VremePrijave = TimeSpan.Parse(Request["VremePrijave"]);
            a.VremeOdjave = TimeSpan.Parse(Request["VremeOdjave"]);

            a.CenaPoNoci = int.Parse(Request["CenaPoNoci"]);
            
            a.Amenities = new List<Amenitie>();
            foreach (Amenitie item in SingInOrRegisterController.basic)
            {
                if (Request[item.Naziv] == "on")
                {
                    a.Amenities.Add(item);
                }
            }

            foreach (Amenitie item in SingInOrRegisterController.family)
            {
                if (Request[item.Naziv] == "on")
                {
                    a.Amenities.Add(item);
                }
            }

            foreach (Amenitie item in SingInOrRegisterController.dining)
            {
                if (Request[item.Naziv] == "on")
                {
                    a.Amenities.Add(item);
                }
            }

            //a.StatusApartmana = (StatusApartmana)Enum.Parse(typeof(StatusApartmana), Request["StatusApartmana"]);
            a.StatusApartmana = StatusApartmana.Neaktivno;
            a.Id = apartmani.Count();
            a.IdKorisnika = ((Domacin)Session["aktivanDomacin"]).Id;

            //((Domacin)Session["aktivanDomacin"]).ApartmaniZaIzdavanje = new List<Apartman>();
            //((Domacin)Session["aktivanDomacin"]).ApartmaniZaIzdavanje.Add(a);
            if (a.Slike == null)
            {
                a.Slike = new List<string>();
            }



            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                if (!a.Slike.Contains("/Slike/" + pic))
                {
                    string path = System.IO.Path.Combine(Server.MapPath("~/Slike"), pic);
                    // file is uploaded
                    file.SaveAs(path);
                    a.Slike.Add("/Slike/" + pic);
                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }
            }

            apartmani.Add(a);
            UpisApartmanJson();

            

            //SingInOrRegisterController.domacini.Add((Domacin)Session["aktivanDomacin"]);
            
            SingInOrRegisterController.UpisDomacinJson();
            //SingInOrRegisterController.putanjaDomacini = HttpContext.Server.MapPath("/Database/Domacini.json");
            //SingInOrRegisterController.UpisDomacinJson();

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;


            ViewBag.hide = "nohide";
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            Session["TrenutniApartman"] = a;

            return View("DodajApartman");
        }




        public ActionResult IzmeniApartman(int IdIzmene)
        {
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            foreach (var item in apartmani)
            {
                if (item.Id == IdIzmene)
                    ViewBag.a = item;
            }

            ViewBag.hide = "hide";
            
            return View("DodajApartman");
        }


        public ActionResult Obrisi(int IdBrisanja)
        {
            foreach (var item in apartmani)
            {
                if (item.Id == IdBrisanja)
                    item.Obirsan = true;
            }

            UpisApartmanJson();
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;


            ViewBag.sakrij10 = "svi";
            return View("StartDomacin");
        }
        


        [HttpPost]
        public ActionResult Sacuvaj(Apartman novi, double GeografskaSirina, double GeografskaDuzina, string Ulica, int Broj, string NaseljenoMesto, string PostanskiBroj)
        {
            
            int i = apartmani.FindIndex(a => a.CenaPoNoci == novi.CenaPoNoci);
            apartmani[i] = novi;

       
                    apartmani[i].Tip = novi.Tip;
                    apartmani[i].BrojSoba = novi.BrojSoba;
                    apartmani[i].BrojGostiju = novi.BrojGostiju;

                    apartmani[i].Lokacija = new Lokacija();
                    apartmani[i].Lokacija.Adresa = new Adresa();
                    apartmani[i].Lokacija.GeografskaSirina = GeografskaSirina;
                    apartmani[i].Lokacija.GeografskaDuzina = GeografskaDuzina;
                    apartmani[i].Lokacija.Adresa.Ulica = Ulica;
                    apartmani[i].Lokacija.Adresa.Broj = Broj;
                    apartmani[i].Lokacija.Adresa.NaseljenoMesto = NaseljenoMesto;
                    apartmani[i].Lokacija.Adresa.PostanskiBroj = PostanskiBroj;
                    DomacinController.apartmani[i].Id = int.Parse(Request["idap"]);
                    DomacinController.apartmani[i].Slike = new List<string>();
                    DomacinController.apartmani[i].Slike.Add(Request["slap"]);

                    apartmani[i].CenaPoNoci = novi.CenaPoNoci;
                    apartmani[i].VremeOdjave = novi.VremeOdjave;
                    apartmani[i].VremePrijave = novi.VremePrijave;
                    apartmani[i].StatusApartmana = novi.StatusApartmana;
                    apartmani[i].VremePrijave = TimeSpan.Parse(Request["VremePrijave"]);
                    apartmani[i].VremeOdjave = TimeSpan.Parse(Request["VremeOdjave"]);

                    apartmani[i].Amenities = new List<Amenitie>();
            foreach (var item in SingInOrRegisterController.basic)
            {
                if (Request[item.Naziv] != null)
                    apartmani[i].Amenities.Add(item);
            }
            foreach (var item in SingInOrRegisterController.dining)
            {
                if (Request[item.Naziv] != null)
                    apartmani[i].Amenities.Add(item);
            }
            foreach (var item in SingInOrRegisterController.family)
            {
                if (Request[item.Naziv] != null)
                    apartmani[i].Amenities.Add(item);
            }

            ViewBag.hide = "nohide";
            UpisApartmanJson();
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.sakrij10 = "svi";

            return View("StartDomacin");
        }

        [HttpPost]
        public ActionResult Pretraga()
        {
            ViewBag.sakrij10 = "pretraga";
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();

            foreach (var item in apartmani)
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
            return View("StartDomacin");
        }

        public ActionResult Filter()
        {
            List<Amenitie> k = (List<Amenitie>)Session["filter"];
            if(k == null)
            {
                k = new List<Amenitie>();
                Session["filter"] = k;
            }
            foreach (var item in SingInOrRegisterController.basic)
            {
                if (item.Naziv==Request["naziv"])
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

            
            ViewBag.sakrij10 = "filtrirani";
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
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
                        
            }


            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;

            return View("StartDomacin");

        }


        public ActionResult Sortiranje()
        {
            ViewBag.sakrij10 = "sortiranje";
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;

            ViewBag.domacini = SingInOrRegisterController.domacini;


            CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }

            if (Request["sort"] != null)
            {
                if (Request["sort"] == "dole")
                {
                    for (int i = 0; i < lista.Count-1; i++)
                    {
                        for (int j = i+1; j < lista.Count; j++)
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
            return View("StartDomacin");
        }


        public ActionResult Kraj()
        {
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista.Add(item);
            }
            ViewBag.Aktivni2 = lista;


            List<Apartman> lista2 = new List<Apartman>();
            foreach (var item in apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Neaktivno)
                    if (item.IdKorisnika == ((Domacin)Session["aktivanDomacin"]).Id)
                        lista2.Add(item);
            }
            ViewBag.Neaktivni2 = lista2;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.taj3 = ((Domacin)Session["aktivanDomacin"]).KorisnickoIme;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.sakrij10 = "svi";
            return View("StartDomacin");
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


            public static void CitajApartmaneJson()
        {
            using (StreamReader r = new StreamReader(putanjaApartmani))
            {
                string json = r.ReadToEnd();
                apartmani = JsonConvert.DeserializeObject<List<Apartman>>(json);
            }
        }

        public static void UpisApartmanJson()
        {
            System.IO.File.WriteAllText(putanjaApartmani, JsonConvert.SerializeObject(apartmani, Formatting.Indented));
        }
    }
}