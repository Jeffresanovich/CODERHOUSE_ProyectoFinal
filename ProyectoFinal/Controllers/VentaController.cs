using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [Route("[Controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpGet]
        public List<Venta> GetAll()
        {
            return VentaHandler.GetAll();
        }
    }
}