using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
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