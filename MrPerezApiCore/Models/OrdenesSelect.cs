namespace MrPerezApiCore.Models
{
    public class OrdenesSelect
    {
        public int OrdenId { get; set; }
        public string? NumeroDeOrden { get; set; }
        public string? OrdenDescripcion { get; set; }
        public int? ProductoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? CantidadOrdenada { get; set; }
        public decimal? TotalCantidad { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaRuta { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int EstadoOrden { get; set; }
        public int OrdenActiva { get; set; }
        public int? CantidadProducto { get; set; }
        public int? CategoriaId { get; set; }
        public string? ProductoDescripcion { get; set; }
        public int? GeneroId { get; set; }
        public int? MarcaId { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? PrecioProducto { get; set; }
        public string? Ciudad { get; set; }
        public string? DireccionUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public string? Municipio { get; set; }
        public string? Nit { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Pais { get; set; }
        public string? Referencia { get; set; }
        public string? Telefono { get; set; }
    }
}

