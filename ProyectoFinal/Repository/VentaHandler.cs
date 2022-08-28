using ProyectoFinal.Controllers.DTOS.Get;
using ProyectoFinal.Controllers.DTOS.Post;
using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class VentaHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        //Metodo COMUN a los GET, para almacenar los datos de la Base de Datos... 
        private static GetVenta GetDataFromDataBase(GetVenta venta, SqlDataReader dataReader)
        {
            venta.IdVenta = Convert.ToInt32(dataReader["Id"]);
            venta.Comentarios = dataReader["Comentarios"].ToString();
            venta.Descripciones = dataReader["Descripciones"].ToString();
            venta.Costo = Convert.ToDouble(dataReader["Costo"]);
            venta.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
            venta.Stock = Convert.ToInt32(dataReader["Stock"]);
            venta.NombreUsuario = dataReader["NombreUsuario"].ToString();

            return venta;
        }

        //METODO USADO PARA TRAER TODOS LOS PRODUCTOS VENDIDOS ORDENADOS POR VENTA
        public static List<GetVenta> GetAll()
        {
            List<GetVenta> listaVentas = new List<GetVenta>();

            string querySelect = "SELECT Venta.Id,Comentarios,Descripciones,Costo,PrecioVenta,ProductoVendido.Stock, NombreUsuario FROM (((ProductoVendido INNER JOIN Producto ON ProductoVendido.IdProducto = Producto.Id) INNER JOIN Venta ON ProductoVendido.IdVenta = Venta.Id)) INNER JOIN Usuario ON Producto.IdUsuario = Usuario.Id ORDER BY Venta.Id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetVenta venta = new GetVenta();

                                venta = GetDataFromDataBase(venta, dataReader);

                                listaVentas.Add(venta);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return listaVentas;
        }        

        //METODO USADO PARA TRAER TODOS LOS PRODUCTOS VENDIDOS DE UN USUARIO 
        public static List<GetVenta> GetByIdUsuario(int idUsuario)
        {
            List<GetVenta> listaVentas = new List<GetVenta>();

            string querySelect = "SELECT Venta.Id,Comentarios,Descripciones,Costo,PrecioVenta,ProductoVendido.Stock, NombreUsuario  FROM (((ProductoVendido INNER JOIN Producto  ON ProductoVendido.IdProducto = Producto.Id) INNER JOIN Venta  ON ProductoVendido.IdVenta = Venta.Id)) INNER JOIN Usuario  ON Producto.IdUsuario = Usuario.Id WHERE Usuario.Id = @idUsuario ORDER BY Venta.Id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", System.Data.SqlDbType.BigInt) { Value = idUsuario };
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                GetVenta venta = new GetVenta();

                                venta = GetDataFromDataBase(venta, dataReader);

                                listaVentas.Add(venta);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return listaVentas;
        }

        //CREA UNA VENTA Y RESTA EL STOCK A PRODUCTO
        public static string Create(PostVenta venta)
        {
            string mensaje = "Venta NO registrada";

            string queryCreate = "INSERT INTO Venta (Comentarios) " +
                                 "VALUES (@comentarios)  SELECT SCOPE_IDENTITY()";
            
            int idNuevaVenta;
            
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                    {
                        SqlParameter comentariosParameter = new SqlParameter("comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                        sqlCommand.Parameters.Add(comentariosParameter);

                        //ALMACENO EL IdVenta DEL NUEVO REGISTRO PARA USARLO EN LA
                        //INSERCION DEL LOS PRODUCTOS
                        idNuevaVenta = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    }
                    sqlConnection.Close();
                }

                if (idNuevaVenta > 0)
                {
                    if (venta.listaProductosVendidos.Count > 0)
                    {
                        foreach (PostProductoVendido productoVendido in venta.listaProductosVendidos)
                        {
                            ProductoVendidoHandler.Create(new ProductoVendido
                            {
                                Stock = productoVendido.Stock,
                                IdProducto = productoVendido.IdProducto,
                                IdVenta = idNuevaVenta
                            });

                            mensaje = "Venta REGISTRADA";
                        }
                    }
                    else
                    {
                        mensaje = "Venta REGISTRADA, pero sin productos vendidos";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }

            return mensaje;
        }

        //ELIMINA UNA VENTA Y SUMA EL STOCK A PRODUCTO
        public static bool Delete(int id)
        {
            int numeroDeRows;
            bool resultado = false;

            string queryDelete = "DELETE FROM Venta WHERE Id = @id";

            //PRIMERO SUMA EL STOCK DEL PRODUCTO VENDIDO A CADA PRODUCTO
            //Y LUEGO ELIMIMA LA VENTA
            ProductoVendidoHandler.DeleteByIdVenta(id);

            //LUEGO PROCEDE A ELIMINAR LA VENTA PROPIAMENTE DICHA
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = id };
                    sqlCommand.Parameters.Add(idParameter);

                    numeroDeRows = sqlCommand.ExecuteNonQuery();
                    if (numeroDeRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

    }
}
