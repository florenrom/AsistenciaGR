using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AsistenciaGR.Models
{
    public class Roles
    {
        [Key]
        public int RoId { get; set; }
        public string RoDenominacion { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
    }
}
