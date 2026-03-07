using System;
using BibliotecaAvalonia.Models;

namespace BibliotecaAvalonia.ViewModels
{
    public class CrearArticuloViewModel : ViewModelBase
    {
        private int _tipoSeleccionado = 0;
        public int TipoSeleccionado
        {
            get => _tipoSeleccionado;
            set
            {
                if (SetProperty(ref _tipoSeleccionado, value))
                {
                    OnPropertyChanged(nameof(EsLibro));
                    OnPropertyChanged(nameof(EsAudiolibro));
                }
            }
        }

        public string[] TiposArticulo { get; } = { "Libro", "Audiolibro" };

        private string _titulo = "";
        public string Titulo
        {
            get => _titulo;
            set => SetProperty(ref _titulo, value);
        }

        private string _anioTexto = "";
        public string AnioTexto
        {
            get => _anioTexto;
            set => SetProperty(ref _anioTexto, value);
        }

        private DateTimeOffset? _fechaAdquisicion = DateTimeOffset.Now;
        public DateTimeOffset? FechaAdquisicion
        {
            get => _fechaAdquisicion;
            set => SetProperty(ref _fechaAdquisicion, value);
        }

        private string _autor = "";
        public string Autor
        {
            get => _autor;
            set => SetProperty(ref _autor, value);
        }

        private string _isbn = "";
        public string Isbn
        {
            get => _isbn;
            set => SetProperty(ref _isbn, value);
        }

        private bool _prestado;
        public bool Prestado
        {
            get => _prestado;
            set => SetProperty(ref _prestado, value);
        }

        private DateTimeOffset? _fechaInicioDisp = DateTimeOffset.Now;
        public DateTimeOffset? FechaInicioDisp
        {
            get => _fechaInicioDisp;
            set => SetProperty(ref _fechaInicioDisp, value);
        }

        private DateTimeOffset? _fechaFinDisp = DateTimeOffset.Now.AddDays(30);
        public DateTimeOffset? FechaFinDisp
        {
            get => _fechaFinDisp;
            set => SetProperty(ref _fechaFinDisp, value);
        }

        public bool EsLibro => TipoSeleccionado == 0;
        public bool EsAudiolibro => TipoSeleccionado == 1;

        public Articulo? ConstruirArticulo(int nuevoId)
        {
            if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Autor))
                return null;

            if (!int.TryParse(AnioTexto, out int anio))
                return null;

            if (FechaAdquisicion == null)
                return null;

            if (EsLibro)
            {
                if (string.IsNullOrWhiteSpace(Isbn))
                    return null;

                return new Libro(
                    id: nuevoId,
                    titulo: Titulo.Trim(),
                    anio: anio,
                    fechaAdquisicion: FechaAdquisicion.Value.DateTime,
                    autor: Autor.Trim(),
                    isbn: Isbn.Trim(),
                    prestado: Prestado
                );
            }

            if (FechaInicioDisp == null || FechaFinDisp == null)
                return null;

            return new AudioLibro(
                id: nuevoId,
                titulo: Titulo.Trim(),
                anio: anio,
                fechaAdquisicion: FechaAdquisicion.Value.DateTime,
                autor: Autor.Trim(),
                fechaInicioDisp: FechaInicioDisp.Value.DateTime,
                fechaFinDisp: FechaFinDisp.Value.DateTime
            );
        }
    }
}