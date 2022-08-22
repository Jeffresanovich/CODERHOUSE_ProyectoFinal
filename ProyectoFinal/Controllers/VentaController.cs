using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controller
{
    [ApiController]
    //[Route("[Controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        [Route("api/Venta")]    //Carga Venta
        public bool Create([FromBody] PostVenta venta)
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