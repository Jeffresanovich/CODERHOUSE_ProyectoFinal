using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]   
    public class NombreController : ControllerBase
    {
        //Traer Nombre del Local: Se debe enviar un json al front que contenga
        //únicamente un string con el Nombre de la App a elección.

        [HttpGet]
        public string GetNombreLocal()
        {
            return "Tienda Digital Ecomerce";
        }        
    }
}
