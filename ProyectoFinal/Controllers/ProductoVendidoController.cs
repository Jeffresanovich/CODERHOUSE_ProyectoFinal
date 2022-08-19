using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controller
{
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet]
        [Route("api/productoVendido/{id}")]
        public List<ProductoVendido> GetAll()
        {
            return ProductoVendidoHandler.GetAll();
        }
    }
}