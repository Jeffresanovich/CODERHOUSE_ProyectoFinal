using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;
using Microsoft.VisualBasic;

namespace ProyectoFinal.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        //Venta: Recibe una venta con número de id 0 y dentro una lista de productos
        //por json, debe cargarlos en la base de ProductosVendidos uno por uno por un
        //lado, cargar la venta propiamente dicha a la base de ventas y
        //descontar el stock del producto por el otro.

        [HttpPost]
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
                productoAlmacenado.Stock = productoAlmacenado.Stock - productoVendido.Stock;
                ProductoHandler.Update(productoAlmacenado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }


        //Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario,
        //cuya información está en su producto(Utilizar dentro de esta función el "Traer
        //Productos" anteriormente hecho para saber que productosVendidos ir a buscar).

        [HttpGet("{idUsuario}")]
        public List<ProductoVendido> GetByIdUsuario(int idUsuario)
        {
            return ProductoVendidoHandler.GetByIdUsuario(idUsuario);
        }

    }
}