using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Gost : Korisnik
{
    public List<Apartman> IznajmljeniApartami { get; set; }
    public List<Rezervacija> Rezervacije { get; set; }
}
