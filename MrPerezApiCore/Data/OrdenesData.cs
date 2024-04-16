﻿using Microsoft.Extensions.Configuration;
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

        public async Task<List<Ordenes>> Lista()
        {
            List<Ordenes> lista = new List<Ordenes>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Ordenes", con);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Ordenes
                        {
                            OrdenId = Convert.ToInt32(reader["OrdenId"]),
                            NumeroDeOrden = reader["NumeroDeOrden"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ProductoId = reader["ProductoId"] == DBNull.Value ? null : (int?)reader["ProductoId"],
                            UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : (int?)reader["UsuarioId"],
                            Cantidad = reader["Cantidad"] == DBNull.Value ? null : (int?)reader["Cantidad"],
                            TotalCantidad = reader["TotalCantidad"] == DBNull.Value ? null : (decimal?)reader["TotalCantidad"],
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? null : (DateTime?)reader["FechaPedido"],
                            FechaPago = reader["FechaPago"] == DBNull.Value ? null : (DateTime?)reader["FechaPago"],
                            FechaRuta = reader["FechaRuta"] == DBNull.Value ? null : (DateTime?)reader["FechaRuta"],
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? null : (DateTime?)reader["FechaEntrega"],
                            Estado = Convert.ToInt32(reader["Estado"]),
                            Activo = Convert.ToInt32(reader["Activo"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Ordenes> Obtener(int id)
        {
            Ordenes objeto = new Ordenes();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Ordenes WHERE OrdenId = @OrdenId", con);
                cmd.Parameters.AddWithValue("@OrdenId", id);
                cmd.CommandType = CommandType.Text;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Ordenes
                        {
                            OrdenId = Convert.ToInt32(reader["OrdenId"]),
                            NumeroDeOrden = reader["NumeroDeOrden"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ProductoId = reader["ProductoId"] == DBNull.Value ? null : (int?)reader["ProductoId"],
                            UsuarioId = reader["UsuarioId"] == DBNull.Value ? null : (int?)reader["UsuarioId"],
                            Cantidad = reader["Cantidad"] == DBNull.Value ? null : (int?)reader["Cantidad"],
                            TotalCantidad = reader["TotalCantidad"] == DBNull.Value ? null : (decimal?)reader["TotalCantidad"],
                            FechaPedido = reader["FechaPedido"] == DBNull.Value ? null : (DateTime?)reader["FechaPedido"],
                            FechaPago = reader["FechaPago"] == DBNull.Value ? null : (DateTime?)reader["FechaPago"],
                            FechaRuta = reader["FechaRuta"] == DBNull.Value ? null : (DateTime?)reader["FechaRuta"],
                            FechaEntrega = reader["FechaEntrega"] == DBNull.Value ? null : (DateTime?)reader["FechaEntrega"],
                            Estado = Convert.ToInt32(reader["Estado"]),
                            Activo = Convert.ToInt32(reader["Activo"])
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Ordenes (NumeroDeOrden, Descripcion, ProductoId, UsuarioId, Cantidad, TotalCantidad, FechaPedido, FechaPago, FechaRuta, FechaEntrega, Estado, Activo) " +
                                                "VALUES (@NumeroDeOrden, @Descripcion, @ProductoId, @UsuarioId, @Cantidad, @TotalCantidad, @FechaPedido, @FechaPago, @FechaRuta, @FechaEntrega, @Estado, @Activo)", con);
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