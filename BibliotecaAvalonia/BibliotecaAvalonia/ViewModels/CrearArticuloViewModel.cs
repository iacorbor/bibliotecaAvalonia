using System;
using BibliotecaAvalonia.Models;

namespace BibliotecaAvalonia.ViewModels
{
    public class CrearArticuloViewModel : ViewModelBase
    {
        private int _tipoSeleccionado = 0;
        public int tipoSeleccionado
        {
            get => _tipoSeleccionado;
            set
            {
                if (SetProperty(ref _tipoSeleccionado, value))
                {
                    OnPropertyChanged(nameof(esLibro));
                    OnPropertyChanged(nameof(esAudiolibro));
                    mensajeError = "";
                }
            }
        }

        public string[] tiposArticulo { get; } = { "Libro", "Audiolibro" };

        private string _titulo = "";
        public string titulo
        {
            get => _titulo;
            set => SetProperty(ref _titulo, value);
        }

        private string _anioTexto = "";
        public string anioTexto
        {
            get => _anioTexto;
            set => SetProperty(ref _anioTexto, value);
        }

        private DateTimeOffset? _fechaAdquisicion = DateTimeOffset.Now;
        public DateTimeOffset? fechaAdquisicion
        {
            get => _fechaAdquisicion;
            set => SetProperty(ref _fechaAdquisicion, value);
        }

        private string _autor = "";
        public string autor
        {
            get => _autor;
            set => SetProperty(ref _autor, value);
        }

        private string _isbn = "";
        public string isbn
        {
            get => _isbn;
            set => SetProperty(ref _isbn, value);
        }

        private bool _prestado;
        public bool prestado
        {
            get => _prestado;
            set => SetProperty(ref _prestado, value);
        }

        private DateTimeOffset? _fechaInicioDisp = DateTimeOffset.Now;
        public DateTimeOffset? fechaInicioDisp
        {
            get => _fechaInicioDisp;
            set => SetProperty(ref _fechaInicioDisp, value);
        }

        private DateTimeOffset? _fechaFinDisp = DateTimeOffset.Now.AddDays(30);
        public DateTimeOffset? fechaFinDisp
        {
            get => _fechaFinDisp;
            set => SetProperty(ref _fechaFinDisp, value);
        }

        private string _mensajeError = "";
        public string mensajeError
        {
            get => _mensajeError;
            set => SetProperty(ref _mensajeError, value);
        }

        public bool esLibro => tipoSeleccionado == 0;
        public bool esAudiolibro => tipoSeleccionado == 1;

        public CrearArticuloViewModel()
        {
        }

        public CrearArticuloViewModel(Articulo articulo)
        {
            titulo = articulo.titulo;
            anioTexto = articulo.anio.ToString();
            fechaAdquisicion = new DateTimeOffset(articulo.fechaAdquisicion);

            if (articulo is Libro libro)
            {
                tipoSeleccionado = 0;
                autor = libro.autor;
                isbn = libro.isbn;
                prestado = libro.prestado;
            }
            else if (articulo is AudioLibro audio)
            {
                tipoSeleccionado = 1;
                autor = audio.autor;
                fechaInicioDisp = new DateTimeOffset(audio.fechaInicioDisp);
                fechaFinDisp = new DateTimeOffset(audio.fechaFinDisp);
            }
        }

        public bool ValidarDatos()
        {
            mensajeError = "";

            string tituloLimpio = titulo?.Trim() ?? "";
            string autorLimpio = autor?.Trim() ?? "";
            string isbnLimpio = isbn?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(tituloLimpio))
            {
                mensajeError = "El título es obligatorio.";
                return false;
            }

            if (titulo != tituloLimpio)
            {
                mensajeError = "El título no puede tener espacios al inicio o al final.";
                return false;
            }

            if (!char.IsUpper(tituloLimpio[0]))
            {
                mensajeError = "La primera letra del título debe estar en mayúscula.";
                return false;
            }

            if (!int.TryParse(anioTexto, out int anio))
            {
                mensajeError = "El año debe ser numérico.";
                return false;
            }

            int anioActual = DateTime.Now.Year;
            if (anio < 1500 || anio > anioActual)
            {
                mensajeError = $"El año debe estar entre 1500 y {anioActual}.";
                return false;
            }

            if (fechaAdquisicion == null)
            {
                mensajeError = "La fecha de adquisición es obligatoria.";
                return false;
            }

            if (fechaAdquisicion.Value.Date > DateTimeOffset.Now.Date)
            {
                mensajeError = "La fecha de adquisición no puede ser futura.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(autorLimpio))
            {
                mensajeError = "El autor es obligatorio.";
                return false;
            }

            if (autor != autorLimpio)
            {
                mensajeError = "El autor no puede tener espacios al inicio o al final.";
                return false;
            }

            if (esLibro)
            {
                if (string.IsNullOrWhiteSpace(isbnLimpio))
                {
                    mensajeError = "El ISBN es obligatorio para un libro.";
                    return false;
                }

                if (!EsIsbn10Valido(isbnLimpio))
                {
                    mensajeError = "El ISBN-10 no es válido.";
                    return false;
                }
            }

            if (esAudiolibro)
            {
                if (fechaInicioDisp == null)
                {
                    mensajeError = "La fecha de inicio de disponibilidad es obligatoria.";
                    return false;
                }

                if (fechaFinDisp == null)
                {
                    mensajeError = "La fecha de fin de disponibilidad es obligatoria.";
                    return false;
                }

                if (fechaFinDisp.Value.Date < fechaInicioDisp.Value.Date)
                {
                    mensajeError = "La fecha fin no puede ser anterior a la fecha inicio.";
                    return false;
                }
            }

            return true;
        }

        public Articulo ConstruirArticulo(int id)
        {
            string tituloLimpio = titulo.Trim();
            string autorLimpio = autor.Trim();

            if (esLibro)
            {
                return new Libro(
                    id: id,
                    titulo: tituloLimpio,
                    anio: int.Parse(anioTexto),
                    fechaAdquisicion: fechaAdquisicion!.Value.DateTime,
                    autor: autorLimpio,
                    isbn: isbn.Trim().Replace("-", "").Replace(" ", "").ToUpper(),
                    prestado: prestado
                );
            }

            return new AudioLibro(
                id: id,
                titulo: tituloLimpio,
                anio: int.Parse(anioTexto),
                fechaAdquisicion: fechaAdquisicion!.Value.DateTime,
                autor: autorLimpio,
                fechaInicioDisp: fechaInicioDisp!.Value.DateTime,
                fechaFinDisp: fechaFinDisp!.Value.DateTime
            );
        }

        private bool EsIsbn10Valido(string valor)
        {
            string limpio = valor.Replace("-", "").Replace(" ", "").ToUpper();

            if (limpio.Length != 10)
                return false;

            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                int numero;

                if (i == 9 && limpio[i] == 'X')
                {
                    numero = 10;
                }
                else
                {
                    if (!char.IsDigit(limpio[i]))
                        return false;

                    numero = limpio[i] - '0';
                }

                suma += numero * (10 - i);
            }

            return suma % 11 == 0;
        }
    }
}