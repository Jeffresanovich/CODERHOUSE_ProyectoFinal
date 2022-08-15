using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [Route("[Controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet]
        public List<ProductoVendido> GetAll()
        {
            return ProductoVendidoHandler.GetAll();
        }
    }
}