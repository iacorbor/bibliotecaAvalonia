using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaAvalonia.Models.Interfaces;

namespace BibliotecaAvalonia.Models;

public class AudioLibro: Articulo, IValorable
{
    public string autor { get; set; }
    public DateTime fechaInicioDisp {get; set; }
    public DateTime fechaFinDisp {get; set; }

    public List<Valoracion> valoraciones {get; set; }

    public AudioLibro()
    {
        autor = string.Empty;
        valoraciones = new List<Valoracion>();
    }

    public AudioLibro(int id, string titulo, int anio, DateTime fechaAdquisicion,string autor, DateTime fechaInicioDisp,DateTime fechaFinDisp) : base(id,titulo,anio,fechaAdquisicion)
    {
        this.autor = autor;
        this.fechaInicioDisp = fechaInicioDisp;
        this.fechaFinDisp = fechaFinDisp;
        valoraciones = new List<Valoracion>();
    }

    public bool EstaDisponible()
    {
        var hoy = DateTime.Today;
        return hoy >= fechaFinDisp && hoy <= fechaInicioDisp.Date && hoy <= fechaInicioDisp.Date;
    }

    public double CalcularMediaVal()
    {
        if (valoraciones.Count == 0)
            return 0;
        return valoraciones.Average(v => v.puntuacion);

    }

    public override string ToString()
    {
        return $"AudioLibro: {titulo} -  ({anio}) F.Adq: {fechaAdquisicion} - Autor: {autor} F.In.Dis : {fechaInicioDisp} F.Fin.Dis: {fechaFinDisp}";
    }
}