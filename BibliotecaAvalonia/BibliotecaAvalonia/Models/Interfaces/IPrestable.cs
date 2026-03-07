namespace BibliotecaAvalonia.Models.Interfaces;

public interface IPrestable
{
    bool prestado { get; set; }
    int diasMaxPrestado { get; }
}