using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AsistenciaGR.Models
{
    public class Carreras
    {
        [Key]
        public int CaId { get; set; }
        public string CaDenominacion { get; set; }
        public ICollection<Carreras_Materias> Carreras_Materias { get; set; }
    }
}
