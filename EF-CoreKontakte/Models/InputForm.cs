using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EF_CoreKontakte;

namespace EF_CoreKontakte.Models;

public static class InputForm
{
    private static HttpClient client = new();

    public static async Task NewContact(DatabaseContext ctx)
    {
        try
        {
            Kontakt k = new();

            k.FirstName = ParseField( k.FirstName );
            k.LastName = ParseField( k.LastName );
            k.Mail = ParseMail();
            k.ZipCode = ParseField( k.ZipCode );
            k.City = await GetCityFromZipCode( k.ZipCode );
            ctx.Kontakte?.Add( k );
            await ctx.SaveChangesAsync();
        }
        catch ( AggregateException ex )
        {
            foreach ( var item in ex.InnerExceptions )
                Console.WriteLine("Error: " + item.Message);
        }
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
                Console.WriteLine( $"Please enter a {nameof( k )}" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
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
            Console.WriteLine();
            Console.Write( $"E-Mail: " );
            s = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"Please enter an E-Mail" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                continue;
            }

            if ( !Regex.IsMatch( s , pattern ) )
            {
                Console.WriteLine( $"Invalid E-Mail" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
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
            Console.Write( $"ZIP Code: " );
            s = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"Please enter a ZIP Code" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                continue;
            }

            if ( !Regex.IsMatch( s , pattern ) )
            {
                Console.WriteLine( $"Invalid ZIP Code" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                continue;
            }

            break;
        }

        var arr = s.Split( ' ' );
        s = arr [ 0 ].Trim();
        return s;
    }

    private static async Task<string> GetCityFromZipCode( string zipCode )
    {
        while ( true )
        {
            try
            {
                Console.Clear();
                var response = await client.GetAsync( $"http://api.zippopotam.us/DE/{zipCode}" );

                response.EnsureSuccessStatusCode();
                string? jsonString = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse( jsonString );

                return doc.RootElement.GetProperty( "places" ) [ 0 ].GetProperty( "place name" ).GetString() ?? "Unknown";
            }
            catch ( HttpRequestException )
            {
                Console.WriteLine( "City not found" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                return null;
                
            }
            catch ( JsonException )
            {
                Console.WriteLine( "Unable to parse city data" );
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                return null;            
            }
        }
    }

    public static async Task ShowContacts( DatabaseContext ctx )
    {
        var contacts = await ctx.GetKontakteAsync();

        foreach ( var contact in contacts )
            Console.WriteLine( contact );
    }

    // Not finished
    public static async Task UpdateContacts( DatabaseContext ctx )
    {
        int id;
        while ( true )
        {
            Console.Clear();
            Console.Write( "Which Contact do you which to change? ( Contact ID ): " );

            if ( !int.TryParse( Console.ReadLine() , out id ) )
            {
                Console.WriteLine("Please enter a number");
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                continue;
            }

            if ( !ctx.Kontakte.Select(k => k.ID).Contains(id) )
            {
                Console.WriteLine("ID not found");
                Console.WriteLine( "<Press Any Key>" );
                Console.ReadKey();
                continue;
            }

            break;
        }

        Kontakt? k = ctx.Kontakte.First( c => c.ID == id );
        Console.Clear();
        k.FirstName = ReadLine.Read( "Firstname: " , k.FirstName );
        k.LastName = ReadLine.Read( "Lastname: " , k.LastName );
        k.Mail = ReadLine.Read( "E-Mail:" , k.FirstName );
        Console.WriteLine();
        Console.WriteLine();
        Console.ReadLine();

        ctx.Update( k );
        ctx.SaveChanges();
    }
}