using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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

        private void Guardar_Click(object? sender, RoutedEventArgs e)
        {
            Close(true);
        }

        private void Cancelar_Click(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }
    }
}