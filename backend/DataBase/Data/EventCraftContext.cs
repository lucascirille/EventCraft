using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Data
{
    public class EventCraftContext : DbContext
    {
        public EventCraftContext(DbContextOptions<EventCraftContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salon>().ToTable("Salon");
            modelBuilder.Entity<SalonCaracteristica>().ToTable("SalonCaracteristica");
            modelBuilder.Entity<SalonServicio>().ToTable("SalonServicio");
            modelBuilder.Entity<ReservaServicio>().ToTable("ReservaServicio");
            modelBuilder.Entity<Caracteristica>().ToTable("Caracteristica");
            modelBuilder.Entity<Servicio>().ToTable("Servicio");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<Usuario>().ToTable("Usuario").HasData(
                    new Usuario { Id = 1, NombreUsuario = "superUsuario", Nombre = "super", Apellido = "usuario", Rol = "Admin", Correo = "superUsuario@superUsuario.com", ClaveHasheada = "$2a$10$zGO8.UGzTn2yR4DwgLZsnOwhdkz8B5VJ6b6KQ7gnHlSPxKNarZcLq" }
                );
        }

        public DbSet<Salon> Salones { get; set; }
        public DbSet<SalonCaracteristica> SalonCaracteristicas { get; set; }
        public DbSet<SalonServicio> SalonServicios { get; set; }
        public DbSet<ReservaServicio> ReservaServicios { get; set; }
        public DbSet<Caracteristica> Caracteristicas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
