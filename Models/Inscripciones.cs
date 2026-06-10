using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Inscripciones
    {
        [Key]
        public int InId { get; set; }
        public int UsId { get; set; }
        public int CaMaId { get; set; }
        // Navigation to single Usuario (estudiante) for this inscripción
        public virtual Usuarios? Usuarios { get; set; }
        // Navigation to single Carreras_Materias for this inscripción
        public virtual Carreras_Materias? Carreras_Materias { get; set; }
    }
}
