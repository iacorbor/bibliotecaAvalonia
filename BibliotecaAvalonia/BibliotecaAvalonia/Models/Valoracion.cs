namespace BibliotecaAvalonia.Models
{
    public class Valoracion
    {
        public int id { get; set; }

        public int articuloId { get; set; }

        public int usuarioId { get; set; }
        public double puntuacion { get; set; }
        public string comentario { get; set; } = "";
        
    }
}