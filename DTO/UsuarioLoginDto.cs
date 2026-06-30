using System.ComponentModel.DataAnnotations;

namespace AsistenciaGR.DTO
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Debe ingresar un usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Contrasena { get; set; }
    }
}
