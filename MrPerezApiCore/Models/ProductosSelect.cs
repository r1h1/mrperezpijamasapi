namespace MrPerezApiCore.Models
{
    public class ProductosSelect
    {
        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int? MarcaId { get; set; }
        public string? MarcaNombre { get; set; }
        public string? ProveedorMarca { get; set; }
        public int? CategoriaId { get; set; }
        public string? DescripcionCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public int? GeneroId { get; set; }
        public string? GeneroNombre { get; set; }
        public string? GeneroResumen { get; set; }
        public int Estado { get; set; }
    }
}
