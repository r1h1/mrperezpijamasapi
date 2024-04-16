namespace MrPerezApiCore.Models
{
    public class Departamento
    {
        public int DepartamentoId { get; set; }
        public string? Nombre { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
    }
}
