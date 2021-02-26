using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 public class Adresa
 {
    public string Ulica { get; set; }
    public int Broj { get; set; }
    public string NaseljenoMesto { get; set; }
    public string PostanskiBroj { get; set; }

    public override string ToString()
    {
        return string.Format("{0} {1}"+Environment.NewLine+"{2} {3}", Ulica, Broj, NaseljenoMesto, PostanskiBroj);
    }
}
