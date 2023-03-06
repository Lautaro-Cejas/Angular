using back_end.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new { x.ActorID, x.PeliculaID });
            modelBuilder.Entity<PeliculasGeneros>()
                .HasKey(x => new { x.GeneroID, x.PeliculaID });
            modelBuilder.Entity<PeliculasCines>()
                .HasKey(x => new { x.CineID, x.PeliculaID });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<PeliculasCines> PeliculasCines { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
