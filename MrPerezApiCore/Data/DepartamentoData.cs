using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class DepartamentoData
    {
        private readonly string conexion;

        public DepartamentoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<DepartamentoSelect>> Lista()
        {
            List<DepartamentoSelect> lista = new List<DepartamentoSelect>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.DepartamentoId,a.EmpresaId,a.Estado,a.Nombre,b.Direccion,b.Nit,b.Telefono " +
                    "FROM Departamento a " +
                    "LEFT JOIN Empresa b ON b.EmpresaId = a.EmpresaId " +
                    "WHERE a.Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new DepartamentoSelect
                        {
                            DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                            Nombre = reader["Nombre"].ToString(),
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

        public async Task<DepartamentoSelect> Obtener(int Id)
        {
            DepartamentoSelect objeto = new DepartamentoSelect();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.DepartamentoId,a.EmpresaId,a.Estado,a.Nombre,b.Direccion,b.Nit,b.Telefono " +
                    "FROM Departamento a " +
                    "LEFT JOIN Empresa b ON b.EmpresaId = a.EmpresaId " +
                    "WHERE a.Estado = 1 " +
                    "AND a.DepartamentoId = @PDepartamentoId", con);
                cmd.Parameters.AddWithValue("@PDepartamentoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new DepartamentoSelect
                        {
                            DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
                            Nombre = reader["Nombre"].ToString(),
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

        public async Task<bool> Crear(Departamento objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Departamento(Nombre,EmpresaId,Estado) VALUES(@PNombre,@PEmpresaId,@PEstado)", con);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PEmpresaId", objeto.EmpresaId);
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

        public async Task<bool> Editar(Departamento objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Departamento SET Nombre = @PNombre, EmpresaId = @PEmpresaId, Estado = @PEstado WHERE DepartamentoId = @PDepartamentoId", con);
                cmd.Parameters.AddWithValue("@PDepartamentoId", objeto.DepartamentoId);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PEmpresaId", objeto.EmpresaId);
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
