using System;

namespace BibliotecaAvalonia.Models;

public abstract class Articulo
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int Anio { get; set; }
    public DateTime FechaAdquisicion { get; set; }

    protected Articulo()
    {
        Titulo = string.Empty;
    }

    protected Articulo(int id,string titulo,int anio,DateTime fechaAdquisicion)
    {
        Id = id;
        Titulo = titulo;
        Anio = anio;
        FechaAdquisicion = fechaAdquisicion;
    }

    public override string ToString()
    {
        return $"{Titulo} ({Anio})";
    }

}