using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum StatusRezervacije { Kreirana, Odbijena, Odustanak, Prihvacena, Zavrsena}

public class Rezervacija
{
    public Apartman Apartman { get; set; }
    public DateTime PocetniDatumRezervacije { get; set; }

    private int m_BrojNociRezervacije = 1;
    public int BrojNociRezervacije { get { return m_BrojNociRezervacije; }
                                     set { m_BrojNociRezervacije = value; } }

    public double UkupnaCenaRezervacije { get; set; }
    public Gost Gost { get; set; }
    public StatusRezervacije StatusRezervacije { get; set; }


}
