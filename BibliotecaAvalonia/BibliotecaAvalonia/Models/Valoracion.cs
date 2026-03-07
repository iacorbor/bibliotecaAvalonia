namespace BibliotecaAvalonia.Models;

public class Valoracion
{
    public int id { get; set; }
    public int articuloId { get; set; }
    public int puntuacion  { get; set; }
    public string identificadorUsuario { get; set; }
    public string? comentario { get; set; }
    public string? palabrasClave  { get; set; }
    
}