using Microsoft.Extensions.Configuration;
using MrPerezApiCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MrPerezApiCore.Data
{
    public class OrdenesData
    {
        private readonly string conexion;

        public OrdenesData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<OrdenesSelect>> Lista()
        {
            List<OrdenesSelect> lista = new List<OrdenesSelect>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.OrdenId, a.NumeroDeOrden, a.Descripcion AS OrdenDescripcion, " +
                    "a.ProductoId, a.UsuarioId, a.Cantidad AS CantidadOrdenada, a.TotalCantidad, a.FechaPedido, " +
                    "a.FechaPago, a.FechaRuta, a.FechaEntrega, a.Estado AS EstadoOrden, a.Activo AS OrdenActiva, " +
                    "b.Cantidad AS CantidadProducto, b.CategoriaId, b.Descripcion AS ProductoDescripcion, " +
                    "b.GeneroId, b.MarcaId, b.Nombre AS NombreProducto, b.Precio AS PrecioProducto, c.Ciudad, " +
                    "c.Direccion AS DireccionUsuario, c.Email AS EmailUsuario, c.Municipio, c.Nit, " +
                    "c.NombreCompleto AS NombreUsuario, c.Pais, c.Referencia, c.Telefono " +
                    "FROM Ordenes a " +
                    "LEFT JOIN Productos b ON b.ProductoId = a.ProductoId " +
                    "LEFT JOIN Usuario c ON c.UsuarioId = a.UsuarioId " +
                    "WHERE a.Activo = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new OrdenesSelect
                        {
                            OrdenId = Convert.ToInt32(reader["OrdenId"]),
                            NumeroDeOrden = reader["NumeroDeOrden"].ToString(),
                            OrdenDescripcion = reader["OrdenDescripcion"].ToString(),
                            ProductoId = reader["ProductoId"] == DBNull.Value ? null : (int?)reader["ProductoId"],
                            UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : (int?)reader["UsuarioId"],
                            CantidadOrdenada = reader["CantidadOrdenada"] == DBNull.Value ? null : (int?)reader["CantidadOrdenada"],
                            TotalCantidad = reader["TotalCantidad"] == DBNull.Value ? null : (decimal?)reader["TotalCantidad"],
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? null : (DateTime?)reader["FechaPedido"],
                            FechaPago = reader["FechaPago"] == DBNull.Value ? null : (DateTime?)reader["FechaPago"],
                            FechaRuta = reader["FechaRuta"] == DBNull.Value ? null : (DateTime?)reader["FechaRuta"],
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? null : (DateTime?)reader["FechaEntrega"],
                            EstadoOrden = Convert.ToInt32(reader["EstadoOrden"]),
                            OrdenActiva = Convert.ToInt32(reader["OrdenActiva"]),
                            CantidadProducto = reader["CantidadProducto"] == DBNull.Value ? null : (int?)reader["CantidadProducto"],
                            CategoriaId = reader["CategoriaId"] == DBNull.Value ? null : (int?)reader["CategoriaId"],
                            ProductoDescripcion = reader["ProductoDescripcion"].ToString(),
                            GeneroId = reader["GeneroId"] == DBNull.Value ? null : (int?)reader["GeneroId"],
                            MarcaId = reader["MarcaId"] == DBNull.Value ? null : (int?)reader["MarcaId"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            PrecioProducto = reader["PrecioProducto"] == DBNull.Value ? null : (decimal?)reader["PrecioProducto"],
                            Ciudad = reader["Ciudad"].ToString(),
                            DireccionUsuario = reader["DireccionUsuario"].ToString(),
                            EmailUsuario = reader["EmailUsuario"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Referencia = reader["Referencia"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<OrdenesSelect> Obtener(int id)
        {
            OrdenesSelect objeto = new OrdenesSelect();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.OrdenId,a.NumeroDeOrden,a.Descripcion,a.ProductoId,a.UsuarioId," +
                    "a.Cantidad,a.TotalCantidad,a.FechaPedido,a.FechaPago,a.FechaRuta,a.FechaEntrega,a.Estado,a.Activo," +
                    "b.Cantidad,b.CategoriaId,b.Descripcion,b.GeneroId,b.MarcaId,b.Nombre,b.Precio,c.Ciudad,c.Direccion," +
                    "c.Email,c.Municipio,c.Nit,c.NombreCompleto,c.Pais,c.Referencia,c.Telefono " +
                    "FROM Ordenes a " +
                    "LEFT JOIN Productos b ON b.ProductoId = a.ProductoId " +
                    "LEFT JOIN Usuario c ON c.UsuarioId = a.UsuarioId " +
                    "WHERE a.Activo = 1 " +
                    "AND a.OrdenId = @POrdenId", con);
                cmd.Parameters.AddWithValue("@POrdenId", id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new OrdenesSelect
                        {
                            OrdenId = Convert.ToInt32(reader["OrdenId"]),
                            NumeroDeOrden = reader["NumeroDeOrden"].ToString(),
                            OrdenDescripcion = reader["OrdenDescripcion"].ToString(),
                            ProductoId = reader["ProductoId"] == DBNull.Value ? null : (int?)reader["ProductoId"],
                            UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : (int?)reader["UsuarioId"],
                            CantidadOrdenada = reader["CantidadOrdenada"] == DBNull.Value ? null : (int?)reader["CantidadOrdenada"],
                            TotalCantidad = reader["TotalCantidad"] == DBNull.Value ? null : (decimal?)reader["TotalCantidad"],
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? null : (DateTime?)reader["FechaPedido"],
                            FechaPago = reader["FechaPago"] == DBNull.Value ? null : (DateTime?)reader["FechaPago"],
                            FechaRuta = reader["FechaRuta"] == DBNull.Value ? null : (DateTime?)reader["FechaRuta"],
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? null : (DateTime?)reader["FechaEntrega"],
                            EstadoOrden = Convert.ToInt32(reader["EstadoOrden"]),
                            OrdenActiva = Convert.ToInt32(reader["OrdenActiva"]),
                            CantidadProducto = reader["CantidadProducto"] == DBNull.Value ? null : (int?)reader["CantidadProducto"],
                            CategoriaId = reader["CategoriaId"] == DBNull.Value ? null : (int?)reader["CategoriaId"],
                            ProductoDescripcion = reader["ProductoDescripcion"].ToString(),
                            GeneroId = reader["GeneroId"] == DBNull.Value ? null : (int?)reader["GeneroId"],
                            MarcaId = reader["MarcaId"] == DBNull.Value ? null : (int?)reader["MarcaId"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            PrecioProducto = reader["PrecioProducto"] == DBNull.Value ? null : (decimal?)reader["PrecioProducto"],
                            Ciudad = reader["Ciudad"].ToString(),
                            DireccionUsuario = reader["DireccionUsuario"].ToString(),
                            EmailUsuario = reader["EmailUsuario"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Referencia = reader["Referencia"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<OrdenesSelect> ObtenerPorNumeroOrden(string numeroOrden)
        {
            OrdenesSelect objeto = new OrdenesSelect();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.OrdenId, a.NumeroDeOrden, a.Descripcion AS OrdenDescripcion, " +
                    "a.ProductoId, a.UsuarioId, a.Cantidad AS CantidadOrdenada, a.TotalCantidad, a.FechaPedido, " +
                    "a.FechaPago, a.FechaRuta, a.FechaEntrega, a.Estado AS EstadoOrden, a.Activo AS OrdenActiva, " +
                    "b.Cantidad AS CantidadProducto, b.CategoriaId, b.Descripcion AS ProductoDescripcion, " +
                    "b.GeneroId, b.MarcaId, b.Nombre AS NombreProducto, b.Precio AS PrecioProducto, c.Ciudad, " +
                    "c.Direccion AS DireccionUsuario, c.Email AS EmailUsuario, c.Municipio, c.Nit, " +
                    "c.NombreCompleto AS NombreUsuario, c.Pais, c.Referencia, c.Telefono " +
                    "FROM Ordenes a " +
                    "LEFT JOIN Productos b ON b.ProductoId = a.ProductoId " +
                    "LEFT JOIN Usuario c ON c.UsuarioId = a.UsuarioId " +
                    "WHERE a.Activo = 1 " +
                    "AND a.NumeroDeOrden = @PNumeroDeOrden", con);
                cmd.Parameters.AddWithValue("@PNumeroDeOrden", numeroOrden);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new OrdenesSelect
                        {
                            OrdenId = Convert.ToInt32(reader["OrdenId"]),
                            NumeroDeOrden = reader["NumeroDeOrden"].ToString(),
                            OrdenDescripcion = reader["OrdenDescripcion"].ToString(),
                            ProductoId = reader["ProductoId"] == DBNull.Value ? null : (int?)reader["ProductoId"],
                            UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : (int?)reader["UsuarioId"],
                            CantidadOrdenada = reader["CantidadOrdenada"] == DBNull.Value ? null : (int?)reader["CantidadOrdenada"],
                            TotalCantidad = reader["TotalCantidad"] == DBNull.Value ? null : (decimal?)reader["TotalCantidad"],
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? null : (DateTime?)reader["FechaPedido"],
                            FechaPago = reader["FechaPago"] == DBNull.Value ? null : (DateTime?)reader["FechaPago"],
                            FechaRuta = reader["FechaRuta"] == DBNull.Value ? null : (DateTime?)reader["FechaRuta"],
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? null : (DateTime?)reader["FechaEntrega"],
                            EstadoOrden = Convert.ToInt32(reader["EstadoOrden"]),
                            OrdenActiva = Convert.ToInt32(reader["OrdenActiva"]),
                            CantidadProducto = reader["CantidadProducto"] == DBNull.Value ? null : (int?)reader["CantidadProducto"],
                            CategoriaId = reader["CategoriaId"] == DBNull.Value ? null : (int?)reader["CategoriaId"],
                            ProductoDescripcion = reader["ProductoDescripcion"].ToString(),
                            GeneroId = reader["GeneroId"] == DBNull.Value ? null : (int?)reader["GeneroId"],
                            MarcaId = reader["MarcaId"] == DBNull.Value ? null : (int?)reader["MarcaId"],
                            NombreProducto = reader["NombreProducto"].ToString(),
                            PrecioProducto = reader["PrecioProducto"] == DBNull.Value ? null : (decimal?)reader["PrecioProducto"],
                            Ciudad = reader["Ciudad"].ToString(),
                            DireccionUsuario = reader["DireccionUsuario"].ToString(),
                            EmailUsuario = reader["EmailUsuario"].ToString(),
                            Municipio = reader["Municipio"].ToString(),
                            Nit = reader["Nit"].ToString(),
                            NombreUsuario = reader["NombreUsuario"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            Referencia = reader["Referencia"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Ordenes objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();

                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Update
                        SqlCommand updateCmd = new SqlCommand("UPDATE Carrito SET Estado = 0 WHERE UsuarioId = @UsuarioId;", con, transaction);
                        updateCmd.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId);
                        await updateCmd.ExecuteNonQueryAsync();

                        // Insert
                        SqlCommand cmd = new SqlCommand("INSERT INTO Ordenes (NumeroDeOrden, Descripcion, ProductoId, UsuarioId, Cantidad, TotalCantidad, FechaPedido, FechaPago, FechaRuta, FechaEntrega, Estado, Activo) " +
                                                        "VALUES (@NumeroDeOrden, @Descripcion, @ProductoId, @UsuarioId, @Cantidad, @TotalCantidad, GETDATE(), @FechaPago, @FechaRuta, @FechaEntrega, @Estado, @Activo);", con, transaction);
                        cmd.Parameters.AddWithValue("@NumeroDeOrden", objeto.NumeroDeOrden);
                        cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                        cmd.Parameters.AddWithValue("@ProductoId", objeto.ProductoId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TotalCantidad", objeto.TotalCantidad ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaPedido", objeto.FechaPedido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaPago", objeto.FechaPago ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaRuta", objeto.FechaRuta ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaEntrega", objeto.FechaEntrega ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                        cmd.Parameters.AddWithValue("@Activo", objeto.Activo);

                        await cmd.ExecuteNonQueryAsync();

                        transaction.Commit();
                        respuesta = true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        respuesta = false;
                    }
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Ordenes objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Ordenes SET NumeroDeOrden = @NumeroDeOrden, Descripcion = @Descripcion, ProductoId = @ProductoId, UsuarioId = @UsuarioId, " +
                                                "Cantidad = @Cantidad, TotalCantidad = @TotalCantidad, FechaPedido = @FechaPedido, FechaPago = @FechaPago, " +
                                                "FechaRuta = @FechaRuta, FechaEntrega = @FechaEntrega, Estado = @Estado, Activo = @Activo " +
                                                "WHERE OrdenId = @OrdenId", con);
                cmd.Parameters.AddWithValue("@OrdenId", objeto.OrdenId);
                cmd.Parameters.AddWithValue("@NumeroDeOrden", objeto.NumeroDeOrden);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@ProductoId", objeto.ProductoId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UsuarioId", objeto.UsuarioId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalCantidad", objeto.TotalCantidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaPedido", objeto.FechaPedido ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaPago", objeto.FechaPago ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaRuta", objeto.FechaRuta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaEntrega", objeto.FechaEntrega ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", objeto.Estado);
                cmd.Parameters.AddWithValue("@Activo", objeto.Activo);

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