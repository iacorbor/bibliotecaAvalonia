using Microsoft.Data.Sqlite;

namespace BibliotecaAvalonia.Services
{
    public static class DatabaseService
    {
        private const string ConnectionString = "Data Source=biblioteca.db";

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }

        public static void Inicializar()
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                @"
            CREATE TABLE IF NOT EXISTS Articulos(
                Id INTEGER PRIMARY KEY,
                Tipo TEXT NOT NULL,
                Titulo TEXT,
                Anio INTEGER,
                FechaAdquisicion TEXT,
                Autor TEXT,
                Isbn TEXT,
                Prestado INTEGER,
                FechaInicioDisp TEXT,
                FechaFinDisp TEXT
            );
            ";

            command.ExecuteNonQuery();
            command.CommandText =
                @"
            CREATE TABLE IF NOT EXISTS Valoraciones(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ArticuloId INTEGER,
                Puntuacion REAL,
                Comentario TEXT,
                Usuario INTEGER
            );
            ";

            command.ExecuteNonQuery();
        }
    }
}