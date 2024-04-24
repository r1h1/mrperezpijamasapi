namespace MrPerezApiCore.Models
{
    public class DepartamentoSelect
    {
        public int DepartamentoId { get; set; }
        public string? Nombre { get; set; }
        public int EmpresaId { get; set; }
        public string? Direccion { get; set; }
        public string? Nit { get; set; }
        public string? Telefono { get; set; }
        public int Estado { get; set; }
    }
}
