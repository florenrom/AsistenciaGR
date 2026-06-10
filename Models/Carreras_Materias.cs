using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Carreras_Materias
    {
        [Key]
        public int CaMaId { get; set; }
        public string CaMaDenominacion { get; set; }
        public int CaId { get; set; }
        public virtual Carreras? Carreras { get; set; }
        public int MaId { get; set; }
        public virtual Materias? Materias { get; set; }
        // navigation: multiple inscripciones can reference this Carreras_Materias
        public virtual ICollection<Inscripciones> Inscripciones { get; set; } = new List<Inscripciones>();
    }
}
