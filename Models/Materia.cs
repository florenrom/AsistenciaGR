using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AsistenciaGR.Models
{
    public class Materia
    {
        [Key]
        [Display(Name = "ID Materia")]
        public int MaId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la materia.")]
        [StringLength(30, ErrorMessage = "No se permiten más de 30 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ0-9\s.,()-]*$",
            ErrorMessage = "Ingrese una materia válida."
        )]
        [Display(Name = "Materia")]
        public string MaDenominacion { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar una modalidad.")]
        [StringLength(25, ErrorMessage = "No se permiten más de 25 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÜüÑñ\s]*$",
            ErrorMessage = "Ingrese una modalidad válida."
        )]

        [Display(Name = "Modalidad")]
        public string? MaModalidad { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de módulos.")]
        [Range(1, 4, ErrorMessage = "La cantidad de módulos debe estar entre 1 y 4.")]
        [Display(Name = "Cantidad de Módulos")]
        public int? MaCantModulos { get; set; }

        public int CaMaId { get; set; } // Foreign key to CarreraMateria
        public int AsId { get; set; } // Foreign key to Asistencia
        public int CaId { get; set; }

        // RELACION
        public virtual ICollection<CarreraMateria>? CarreraMaterias { get; set; }
        public virtual ICollection<Asistencia>? Asistencias { get; set; }
        public virtual ICollection<Carrera>? Carreras { get; set; }
    }
}
