namespace ProyectoFinal.Controllers.DTOS.Put;

public class PutProducto
{
    public int Id { set; get; }
    public string Descripciones { set; get; }
    public double Costo { set; get; }
    public double PrecioVenta { set; get; }
    public int Stock { set; get; }
    public int IdUsuario { set; get; }
}
