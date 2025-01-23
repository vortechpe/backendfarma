using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<TipoEntidad> TiposEntidad { get; set; }
        public DbSet<TipoPersona> TiposPersona { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<ParametroMinsa> ParametrosMinsa { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura las relaciones de clave primaria, si es necesario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.UsuarioID);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.TipoPersona)
                .WithMany(tp => tp.Personas)
                .HasForeignKey(p => p.TipoPersonaID);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.TipoEntidad)
                .WithMany(te => te.Personas)
                .HasForeignKey(p => p.TipoEntidadID);
        }

    }
}
