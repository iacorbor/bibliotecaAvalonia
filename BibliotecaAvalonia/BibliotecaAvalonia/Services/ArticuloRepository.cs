using System;
using System.Collections.Generic;
using BibliotecaAvalonia.Models;
using Microsoft.Data.Sqlite;

namespace BibliotecaAvalonia.Services
{
    public class ArticuloRepository
    {
        public List<Articulo> ObtenerTodos()
        {
            var lista = new List<Articulo>();

            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Articulos";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string tipo = reader.GetString(1);

                if (tipo == "Libro")
                {
                    var libro = new Libro(
                        id: reader.GetInt32(0),
                        titulo: reader.GetString(2),
                        anio: reader.GetInt32(3),
                        fechaAdquisicion: DateTime.Parse(reader.GetString(4)),
                        autor: reader.GetString(5),
                        isbn: reader.IsDBNull(6) ? "" : reader.GetString(6),
                        prestado: !reader.IsDBNull(7) && reader.GetInt32(7) == 1
                    );

                    lista.Add(libro);
                }
                else if (tipo == "Audiolibro")
                {
                    var audioLibro = new AudioLibro(
                        id: reader.GetInt32(0),
                        titulo: reader.GetString(2),
                        anio: reader.GetInt32(3),
                        fechaAdquisicion: DateTime.Parse(reader.GetString(4)),
                        autor: reader.GetString(5),
                        fechaInicioDisp: DateTime.Parse(reader.GetString(8)),
                        fechaFinDisp: DateTime.Parse(reader.GetString(9))
                    );

                    lista.Add(audioLibro);
                }
            }

            return lista;
        }

        public void Insertar(Articulo articulo)
        {
            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            if (articulo is Libro libro)
            {
                command.CommandText =
                @"
                INSERT INTO Articulos
                (Id, Tipo, Titulo, Anio, FechaAdquisicion, Autor, Isbn, Prestado, FechaInicioDisp, FechaFinDisp)
                VALUES
                (@id, 'Libro', @titulo, @anio, @fecha, @autor, @isbn, @prestado, NULL, NULL)
                ";

                command.Parameters.AddWithValue("@id", libro.id);
                command.Parameters.AddWithValue("@titulo", libro.titulo);
                command.Parameters.AddWithValue("@anio", libro.anio);
                command.Parameters.AddWithValue("@fecha", libro.fechaAdquisicion.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@autor", libro.autor);
                command.Parameters.AddWithValue("@isbn", libro.isbn);
                command.Parameters.AddWithValue("@prestado", libro.prestado ? 1 : 0);
            }
            else if (articulo is AudioLibro audio)
            {
                command.CommandText =
                @"
                INSERT INTO Articulos
                (Id, Tipo, Titulo, Anio, FechaAdquisicion, Autor, Isbn, Prestado, FechaInicioDisp, FechaFinDisp)
                VALUES
                (@id, 'Audiolibro', @titulo, @anio, @fecha, @autor, NULL, NULL, @inicio, @fin)
                ";

                command.Parameters.AddWithValue("@id", audio.id);
                command.Parameters.AddWithValue("@titulo", audio.titulo);
                command.Parameters.AddWithValue("@anio", audio.anio);
                command.Parameters.AddWithValue("@fecha", audio.fechaAdquisicion.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@autor", audio.autor);
                command.Parameters.AddWithValue("@inicio", audio.fechaInicioDisp.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@fin", audio.fechaFinDisp.ToString("yyyy-MM-dd"));
            }

            command.ExecuteNonQuery();
        }

        public void Actualizar(Articulo articulo)
        {
            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            if (articulo is Libro libro)
            {
                command.CommandText =
                @"
                UPDATE Articulos
                SET Tipo = 'Libro',
                    Titulo = @titulo,
                    Anio = @anio,
                    FechaAdquisicion = @fecha,
                    Autor = @autor,
                    Isbn = @isbn,
                    Prestado = @prestado,
                    FechaInicioDisp = NULL,
                    FechaFinDisp = NULL
                WHERE Id = @id
                ";

                command.Parameters.AddWithValue("@id", libro.id);
                command.Parameters.AddWithValue("@titulo", libro.titulo);
                command.Parameters.AddWithValue("@anio", libro.anio);
                command.Parameters.AddWithValue("@fecha", libro.fechaAdquisicion.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@autor", libro.autor);
                command.Parameters.AddWithValue("@isbn", libro.isbn);
                command.Parameters.AddWithValue("@prestado", libro.prestado ? 1 : 0);
            }
            else if (articulo is AudioLibro audio)
            {
                command.CommandText =
                @"
                UPDATE Articulos
                SET Tipo = 'Audiolibro',
                    Titulo = @titulo,
                    Anio = @anio,
                    FechaAdquisicion = @fecha,
                    Autor = @autor,
                    Isbn = NULL,
                    Prestado = NULL,
                    FechaInicioDisp = @inicio,
                    FechaFinDisp = @fin
                WHERE Id = @id
                ";

                command.Parameters.AddWithValue("@id", audio.id);
                command.Parameters.AddWithValue("@titulo", audio.titulo);
                command.Parameters.AddWithValue("@anio", audio.anio);
                command.Parameters.AddWithValue("@fecha", audio.fechaAdquisicion.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@autor", audio.autor);
                command.Parameters.AddWithValue("@inicio", audio.fechaInicioDisp.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@fin", audio.fechaFinDisp.ToString("yyyy-MM-dd"));
            }

            command.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Articulos WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}