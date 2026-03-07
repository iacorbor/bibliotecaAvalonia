using System;
using System.Collections.ObjectModel;
using BibliotecaAvalonia.Models;
using CommunityToolkit.Mvvm.Input;

namespace BibliotecaAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Articulo> Articulos { get; set; }

        public Articulo? _articuloseleccionado;

        public Articulo? ArticuloSeleccionado
        {
            get => _articuloseleccionado;
            set
            {
                SetProperty(ref _articuloseleccionado, value);
                EliminarArticuloCommand.NotifyCanExecuteChanged();

            } 
        }

        public RelayCommand EliminarArticuloCommand { get; }

        public MainWindowViewModel()
        {
            Articulos = new ObservableCollection<Articulo>();
            EliminarArticuloCommand = new RelayCommand(EliminarArticulo, PuedeEliminarArticulo);
            CargarDatosPrueba();
        }

        private void CargarDatosPrueba()
        {
            Articulos.Add(new Libro(id:1,titulo:"Harry Potter",anio:1997,fechaAdquisicion: DateTime.Today,autor:"J.K Rowling",isbn:":123456789K",prestado:false));
            Articulos.Add(new Libro(id:2,titulo:"Isra",anio:2000,fechaAdquisicion:DateTime.Today,autor:"Isra",isbn:"987654321K",prestado:true));
            Articulos.Add(new AudioLibro(id:3,titulo:"AudioIsra",anio:2026,fechaAdquisicion: new DateTime(2025,5,20),autor:"Isra",fechaInicioDisp: DateTime.Today, fechaFinDisp: new DateTime(2027,1,1)));
        }

        private void EliminarArticulo()
        {
            if (ArticuloSeleccionado is null)
                return;
            Articulos.Remove(ArticuloSeleccionado);
            ArticuloSeleccionado = null;
        }

        private bool PuedeEliminarArticulo()
        {
            return ArticuloSeleccionado != null;
        }
    }
}