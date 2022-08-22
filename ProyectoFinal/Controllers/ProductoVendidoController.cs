using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controller
{
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
         [HttpPost]
        [Route("api/ProductoVendido")]  //  Carga Venta
        public bool Create([FromBody] PostProductoVendido productoVendido)
        {
            bool resultado = false;
            try
            {              
                    resultado = ProductoVendidoHandler.Create(new ProductoVendido
                    {
                        Stock = productoVendido.Stock,
                        IdProducto = productoVendido.IdProducto,
                        IdVenta = productoVendido.IdVenta
                    });                  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }        
    }
}