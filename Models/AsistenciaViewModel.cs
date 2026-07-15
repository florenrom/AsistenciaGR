using System.Collections.Generic;

namespace AsistenciaGR.Models
{
    public class AsistenciaRowViewModel
    {
        public int UsId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool AsPresente { get; set; }
        public bool AsJustificacion { get; set; }
        // modules - represent as list of booleans for checkbox per module
        public List<string> Modulos { get; set; } = new List<string>();
    }

    public class AsistenciaFormViewModel
    {
        // number of modules for the selected materia
        public int ModuleCount { get; set; } = 1;
        public int? CaMaId { get; set; }
        public List<AsistenciaRowViewModel> Rows { get; set; } = new List<AsistenciaRowViewModel>();
    }
}