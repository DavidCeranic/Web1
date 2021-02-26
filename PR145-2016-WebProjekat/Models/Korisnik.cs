using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum Polovi { Zensko, Musko}
public enum Uloge { Administrator, Domacin, Gost}

public class Korisnik
{
    public string KorisnickoIme { get; set; }
    public string Lozinka { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public Polovi Pol { get; set; }
    public Uloge Uloga { get; set; }
    public int Id { get; set; }
}
