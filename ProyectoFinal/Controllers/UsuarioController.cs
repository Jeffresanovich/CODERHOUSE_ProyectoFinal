//using ProyectoFinal.Controllers;
using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    //[Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("api/Usuario/{nombreUsuario}/{contraseña}")]     //Inicio de sesión
        public bool LoginByUsernameAndPassword(string nombreUsuario,string contraseña)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.LoginByUsernameAndPassword(nombreUsuario,contraseña);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }

        [HttpGet]
        [Route("api/Usuario/{nombreUsuario}")]      //Trae Usuario
        public Usuario GetOneByUsername(string nombreUsuario)
        {
            return UsuarioHandler.GetOneByUsername(nombreUsuario);
        }

        [HttpPost]
        [Route("api/Usuario")]      //Crea usuario
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
                    Contraseña = usuario.Contraseña,
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
        [Route("api/Usuario")]      //Modifica usuario
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
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);

            }


            return resultado;
        }
        
        [HttpDelete]        //Elimina Usuario
        [Route("api/Usuario")]
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