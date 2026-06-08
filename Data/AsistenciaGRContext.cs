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
    }
}
