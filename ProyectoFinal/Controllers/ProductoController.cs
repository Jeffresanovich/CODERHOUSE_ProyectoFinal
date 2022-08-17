using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public List<Producto> GetAll()
        {
            return ProductoHanlder.GetAll();
        }
    }
}