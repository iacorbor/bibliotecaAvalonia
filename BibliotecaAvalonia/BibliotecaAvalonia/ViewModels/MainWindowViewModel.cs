using System;
using System.Collections.ObjectModel;
using BibliotecaAvalonia.Models;

namespace BibliotecaAvalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Articulo> Articulos { get; set; }

        public MainWindowViewModel()
        {
            Articulos = new ObservableCollection<Articulo>();
            CargarDatosPrueba();
        }

        private void CargarDatosPrueba()
        {
            Articulos.Add(new Libro(id:1,titulo:"Harry Potter",anio:1997,fechaAdquisicion: DateTime.Today,autor:"J.K Rowling",isbn:":123456789K",prestado:false));
            Articulos.Add(new Libro(id:2,titulo:"Isra",anio:2000,fechaAdquisicion:DateTime.Today,autor:"Isra",isbn:"987654321K",prestado:true));
            Articulos.Add(new AudioLibro(Id:3,Titulo:"AudioIsra",anio:2026,FechaAdquisicion: new DateTime(2025,5,20),autor:"Isra",fechaInicioDisp: DateTime.Today, fechaFinDisp: new DateTime(2027,1,1)));
        }
    }
}