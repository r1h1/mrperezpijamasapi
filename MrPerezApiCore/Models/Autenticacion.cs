﻿namespace MrPerezApiCore.Models
{
    public class Autenticacion
    {
        public int AutenticacionId { get; set; }
        public int? UsuarioId { get; set; }
        public int? EmpleadoId { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public int? RolUsuario { get; set; }
        public int? RolEmpleado { get; set; }
        public int Estado { get; set; }
    }
}
