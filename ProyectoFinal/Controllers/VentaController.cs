using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : ControllerBase
    {
        //Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la
        //efectuó, primero cargar una nueva venta en la base de datos, luego debe cargar
        //los productos recibidos en la base de ProductosVendidos uno por uno por un lado,
        //y descontar el stock en la base de productos por el otro.

        [HttpPost]
        public string Create([FromBody] PostVenta venta)
        {
            string mensaje = "Venta NO registrada";

            int idNuevaVenta = 6;

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


        //Eliminar Venta: Recibe una venta con su número de Id, debe buscar en la base de
        //Productos Vendidos cuáles lo tienen eliminándolos, sumar el stock a los
        //productos incluidos, y eliminar la venta de la base de datos.

        [HttpDelete] //FALTA PROGRAMAR
        public bool Delete(int id)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }
            return resultado;
        }


        //Traer Ventas: Debe traer todas las ventas de la base,
        //incluyendo sus Productos, cuya información está en ProductosVendidos.
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

        [HttpGet ("{idVenta}")]
        public List<GetVenta> GetById(int idVenta)
        {
            List<GetVenta> repuesta = new List<GetVenta>();

            try
            {
                repuesta = VentaHandler.GetById(idVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR MESSAGE: " + ex.Message);
            }

            return repuesta;
        }
    }
}