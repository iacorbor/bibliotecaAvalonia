using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaAvalonia.Models.Interfaces;

namespace BibliotecaAvalonia.Models;

public class AudioLibro: Articulo, IValorable
{
    public string Autor { get; set; }
    public DateTime FechaInicioDisp {get; set; }
    public DateTime FechaFinDisp {get; set; }

    public List<Valoracion> Valoraciones {get; set; }

    public AudioLibro()
    {
        Autor = string.Empty;
        Valoraciones = new List<Valoracion>();
    }

    public AudioLibro(int Id, string Titulo, int anio, DateTime FechaAdquisicion,string autor, DateTime fechaInicioDisp,DateTime fechaFinDisp) : base(Id,Titulo,anio,FechaAdquisicion)
    {
        Autor = autor;
        FechaInicioDisp = fechaInicioDisp;
        FechaFinDisp = fechaFinDisp;
        Valoraciones = new List<Valoracion>();
    }

    public bool EstaDisponible()
    {
        var hoy = DateTime.Today;
        return hoy >= FechaFinDisp && hoy <= FechaInicioDisp.Date && hoy <= FechaInicioDisp.Date;
    }

    public double CalcularMediaVal()
    {
        if (Valoraciones.Count == 0)
            return 0;
        return Valoraciones.Average(v => v.Puntuacion);

    }

    public override string ToString()
    {
        return $"AudioLibro: {Titulo} -  ({Anio})";
    }
}