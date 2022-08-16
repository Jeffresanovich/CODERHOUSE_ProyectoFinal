using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoHanlder
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetAll()
        {
            List<Producto> productos = new List<Producto>();

            string querySelect = "SELECT * FROM Producto";

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
                                Producto producto = new Producto();

                                GetDataFromDataBase(producto, dataReader);

                                productos.Add(producto);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productos;
        }
        public static Producto GetOneById(int id)
        {
            Producto producto = new Producto();

            string querySelect = "SELECT * FROM Producto WHERE Id = @id";

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
                                producto = GetDataFromDataBase(producto, dataReader);

                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return producto;
        }
        private static Producto GetDataFromDataBase(Producto producto, SqlDataReader dataReader)
        {
            producto.Id = Convert.ToInt32(dataReader["Id"]);
            producto.Descripciones = dataReader["Descripciones"].ToString();
            producto.Costo = Convert.ToDouble(dataReader["Costo"]);
            producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
            producto.Stock = Convert.ToInt32(dataReader["Stock"]);
            producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

            return producto;
        }

        public static void Create(Producto producto)
        {
            string queryCreate = "INSERT INTO Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario) " +
                                 "VALUES (@descripciones,@costo,@precioVenta,@stock,@idUsuario)";

            CreateUpdateConnection(producto, queryCreate);
        }
        public static void Update(int id)
        {
            Producto producto = GetOneById(id);

            string queryUpdate = "UPDATE Producto " +
                                "SET Descripciones=@descripciones, " +
                                    "Costo=@costo, " +
                                    "PrecioVenta=@precioVenta, " +
                                    "Stock=@stock, " +
                                    "IdUsuario=@idUsuario " +
                                "WHERE = Id = @id";

            CreateUpdateConnection(producto, queryUpdate);
        }
        private static void CreateUpdateConnection(Producto producto, string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = producto.Id };
                    SqlParameter descripcionesParameter = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
                    SqlParameter costoParameter = new SqlParameter("costo", System.Data.SqlDbType.Money) { Value = producto.Costo };
                    SqlParameter precioVentaUsuarioParameter = new SqlParameter("precioVenta", System.Data.SqlDbType.Money) { Value = producto.PrecioVenta };
                    SqlParameter stockParameter = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
                    SqlParameter idUsuarioParameter = new SqlParameter("idUsuario", System.Data.SqlDbType.BigInt) { Value = producto.IdUsuario };

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaUsuarioParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void Delete(int id)
        {
            string queryDelete = "DELETE FROM Producto WHERE Id = @id";

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