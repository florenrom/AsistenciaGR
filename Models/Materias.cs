using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Materias
    {
        [Key]
        public int MaId { get; set; }
        public string MaDenominacion { get; set; }
        public string MaModalidad { get; set; }
        public int MaCantHoras { get; set; }
        public int CaId { get; set; }
        public ICollection<Carreras_Materias> Carreras_Materias { get; set; }
    }
}
