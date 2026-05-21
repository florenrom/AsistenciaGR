using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Asistencia
    {
        // Identificador único del registro.
        [Key]
        public int AsId { get; set; }

        // Fecha y hora de la toma de asistencia.
        public DateTime AsFechaHora { get; set; }

        // Bloque horario de la clase impartida.
        public string AsModulo { get; set; }

        // Motivo de ausencia.
        public string? AsJustificacion { get; set; }

        // Asignatura correspondiente al registro.
        public string AsMateria { get; set; }

        // Programa de estudio o carrera.
        public string AsCarrera { get; set; }

        // Profesor que dicta la clase.
        public string AsDocente { get; set; }

        // Clave foránea que conecta con el estudiante.
        // public int UsId { get; set; } por el momento no se utiliza

        // conexion hacia el modelo Usuarios.
        // public virtual Usuarios? Usuario { get; set; }
    }
}