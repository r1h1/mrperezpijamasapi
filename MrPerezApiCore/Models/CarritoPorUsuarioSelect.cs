namespace MrPerezApiCore.Models
{
    public class CarritoPorUsuarioSelect
    {
        public int CarritoId { get; set; }
        public int? ProductoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? TotalCantidad { get; set; }
        public int Estado { get; set; }

        // Propiedades adicionales del JOIN con la tabla Usuario
        public string? NombreCompleto { get; set; }
        public string? Nit { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Municipio { get; set; }
        public string? Pais { get; set; }
        public string? Referencia { get; set; }
        public string? Telefono { get; set; }

        // Propiedades adicionales del JOIN con la tabla Productos
        public int? ProductoCantidad { get; set; }
        public string? ProductoDescripcion { get; set; }
        public string? ProductoNombre { get; set; }
        public decimal? ProductoPrecio { get; set; }
        public string? ProductoImagen { get; set; }
        public int? marcaId { get; set; }
        public int? categoriaId { get; set; }
        public int? generoId { get; set; }
    }
}
