using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaAvalonia.Models.Interfaces;

namespace BibliotecaAvalonia.Models;

public class Libro : Articulo, IPrestable, IValorable
{
    public string Autor { get; set; }
    public string Isbn { get; set; }
    public bool Prestado { get; set; }
    public int DiasMaxPrestado => 31;
    
    public List<Valoracion> Valoraciones { get; set; }

    public Libro()
    {
        Autor = string.Empty;
        Isbn = string.Empty;
        Valoraciones = new List<Valoracion>();
    }

    public Libro(int id, string titulo, int anio, DateTime fechaAdquisicion, string autor, string isbn, bool prestado) :
        base(id, titulo, anio, fechaAdquisicion)
    {
        Autor = autor;
        Isbn = isbn;
        Prestado = prestado;
        Valoraciones = new List<Valoracion>();
    }

    public double CalcularMediaVal()
    {
        if (Valoraciones.Count == 0)
            return 0;
        return Valoraciones.Average(v => v.Puntuacion);
    }

    public override string ToString()
    {
        return $"Libro: {Titulo} -  ({Anio}) - ISBN: {Isbn}";
    }
    
}