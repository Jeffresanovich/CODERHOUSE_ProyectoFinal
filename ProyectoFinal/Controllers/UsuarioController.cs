using ProyectoFinal.Controllers;
using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("{nombreUsuario}/{contrase�a}")]
        public bool GetOneByUsernameAndPassword(string nombreUsuario,string contrase�a)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.GetOneByUsernameAndPassword(nombreUsuario,contrase�a);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }

        [HttpPost]
        public bool Create([FromBody] PostUsuario usuario)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.Create(new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contrase�a = usuario.Contrase�a,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }
        [HttpPut]
        public bool Update([FromBody]PutUsuario usuario)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.Update(new Usuario
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contrase�a = usuario.Contrase�a,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }


            return resultado;
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.Delete(id);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }
            return resultado;
        }
    }
}