//using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [Route("[Controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{username}/{password}")]
        public bool GetOneByUsernameAndPassword(string username, string password)
        {
            return UsuarioHandler.GetOneByUsernameAndPassword(username, password);
        }
    }
}