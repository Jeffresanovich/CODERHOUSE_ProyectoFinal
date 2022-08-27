using ProyectoFinal.Controllers.DTOS;
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
            venta.Id = Convert.ToInt32(dataReader["Id"]);
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

        //
        public static int Create(Venta venta)
        {
            int idNuevaVenta = 0;

            string queryCreate = "INSERT INTO Venta (Comentarios) " +
                                 "VALUES (@comentarios)";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    SqlParameter comentariosParameter = new SqlParameter("comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                    sqlCommand.Parameters.Add(comentariosParameter);

                    if (sqlCommand.ExecuteScalar() != null)
                    {
                        idNuevaVenta = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    }
                }
                sqlConnection.Close();
            }
            return idNuevaVenta;
        }

        public static bool Delete(int id)
        {
            int numeroDeRows;
            bool resultado = false;

            string queryDelete = "DELETE FROM Venta WHERE Id = @id";

            //PRIMERO SUMA EL STOCK DEL PRODUCTO VENDIDO A CADA PRODUCTO
            //Y LUEGO ELIMIMA LA VENTA

            ProductoVendidoHandler.DeleteByIdVenta(id);

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
