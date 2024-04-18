using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class UsuarioData
    {
        private readonly string conexion;

        public UsuarioData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Usuario>> Lista()
        {
            List<Usuario> lista = new List<Usuario>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Usuario
                        {
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Referencia = reader["Referencia"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            RolId = Convert.ToInt32(reader["RolId"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Usuario> Obtener(int Id)
        {
            Usuario objeto = new Usuario();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE UsuarioId = @PUsuarioId AND Estado = 1", con);
                cmd.Parameters.AddWithValue("@PUsuarioId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Usuario
                        {
                            UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Referencia = reader["Referencia"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            RolId = Convert.ToInt32(reader["RolId"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<(bool, int)> Crear(Usuario objeto)
        {
            bool respuesta = true;
            int ultimoIdInsertado = -1;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(NombreCompleto,Direccion,Telefono,Email,Ciudad,Municipio,Pais,Referencia,Nit,RolId,Estado) VALUES(@PNombreCompleto,@PDireccion,@PTelefono,@PEmail,@PCiudad,@PMunicipio,@PPais,@PReferencia,@PNit,@PRolId,@PEstado); SELECT SCOPE_IDENTITY();", con);
                cmd.Parameters.AddWithValue("@PNombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@PEmail", objeto.Email);
                cmd.Parameters.AddWithValue("@PCiudad", objeto.Ciudad);
                cmd.Parameters.AddWithValue("@PMunicipio", objeto.Municipio);
                cmd.Parameters.AddWithValue("@PPais", objeto.Pais);
                cmd.Parameters.AddWithValue("@PReferencia", objeto.Referencia);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PRolId", objeto.RolId);
                cmd.Parameters.AddWithValue("@PEstado", objeto.Estado);

                cmd.CommandType = CommandType.Text;
                try
                {
                    await con.OpenAsync();
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        ultimoIdInsertado = Convert.ToInt32(result);
                        respuesta = true;
                    }
                }
                catch
                {
                    respuesta = false;
                }
            }
            return (respuesta, ultimoIdInsertado);
        }


        public async Task<bool> Editar(Usuario objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Usuario SET NombreCompleto = @PNombreCompleto, Direccion = @PDireccion, Telefono = @PTelefono, Email = @PEmail, Ciudad = @PCiudad, Municipio = @PMunicipio, Pais = @PPais, Referencia = @PReferencia, Nit = @PNit, RolId = @PRolId, Estado = @PEstado WHERE UsuarioId = @PUsuarioId", con);
                cmd.Parameters.AddWithValue("@PUsuarioId", objeto.UsuarioId);
                cmd.Parameters.AddWithValue("@PNombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@PEmail", objeto.Email);
                cmd.Parameters.AddWithValue("@PCiudad", objeto.Ciudad);
                cmd.Parameters.AddWithValue("@PMunicipio", objeto.Municipio);
                cmd.Parameters.AddWithValue("@PPais", objeto.Pais);
                cmd.Parameters.AddWithValue("@PReferencia", objeto.Referencia);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PRolId", objeto.RolId);
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
    }
}
