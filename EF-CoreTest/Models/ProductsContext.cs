using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreTest.Models;

public class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public ProductsContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseLazyLoadingProxies( false )
                      .UseSqlServer( @"Server=.\SQLEXPRESS;Database=ProductsDB;Trusted_Connection=True;Encrypt=False" );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Product>().ToTable( "Product" );
        modelBuilder.Entity<Category>().ToTable( "Category" );
    }
}