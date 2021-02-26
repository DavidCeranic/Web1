using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum Tip { CeoApartman, Soba}
public enum StatusApartmana { Aktivno, Neaktivno}

public class Apartman
{
    public Tip Tip { get; set; }
    public int BrojSoba { get; set; }
    public int BrojGostiju { get; set; }
    public Lokacija Lokacija { get; set; }
    public List<DateTime> DatumiIzdavanja { get; set; }
    public List<DateTime> DatumiDostupnost { get; set; }
    public Domacin Domacin { get; set; }
    public List<Komentar> komentari { get; set; }
    public List<string> Slike { get; set; }
    public double CenaPoNoci { get; set; }
    public TimeSpan VremePrijave { get; set; }
    public TimeSpan VremeOdjave { get; set; }
    public StatusApartmana StatusApartmana { get; set; }
    public List<Amenitie> Amenities { get; set; }
    public List<Rezervacija> Rezervacije { get; set; }
    public int Id { get; set; }
    public int IdKorisnika { get; set; }

    private bool m_Obrisan = false;
    public bool Obirsan { get { return m_Obrisan; }
                          set { m_Obrisan = value; }}


}
