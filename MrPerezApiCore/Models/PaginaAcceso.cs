namespace MrPerezApiCore.Models
{
    public class PaginaAcceso
    {
        public int PaginaAccesoId { get; set; }
        public int RolIdPertenece { get; set; }
        public string? FormularioAcceso { get; set; }
        public int Estado { get; set; }
    }
}
