using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BibliotecaAvalonia.Models;
using BibliotecaAvalonia.ViewModels;

namespace BibliotecaAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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
                }
            }
        }
    }
}