using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class EmpresaData
    {
        private readonly string conexion;

        public EmpresaData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Empresa>> Lista()
        {
            List<Empresa> lista = new List<Empresa>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empresa WHERE Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empresa
                        {
                            EmpresaId = Convert.ToInt32(reader["EmpresaId"]),
                            Direccion = reader["Direccion"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Empresa> Obtener(int Id)
        {
            Empresa objeto = new Empresa();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empresa WHERE EmpresaId = @PEmpresaId AND Estado = 1", con);
                cmd.Parameters.AddWithValue("@PEmpresaId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Empresa
                        {
                            EmpresaId = Convert.ToInt32(reader["EmpresaId"]),
                            Direccion = reader["Direccion"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Empresa objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Empresa(Direccion,Nit,Telefono,Estado) VALUES(@PDireccion,@PNit,@PTelefono,@PEstado)", con);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
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

        public async Task<bool> Editar(Empresa objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Empresa SET Direccion = @PDireccion, Nit = @PNit, Telefono = @PTelefono, Estado = @PEstado WHERE EmpresaId = @PEmpresaId", con);
                cmd.Parameters.AddWithValue("@PEmpresaId", objeto.EmpresaId);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
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
