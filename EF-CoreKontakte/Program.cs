using EF_CoreKontakte.Models;

namespace EF_CoreKontakte
{
    public static class Program
    {
        public static DatabaseContext ctx = new();

        static async Task Main( string [] args )
        {

            do 
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
                        await InputForm.NewContact(ctx);
                        break;
                    case ConsoleKey.D2:
                        // Not finished yet
                        break;
                    case ConsoleKey.D3:
                        await InputForm.ShowContacts( ctx );
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit( 0 );
                        break;
                    default:
                        break;
                }

            } while ( true );
        }         
    }
}