using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProyectoFinal.Controllers.DTOS.Get;
using ProyectoFinal.Controllers.DTOS.Post;

namespace ProyectoFinal.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : ControllerBase
    {
        //Traer Ventas: Debe traer todas las ventas de la base,
        //incluyendo sus Productos, cuya información está en ProductosVendidos.
        [HttpGet]
        public List<GetVenta> GetAll()
        {
            return VentaHandler.GetAll();
        }

        //Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario,
        //cuya información está en su producto(Utilizar dentro de esta función el
        //"Traer Productos" anteriormente hecho para saber que
        //productosVendidos ir a buscar).
        [HttpGet("{idUsuario}")]
        public List<GetVenta> GetByIdUsuario(int idUsuario)
        {
            return VentaHandler.GetByIdUsuario(idUsuario);
        }



        //Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la
        //efectuó, primero cargar una nueva venta en la base de datos, luego debe cargar
        //los productos recibidos en la base de ProductosVendidos uno por uno por un lado,
        //y descontar el stock en la base de productos por el otro.
        [HttpPost]
        public string Create([FromBody] PostVenta venta)
        {
            string mensaje = "Venta NO registrada";

            int idNuevaVenta = VentaHandler.Create(new Venta
            {
                Comentarios = venta.Comentarios
            });

            idNuevaVenta = venta.Id;

            if (idNuevaVenta > 0)
            {
                if (venta.listaProductosVendidos.Count > 0)
                {
                    foreach (PostProductoVendido productoVendido in venta.listaProductosVendidos)
                    {
                        ProductoVendidoHandler.Create(new ProductoVendido
                        {
                            Stock = productoVendido.Stock,
                            IdProducto = productoVendido.IdProducto,
                            IdVenta = idNuevaVenta
                        });

                        mensaje = "Venta REGISTRADA";
                    }
                }
                else
                {
                    mensaje = "Venta REGISTRADA, pero sin productos vendidos";
                }                
            }
            return mensaje;
        }

        //Eliminar Venta: Recibe una venta con su número de Id, debe buscar en la base de
        //Productos Vendidos cuáles lo tienen eliminándolos, sumar el stock a los
        //productos incluidos, y eliminar la venta de la base de datos.
        [HttpDelete]
        public string Delete(int id)
        {
            string mensaje = "Venta NO eliminada";
            try
            {
                bool resultado = VentaHandler.Delete(id);
                if (resultado) mensaje = "Venta ELIMINADA";

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);

            }
            return mensaje;
        }        
    }
}