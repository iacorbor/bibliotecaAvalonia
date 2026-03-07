using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaAvalonia.Models.Interfaces;

namespace BibliotecaAvalonia.Models;

public class Libro : Articulo, IPrestable, IValorable
{
    public string autor { get; set; }
    public string isbn { get; set; }
    public bool prestado { get; set; }
    public int diasMaxPrestado => 31;
    
    public List<Valoracion> valoraciones { get; set; }

    public Libro()
    {
        autor = string.Empty;
        isbn = string.Empty;
        valoraciones = new List<Valoracion>();
    }

    public Libro(int id, string titulo, int anio, DateTime fechaAdquisicion, string autor, string isbn, bool prestado) :
        base(id, titulo, anio, fechaAdquisicion)
    {
        this.autor = autor;
        this.isbn = isbn;
        this.prestado = prestado;
        valoraciones = new List<Valoracion>();
    }

    public double CalcularMediaVal()
    {
        if (valoraciones.Count == 0)
            return 0;
        return valoraciones.Average(v => v.puntuacion);
    }

    public override string ToString()
    {
        return $"Libro: {titulo} -  ({anio}) F.Adq: {fechaAdquisicion} - ISBN: {isbn} Autor: {autor} Prestado: {prestado}";
    }
    
}