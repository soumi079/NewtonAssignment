using DLCGGameCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLCGGameCore.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<VideoGame> VideoGames => Set<VideoGame>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGame>()
                .HasIndex(vg => vg.Title);
            modelBuilder.Entity<VideoGame>()
                .HasIndex(vg => vg.Genre);

            modelBuilder.Entity<VideoGame>()
                .HasIndex(vg => vg.ReleaseYear);

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<VideoGame>()
                .Property(vg => vg.Price)                
                .HasPrecision(18, 2);
        }
    }
}
