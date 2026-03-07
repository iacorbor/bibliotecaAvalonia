using System.Globalization;
using BibliotecaAvalonia.Models;

namespace BibliotecaAvalonia.ViewModels
{
    public class ValorarArticuloViewModel : ViewModelBase
    {
        private string _usuarioId = "";
        public string usuarioId
        {
            get => _usuarioId;
            set => SetProperty(ref _usuarioId, value);
        }

        private string _puntuacion = "";
        public string puntuacion
        {
            get => _puntuacion;
            set => SetProperty(ref _puntuacion, value);
        }

        private string _comentario = "";
        public string comentario
        {
            get => _comentario;
            set => SetProperty(ref _comentario, value);
        }

        private string _mensajeError = "";
        public string mensajeError
        {
            get => _mensajeError;
            set => SetProperty(ref _mensajeError, value);
        }

        public bool ValidarDatos()
        {
            mensajeError = "";

            if (!int.TryParse(usuarioId, out int idUsuario) || idUsuario <= 0)
            {
                mensajeError = "El identificador de usuario debe ser un entero positivo.";
                return false;
            }

            string textoPuntuacion = (puntuacion ?? "").Trim().Replace(',', '.');

            if (!double.TryParse(textoPuntuacion, NumberStyles.Any, CultureInfo.InvariantCulture, out double nota))
            {
                mensajeError = "La puntuación debe ser numérica.";
                return false;
            }

            if (nota < 0 || nota > 10)
            {
                mensajeError = "La puntuación debe estar entre 0 y 10.";
                return false;
            }

            return true;
        }

        public Valoracion ConstruirValoracion(int articuloId)
        {
            string textoPuntuacion = puntuacion.Trim().Replace(',', '.');

            return new Valoracion
            {
                articuloId = articuloId,
                usuarioId = int.Parse(usuarioId),
                puntuacion = double.Parse(textoPuntuacion, CultureInfo.InvariantCulture),
                comentario = comentario?.Trim() ?? ""
            };
        }
    }
}