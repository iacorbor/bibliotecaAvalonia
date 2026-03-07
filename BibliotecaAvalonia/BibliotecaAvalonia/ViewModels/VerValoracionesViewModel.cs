using System.Collections.ObjectModel;
using BibliotecaAvalonia.Models;
using BibliotecaAvalonia.Services;

namespace BibliotecaAvalonia.ViewModels
{
    public class VerValoracionesViewModel : ViewModelBase
    {
        private readonly ValoracionRepository _repo = new ValoracionRepository();

        public ObservableCollection<Valoracion> valoraciones { get; set; }

        public VerValoracionesViewModel(int articuloId)
        {
            valoraciones = new ObservableCollection<Valoracion>(_repo.ObtenerPorArticulo(articuloId));
        }
    }
}