using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Inscripciones
    {
        [Key]
        public int InId { get; set; }
        public int UsId { get; set; }
        public int CaMaId { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
        public virtual ICollection<Carreras_Materias> Carreras_Materias { get; set; } = new List<Carreras_Materias>();
    }
}
