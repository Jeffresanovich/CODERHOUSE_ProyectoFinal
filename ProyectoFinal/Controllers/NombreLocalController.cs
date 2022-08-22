using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    public class NombreLocalController : ControllerBase
    {        
        [HttpGet]
        [Route("/api/Nombre")]      //Trae Nombre del Local
        public string GetNombreLocal()
        {
            return "Tienda Digital Ecomerce";
        }        
    }
}
