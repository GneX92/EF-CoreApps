using EF_CoreWarenlager.Models;
using System.Globalization;

Warehouse lager = ReadFile();

using ( DatabaseContext ctx = new() )
{
    Create( ctx );
    //Delete( ctx );
}

Console.ReadLine();

static void Create( DatabaseContext ctx )
{
    Warehouse lager = ReadFile();

    ctx.Warehouses.Add( lager );

    foreach ( var item in lager.Products )
        ctx.Products.Add( item );

    ctx.SaveChanges();
}

static void Delete( DatabaseContext ctx )
{
    Warehouse? warehouse = ( from w in ctx.Warehouses
                             where w.WarehouseID.Equals( 3 )
                             select w ).FirstOrDefault();

    warehouse?.Products.ToList().ForEach( p => ctx.Products.Remove( p ) );

    ctx.Warehouses.Remove( warehouse );
    ctx.SaveChanges();
}

static Warehouse ReadFile()
{
    Warehouse? lager = new();

    string path = @"C:\Users\ITA5-TN05\Desktop\artikel.txt";

    string [] lines = File.ReadAllLines( path );

    foreach ( string line in lines )
    {
        string [] values = line.Split( ';' );

        values [ 4 ] = values [ 4 ].Replace( ',' , '.' );

        Product p = new()
        {
            ProductID = int.Parse( values [ 0 ] ) ,
            Bezeichnung = values [ 1 ] ,
            IstBestand = int.Parse( values [ 2 ] ) ,
            Höchstbestand = int.Parse( values [ 3 ] ) ,
            Preis = double.Parse( values [ 4 ] , CultureInfo.InvariantCulture ) ,
            Tagesverbrauch = int.Parse( values [ 5 ] ) ,
            Bestelldauer = int.Parse( values [ 6 ] ) ,
            Meldebestand = int.Parse( values [ 5 ] ) * ( int.Parse( values [ 6 ] ) + 2 ) ,
            Bestellvorschlag = int.Parse( values [ 2 ] ) <= ( int.Parse( values [ 5 ] ) * ( int.Parse( values [ 6 ] ) + 2 ) ) ? true : false
        };

        lager?.Products?.Add( p );
    }

    return lager;
}