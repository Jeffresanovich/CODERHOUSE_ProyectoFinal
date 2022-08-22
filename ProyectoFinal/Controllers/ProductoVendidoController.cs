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

                //DESPUES DE REGISTAR EL LA VENTA,
                //SE PROCEDE A DESCONTAR EL STOCK DEL PRODUCTO EN DB:
                Producto productoAlmacenado = ProductoHandler.GetOneById(productoVendido.IdProducto);
                productoAlmacenado.Stock = productoAlmacenado.Stock-productoVendido.Stock;
                ProductoHandler.Update(productoAlmacenado);



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }        
    }
}