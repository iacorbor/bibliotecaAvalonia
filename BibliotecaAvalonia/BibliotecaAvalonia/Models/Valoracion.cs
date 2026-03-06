namespace BibliotecaAvalonia.Models;

public class Valoracion
{
    public int Id { get; set; }
    public int ArticuloId { get; set; }
    public int Puntuacion  { get; set; }
    public string IdentificadorUsuario { get; set; }
    public string? Comentario { get; set; }
    public string? PalabrasClave  { get; set; }
    
}