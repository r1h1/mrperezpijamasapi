using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class MetodosPagoData
    {
        private readonly string conexion;

        public MetodosPagoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<MetodosPago>> Lista()
        {
            List<MetodosPago> lista = new List<MetodosPago>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Metodos_Pago WHERE Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new MetodosPago
                        {
                            MetodoPagoId = Convert.ToInt32(reader["MetodoPagoId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<MetodosPago> Obtener(int Id)
        {
            MetodosPago objeto = new MetodosPago();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Metodos_Pago WHERE MetodoPagoId = @PMetodoPagoId AND Estado = 1", con);
                cmd.Parameters.AddWithValue("@PMetodoPagoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new MetodosPago
                        {
                            MetodoPagoId = Convert.ToInt32(reader["MetodoPagoId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(MetodosPago objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Metodos_Pago(Nombre,Estado) VALUES(@PNombre,@PEstado)", con);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
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

        public async Task<bool> Editar(MetodosPago objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Metodos_Pago SET Nombre = @PNombre, Estado = @PEstado WHERE MetodoPagoId = @PMetodoPagoId", con);
                cmd.Parameters.AddWithValue("@PMetodoPagoId", objeto.MetodoPagoId);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
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
