using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MrPerezApiCore.Data
{
    public class AutenticacionData
    {
        private readonly string conexion;

        public AutenticacionData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Autenticacion>> Lista()
        {
            List<Autenticacion> lista = new List<Autenticacion>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.AutenticacionId,a.UsuarioId,a.EmpleadoId,a.Usuario,a.Clave,a.Token, " +
                    "a.Estado,b.RolId AS RolUsuario,c.RolId AS RolEmpleado " +
                    "FROM Autenticacion a " +
                    "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                    "LEFT JOIN Empleado c ON c.EmpleadoId = a.EmpleadoId " +
                    "WHERE a.Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Autenticacion
                        {
                            AutenticacionId = Convert.ToInt32(reader["AutenticacionId"]),
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            EmpleadoId = Convert.ToInt32(reader["EmpleadoId"]),
                            Usuario = reader["Usuario"].ToString(),
                            Clave = reader["Clave"].ToString(),
                            RolEmpleado = Convert.ToInt32(reader["RolUsuario"]),
                            RolUsuario = Convert.ToInt32(reader["RolEmpleado"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Autenticacion> Obtener(int Id)
        {
            Autenticacion objeto = new Autenticacion();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.AutenticacionId,a.UsuarioId,a.EmpleadoId,a.Usuario,a.Clave,a.Token, " +
                    "a.Estado,b.RolId AS RolUsuario,c.RolId AS RolEmpleado " +
                    "FROM Autenticacion a " +
                    "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                    "LEFT JOIN Empleado c ON c.EmpleadoId = a.EmpleadoId " +
                    "WHERE a.Estado = 1 " +
                    "AND a.AutenticacionId = @PAutenticacionId", con);
                cmd.Parameters.AddWithValue("@PAutenticacionId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Autenticacion
                        {
                            AutenticacionId = Convert.ToInt32(reader["AutenticacionId"]),
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            EmpleadoId = Convert.ToInt32(reader["EmpleadoId"]),
                            Usuario = reader["Usuario"].ToString(),
                            Clave = reader["Clave"].ToString(),
                            RolEmpleado = Convert.ToInt32(reader["RolUsuario"]),
                            RolUsuario = Convert.ToInt32(reader["RolEmpleado"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<Autenticacion> ValidarUsuario(string Usuario, string Clave)
        {
            Autenticacion objeto = new Autenticacion();

            byte[] bytesClave = Encoding.UTF8.GetBytes(Clave);
            byte[] hashClave;

            using (SHA256 sha256 = SHA256.Create())
            {
                hashClave = sha256.ComputeHash(bytesClave);
            }

            string claveEncriptada = Convert.ToBase64String(hashClave);

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT a.AutenticacionId,a.UsuarioId,a.EmpleadoId,a.Usuario,a.Clave,a.Token, " +
                                                "a.Estado,b.RolId AS RolUsuario,c.RolId AS RolEmpleado " +
                                                "FROM Autenticacion a " +
                                                "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                                                "LEFT JOIN Empleado c ON c.EmpleadoId = a.EmpleadoId " +
                                                "WHERE a.Usuario = @PUsuario AND a.Clave = @PClave AND a.Estado = 1", con);
                cmd.Parameters.AddWithValue("@PUsuario", Usuario);
                cmd.Parameters.AddWithValue("@PClave", claveEncriptada);
                cmd.CommandType = CommandType.Text;
                try
                {
                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            // Verificar si los datos devueltos coinciden con los datos de la solicitud
                            if (reader["Usuario"].ToString() == Usuario && reader["Clave"].ToString() == claveEncriptada)
                            {
                                objeto = new Autenticacion
                                {
                                    AutenticacionId = Convert.ToInt32(reader["AutenticacionId"]),
                                    UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                    EmpleadoId = Convert.ToInt32(reader["EmpleadoId"]),
                                    Usuario = reader["Usuario"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    RolEmpleado = Convert.ToInt32(reader["RolUsuario"]),
                                    RolUsuario = Convert.ToInt32(reader["RolEmpleado"]),
                                    Estado = Convert.ToInt32(reader["Estado"])
                                };
                            }
                        }
                    }
                }
                catch
                {
                    
                }
            }
            return objeto;
        }


        public async Task<bool> Crear(AutenticacionInsertadoEditado objeto)
        {
            bool respuesta = true;

            byte[] bytesClave = Encoding.UTF8.GetBytes(objeto.Clave);
            byte[] hashClave;

            using (SHA256 sha256 = SHA256.Create())
            {
                hashClave = sha256.ComputeHash(bytesClave);
            }

            string claveEncriptada = Convert.ToBase64String(hashClave);

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Autenticacion VALUES(@PUsuarioId,@PEmpleadoId,@PUsuario,@PClave,@PToken,@PEstado);", con);
                cmd.Parameters.AddWithValue("@PUsuarioId", objeto.UsuarioId);
                cmd.Parameters.AddWithValue("@PEmpleadoId", objeto.EmpleadoId);
                cmd.Parameters.AddWithValue("@PUsuario", objeto.Usuario);
                cmd.Parameters.AddWithValue("@PClave", claveEncriptada);
                cmd.Parameters.AddWithValue("@PToken", objeto.Token);
                cmd.Parameters.AddWithValue("@PEstado", objeto.Estado);
                cmd.CommandType = CommandType.Text;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }


        public async Task<bool> Editar(AutenticacionInsertadoEditado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Autenticacion SET UsuarioId = @PUsuarioId, EmpleadoId = @PEmpleadoId, " +
                    "Usuario = @PUsuario, Clave = @PClave, Token = @PToken, Estado = @PEstado " +
                    "WHERE AutenticacionId = @PAutenticacionId", con);
                cmd.Parameters.AddWithValue("@PUsuarioId", objeto.UsuarioId);
                cmd.Parameters.AddWithValue("@PEmpleadoId", objeto.EmpleadoId);
                cmd.Parameters.AddWithValue("@PUsuario", objeto.Usuario);
                cmd.Parameters.AddWithValue("@PClave", objeto.Estado);
                cmd.Parameters.AddWithValue("@PToken", objeto.Token);
                cmd.Parameters.AddWithValue("@PEstado", objeto.Estado);
                cmd.Parameters.AddWithValue("@PAutenticacionId", objeto.AutenticacionId);
                cmd.CommandType = CommandType.Text;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
