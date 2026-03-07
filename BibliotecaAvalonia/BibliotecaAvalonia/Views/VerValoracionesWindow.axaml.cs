using Avalonia.Controls;
using Avalonia.Interactivity;
using BibliotecaAvalonia.ViewModels;

namespace BibliotecaAvalonia.Views
{
    public partial class VerValoracionesWindow : Window
    {
        public VerValoracionesWindow(int articuloId)
        {
            InitializeComponent();
            DataContext = new VerValoracionesViewModel(articuloId);
        }

        private void Cerrar_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}