using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controller
{
    [Route("[Controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public bool Insert([FromBody] PostVenta venta)
        {
            bool resultado = false;
            
            resultado = VentaHandler.Create(new Venta 
            {
                Comentarios = venta.Comentarios
            });

            return resultado;
        }
    }
}