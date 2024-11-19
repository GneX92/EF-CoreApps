using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EF_CoreKontakte;
using Microsoft.Identity.Client;

namespace EF_CoreKontakte.Models;

public static class InputForm
{
    private static HttpClient client = new();

    public static async Task NewContact(DatabaseContext ctx)
    {
        try
        {
            Kontakt k = new();

            k.FirstName = ParseField( k.FirstName , "Firstname" );
            k.LastName = ParseField( k.LastName , "Lastname" );
            k.Mail = ParseMail(k.Mail , "E-Mail" );
            k.ZipCode = ParseField( k.ZipCode , "ZIP Code" );
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

    public static string ParseField( string? content, string prompt )
    {
        string? s;

        while ( true )
        {
            Console.Clear();            
            s = ReadLine.Read( $"{prompt}: " , content );

            if ( string.IsNullOrWhiteSpace( s ) )
            {
                Console.WriteLine( $"Please enter a {prompt}" );
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

    public static string ParseMail( string? content , string prompt )
    {
        string? s;
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        while ( true )
        {
            Console.Clear();
            s = ReadLine.Read( $"{prompt}: " , content );

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

    public static string ParseZip( string? content , string prompt )
    {
        string? s;
        string pattern = @"^\d{5}$";

        while ( true )
        {
            Console.Clear();
            s = ReadLine.Read( $"{prompt}: " , content );

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

        Console.Clear();

        Console.WriteLine( string.Format( "{0,-20} {1,-20} {2,-30} {3,-10} {4,-20}" ,
                                          "Last Name" , "First Name" , "Email" , "Zip Code" , "City" ) );
        Console.WriteLine( new string( '-' , 100 ) );

        foreach ( var contact in contacts )
            Console.WriteLine( contact );
    }

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

        k.FirstName = ParseField( k.FirstName , "Firstname" );
        k.LastName = ParseField( k.LastName , "Lastname" );
        k.Mail = ParseMail( k.Mail , "E-Mail" );
        k.ZipCode = ParseField( k.ZipCode , "ZIP Code" );
        k.City = await GetCityFromZipCode( k.ZipCode );

        Console.ReadLine();

        ctx.Update( k );
        await ctx.SaveChangesAsync();
    }
}