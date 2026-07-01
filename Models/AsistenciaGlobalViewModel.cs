namespace AsistenciaGR.Models
{
    public class AsistenciaGlobalRowViewModel
    {
        public int UsId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public int TotalClases { get; set; }

        public int Presentes { get; set; }

        public double Porcentaje { get; set; }
    }

    public class AsistenciaGlobalViewModel
    {
        public int? CaMaId { get; set; }

        public List<AsistenciaGlobalRowViewModel> Rows { get; set; }
            = new List<AsistenciaGlobalRowViewModel>();
    }
}
