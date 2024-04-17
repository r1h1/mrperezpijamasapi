using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class GeneroData
    {
        private readonly string conexion;

        public GeneroData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Genero>> Lista()
        {
            List<Genero> lista = new List<Genero>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Genero WHERE Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Genero
                        {
                            GeneroId = Convert.ToInt32(reader["GeneroId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Resumen = reader["Resumen"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Genero> Obtener(int Id)
        {
            Genero objeto = new Genero();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Genero WHERE GeneroId = @PGeneroId AND Estado = 1", con);
                cmd.Parameters.AddWithValue("@PGeneroId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Genero
                        {
                            GeneroId = Convert.ToInt32(reader["GeneroId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Resumen = reader["Resumen"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Genero objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Genero(Nombre,Resumen,Estado) VALUES(@PNombre,@PResumen,@PEstado)", con);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PResumen", objeto.Resumen);
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

        public async Task<bool> Editar(Genero objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Genero SET Nombre = @PNombre, Resumen = @PResumen, Estado = @PEstado WHERE GeneroId = @PGeneroId", con);
                cmd.Parameters.AddWithValue("@PGeneroId", objeto.GeneroId);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PResumen", objeto.Resumen);
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
