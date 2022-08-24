using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class VentaHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        public static bool Create(Venta venta)
        {
            int numeroDeRows;
            bool resultado = false;

            string queryCreate = "INSERT INTO Venta (Comentarios) " +
                                 "VALUES (@comentarios)";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    SqlParameter comentariosParameter = new SqlParameter("comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                    sqlCommand.Parameters.Add(comentariosParameter);

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

        public static List<GetVenta> GetAll()
        {
            List<GetVenta> listaVentasPersonalizada = new List<GetVenta>();

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

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                                venta.Descripciones = dataReader["Descripciones"].ToString();
                                venta.Costo = Convert.ToDouble(dataReader["Costo"]);
                                venta.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
                                venta.Stock = Convert.ToInt32(dataReader["Stock"]);
                                venta.NombreUsuario = dataReader["NombreUsuario"].ToString();

                                listaVentasPersonalizada.Add(venta);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return listaVentasPersonalizada;
        }
    }
}
