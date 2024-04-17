using MrPerezApiCore.Models;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MrPerezApiCore.Data
{
    public class PaginaAccesoData
    {
        private readonly string conexion;

        public PaginaAccesoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<PaginaAcceso>> Lista()
        {
            List<PaginaAcceso> lista = new List<PaginaAcceso>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.PaginaAccesoId,a.RolIdPertenece,a.FormularioAcceso,a.Estado," +
                    "b.Nombre,b.Permiso " +
                    "FROM Pagina_Acceso a " +
                    "LEFT JOIN Rol b ON b.RolId = a.RolIdPertenece " +
                    "WHERE a.Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new PaginaAcceso
                        {
                            PaginaAccesoId = Convert.ToInt32(reader["PaginaAccesoId"]),
                            RolIdPertenece = Convert.ToInt32(reader["RolIdPertenece"]),
                            FormularioAcceso = reader["FormularioAcceso"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<PaginaAcceso> Obtener(int Id)
        {
            PaginaAcceso objeto = new PaginaAcceso();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.PaginaAccesoId,a.RolIdPertenece,a.FormularioAcceso,a.Estado," +
                    "b.Nombre,b.Permiso " +
                    "FROM Pagina_Acceso a " +
                    "LEFT JOIN Rol b ON b.RolId = a.RolIdPertenece " +
                    "WHERE a.Estado = 1 " +
                    "AND a.PaginaAccesoId = @PPaginaAccesoId", con);
                cmd.Parameters.AddWithValue("@PPaginaAccesoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new PaginaAcceso
                        {
                            PaginaAccesoId = Convert.ToInt32(reader["PaginaAccesoId"]),
                            RolIdPertenece = Convert.ToInt32(reader["RolIdPertenece"]),
                            FormularioAcceso = reader["FormularioAcceso"].ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(PaginaAcceso objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Pagina_Acceso (RolIdPertenece, FormularioAcceso, Estado) VALUES (@PRolIdPertenece, @PFormularioAcceso, @PEstado);)", con);
                cmd.Parameters.AddWithValue("@PRolIdPertenece", objeto.RolIdPertenece);
                cmd.Parameters.AddWithValue("@PFormularioAcceso", objeto.FormularioAcceso);
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

        public async Task<bool> Editar(PaginaAcceso objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Pagina_Acceso SET RolIdPertenece = @PRolIdPertenece, FormularioAcceso = @PFormularioAcceso, Estado = @PEstado WHERE PaginaAccesoId = @PPaginaAccesoId", con);
                cmd.Parameters.AddWithValue("@PPaginaAccesoId", objeto.PaginaAccesoId);
                cmd.Parameters.AddWithValue("@PRolIdPertenece", objeto.RolIdPertenece);
                cmd.Parameters.AddWithValue("@PFormularioAcceso", objeto.FormularioAcceso);
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
