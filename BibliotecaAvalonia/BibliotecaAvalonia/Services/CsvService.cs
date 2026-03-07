using System.Collections.Generic;
using System.IO;
using System.Text;
using BibliotecaAvalonia.Models;

namespace BibliotecaAvalonia.Services
{
    public static class CsvService
    {
        public static void Exportar(string ruta, IEnumerable<Articulo> articulos)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Id,Tipo,Titulo,Autor,Anio,ISBN,Prestado,FechaInicioDisp,FechaFinDisp");

            foreach (var articulo in articulos)
            {
                if (articulo is Libro libro)
                {
                    sb.AppendLine(
                        $"{libro.id},Libro,{libro.titulo},{libro.autor},{libro.anio},{libro.isbn},{(libro.prestado ? 1 : 0)},,"
                    );
                }
                else if (articulo is AudioLibro audio)
                {
                    sb.AppendLine(
                        $"{audio.id},Audiolibro,{audio.titulo},{audio.autor},{audio.anio},,,{audio.fechaInicioDisp:yyyy-MM-dd},{audio.fechaFinDisp:yyyy-MM-dd}"
                    );
                }
            }

            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }
    }
}