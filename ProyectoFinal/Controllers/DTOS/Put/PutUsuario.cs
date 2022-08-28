namespace ProyectoFinal.Controllers.DTOS.Put;

public class PutUsuario
{
    public int Id { set; get; }
    public string Nombre { set; get; }
    public string Apellido { set; get; }
    public string NombreUsuario { set; get; }
    public string Contraseña { set; get; }
    public string Mail { set; get; }

}