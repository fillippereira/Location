using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Location.Models;

namespace Location.Models
{
    public class LocationContext : DbContext
    {
        public LocationContext (DbContextOptions<LocationContext> options)
            : base(options)
        {
        }

        public DbSet<Location.Models.Cliente> Cliente { get; set; }

        public DbSet<Location.Models.Tipo> Tipo { get; set; }

        public DbSet<Location.Models.Imovel> Imovel { get; set; }

        public DbSet<Location.Models.Contrato> Contrato { get; set; }

        public DbSet<Location.Models.Foto> Foto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Imovel>()
                .HasMany(i => i.Fotos)
                .WithOne(f => f.Imovel)
                .HasForeignKey(f => f.ImovelId);

            modelBuilder.Entity<Foto>()
                .HasOne(f => f.Imovel)
                .WithMany(i => i.Fotos)
                .HasForeignKey(f => f.ImovelId);
        }
    }

}
