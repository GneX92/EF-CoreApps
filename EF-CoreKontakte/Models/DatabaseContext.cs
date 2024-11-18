using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CoreKontakte.Models;

public class DatabaseContext : DbContext
{
    private DbSet<Kontakt>? contacts;

    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseLazyLoadingProxies()
                      .UseSqlServer( @"Server=.\SQLEXPRESS;Database=Warehouse3DB;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False" );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Kontakt>().ToTable( "Contact" );
    }
}