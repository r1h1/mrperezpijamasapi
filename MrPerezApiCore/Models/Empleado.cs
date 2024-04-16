namespace MrPerezApiCore.Models
{
    public class Empleado
    {   
        public int EmpleadoId { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Ciudad { get; set; }
        public string? Municipio { get; set; }
        public string? Pais { get; set; }
        public int Sexo { get; set; }
        public string? DPI { get; set; }
        public string? Nit { get; set; }
        public int RolId { get; set; }
        public int EmpresaId { get; set; }
        public int Estado { get; set; }
    }
}
