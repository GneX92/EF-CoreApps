using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_CoreKontakte.Models;

public static class InputForm
{
    private HttpClient client = new();

    public static async Task NewContact()
    {
        Kontakt k = new();

        k.FirstName = ParseField( k.FirstName );
        k.LastName = ParseField( k.LastName );
        k.Mail = ParseMail();
        k.ZipCode = ParseField( k.ZipCode );
        k.City = await client.GetFromJsonAsync<string>( "api.zippopotam.us/DE/" + k.ZipCode );
    }

    public static string ParseField( string k )
    {
        string? s;

        while ( true )
        {
            Console.Clear();
            Console.Write( $"{nameof( k )}: " );
            s = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"{nameof( k )} muss ausgefüllt sein." );
                Console.WriteLine( "<Press Any Key>" );
                continue;
            }

            break;
        }

        var arr = s.Split( ' ' );
        k = arr [ 0 ].Trim();
        return k;
    }

    public static string ParseMail()
    {
        string? s;
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        while ( true )
        {
            Console.Clear();
            Console.Write( $"E-Mail: " );
            s = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"E-Mail muss ausgefüllt sein." );
                Console.WriteLine( "<Press Any Key>" );
                continue;
            }

            if ( !Regex.IsMatch( s , pattern ) )
            {
                Console.WriteLine( $"Keine gültige E-Mail Adresse." );
                Console.WriteLine( "<Press Any Key>" );
                continue;
            }

            break;
        }

        var arr = s.Split( ' ' );
        s = arr [ 0 ].Trim();
        return s;
    }

    public static string ParseZip()
    {
        string? s;
        string pattern = @"^\d{5}$";

        while ( true )
        {
            Console.Clear();
            Console.Write( $"E-Mail: " );
            s = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"Postleitzahl muss ausgefüllt sein." );
                Console.WriteLine( "<Press Any Key>" );
                continue;
            }

            if ( !Regex.IsMatch( s , pattern ) )
            {
                Console.WriteLine( $"Keine gültige Postleitzahl." );
                Console.WriteLine( "<Press Any Key>" );
                continue;
            }

            break;
        }

        var arr = s.Split( ' ' );
        s = arr [ 0 ].Trim();
        return s;
    }
}