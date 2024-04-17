namespace MrPerezApiCore.Models
{
    public class AutenticacionInsertadoEditado
    {
        public int AutenticacionId { get; set; }
        public int? UsuarioId { get; set; }
        public int? EmpleadoId { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public string? Token { get; set; }
        public int Estado { get; set; }
    }
}
