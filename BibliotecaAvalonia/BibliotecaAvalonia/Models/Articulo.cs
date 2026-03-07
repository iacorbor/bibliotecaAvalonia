using System;

namespace BibliotecaAvalonia.Models;

public abstract class Articulo
{
    public int id { get; set; }
    public string titulo { get; set; }
    public int anio { get; set; }
    public DateTime fechaAdquisicion { get; set; }

    protected Articulo()
    {
        titulo = string.Empty;
    }

    protected Articulo(int id,string titulo,int anio,DateTime fechaAdquisicion)
    {
        this.id = id;
        this.titulo = titulo;
        this.anio = anio;
        this.fechaAdquisicion = fechaAdquisicion;
    }

    public override string ToString()
    {
        return $" {id} // {titulo} ({anio}) - {fechaAdquisicion}";
    }

}