namespace MrPerezApiCore.Models
{
    public class Productos
    {
        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int? MarcaId { get; set; }
        public int? CategoriaId { get; set; }
        public int? GeneroId { get; set; }
        public int Estado { get; set; }
    }
}
