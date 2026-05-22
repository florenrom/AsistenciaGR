using System;
using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{

    // Representa el modelo de datos para registrar la asistencia a una clase. 
    public class Asistencia
    {
        // Identificador único del registro.
        public int AsId { get; set; }
        // Fecha y hora exacta en la toma de asistencia.
        public DateTime AsFechaHora { get; set; }
        //Bloque horario de la clase.
        public string AsModulo { get; set; }
        // Motivo o justificación en caso de ausencia.
        public string? AsJustificacion { get; set; }
        // Nombre de la materia correspondiente al registro.
        public string AsMateria { get; set; }
        // Nombre del programa de la carrera.
        public string AsCarrera { get; set; }
        // Nombre del docente a cargo de dictar la clase.
        public string AsDocente { get; set; }

        // Clave foránea que conecta con el estudiante.
        // public int UsId { get; set; } // Por el momento no se utiliza

        // Conexión hacia el modelo Usuarios.
        // public virtual Usuarios? Usuario { get; set; } // Por el momento no se utiliza
    }
}