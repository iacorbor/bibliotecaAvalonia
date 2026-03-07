using System.Collections.Generic;

namespace BibliotecaAvalonia.Models.Interfaces;

public interface IValorable
{
    List<Valoracion> valoraciones {get; set; }
    double CalcularMediaVal();
}