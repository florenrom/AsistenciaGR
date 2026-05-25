using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.Models
{
    public class Usuarios
    {
        [Key]
        public int UsId { get; set; }
        public string? UsApellido { get; set; }
        public string? UsNombre { get; set; }
        public int UsDNI { get; set; }
        public int RoId { get; set; }
        public virtual Roles? Roles { get; set; }
    }
}
