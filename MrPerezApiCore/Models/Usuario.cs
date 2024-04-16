namespace MrPerezApiCore.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Ciudad { get; set; }
        public string? Municipio { get; set; }
        public string? Pais { get; set; }
        public string? Referencia { get; set; }
        public string? Nit { get; set; }
        public int Estado { get; set; }
    }
}
