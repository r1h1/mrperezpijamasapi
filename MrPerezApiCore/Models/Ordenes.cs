namespace MrPerezApiCore.Models
{
    public class Ordenes
    {
        public int OrdenId { get; set; }
        public string? NumeroDeOrden { get; set; }
        public string? Descripcion { get; set; }
        public int? ProductoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? TotalCantidad { get; set; }
        public DateTime? FechaPedido { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaRuta { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int Estado { get; set; }
        public int Activo { get; set; }
    }
}

