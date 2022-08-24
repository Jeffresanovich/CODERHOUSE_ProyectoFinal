using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;
using System.Collections.Generic;

namespace ProyectoFinal.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VentaController : ControllerBase
    {
        //Venta: Recibe una venta con número de id 0 y dentro una lista
        //de productos por json, debe cargarlos en la base de ProductosVendidos
        //uno por uno por un lado, cargar la venta propiamente dicha a la base de
        //ventas y descontar el stock del producto por el otro.
        
        [HttpPost]
        public bool Create([FromBody] PostVenta venta)
        {
            bool resultado = false;
            
            resultado = VentaHandler.Create(new Venta 
            {
                Comentarios = venta.Comentarios
            });

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
    }
}