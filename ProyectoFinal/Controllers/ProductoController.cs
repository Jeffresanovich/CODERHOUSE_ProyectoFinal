using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using ProyectoFinal.Controllers.DTOS.Post;
using ProyectoFinal.Controllers.DTOS.Put;

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

        //Crear producto: Recibe una --Lista de tareas-- (SERÁ UN PRODUCTO) por JSON, número de Id 0,
        //Descripción , costo, precio venta y stock.

        [HttpPost]
        public string Create([FromBody] PostProducto producto)
        {
            string mensaje="Poducto NO creado";

            try
            {
                bool resultado = ProductoHandler.Create(new Producto
                {
                     Descripciones=producto.Descripciones,
                     Costo=producto.Costo,
                     PrecioVenta=producto.PrecioVenta,
                     Stock=producto.Stock,
                     IdUsuario=producto.IdUsuario
                });

                if (resultado) mensaje = "Poducto CREADO";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }
            return mensaje;
        }
        
        //Modificar producto: Recibe un producto con su número de Id, debe modificarlo con
        //la nueva información.

        [HttpPut]
        public string Update([FromBody] PutProducto producto)  
        {
            string mensaje = "Producto NO actualizado";
            try
            {
                bool resultado = ProductoHandler.Update(new Producto
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });

                if (resultado) mensaje = "Producto ACTUALIZADO";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);

            }
            return mensaje;
        }
                     
        //Eliminar producto: Recibe el número de Id de un producto a eliminar y
        //debe eliminarlo de la base de datos.

        [HttpDelete ("{id}")]
        public string Delete(int id)
        {

            string mensaje = "Producto NO borrado";
            try
            {
                //PRIMERO ELIMINO EL PRODUCTO VENDIDO
                ProductoVendidoHandler.DeleteByIdProducto(id);
                //LUEGO ELIMINO EL PRODUCTO
                bool resultado = ProductoHandler.Delete(id);

                if (resultado) mensaje = "Producto BORRADO";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);

            }
            return mensaje;
        }
    }
}