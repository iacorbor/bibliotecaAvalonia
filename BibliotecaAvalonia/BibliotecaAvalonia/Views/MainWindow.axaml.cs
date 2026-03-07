using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BibliotecaAvalonia.Models;
using BibliotecaAvalonia.ViewModels;
using Avalonia.Platform.Storage;
using BibliotecaAvalonia.Services;
using BibliotecaAvalonia.Services;

namespace BibliotecaAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private readonly ValoracionRepository _valoracionRepo = new ValoracionRepository();

        private async void Anadir_Click(object? sender, RoutedEventArgs e)
        {
            var ventana = new CrearArticuloWindow();
            var resultado = await ventana.ShowDialog<bool>(this);

            if (!resultado)
                return;

            if (DataContext is MainWindowViewModel vm)
            {
                int nuevoId = vm.Articulos.Any() ? vm.Articulos.Max(a => a.id) + 1 : 1;

                Articulo? nuevoArticulo = ventana.ViewModel.ConstruirArticulo(nuevoId);

                if (nuevoArticulo != null)
                {
                    vm.Articulos.Add(nuevoArticulo);
                    vm.GuardarArticulo(nuevoArticulo);
                }
            }
        }
        private async void Editar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;

            if (vm.ArticuloSeleccionado == null)
                return;

            var articuloOriginal = vm.ArticuloSeleccionado;
            var ventana = new CrearArticuloWindow(articuloOriginal);

            var resultado = await ventana.ShowDialog<bool>(this);

            if (!resultado)
                return;

            Articulo? articuloEditado = ventana.ViewModel.ConstruirArticulo(articuloOriginal.id);

            if (articuloEditado == null)
                return;

            int indice = vm.Articulos.IndexOf(articuloOriginal);
            if (indice >= 0)
            {
                vm.Articulos[indice] = articuloEditado;
                vm.ArticuloSeleccionado = articuloEditado;
                vm.ActualizarArticulo(articuloOriginal, articuloEditado);
            }
        }

        private async void Exportar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;
            var topLevel = TopLevel.GetTopLevel(this);
            var archivo = await topLevel.StorageProvider.SaveFilePickerAsync(
                new FilePickerSaveOptions
                {
                    Title = "Guardar CSV",
                    SuggestedFileName = "articulos.csv"
                });
            if (archivo == null)
                return;
            var ruta = archivo.Path.LocalPath;
            CsvService.Exportar(ruta, vm.Articulos);
        }
        
        private async void Valorar_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;

            if (vm.ArticuloSeleccionado == null)
                return;

            var ventana = new ValorarArticuloWindow();

            var resultado = await ventana.ShowDialog<bool>(this);

            if (!resultado)
                return;

            var valoracion = ventana.ViewModel.ConstruirValoracion(vm.ArticuloSeleccionado.id);

            _valoracionRepo.Insertar(valoracion);

            vm.ActualizarMediaValoraciones(vm.ArticuloSeleccionado);
        }
        private async void VerValoraciones_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainWindowViewModel vm)
                return;

            if (vm.ArticuloSeleccionado == null)
                return;

            var ventana = new VerValoracionesWindow(vm.ArticuloSeleccionado.id);
            await ventana.ShowDialog(this);
        }
    }
}
