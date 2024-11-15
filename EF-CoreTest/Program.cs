using EF_CoreTest.Models;
using Microsoft.Identity.Client;

Category support = new()
{
    Name = "Support"
};

Product lkb = new()
{
    Name = "Lerberkäsbrötchen" ,
    Price = 3.50 ,
    Category = support
};

//using ( ProductsContext ctx = new() )
//    Create( ctx );

using ( ProductsContext ctx = new() )
    Read( ctx );

//using ( ProductsContext ctx = new() )
//    Update( ctx );

//using ( ProductsContext ctx = new() )
//    Read( ctx );

Console.ReadLine();

static void Create( ProductsContext ctx )
{
    Category support = new()
    {
        Name = "Support"
    };

    Product lkb = new()
    {
        Name = "Lerberkäsbrötchen" ,
        Price = 3.50 ,
        Category = support
    };

    ctx.Products.Add( lkb );
    ctx.SaveChanges();
}

static void Read( ProductsContext ctx )
{
    foreach ( var product in ctx.Products )
    {
        Console.WriteLine( "{0}, {1} Euro Kategorie: {2}" , product.Name ,
        product.Price , product.Category.Name );
    }

    foreach ( var category in ctx.Categories )
    {
        Console.WriteLine( "Kategorie: {0}" , category.Name );
        foreach ( var product in category.Products )
        {
            Console.WriteLine( "\t - {0}" , product.Name );
        }
    }
}

static void Update( ProductsContext ctx )
{
    var product = ctx.Categories.Where( x => x.Name == "Support" ).Select( p => p.Products ).First();

    foreach ( var p in product )
    {
        p.Price += 1;
    }

    ctx.SaveChanges();
}

static void Delete( ProductsContext ctx )
{
    Category? category = ( from c in ctx.Categories
                           where c.Name.Equals( "Support" )
                           select c ).FirstOrDefault();

    category?.Products.ToList().ForEach( p => ctx.Products.Remove( p ) );

    ctx.Categories.Remove( category );
    ctx.SaveChanges();
}