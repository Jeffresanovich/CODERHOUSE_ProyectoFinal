using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        public static List<ProductoVendido> GetAll()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido = GetDataFromDataBase(productoVendido, dataReader);

                                productosVendidos.Add(productoVendido);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productosVendidos;
        }
        public static ProductoVendido GetOneById(int id)
        {
            ProductoVendido productoVendido = new ProductoVendido();

            string querySelect = "SELECT * FROM ProductoVendido WHERE Id = @id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = id };
                    sqlCommand.Parameters.Add(idParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                productoVendido = GetDataFromDataBase(productoVendido, dataReader);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productoVendido;
        }
        private static ProductoVendido GetDataFromDataBase(ProductoVendido productoVendido, SqlDataReader dataReader)
        {
            productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
            productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
            productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
            productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

            return productoVendido;
        }

        public static bool Create(ProductoVendido productoVendido)
        {
            int numeroDeRows;
            bool resultado = false;

            string queryCreate = "INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta) " +
                                 "VALUES (@stock,@idProducto,@idVenta)";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryCreate, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = productoVendido.Id };
                    SqlParameter stockParameter = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);

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

        public static void DeleteByIdProducto(int idProducto)
        {
            string queryDeleteProducto = "DELETE FROM ProductoVendido WHERE IdProducto = @idProducto";

            DeleteByIdVentaOProducto(idProducto, queryDeleteProducto);

        }
        public static void DeleteByIdVenta(int idVenta)
        {
            string queryDeleteVenta = "DELETE FROM ProductoVendido WHERE IdVenta = @id";

            DeleteByIdVentaOProducto(idVenta, queryDeleteVenta);

        }
        private static void DeleteByIdVentaOProducto(int id, string queryDelete)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    SqlParameter idVentaOProductoParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = id };
                    sqlCommand.Parameters.Add(idVentaOProductoParameter);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
    }
}
