namespace ProyectoFinal.Model
{
    public class Producto
    {
        public int Id { set; get; }
        public string Descripciones { set; get; }
        public double Costo { set; get; }
        public double PrecioVenta { set; get; }
        public int Stock { set; get; }
        public int IdUsuario { set; get; }
    }
}