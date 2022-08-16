using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [Route("[Controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{id}")]
        public Usuario GetOneById(int id)
        {
            return UsuarioHandler.GetOneById(id);
        }
    }
}