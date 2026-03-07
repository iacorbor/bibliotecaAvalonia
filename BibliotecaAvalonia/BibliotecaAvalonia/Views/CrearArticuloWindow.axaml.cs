using Avalonia.Controls;
using Avalonia.Interactivity;
using BibliotecaAvalonia.Models;
using BibliotecaAvalonia.ViewModels;

namespace BibliotecaAvalonia.Views
{
    public partial class CrearArticuloWindow : Window
    {
        public CrearArticuloViewModel ViewModel => (CrearArticuloViewModel)DataContext!;

        public CrearArticuloWindow()
        {
            InitializeComponent();
            DataContext = new CrearArticuloViewModel();
        }

        public CrearArticuloWindow(Articulo articulo)
        {
            InitializeComponent();
            DataContext = new CrearArticuloViewModel(articulo);
        }

        private void Guardar_Click(object? sender, RoutedEventArgs e)
        {
            if (!ViewModel.ValidarDatos())
                return;

            Close(true);
        }

        private void Cancelar_Click(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }
    }
}