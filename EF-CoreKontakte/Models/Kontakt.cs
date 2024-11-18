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
        return $"{LastName}, {FirstName} {Mail, 20} {ZipCode, 20} {City, 20}";
    }
}