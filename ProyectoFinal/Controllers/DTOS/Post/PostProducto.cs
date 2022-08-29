namespace ProyectoFinal.Controllers.DTOS.Post;

public class PostProducto
{
    public int Id { get; set; }//SEGUN LA CONSIGNA RECIBE UN ID 0, PERO ESTE, NO SE ALMACENA EN LA BASE DE DATOS
    public string Descripciones { set; get; }
    public double Costo { set; get; }
    public double PrecioVenta { set; get; }
    public int Stock { set; get; }
    public int IdUsuario { set; get; }
}
