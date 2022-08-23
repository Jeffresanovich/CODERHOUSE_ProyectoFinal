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


    }
}
