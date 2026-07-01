namespace AsistenciaGR.Models
{
    public class AsistenciaGlobalViewModel
    {
        public int? CaMaId { get; set; }
        public List<DateTime> Fechas { get; set; } = new();        // columnas
        public List<AsistenciaGlobalRowViewModel> Rows { get; set; } = new(); // filas
    }


    public class AsistenciaGlobalRowViewModel
    {
        public int UsId { get; set; }
        public string FullName { get; set; }
        public Dictionary<DateTime, bool> AsistenciaPorFecha { get; set; } = new(); // fecha → presente/ausente
        public decimal PorcentajeAsistencia { get; set; }
    }
}
