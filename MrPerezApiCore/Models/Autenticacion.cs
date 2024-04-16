namespace MrPerezApiCore.Models
{
    public class Autenticacion
    {
        public int AutenticacionId { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public int Estado { get; set; }
    }
}
