using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat.Controllers
{
    public class UserController : Controller
    {
        private string id_;
        // GET: User
        public ActionResult Index()
        {
            Session["filter"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeInfo(string id, int Id_)
        {
            ViewBag.info2 = "sakrij";
            id_ = id;
            ViewBag.korisnici = SingInOrRegisterController.gosti;
            foreach (var item in SingInOrRegisterController.gosti)
            {
                if (item.KorisnickoIme == id)
                {
                    ViewBag.trenutni = item;
                }
            }

            foreach (var item in SingInOrRegisterController.gosti)
            {
                if (item.Id == Id_)
                {
                    ViewBag.trenutni2 = item;
                }
            }

            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni10 = lista;
            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.gosti = SingInOrRegisterController.gosti;

            SingInOrRegisterController.UpisGostJson();
            
            ViewBag.taj3 = ((Gost)Session["aktivanGost"]).KorisnickoIme;

            return View("StartUser");
        }

        [HttpPost]
        public ActionResult SaveChanges(Gost novi)
        {
            if(novi.Ime==null || novi.Lozinka==null || novi.Prezime== null)
            {
                return View("Error");
            }
            int i = SingInOrRegisterController.gosti.FindIndex(a => a.KorisnickoIme == novi.KorisnickoIme);
            SingInOrRegisterController.gosti[i] = novi;

            foreach (var item in SingInOrRegisterController.gosti)
            {
                if (item.KorisnickoIme == novi.KorisnickoIme)
                {
                    item.KorisnickoIme = novi.KorisnickoIme;
                    item.Ime = novi.Ime;
                    item.Prezime = novi.Prezime;
                    item.Lozinka = novi.Lozinka;
                    item.Pol = novi.Pol;
                    item.Uloga = novi.Uloga;
                    item.Id = novi.Id;
                    ViewBag.taj = item.KorisnickoIme;
                }
            }



            ViewBag.info2 = "nesakrij";
            ViewBag.gosti = SingInOrRegisterController.gosti;
            
            SingInOrRegisterController.UpisGostJson();


            DomacinController.CitajApartmaneJson();
            List<Apartman> lista = new List<Apartman>();
            foreach (var item in DomacinController.apartmani)
            {
                if (item.StatusApartmana == StatusApartmana.Aktivno)
                    lista.Add(item);
            }
            ViewBag.Aktivni10 = lista;
            ViewBag.Aktivni10 = lista;
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.gosti = SingInOrRegisterController.gosti;

            SingInOrRegisterController.UpisGostJson();
            ViewBag.info2 = "nesakrij";
            ViewBag.taj3 = ((Gost)Session["aktivanGost"]).KorisnickoIme;
            ViewBag.sakrij10 = "svi";

            return View("StartUser");
        }


        [HttpPost]
        public ActionResult Pretraga()
        {
            ViewBag.sakrij10 = "pretraga";
            ViewBag.info2 = "nesakrij";


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
            ViewBag.gosti = SingInOrRegisterController.gosti;

            SingInOrRegisterController.UpisGostJson();
            

            //DomacinController.CitajApartmaneJson();
            //List<Apartman> lista2 = new List<Apartman>();
            //foreach (var item in DomacinController.apartmani)
            //{
            //    if (item.StatusApartmana == StatusApartmana.Aktivno)
            //        lista.Add(item);
            //}
            //ViewBag.Aktivni = lista2;
            ViewBag.info2 = "nesakrij";
            ViewBag.taj3 = ((Gost)Session["aktivanGost"]).KorisnickoIme;
            return View("StartUser");
        }



        public ActionResult Sortiranje()
        {
            ViewBag.sakrij10 = "sortirani";
            ViewBag.info2 = "nesakrij";

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
            ViewBag.gosti = SingInOrRegisterController.gosti;

            SingInOrRegisterController.UpisGostJson();


            //DomacinController.CitajApartmaneJson();
            //List<Apartman> lista2 = new List<Apartman>();
            //foreach (var item in DomacinController.apartmani)
            //{
            //    if (item.StatusApartmana == StatusApartmana.Aktivno)
            //        lista.Add(item);
            //}
            //ViewBag.Aktivni = lista2;
            ViewBag.info2 = "nesakrij";
            ViewBag.taj3 = ((Gost)Session["aktivanGost"]).KorisnickoIme;
            return View("StartUser");
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

            ViewBag.sakrij10 = "filtrirani";

            ViewBag.taj3 = ((Gost)Session["aktivanGost"]).KorisnickoIme;

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




            
            ViewBag.basic = SingInOrRegisterController.basic;
            ViewBag.family = SingInOrRegisterController.family;
            ViewBag.dining = SingInOrRegisterController.dining;
            ViewBag.admini = SingInOrRegisterController.admini;
            ViewBag.korisnici = SingInOrRegisterController.svi;
            ViewBag.domacini = SingInOrRegisterController.domacini;
            ViewBag.Aktivni10 = lista;
            

            ViewBag.gosti = SingInOrRegisterController.gosti;

            //DomacinController.CitajApartmaneJson();
            //List<Apartman> lista2 = new List<Apartman>();
            //foreach (var item in DomacinController.apartmani)
            //{
            //    if (item.StatusApartmana == StatusApartmana.Aktivno)
            //        lista.Add(item);
            //}
            //ViewBag.Aktivni = lista2;
            ViewBag.info2 = "nesakrij";
            return View("StartUser");

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
    }
}




