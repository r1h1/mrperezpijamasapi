using MrPerezApiCore.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MrPerezApiCore.Data
{
    public class EmpleadoData
    {
        private readonly string conexion;

        public EmpleadoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empleado", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empleado
                        {
                            EmpleadoId = Convert.ToInt32(reader["EmpleadoId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Sexo = Convert.ToInt32(reader["Sexo"]),
                            DPI = reader["DPI"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            RolId = Convert.ToInt32(reader["RolId"]),
                            EmpresaId = Convert.ToInt32(reader["EmpresaId"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Empleado> Obtener(int Id)
        {
            Empleado objeto = new Empleado();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Empleado WHERE EmpleadoId = @PEmpleadoId", con);
                cmd.Parameters.AddWithValue("@PEmpleadoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Empleado
                        {
                            EmpleadoId = Convert.ToInt32(reader["EmpleadoId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Sexo = Convert.ToInt32(reader["Sexo"]),
                            DPI = reader["DPI"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            RolId = Convert.ToInt32(reader["RolId"]),
                            EmpresaId = Convert.ToInt32(reader["EmpresaId"]),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Empleado(NombreCompleto,Direccion,Telefono,Email,Ciudad,Municipio,Pais,Sexo,DPI,Nit,RolId,EmpresaId,Estado) VALUES(@PNombreCompleto, @PDireccion, @PTelefono, @PEmail, @PCiudad, @PMunicipio, @PPais, @PSexo, @PDPI, @PNit, @PRolId, @PEmpresaId, @PEstado)", con);
                cmd.Parameters.AddWithValue("@PNombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@PEmail", objeto.Email);
                cmd.Parameters.AddWithValue("@PCiudad", objeto.Ciudad);
                cmd.Parameters.AddWithValue("@PMunicipio", objeto.Municipio);
                cmd.Parameters.AddWithValue("@PPais", objeto.Pais);
                cmd.Parameters.AddWithValue("@PSexo", objeto.Sexo);
                cmd.Parameters.AddWithValue("@PDPI", objeto.DPI);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PRolId", objeto.RolId);
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

        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Empleado SET NombreCompleto = @PNombreCompleto, Direccion = @PDireccion, Telefono = @PTelefono, Email = @PEmail, Ciudad = @PCiudad, Municipio = @PMunicipio, Pais = @PPais, Sexo = @PSexo, DPI = @PDPI, Nit = @PNit, RolId = @PRolId, EmpresaId = @PEmpresaId, Estado = @PEstado WHERE EmpleadoId = @PEmpleadoId", con);
                cmd.Parameters.AddWithValue("@PEmpleadoId", objeto.EmpleadoId);
                cmd.Parameters.AddWithValue("@PNombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@PDireccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@PTelefono", objeto.Telefono);
                cmd.Parameters.AddWithValue("@PEmail", objeto.Email);
                cmd.Parameters.AddWithValue("@PCiudad", objeto.Ciudad);
                cmd.Parameters.AddWithValue("@PMunicipio", objeto.Municipio);
                cmd.Parameters.AddWithValue("@PPais", objeto.Pais);
                cmd.Parameters.AddWithValue("@PSexo", objeto.Sexo);
                cmd.Parameters.AddWithValue("@PDPI", objeto.DPI);
                cmd.Parameters.AddWithValue("@PNit", objeto.Nit);
                cmd.Parameters.AddWithValue("@PRolId", objeto.RolId);
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