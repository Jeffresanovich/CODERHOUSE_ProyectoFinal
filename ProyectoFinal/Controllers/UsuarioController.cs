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
        //Inicio de sesión: Se le pasa como parámetro el nombre del usuario y
        //la contraseña, buscar en la base de datos si el usuario existe y
        //si coincide con la contraseña lo devuelve, caso contrario devuelve error.

        [HttpGet("{nombreUsuario}/{contraseña}")]
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

        //Traer Usuario: Debe recibir un nombre del usuario, buscarlo en la base de datos y
        //devolver todos sus datos(Esto se hará para la página en la que se mostrara los
        //datos del usuario y en la página para modificar sus datos).

        [HttpGet("{nombreUsuario}")]
        public Usuario GetOneByUsername(string nombreUsuario)
        {
            return UsuarioHandler.GetOneByUsername(nombreUsuario);
        }


        //Crear usuario: Recibe como parámetro un JSON con todos los datos
        //cargados y debe dar un alta inmediata del usuario con los mismos
        //validando que todos los datos obligatorios estén cargados, por el
        //contrario devolverá error(No se puede repetir el nombre de usuario.
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

        //Modificar usuario: Se recibirán todos los datos del usuario por un JSON y
        //se deberá modificar el mismo con los datos nuevos(No crear uno nuevo).

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
        
        //Eliminar Usuario: Recibe el ID del usuario a eliminar y
        //lo deberá eliminar de la base de datos.

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