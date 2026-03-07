using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using BibliotecaAvalonia.Models;
using BibliotecaAvalonia.Services;

namespace BibliotecaAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ArticuloRepository _repo = new ArticuloRepository();
        private readonly ValoracionRepository _valoracionRepo = new ValoracionRepository();

        private ObservableCollection<Articulo> _todosLosArticulos;

        public ObservableCollection<Articulo> Articulos { get; set; }

        private Articulo? _articuloSeleccionado;
        public Articulo? ArticuloSeleccionado
        {
            get => _articuloSeleccionado;
            set
            {
                SetProperty(ref _articuloSeleccionado, value);
                EliminarArticuloCommand.NotifyCanExecuteChanged();
                CambiarPrestamoCommand.NotifyCanExecuteChanged();
                DescargarAudiolibroCommand.NotifyCanExecuteChanged();
            }
        }

        private string _textoBusqueda = "";
        public string textoBusqueda
        {
            get => _textoBusqueda;
            set
            {
                if (SetProperty(ref _textoBusqueda, value))
                    AplicarFiltros();
            }
        }

        private int _tipoFiltroSeleccionado = 0;
        public int tipoFiltroSeleccionado
        {
            get => _tipoFiltroSeleccionado;
            set
            {
                if (SetProperty(ref _tipoFiltroSeleccionado, value))
                    AplicarFiltros();
            }
        }

        public string[] tiposFiltro { get; } = { "Todos", "Libro", "Audiolibro" };

        public RelayCommand EliminarArticuloCommand { get; }
        public RelayCommand CambiarPrestamoCommand { get; }
        public RelayCommand DescargarAudiolibroCommand { get; }

        public MainWindowViewModel()
        {
            _todosLosArticulos = new ObservableCollection<Articulo>(_repo.ObtenerTodos());
            Articulos = new ObservableCollection<Articulo>(_todosLosArticulos);

            EliminarArticuloCommand = new RelayCommand(EliminarArticulo, PuedeEliminarArticulo);
            CambiarPrestamoCommand = new RelayCommand(CambiarPrestamo, PuedeCambiarPrestamo);
            DescargarAudiolibroCommand = new RelayCommand(DescargarAudiolibro, PuedeDescargarAudiolibro);

            CargarMedias();
            AplicarFiltros();
        }

        public void GuardarArticulo(Articulo articulo)
        {
            _repo.Insertar(articulo);
            _todosLosArticulos.Add(articulo);
            AplicarFiltros();
        }

        public void ActualizarArticulo(Articulo articuloOriginal, Articulo articuloEditado)
        {
            _repo.Actualizar(articuloEditado);

            int indice = _todosLosArticulos.IndexOf(articuloOriginal);
            if (indice >= 0)
                _todosLosArticulos[indice] = articuloEditado;

            AplicarFiltros();
            ArticuloSeleccionado = articuloEditado;
        }

        public void ActualizarMediaValoraciones(Articulo articulo)
        {
            var valoraciones = _valoracionRepo.ObtenerPorArticulo(articulo.id);

            if (valoraciones.Count == 0)
                articulo.mediaValoraciones = 0;
            else
                articulo.mediaValoraciones = valoraciones.Average(v => v.puntuacion);

            AplicarFiltros();
        }

        private void CargarMedias()
        {
            foreach (var articulo in _todosLosArticulos)
            {
                var valoraciones = _valoracionRepo.ObtenerPorArticulo(articulo.id);

                if (valoraciones.Count == 0)
                    articulo.mediaValoraciones = 0;
                else
                    articulo.mediaValoraciones = valoraciones.Average(v => v.puntuacion);
            }
        }

        private void AplicarFiltros()
        {
            var filtrados = _todosLosArticulos.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                string texto = textoBusqueda.Trim().ToLower();

                filtrados = filtrados.Where(a =>
                    a.titulo.ToLower().Contains(texto) ||
                    (a is Libro l && l.autor.ToLower().Contains(texto)) ||
                    (a is AudioLibro al && al.autor.ToLower().Contains(texto)));
            }

            if (tipoFiltroSeleccionado == 1)
                filtrados = filtrados.Where(a => a is Libro);
            else if (tipoFiltroSeleccionado == 2)
                filtrados = filtrados.Where(a => a is AudioLibro);

            Articulos.Clear();
            foreach (var articulo in filtrados)
                Articulos.Add(articulo);
        }

        private void EliminarArticulo()
        {
            if (ArticuloSeleccionado == null)
                return;

            _valoracionRepo.EliminarPorArticulo(ArticuloSeleccionado.id);
            _repo.Eliminar(ArticuloSeleccionado.id);

            _todosLosArticulos.Remove(ArticuloSeleccionado);
            Articulos.Remove(ArticuloSeleccionado);
            ArticuloSeleccionado = null;
        }

        private bool PuedeEliminarArticulo()
        {
            return ArticuloSeleccionado != null;
        }

        private void CambiarPrestamo()
        {
            if (ArticuloSeleccionado is not Libro libro)
                return;

            libro.prestado = !libro.prestado;
            _repo.Actualizar(libro);
            AplicarFiltros();
            ArticuloSeleccionado = libro;
        }

        private bool PuedeCambiarPrestamo()
        {
            return ArticuloSeleccionado is Libro;
        }

        private void DescargarAudiolibro()
        {
            if (ArticuloSeleccionado is not AudioLibro audio)
                return;

            audio.mensajeDescarga = audio.EstaDisponibleHoy()
                ? "Descarga permitida"
                : "No disponible para descarga en esta fecha";

            AplicarFiltros();
            ArticuloSeleccionado = audio;
        }

        private bool PuedeDescargarAudiolibro()
        {
            return ArticuloSeleccionado is AudioLibro;
        }
    }
}