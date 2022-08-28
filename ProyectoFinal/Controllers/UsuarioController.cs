using ProyectoFinal.Model;
using ProyectoFinal.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections;
using ProyectoFinal.Controllers.DTOS.Post;
using ProyectoFinal.Controllers.DTOS.Put;

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
        public string LoginByUsernameAndPassword(string nombreUsuario,string contraseña)
        {
            string mensaje = "Usuario o contraseña NO valido";
            try
            {
                bool resultado = UsuarioHandler.LoginByUsernameAndPassword(nombreUsuario,contraseña);
                if (resultado) mensaje = "Login exitoso";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }
            return mensaje;
        }

        //Traer Usuario: Debe recibir un nombre del usuario, buscarlo en la base de datos y
        //devolver todos sus datos(Esto se hará para la página en la que se mostrara los
        //datos del usuario y en la página para modificar sus datos).

        [HttpGet("{nombreUsuario}")]
        public Usuario GetOneByUsername(string nombreUsuario)
        {
            Usuario usuario = new Usuario();
            try
            {
            usuario = UsuarioHandler.GetOneByUsername(nombreUsuario);

            }
            catch(Exception ex) 
            {
                Console.WriteLine("ERROR_MESSAGE:" + ex.Message);
            }

            return usuario;
        }


        //Crear usuario: Recibe como parámetro un JSON con todos los datos
        //cargados y debe dar un alta inmediata del usuario con los mismos
        //validando que todos los datos obligatorios estén cargados, por el
        //contrario devolverá error(No se puede repetir el nombre de usuario.
        //Pista...se puede usar el "Traer Usuario" si se quiere reutilizar para
        //corroborar si el nombre ya existe).

        [HttpPost]
        public string Create([FromBody] PostUsuario nuevoUsuario)
        {
            string mensaje = "Usuario NO creado";
            try
            {
                Usuario usuarioAlmacenado = UsuarioHandler.GetOneByUsername(nuevoUsuario.NombreUsuario);

                if (nuevoUsuario.NombreUsuario == usuarioAlmacenado.NombreUsuario)
                {
                    mensaje = "Nombre de Usuario YA EXISTE!";
                }
                else
                {
                    bool resultado = UsuarioHandler.Create(new Usuario
                    {
                        Nombre = nuevoUsuario.Nombre,
                        Apellido = nuevoUsuario.Apellido,
                        NombreUsuario = nuevoUsuario.NombreUsuario,
                        Contraseña = nuevoUsuario.Contraseña,
                        Mail = nuevoUsuario.Mail
                    });

                    if (resultado) mensaje = "Usuario CREADO";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }
            return mensaje;
        }

        //Modificar usuario: Se recibirán todos los datos del usuario por un JSON y
        //se deberá modificar el mismo con los datos nuevos(No crear uno nuevo).

        [HttpPut]
        public string Update([FromBody]PutUsuario usuario)
        {
            string mensaje = "Usuario NO actualizado";
            try
            {
                bool resultado = UsuarioHandler.Update(new Usuario
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });

                if (resultado) mensaje = "Usuario ACTUALIZADO";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }
            return mensaje;
        }
        
        //Eliminar Usuario: Recibe el ID del usuario a eliminar y
        //lo deberá eliminar de la base de datos.

        [HttpDelete]
        public string Delete(int id)
        {
            string mensaje = "Usuario NO eliminado";
            try
            {
                bool resultado = UsuarioHandler.Delete(id);
                if (resultado) mensaje = "Usuario ELIMINADO";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR_MESSAGE: " + ex.Message);
            }
            return mensaje;
        }
    }
}