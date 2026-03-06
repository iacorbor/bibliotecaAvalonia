namespace BibliotecaAvalonia.Models.Interfaces;

public interface IPrestable
{
    bool Prestado { get; set; }
    int DiasMaxPrestado { get; }
}