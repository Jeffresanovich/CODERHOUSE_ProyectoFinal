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
        [HttpGet]//NO ESTA EN LA CONSIGNA: Trae UN Producto
        [Route("api/Producto/{idUsuario}")]
        public List<Producto> GetOneByIdUsuario(int idUsuario)
        {
            return ProductoHandler.GetOneByIdUsuario(idUsuario);
        }

        [HttpGet]
        [Route("api/Producto/")]    //Trae Productos
        public List<Producto> GetAll()
        {
            return ProductoHandler.GetAll();
        }

        [HttpPost]
        [Route("api/Producto")]     //Crea producto
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
        [Route("api/Producto")]     //Modifica producto
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
        [Route("api/Producto/{id}")]    //Elimina producto
        public bool Delete(int id)
        {
            bool resultado = false;
            try
            {
                //PRIMERO ELIMINO EL PRODUCTO VENDIDO
                ProductoVendidoHandler.DeleteByIdProducto(id);
                //LUEGO ELIMINO EL PRODUCTO
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