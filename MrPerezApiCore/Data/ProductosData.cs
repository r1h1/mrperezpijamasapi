using MrPerezApiCore.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MrPerezApiCore.Data
{
    public class ProductosData
    {
        private readonly string conexion;

        public ProductosData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductosSelect>> Lista()
        {
            List<ProductosSelect> lista = new List<ProductosSelect>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.ProductoId, a.Nombre, a.Descripcion, a.Cantidad, a.Precio, a.Imagen, a.MarcaId, " +
                    "a.CategoriaId, a.GeneroId, a.Estado, b.Nombre AS MarcaNombre, b.Proveedor AS ProveedorMarca, " +
                    "c.Descripcion AS DescripcionCategoria, c.Nombre AS NombreCategoria, d.Nombre AS GeneroNombre, d.Resumen AS GeneroResumen " +
                    "FROM Productos a " +
                    "LEFT JOIN Marcas b ON b.MarcasId = a.MarcaId " +
                    "LEFT JOIN Categorias c ON c.CategoriaId = a.CategoriaId " +
                    "LEFT JOIN Genero d ON d.GeneroId = a.GeneroId " +
                    "WHERE a.Estado = 1", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductosSelect
                        {
                            ProductoId = Convert.ToInt32(reader["ProductoId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (int?)null,
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Imagen = reader["Imagen"].ToString(),
                            MarcaId = reader["MarcaId"] != DBNull.Value ? Convert.ToInt32(reader["MarcaId"]) : (int?)null,
                            MarcaNombre = reader["MarcaNombre"]?.ToString(),
                            ProveedorMarca = reader["ProveedorMarca"]?.ToString(),
                            CategoriaId = reader["CategoriaId"] != DBNull.Value ? Convert.ToInt32(reader["CategoriaId"]) : (int?)null,
                            DescripcionCategoria = reader["DescripcionCategoria"]?.ToString(),
                            NombreCategoria = reader["NombreCategoria"]?.ToString(),
                            GeneroId = reader["GeneroId"] != DBNull.Value ? Convert.ToInt32(reader["GeneroId"]) : (int?)null,
                            GeneroNombre = reader["GeneroNombre"]?.ToString(),
                            GeneroResumen = reader["GeneroResumen"]?.ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        });
                    }
                }
            }
            return lista;
        }


        public async Task<ProductosSelect> Obtener(int Id)
        {
            ProductosSelect objeto = new ProductosSelect();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT a.ProductoId, a.Nombre, a.Descripcion, a.Cantidad, a.Precio, a.Imagen, a.MarcaId, " +
                    "a.CategoriaId, a.GeneroId, a.Estado, b.Nombre AS MarcaNombre, b.Proveedor AS ProveedorMarca, " +
                    "c.Descripcion AS DescripcionCategoria, c.Nombre AS NombreCategoria, d.Nombre AS GeneroNombre, d.Resumen AS GeneroResumen " +
                    "FROM Productos a " +
                    "LEFT JOIN Marcas b ON b.MarcasId = a.MarcaId " +
                    "LEFT JOIN Categorias c ON c.CategoriaId = a.CategoriaId " +
                    "LEFT JOIN Genero d ON d.GeneroId = a.GeneroId " +
                    "WHERE a.Estado = 1 " +
                    "AND a.ProductoId = @PProductoId", con);
                cmd.Parameters.AddWithValue("@PProductoId", Id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new ProductosSelect
                        {
                            ProductoId = Convert.ToInt32(reader["ProductoId"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (int?)null,
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Imagen = reader["Imagen"].ToString(),
                            MarcaId = reader["MarcaId"] != DBNull.Value ? Convert.ToInt32(reader["MarcaId"]) : (int?)null,
                            MarcaNombre = reader["MarcaNombre"]?.ToString(),
                            ProveedorMarca = reader["ProveedorMarca"]?.ToString(),
                            CategoriaId = reader["CategoriaId"] != DBNull.Value ? Convert.ToInt32(reader["CategoriaId"]) : (int?)null,
                            DescripcionCategoria = reader["DescripcionCategoria"]?.ToString(),
                            NombreCategoria = reader["NombreCategoria"]?.ToString(),
                            GeneroId = reader["GeneroId"] != DBNull.Value ? Convert.ToInt32(reader["GeneroId"]) : (int?)null,
                            GeneroNombre = reader["GeneroNombre"]?.ToString(),
                            GeneroResumen = reader["GeneroResumen"]?.ToString(),
                            Estado = Convert.ToInt32(reader["Estado"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Productos objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO Productos(Nombre, Descripcion, Cantidad, Precio, MarcaId, CategoriaId, GeneroId, Estado) VALUES(@PNombre, @PDescripcion, @PCantidad, @PPrecio, @PMarcaId, @PCategoriaId, @PGeneroId, @PEstado)", con);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PDescripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@PCantidad", objeto.Cantidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PPrecio", objeto.Precio);
                cmd.Parameters.AddWithValue("@PMarcaId", objeto.MarcaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PCategoriaId", objeto.CategoriaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PGeneroId", objeto.GeneroId ?? (object)DBNull.Value);
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

        public async Task<bool> Editar(Productos objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("UPDATE Productos SET Nombre = @PNombre, Descripcion = @PDescripcion, Cantidad = @PCantidad, Precio = @PPrecio, MarcaId = @PMarcaId, CategoriaId = @PCategoriaId, GeneroId = @PGeneroId, Estado = @PEstado WHERE ProductoId = @PProductoId", con);
                cmd.Parameters.AddWithValue("@PProductoId", objeto.ProductoId);
                cmd.Parameters.AddWithValue("@PNombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@PDescripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@PCantidad", objeto.Cantidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PPrecio", objeto.Precio);
                cmd.Parameters.AddWithValue("@PMarcaId", objeto.MarcaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PCategoriaId", objeto.CategoriaId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PGeneroId", objeto.GeneroId ?? (object)DBNull.Value);
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
