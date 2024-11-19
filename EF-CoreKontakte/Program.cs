using EF_CoreKontakte.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EF_CoreKontakte;

public static class Program
{
    private static DatabaseContext ctx = new();

    public static DatabaseContext Context { get; set; } = ctx;

    static async Task Main( string [] args )
    {
        while( true ) 
        {
            Console.Clear();
            Console.WriteLine( "[1] Kontakt hinzufügen" );
            Console.WriteLine( "[2] Kontakt bearbeiten" );
            Console.WriteLine( "[3] Kontakte anzeigen" );
            Console.WriteLine( "<ESC> Beenden" );

            var choice = Console.ReadKey( true ).Key;

            switch ( choice )
            {
                case ConsoleKey.D1:
                    await InputForm.NewContact(Context);
                    break;
                case ConsoleKey.D2:
                    await InputForm.UpdateContacts( Context );
                    break;
                case ConsoleKey.D3:
                    await InputForm.ShowContacts( Context );
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit( 0 );
                    break;
                default:
                    break;
            }
        }
    }         
}