using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Amenitie
{
    public Amenitie(int id, string naziv)
    {
        Id = id;
        Naziv = naziv;
    }

    public Amenitie()
    {
    }

    public int Id { get; set; }
    public string Naziv { get; set; }
}
