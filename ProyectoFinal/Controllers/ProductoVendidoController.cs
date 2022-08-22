using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet]
        [Route("api/ProductoVendido/{id}")]
        public List<ProductoVendido> GetAll()
        {
            return ProductoVendidoHandler.GetAll();
        }

        [HttpPost]
        [Route("api/ProductoVendido")]
        public bool InsertWithComment([FromBody]int stock, int idProducto, string comentario)
        {
            bool resultado = false;
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }

    }
}