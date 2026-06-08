using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Carreras_Materias
    {
        [Key]
        public int CaMaId { get; set; }
        public int CaId { get; set; }
        public virtual Carreras? Carreras { get; set; }
        public int MaId { get; set; }
        public virtual Materias? Materias { get; set; }
        public virtual Inscripciones? Inscripciones { get; set; }
    }
}
