using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;
using System.Threading;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoController : ControllerBase
    {
        //Traer Productos: Debe traer todos los productos cargados en la base.

        [HttpGet]
        public List<Producto> GetAll()
        {
            return ProductoHandler.GetAll();
        }

        //Crear producto: Recibe una lista de tareas por json, número de Id 0, 
        //Descripción , costo, precio venta y stock.

        [HttpPost]
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
        
        //Modificar producto: Recibe un producto con su número de Id,
        //debe modificarlo con la nueva información.

        [HttpPut]
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
                     
        //Eliminar producto: Recibe el número de Id de un producto a eliminar
        //y debe eliminarlo de la base de datos.

        [HttpDelete ("{id}")]
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