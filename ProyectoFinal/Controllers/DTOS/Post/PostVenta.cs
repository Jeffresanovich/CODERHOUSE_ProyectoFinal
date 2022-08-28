using ProyectoFinal.Model;

namespace ProyectoFinal.Controllers.DTOS.Post
{
    public class PostVenta
    {
        public int Id { get; set; }
        public string Comentarios { set; get; }
        public List<PostProductoVendido> listaProductosVendidos { set; get; }
    }
}
