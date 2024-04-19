using MrPerezApiCore.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MrPerezApiCore.Data
{
    public class CarritoData
    {
        private readonly string conexion;

        public CarritoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Carrito>> Lista()
        {
            List<Carrito> lista = new List<Carrito>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.CarritoId,a.ProductoId,a.UsuarioId,a.Cantidad,a.TotalCantidad," +
                    "a.Estado,b.NombreCompleto,b.Nit,b.Ciudad,b.Direccion,b.Municipio,b.Pais,b.Referencia,b.Telefono," +
                    "c.Cantidad,c.Descripcion,c.Nombre,c.Precio " +
                    "FROM Carrito a " +
                    "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                    "LEFT JOIN Productos c ON c.ProductoId = a.ProductoId " +
                    "WHERE a.Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Carrito
                        {
                            CarritoId = Convert.ToInt32(reader["CarritoId"]),
                            ProductoId = reader["ProductoId"] != DBNull.Value ? Convert.ToInt32(reader["ProductoId"]) : null,
                            UsuarioId = reader["UsuarioId"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioId"]) : null,
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : null,
                            TotalCantidad = reader["TotalCantidad"] != DBNull.Value ? Convert.ToDecimal(reader["TotalCantidad"]) : null,
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Carrito> Obtener(int Id)
        {
            Carrito objeto = new Carrito();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.CarritoId,a.ProductoId,a.UsuarioId,a.Cantidad,a.TotalCantidad," +
                    "a.Estado,b.NombreCompleto,b.Nit,b.Ciudad,b.Direccion,b.Municipio,b.Pais,b.Referencia,b.Telefono," +
                    "c.Cantidad,c.Descripcion,c.Nombre,c.Precio " +
                    "FROM Carrito a " +
                    "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                    "LEFT JOIN Productos c ON c.ProductoId = a.ProductoId " +
                    "WHERE a.Estado = 1 " +
                    "AND a.CarritoId = @PCarritoId", con);
                cmd.Parameters.AddWithValue("@PCarritoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Carrito
                        {
                            CarritoId = Convert.ToInt32(reader["CarritoId"]),
                            ProductoId = reader["ProductoId"] != DBNull.Value ? Convert.ToInt32(reader["ProductoId"]) : null,
                            UsuarioId = reader["UsuarioId"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioId"]) : null,
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : null,
                            TotalCantidad = reader["TotalCantidad"] != DBNull.Value ? Convert.ToDecimal(reader["TotalCantidad"]) : null,
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<List<CarritoPorUsuarioSelect>> ObtenerPorUsuario(int UsuarioId)
        {
            List<CarritoPorUsuarioSelect> listaObjetos = new List<CarritoPorUsuarioSelect>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.CarritoId, a.ProductoId, a.UsuarioId, a.Cantidad, " +
                    "a.TotalCantidad, a.Estado, b.NombreCompleto, b.Nit, b.Ciudad, b.Direccion, b.Municipio, " +
                    "b.Pais, b.Referencia, b.Telefono, c.Cantidad AS ProductoCantidad, c.Descripcion AS ProductoDescripcion, " +
                    "c.Nombre AS ProductoNombre, c.Precio AS ProductoPrecio " +
                    "FROM Carrito a " +
                    "LEFT JOIN Usuario b ON b.UsuarioId = a.UsuarioId " +
                    "LEFT JOIN Productos c ON c.ProductoId = a.ProductoId " +
                    "WHERE a.UsuarioId = @PUsuarioId AND a.Estado = 1;", con);
                cmd.Parameters.AddWithValue("@PUsuarioId", UsuarioId);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var objeto = new CarritoPorUsuarioSelect
                        {
                            CarritoId = Convert.ToInt32(reader["CarritoId"]),
                            ProductoId = reader["ProductoId"] != DBNull.Value ? Convert.ToInt32(reader["ProductoId"]) : (int?)null,
                            UsuarioId = reader["UsuarioId"] != DBNull.Value ? Convert.ToInt32(reader["UsuarioId"]) : (int?)null,
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (int?)null,
                            TotalCantidad = reader["TotalCantidad"] != DBNull.Value ? Convert.ToDecimal(reader["TotalCantidad"]) : (decimal?)null,
                            Estado = Convert.ToInt32(reader["Estado"]),
                            NombreCompleto = reader["NombreCompleto"] != DBNull.Value ? reader["NombreCompleto"].ToString() : null,
                            Nit = reader["Nit"] != DBNull.Value ? reader["Nit"].ToString() : null,
                            Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
                            Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : null,
                            Municipio = reader["Municipio"] != DBNull.Value ? reader["Municipio"].ToString() : null,
                            Pais = reader["Pais"] != DBNull.Value ? reader["Pais"].ToString() : null,
                            Referencia = reader["Referencia"] != DBNull.Value ? reader["Referencia"].ToString() : null,
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                            ProductoCantidad = reader["ProductoCantidad"] != DBNull.Value ? Convert.ToInt32(reader["ProductoCantidad"]) : (int?)null,
                            ProductoDescripcion = reader["ProductoDescripcion"] != DBNull.Value ? reader["ProductoDescripcion"].ToString() : null,
                            ProductoNombre = reader["ProductoNombre"] != DBNull.Value ? reader["ProductoNombre"].ToString() : null,
                            ProductoPrecio = reader["ProductoPrecio"] != DBNull.Value ? Convert.ToDecimal(reader["ProductoPrecio"]) : (decimal?)null
                        };

                        listaObjetos.Add(objeto);
                    }
                }
            }
            return listaObjetos;
        }


        public async Task<bool> Crear(Carrito objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Carrito(ProductoId, UsuarioId, Cantidad, TotalCantidad, Estado) VALUES(@PProductoId, @PUsuarioId, @PCantidad, @PTotalCantidad, @PEstado)", con);
                cmd.Parameters.AddWithValue("@PProductoId", objeto.ProductoId != null ? (object)objeto.ProductoId : DBNull.Value);
                cmd.Parameters.AddWithValue("@PUsuarioId", objeto.UsuarioId != null ? (object)objeto.UsuarioId : DBNull.Value);
                cmd.Parameters.AddWithValue("@PCantidad", objeto.Cantidad != null ? (object)objeto.Cantidad : DBNull.Value);
                cmd.Parameters.AddWithValue("@PTotalCantidad", objeto.TotalCantidad != null ? (object)objeto.TotalCantidad : DBNull.Value);
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

        public async Task<bool> Editar(Carrito objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Carrito SET ProductoId = @PProductoId, UsuarioId = @PUsuarioId, Cantidad = @PCantidad, TotalCantidad = @PTotalCantidad, Estado = @PEstado WHERE CarritoId = @PCarritoId", con);
                cmd.Parameters.AddWithValue("@PCarritoId", objeto.CarritoId);
                cmd.Parameters.AddWithValue("@PProductoId", objeto.ProductoId != null ? (object)objeto.ProductoId : DBNull.Value);
                cmd.Parameters.AddWithValue("@PUsuarioId", objeto.UsuarioId != null ? (object)objeto.UsuarioId : DBNull.Value);
                cmd.Parameters.AddWithValue("@PCantidad", objeto.Cantidad != null ? (object)objeto.Cantidad : DBNull.Value);
                cmd.Parameters.AddWithValue("@PTotalCantidad", objeto.TotalCantidad != null ? (object)objeto.TotalCantidad : DBNull.Value);
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