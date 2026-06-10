using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AsistenciaGR.Models;

namespace AsistenciaGR.Data
{
    public class AsistenciaGRContext : DbContext
    {
        public DbSet<AsistenciaGR.Models.Inscripciones> Inscripciones { get; set; } = default!;
        public AsistenciaGRContext (DbContextOptions<AsistenciaGRContext> options)
            : base(options)
        {
        }

        public DbSet<AsistenciaGR.Models.Asistencia> Asistencia { get; set; } = default!;
        public DbSet<AsistenciaGR.Models.Carreras> Carreras { get; set; } = default!;
        public DbSet<AsistenciaGR.Models.Carreras_Materias> Carreras_Materias { get; set; } = default!;
        public DbSet<AsistenciaGR.Models.Materias> Materias { get; set; } = default!;
        public DbSet<AsistenciaGR.Models.Roles> Roles { get; set; } = default!;
        public DbSet<AsistenciaGR.Models.Usuarios> Usuarios { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Inscripciones -> Usuarios (many Inscripciones per Usuario)
            modelBuilder.Entity<Inscripciones>()
                .HasOne(i => i.Usuarios)
                .WithMany(u => u.Inscripciones)
                .HasForeignKey(i => i.UsId)
                .OnDelete(DeleteBehavior.Cascade);

            // Inscripciones -> Carreras_Materias (many Inscripciones per Carreras_Materias)
            modelBuilder.Entity<Inscripciones>()
                .HasOne(i => i.Carreras_Materias)
                .WithMany(cm => cm.Inscripciones)
                .HasForeignKey(i => i.CaMaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Asistencia optional relationships
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Usuario)
                .WithMany() // no navigation collection on Usuarios for Asistencia
                .HasForeignKey(a => a.UsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.CarreraMateria)
                .WithMany() // not mapping back to Asistencia
                .HasForeignKey(a => a.CaMaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
