using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    public class NombreLocalController : ControllerBase
    {
        [HttpGet]
        [Route("/api/Nombre")]
        public string GetNombreLocal()
        {
            return "Tienda Digital";
        }
    }
}
