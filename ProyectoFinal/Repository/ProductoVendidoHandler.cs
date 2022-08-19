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

        public static void Create(ProductoVendido productoVendido)
        {
            string queryCreate = "INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta) " +
                                 "VALUES (@stock,@idProducto,@idVenta)";

            CreateUpdateConnection(productoVendido, queryCreate);
        }
        public static void Update(int id)
        {
            ProductoVendido productoVendido = GetOneById(id);

            string queryUpdate = "UPDATE ProductoVendido " +
                                "SET Stock=@stock, " +
                                    "IdProducto=@idProducto, " +
                                    "IdVenta=@idVenta, " +
                                "WHERE Id = @id";

            CreateUpdateConnection(productoVendido, queryUpdate);
        }
        private static void CreateUpdateConnection(ProductoVendido productoVendido, string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = productoVendido.Id };
                    SqlParameter stockParameter = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void Delete(int id)
        {
            string queryDelete = "DELETE FROM ProductoVendido WHERE Id = @id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = id };
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
    }
}
