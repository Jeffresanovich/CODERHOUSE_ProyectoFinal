using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    //[Route("[Controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("api/producto")]
        public List<Producto> GetAll()
        {
            return ProductoHandler.GetAll();
        }

        [HttpGet]
        [Route("api/producto/{id}")]
        public Producto GetOneById(int id)
        {
            return ProductoHandler.GetOneById(id);
        }

        [HttpPost]
        [Route("api/producto")]
        public bool Create([FromBody] PostProducto producto)
        {
            bool resultado = false;
            try
            {
                resultado = ProductoHandler.Create(new Producto
                {
                     Descripciones=producto.Descripciones,
                     Costo=producto.Costo,
                     PrecioVenta=producto.PrecioVenta,
                     Stock=producto.Stock,
                     IdUsuario=producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }
        
        [HttpPut]
        [Route("api/producto")]
        public bool Update([FromBody] PutProducto producto)  
        {
            bool resultado = false;
            try
            {
                resultado = ProductoHandler.Update(new Producto
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }


            return resultado;
        }
        
        [HttpDelete]
        [Route("api/producto")]
        public bool Delete(int id)
        {
            bool resultado = false;
            try
            {
                resultado = ProductoHandler.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }
            return resultado;
        }
    }
}