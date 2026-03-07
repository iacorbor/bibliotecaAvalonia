using Avalonia.Controls;
using Avalonia.Interactivity;
using BibliotecaAvalonia.ViewModels;

namespace BibliotecaAvalonia.Views
{
    public partial class ValorarArticuloWindow : Window
    {
        public ValorarArticuloViewModel ViewModel => (ValorarArticuloViewModel)DataContext!;

        public ValorarArticuloWindow()
        {
            InitializeComponent();
            DataContext = new ValorarArticuloViewModel();
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