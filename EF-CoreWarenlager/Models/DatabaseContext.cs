using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreWarenlager.Models;

public class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Warehouse> Warehouses { get; set; }

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseLazyLoadingProxies()
                      .UseSqlServer( @"Server=.\SQLEXPRESS;Database=WarehouseDB;Trusted_Connection=True;Encrypt=False" );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Product>().ToTable( "Product" );
        modelBuilder.Entity<Warehouse>().ToTable( "Warehouse" );
    }
}