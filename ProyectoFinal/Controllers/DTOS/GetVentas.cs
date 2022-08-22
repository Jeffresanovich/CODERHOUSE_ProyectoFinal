namespace ProyectoFinal.Controllers.DTOS
{
    public class GetVentas
    {
        public int Id { set; get; }
        public string Comentarios { set; get; }
        public string Descripciones { set; get; }
        public double Costo { set; get; }
        public double PrecioVenta { set; get; }
        public int Stock { set; get; }
        public string NombreUsuario { set; get; }
    }
}
