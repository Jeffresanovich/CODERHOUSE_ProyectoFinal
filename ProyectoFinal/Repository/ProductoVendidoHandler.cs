using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using System.Data.SqlClient;

namespace ProyectoFinal.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string connectionString = @"Server=JEFF-PC;Database=SistemaGestion;Trusted_Connection=True";

        //Metodo COMUN a los GET, para almacenar los datos de la Base de Datos... 
        private static ProductoVendido GetDataFromDataBase(ProductoVendido productoVendido, SqlDataReader dataReader)
        {
            productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
            productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
            productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
            productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

            return productoVendido;
        }
        
        //HACE JUSTAMENTE ESO...
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
               
        //METODO USADO PARA TRAER TODOS LOS PRODUCTOS VENDIDOS DE UNA VENTA 
        private static List<ProductoVendido> GetByIdVenta(int idVenta)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();

            string querySelect = "SELECT * FROM ProductoVendido WHERE IdVenta = @idVenta";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt) { Value = idVenta };
                    sqlCommand.Parameters.Add(idVentaParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido = GetDataFromDataBase(productoVendido, dataReader);

                                listaProductosVendidos.Add(productoVendido);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return listaProductosVendidos;
        }

        //Metodo COMUN para sumar (crear) y restar (borrar) a los
        //productos vendidos de una venta...
        private static void SumaORestaDeStock(bool restar, int idProducto, int stockVendido)
        {
            Producto productoAlmacenado = ProductoHandler.GetOneById(idProducto);

            if (restar)
            {
                productoAlmacenado.Stock = productoAlmacenado.Stock - stockVendido;
            }
            else
            {
                productoAlmacenado.Stock = productoAlmacenado.Stock + stockVendido;
            }

            ProductoHandler.Update(productoAlmacenado);

        }

        //PRIMERO CREA EL PRODUCTO VENDIDO Y LUEGO DESCUENTA EL STOCK
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
                    SqlParameter stockParameter = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("idProducto", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                    SqlParameter idVentaParameter = new SqlParameter("idVenta", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdVenta };

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

            //AQUI SE PROCEDE A DESCONTAR EL PRODUCTO VENDIDO
            //DEL STOCK DE PRODUCTO: true = RESTA  Y  false = SUMA
            SumaORestaDeStock(true,productoVendido.IdProducto, productoVendido.Stock);         

            return resultado;
        }
        
        //PRIMERO SUMA EL STOCK DEL PRODUCTO VENDIDO A CADA PRODUCTO
        //Y LUEGO ELIMIMA LA VENTA
        public static void DeleteByIdVenta(int idVenta)
        {
            //AQUI SE PROCEDE A SUMAR EL PRODUCTO VENDIDO
            //DEL STOCK DE PRODUCTO: true = RESTA  Y  false = SUMA
            List<ProductoVendido> listaProductosVendidos = GetByIdVenta(idVenta);

            foreach (ProductoVendido productoVendido in listaProductosVendidos)
            {
                SumaORestaDeStock(false, productoVendido.IdProducto, productoVendido.Stock);
            }

            string queryDeleteVenta = "DELETE FROM ProductoVendido WHERE IdVenta = @id";

            DeleteByIdVentaOrProducto(idVenta, queryDeleteVenta);

        }

        //Metodo COMUN para los DELETE anterior y siguiente...
        private static void DeleteByIdVentaOrProducto(int id, string queryDelete)
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

        //PARA PODER ELIMINAR UN PRODUCTO DE LA BASE DE DATOS
        //PRIMERO HAY QUE ELIMINARLO LA VENTA ASOCIADA A ESTE
        public static void DeleteByIdProducto(int idProducto)
        {
            string queryDeleteProducto = "DELETE FROM ProductoVendido WHERE IdProducto = @id";

            DeleteByIdVentaOrProducto(idProducto, queryDeleteProducto);
        }
    }
}
