using ProyectoFinal.Controllers.DTOS;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        //Inicio de sesi�n: Se le pasa como par�metro el nombre del usuario y
        //la contrase�a, buscar en la base de datos si el usuario existe y
        //si coincide con la contrase�a lo devuelve, caso contrario devuelve error.

        [HttpGet("{nombreUsuario}/{contrase�a}")]
        public bool LoginByUsernameAndPassword(string nombreUsuario,string contrase�a)
        {
            bool resultado = false;
            try
            {
                resultado = UsuarioHandler.LoginByUsernameAndPassword(nombreUsuario,contrase�a);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            return resultado;
        }

        //Traer Usuario: Debe recibir un nombre del usuario, buscarlo en la base de datos y
        //devolver todos sus datos(Esto se har� para la p�gina en la que se mostrara los
        //datos del usuario y en la p�gina para modificar sus datos).

        [HttpGet("{nombreUsuario}")]
        public Usuario GetOneByUsername(string nombreUsuario)
        {
            return UsuarioHandler.GetOneByUsername(nombreUsuario);
        }


        //Crear usuario: Recibe como par�metro un JSON con todos los datos
        //cargados y debe dar un alta inmediata del usuario con los mismos
        //validando que todos los datos obligatorios est�n cargados, por el
        //contrario devolver� error(No se puede repetir el nombre de usuario.
        //Pista...se puede usar el "Traer Usuario" si se quiere reutilizar para
        //corroborar si el nombre ya existe).

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

        //Modificar usuario: Se recibir�n todos los datos del usuario por un JSON y
        //se deber� modificar el mismo con los datos nuevos(No crear uno nuevo).

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
        
        //Eliminar Usuario: Recibe el ID del usuario a eliminar y
        //lo deber� eliminar de la base de datos.

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