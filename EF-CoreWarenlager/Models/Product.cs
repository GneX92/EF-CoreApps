using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreWarenlager.Models;

public class Product
{
    [Key()]
    [DatabaseGenerated( DatabaseGeneratedOption.None )]
    public int ProductID { get; set; }

    public string? Bezeichnung { get; set; }

    public int IstBestand { get; set; }

    public int Höchstbestand { get; set; }

    public double Preis { get; set; }

    public int Tagesverbrauch { get; set; }

    public int Bestelldauer { get; set; }

    public int Meldebestand { get; set; }

    public bool Bestellvorschlag { get; set; }
}