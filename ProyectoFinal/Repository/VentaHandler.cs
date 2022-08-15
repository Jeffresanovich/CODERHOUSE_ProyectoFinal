using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class VentaHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Venta> GetAll()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                ventas.Add(venta);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return ventas;
        }
        public static Venta GetOneById(int id)
        {
            Venta venta = new Venta();

            string querySelect = "SELECT * FROM Venta";

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
                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return venta;
        }

        public static void Create(Venta venta)
        {
            string queryCreate = "INSERT INTO Venta (Comentarios) " +
                                 "VALUES (@comentarios)";

            CreateUpdateConnection(venta, queryCreate);
        }
        public static void Update(int id)
        {
            Venta venta = GetOneById(id);

            string queryUpdate = "UPDATE Venta " +
                                "SET Comentarios=@comentarios, " +
                                "WHERE = Id = @id";

            CreateUpdateConnection(venta, queryUpdate);
        }
        private static void CreateUpdateConnection(Venta venta, string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = venta.Id };
                    SqlParameter comentariosParameter = new SqlParameter("comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(comentariosParameter);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
        public static void Delete(int id)
        {
            string queryDelete = "DELETE FROM Venta WHERE Id = @id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = id };
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
