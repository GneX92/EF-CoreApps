using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EF_CoreKontakte.Models;

public class Kontakt
{
    [Key()]
    public int ID { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Mail { get; set; }

    public string? ZipCode { get; set; }

    [JsonPropertyName( "post code" )]
    public string? City { get; set; }

    public override string ToString()
    {
        const string s = "N/A";

        return string.Format( "{0, -20} {1, -20} {2, -30} {3, -10} {4, -20}" ,
            LastName ?? s ,
            FirstName ?? s ,
            Mail ?? s ,
            ZipCode ?? s ,
            City ?? s );
    }
}