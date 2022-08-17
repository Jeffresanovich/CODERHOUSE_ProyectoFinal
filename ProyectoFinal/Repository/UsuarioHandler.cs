using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class UsuarioHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        public static Usuario GetOneById(int id)
        {
            //TRAE UN SOLO USUARIO POR ID: "Traer Usuario"

            Usuario usuario = new Usuario();

            string querySelect = "SELECT * FROM Usuario WHERE Id = @id";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter();
                    idParameter.ParameterName = "id";
                    idParameter.SqlDbType = System.Data.SqlDbType.BigInt;
                    idParameter.Value = id;

                    sqlCommand.Parameters.Add(idParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = "Contraseña no disponible para compartir";//dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return usuario;
        }
        public static bool GetOneByUsernameAndPassword(string username, string password)
        {
            //DEVUELVE TRUE SI COINCIDE USUARIO Y CONTRASEÑA: "Inicio de sesión"

            bool resultado = false;

            string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @username AND Contraseña = @password";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter usernameParameter = new SqlParameter("username", System.Data.SqlDbType.VarChar) { Value = username };
                    SqlParameter passwordParameter = new SqlParameter("password", System.Data.SqlDbType.VarChar) { Value = password };

                    sqlCommand.Parameters.Add(usernameParameter);
                    sqlCommand.Parameters.Add(passwordParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            resultado = true;
                        }
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }

        public static bool Create(Usuario usuario)
        {
            string queryCreate = "INSERT INTO Usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail) " +
                                 "VALUES (@nombre,@apellido,@nombreUsuario,@contraseña,@mail)";

            return CreateUpdateConnection(usuario, queryCreate);
        }
        public static bool Update(Usuario usuario)
        {       
            string queryUpdate = "UPDATE Usuario " +
                                "SET Nombre = @nombre, " +
                                    "Apellido = @apellido, " +
                                    "NombreUsuario = @nombreUsuario, " +
                                    "Contraseña = @contraseña, " +
                                    "Mail = @mail " +
                                "WHERE Id = @id";

            return CreateUpdateConnection(usuario, queryUpdate);
        }
        private static bool CreateUpdateConnection(Usuario usuario, string query)
        {
            int numeroDeRows;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = usuario.Id };
                    SqlParameter nombreParameter = new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                    SqlParameter apellidoParameter = new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    SqlParameter contraseñaParameter = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                    SqlParameter mailParameter = new SqlParameter("mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

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
       
        public static bool Delete(int id)
        {
            int numeroDeRows;
            bool resultado = false;

            string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

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
                    sqlConnection.Close();
                }
                return resultado;
            }
        }
    }
}
