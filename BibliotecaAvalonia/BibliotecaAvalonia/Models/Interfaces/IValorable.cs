using System.Collections.Generic;

namespace BibliotecaAvalonia.Models.Interfaces;

public interface IValorable
{
    List<Valoracion> Valoraciones {get; set; }
    double CalcularMediaVal();
}