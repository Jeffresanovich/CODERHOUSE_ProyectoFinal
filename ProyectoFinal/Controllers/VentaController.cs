using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;
using Microsoft.VisualBasic;

namespace ProyectoFinal.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : ControllerBase
    {
        //Cargar Venta: Recibe una lista de productos y el n�mero de IdUsuario de quien la
        //efectu�, primero cargar una nueva venta en la base de datos, luego debe cargar
        //los productos recibidos en la base de ProductosVendidos uno por uno por un lado,
        //y descontar el stock en la base de productos por el otro.

        [HttpPost]
        public string Create([FromBody] PostVenta venta)
        {
            string mensaje = "Venta NO registrada";

            int idNuevaVenta = 2;

            idNuevaVenta = VentaHandler.Create(new Venta
            {
                Comentarios = venta.Comentarios
            });

            Console.WriteLine("NUEVA VENTA: " + idNuevaVenta);   //****  BORRAR  ****

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


        //Eliminar Venta: Recibe una venta con su n�mero de Id, debe buscar en la base de
        //Productos Vendidos cu�les lo tienen elimin�ndolos, sumar el stock a los
        //productos incluidos, y eliminar la venta de la base de datos.

        [HttpDelete]
        public bool Delete(int id)
        {
            bool resultado = false;
            try
            {
                resultado = VentaHandler.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }
            return resultado;
        }


        //Traer Ventas: Debe traer todas las ventas de la base,
        //incluyendo sus Productos, cuya informaci�n est� en ProductosVendidos.
        [HttpGet]
        public List<GetVenta> GetAll()
        {
            List<GetVenta> repuesta = new List<GetVenta>();

            try
            {
                repuesta = VentaHandler.GetAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR MESSAGE: " + ex.Message);
            }

            return repuesta;
        }        
        

        //Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario,
        //cuya informaci�n est� en su producto(Utilizar dentro de esta funci�n el
        //"Traer Productos" anteriormente hecho para saber que
        //productosVendidos ir a buscar).

        [HttpGet("{idUsuario}")]
        public List<GetVenta> GetByIdUsuario(int idUsuario)
        {
            return VentaHandler.GetByIdUsuario(idUsuario);
        }        
    }
}